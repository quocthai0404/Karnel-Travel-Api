using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Account;

public interface IAccountService
{
    public bool Register(UserDTO userDto, string securityCode);
    public Task<bool> IsExistEmail(string email);
    public Task<ActiveAccount> FindActiveAccountByEmail(string email);
    public Task<ActiveAccount> FindActiveAccountByCode(string securityCode);
    public Task<bool> ActiveAccount(string securityCode);
    public bool Login(string email, string password);
    public Task<User> FindAccountByEmail(string email);
    public bool AddForgetPasswordRecord(string email, string token);
    public Task<ForgetPassword> FindRecordByToken(string token);
    public bool UpdateAccount(User user);
    public bool DeleteForgetPassRecord(ForgetPassword record);
    public string getFullName(string email);
    public UserDTO findById(int id);
}
