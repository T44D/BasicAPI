using API.Context;
using API.Models;
using API.Utils;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext context) : base(context) 
        {
            this.context = context;
        }

        public int Login(LoginVM loginVM)
        {
            var emp = context.Employees
                .Where(e => (e.NIK == loginVM.NIK) || (e.Email == loginVM.Email)).FirstOrDefault<Employee>();
            if (emp != null)
            {
                if (Hashing.Validate(loginVM.Password, emp.Account.Password))
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        public int ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var find = context.Employees
                .Where(e => e.Email == resetPasswordVM.Email).FirstOrDefault<Employee>();
            if (find != null)
            {
                string guid = GUID.NewGUID();

                Account account = new Account();
                account.NIK = find.Account.NIK;
                account.Password = Hashing.Hash(guid);

                Mailing.SendMail(resetPasswordVM.Email, guid, find.FirstName);
                return Update(account, account.NIK);
            }
            else
            {
                return 0;
            }
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var emp = context.Employees
                .Where(e => (e.NIK == changePasswordVM.NIK) || (e.Email == changePasswordVM.Email)).FirstOrDefault<Employee>();
            if (emp != null)
            {
                if (Hashing.Validate(changePasswordVM.PasswordOld, emp.Account.Password))
                {
                    Account account = new Account();
                    account.NIK = emp.NIK;
                    account.Password = Hashing.Hash(changePasswordVM.PasswordNew);
                    Update(account, emp.NIK);
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
