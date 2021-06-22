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
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var response = repository.Register(registerVM);
            if (response != 0)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Success" });
                return get;
            }
            else
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to register" });
                return get;
            }
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
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to Login(nik tidak ditemukan)" });
                return get;
            }
        }
    }
}
