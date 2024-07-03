using KarnelTravel.DTO;
using KarnelTravel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace KarnelTravel.Services.Account;

public class AccountServiceImpl : IAccountService
{
    private DatabaseContext db;
    public AccountServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public async Task<bool> ActiveAccount(string securityCode)
    {
        var activeRecord = await FindActiveAccountByCode(securityCode);
        if (activeRecord == null || activeRecord.SecurityCode != securityCode || activeRecord.IsActive)
        {
            return false;
        }

        activeRecord.IsActive = true;
        try
        {
            db.Entry(activeRecord).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }


    }

    public bool AddForgetPasswordRecord(string email, string token)
    {
        try
        {
            db.ForgetPasswords.Add(new ForgetPassword { Email = email, Token = token, Expire = DateTime.Now.AddMinutes(15) });
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteForgetPassRecord(ForgetPassword record)
    {
        try
        {
            db.ForgetPasswords.Remove(record);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<User> FindAccountByEmail(string email)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ActiveAccount> FindActiveAccountByCode(string securityCode)
    {
        return await db.ActiveAccounts.FirstOrDefaultAsync(x => x.SecurityCode == securityCode); ;
    }

    public async Task<ActiveAccount> FindActiveAccountByEmail(string email)
    {
        return await db.ActiveAccounts.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<ForgetPassword> FindRecordByToken(string token)
    {
        return await db.ForgetPasswords.FirstOrDefaultAsync(r => r.Token == token);
    }



    // if exist email => return true, else => return false
    public async Task<bool> IsExistEmail(string email)
    {
        var flag1 = await db.Users.FirstOrDefaultAsync(u => u.Email == email) != null;
        var flag2 = await db.ActiveAccounts.FirstOrDefaultAsync(u => u.Email == email) != null;
        return flag1 || flag2;
    }

    public bool Login(string email, string password)
    {
        var ActiveAccount = FindActiveAccountByEmail(email).Result;
        var account = FindAccountByEmail(email).Result;

        if (ActiveAccount.IsActive == false || ActiveAccount == null || account == null) { return false; };
        return BCrypt.Net.BCrypt.Verify(password, account.Password);

    }

    public bool Register(UserDTO userDto, string securityCode)
    {
        using var transaction = db.Database.BeginTransaction();
        try
        {
            db.Users.Add(new User
            {
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                PhoneNumber = userDto.PhoneNumber,
                Gender = userDto.Gender
            });

            db.SaveChanges();

            db.ActiveAccounts.Add(new ActiveAccount
            {
                Email = userDto.Email,
                IsActive = false,
                SecurityCode = securityCode
            });

            db.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            return false;
        }



    }

    public bool UpdateAccount(User user)
    {
        try
        {
            db.Entry(user).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }


}
