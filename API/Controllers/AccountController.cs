using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository) 
        {
            this.repository = repository;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var response = repository.Login(loginVM);
            if (response == 2)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Login Success" });
                return get;
            }
            else if (response == 1)
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to Login(password salah)" });
                return get;
            }
            else
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to Login(nik/email tidak ditemukan)" });
                return get;
            }
        }

        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var response = repository.ResetPassword(resetPasswordVM);
            if (response > 0)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Reset Password Success" });
                return get;
            }
            else
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to send mail(Email tidak ditemukan)" });
                return get;
            }
        }

        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var response = repository.ChangePassword(changePasswordVM);
            if (response == 2)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Change Password Success" });
                return get;
            }
            else if (response == 1)
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to Change Password(password lama salah)" });
                return get;
            }
            else
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to Change Password(nik/email tidak ditemukan)" });
                return get;
            }
        }
    }
}
