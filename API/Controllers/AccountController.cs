using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        IConfiguration configuration;
        MyContext context;
        AccountRepository repository;
        public AccountController(IConfiguration configuration, AccountRepository repository, MyContext context) : base(repository)
        {
            this.configuration = configuration;
            this.repository = repository;
            this.context = context;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var response = repository.Login(loginVM);
            if (response == 2)
            {
                var data = (from account in context.Accounts
                            join employee in context.Employees
                            on account.NIK equals employee.NIK
                            join accountRole in context.AccountRoles
                            on account.NIK equals accountRole.NIK
                            join role in context.Roles
                            on accountRole.RoleId equals role.RoleId
                            where account.NIK == $"{loginVM.NIK}" || employee.Email == $"{loginVM.Email}"
                            select new
                            {
                                Email = employee.Email,
                                RoleName = role.RoleName
                            }).ToList();
                var claims = new List<Claim>();
                foreach (var item in data)
                {
                    //claims.Add(new Claim("email", item.Email));
                    claims.Add(new Claim("role", item.RoleName));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                var get = Ok(new { status = HttpStatusCode.OK, result = new JwtSecurityTokenHandler().WriteToken(token), message = "Login Success" });
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

        [HttpPut("ResetPassword")]
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

        [HttpPut("ChangePassword")]
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
