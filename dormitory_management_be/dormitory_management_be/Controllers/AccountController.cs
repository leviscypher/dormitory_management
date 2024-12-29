using dormitory_management_be.Data;
using dormitory_management_be.Helpers;
using dormitory_management_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly string _key;
        private readonly string _iv;
        private readonly DormitoryContext _context;
        //private readonly EncryptionUtility _encryptionUtility;

        public AccountController(DormitoryContext context, EncryptionUtility encryptionUtility)
        {
            _key = EncryptionUtility.GenerateRandomKey(32);
            _iv = EncryptionUtility.GenerateRandomKey(16);
            _context = context;
        }

        private static List<Account> accounts = new List<Account>();

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            var accounts = _context.Account.ToList();
            if (accounts == null || !accounts.Any())
            {
                return NotFound();
            }

            foreach (var account in accounts)
            {
                account.PasswordHash = EncryptionUtility.DecryptString(account.PasswordHash, account.key, account.iv);
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetAccountById(int id)
        {
            var account = _context.Account.First(a => a.Id == id);
            var PasswordHash = EncryptionUtility.DecryptString(account.PasswordHash, account.key, account.iv);
            account.PasswordHash = PasswordHash;

            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public ActionResult CreateAccount([FromBody] Account newAccount)
        {
            var PasswordHash = EncryptionUtility.EncryptString(newAccount.PasswordHash, _key, _iv);
            var Key = _key;
            var IV = _iv;
            newAccount.key = Key;
            newAccount.iv = IV;
            newAccount.PasswordHash = PasswordHash;
            newAccount.CreatedAt = DateTime.Now;
            _context.Account.Add(newAccount);
            _context.SaveChanges();
            return Ok(newAccount);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAccount(int id, [FromBody] Account updatedAccount)
        {
            var account = _context.Account.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            var PasswordHash = EncryptionUtility.EncryptString(updatedAccount.PasswordHash, account.key, account.iv);
            account.Username = updatedAccount.Username;
            account.PasswordHash = PasswordHash;
            account.key = updatedAccount.key;
            account.iv = updatedAccount.iv;
            account.FirstName = updatedAccount.FirstName;
            account.LastName = updatedAccount.LastName;
            account.Email = updatedAccount.Email;
            account.Phone = updatedAccount.Phone;
            account.UpdatedAt = DateTime.Now;
            _context.Account.Update(account);
            _context.SaveChanges();

            return Ok(account);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            var account = _context.Account.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            _context.Account.Remove(account);
            _context.SaveChanges();
            return Ok("ok");
        }
    }
}
