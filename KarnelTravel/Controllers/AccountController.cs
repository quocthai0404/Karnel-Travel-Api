using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Mail;
using KarnelTravel.Validate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAccountService accountService;
    private IConfiguration configuration;
    private IMailService mailService;
    public AccountController(IAccountService _accountService, IConfiguration _configuration, IMailService _mailService) {
        accountService = _accountService;   
        configuration = _configuration;
        mailService = _mailService;
    }

    //
    [HttpPost("Register")]
    public async Task<IActionResult> register(UserDTO userDTO)
    {
        if (string.IsNullOrEmpty(userDTO.Password) || string.IsNullOrEmpty(userDTO.Fullname) || string.IsNullOrEmpty(userDTO.PhoneNumber) || string.IsNullOrEmpty(userDTO.Email)) {
            return BadRequest(new Response { Code = "400", Msg = "All fields must be filled out completely" });
        }

        if (await accountService.IsExistEmail(userDTO.Email))
        {
            return BadRequest(new Response { Code = "400", Msg = "Error, Email already exists!" });
        }

        if (!Email.IsValidEmailAddress(userDTO.Email)) {
            return BadRequest(new Response { Msg = "Error, The email you entered is not valid!" });
        }

        string url = configuration["BaseUrl"];
        string securityCode = Helpers.RandomHelper.generateSecurityCode();
        var mail = new Mail(url + "api/Account/Verify-Your-Account/" + securityCode);

        //add record to db fail 
        if (!accountService.Register(userDTO, securityCode)) {
            return BadRequest(new Response { Code = "400", Msg = "Registration failed, please check your information again" });
        }

        //send mail fail
        if (!mailService.Send("karneltravelmailservice@gmail.com", userDTO.Email, "Karnel Travel - Activate Your Account", mail.Email))
        {
            return BadRequest(new Response { Code = "400", Msg = "The activation email could not be sent, your email may be corrupted or does not exist" });
        }

        //success all
        return Ok(new Response { Code = "200", Msg = "Successful registration, please check your email to activate your account" });


    }

    //
    [HttpGet("Verify-Your-Account/{securityCode}")]
    public IActionResult Verify(string securityCode) {
        if (accountService.ActiveAccount(securityCode).Result) {
            return Ok(new Response { Code = "200", Msg = "The account has been successfully activated" });
        }
        return BadRequest(new Response { Code = "400", Msg = "Account activation failed" });
    }

    [HttpPost("Login")]
    public IActionResult Login(AccountLoginDTO account)
    {
        if (!accountService.Login(account.email, account.password))
        {
            return BadRequest(new Response { Code = "400", Msg = "Login failed, please check your information again" });
        }
        var user = accountService.FindAccountByEmail(account.email).Result;

        
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("Email", account.email)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: signIn
            );
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { Code = "200", Msg = "Log in successfully", Token = tokenValue });
    }

    //forget password
    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> sendMail(string email)
    {
        if (!await accountService.IsExistEmail(email))
        {
            return BadRequest(new Response { Code = "400", Msg = "This email does not exist for any account" });
            
        }
        

        string url = configuration["BaseUrl"];
        string token = Helpers.RandomHelper.generateSecurityCode() + Helpers.RandomHelper.generateSecurityCode();
        var mail = new Mail(url + "api/Account/ForgetPassword/" + token, true);
        if (!accountService.AddForgetPasswordRecord(email, token)) {
            return BadRequest(new Response { Code = "400", Msg = "You cannot reset your password at this time" });
        }

        if (!mailService.Send("karneltravelmailservice@gmail.com", email, "Karnel Travel - Reset Your Password", mail.Email))
        {
            return BadRequest(new Response { Code = "400", Msg = "The email could not be sent, your email may be corrupted or does not exist" });
        }
        return Ok(new Response { Code = "200", Msg = "Email Sent Successfully , please check your email to reset your password" });

    }

    [HttpGet("ForgetPassword/{token}")]
    public IActionResult VerifyToken(string token)
    {
        var record = accountService.FindRecordByToken(token).Result;
        if (record == null) {
            return BadRequest(new Response { Code = "400", Msg = "Can not verify token, please try again later" });
        }
        if (record.Expire < DateTime.Now) {
            return BadRequest(new Response { Code = "400", Msg = "The password reset link has expired, please try again later" });
        }
        return Ok(new Response { Code = "200", Msg = "Verify Ok", Status="true" });
    }


    [HttpPost("ResetPassword")]
    public IActionResult ResetPassword(string token, string newpassword) 
    {
        var record = accountService.FindRecordByToken(token).Result;
        var account = accountService.FindAccountByEmail(record.Email).Result;
        account.Password = BCrypt.Net.BCrypt.HashPassword(newpassword);
        if (!accountService.UpdateAccount(account)) {
            return BadRequest(new Response { Code = "400", Msg = "Reset Your Password Failed" , Status="false"});
        }

        accountService.DeleteForgetPassRecord(record);
        return Ok(new Response { Code = "200", Msg = "Your Password Has Been Reset", Status = "true" });

    }

    [Authorize]
    [HttpGet("GetUserId")]
    public IActionResult GetUserId()
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return BadRequest(new { Code = "400", Msg = "User ID not found" });
        }

        return Ok(new { UserId = userId });
    }

}
