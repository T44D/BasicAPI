using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [Authorize]
        [HttpGet("UserData")]
        public ActionResult UserData()
        {
            var response = repository.UserData();
            if (response != null)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Success" });
                return get;
            }
            else
            {
                var get = NotFound(new { status = HttpStatusCode.NotFound, result = response, message = "Data Empty" });
                return get;
            }
        }

        [Authorize]
        [HttpGet("UserData/{nik}")]
        public ActionResult UserData(string nik)
        {
            var response = repository.Profil(nik);
            if (response != null)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Success" });
                return get;
            }
            else
            {
                var get = NotFound(new { status = HttpStatusCode.NotFound, result = response, message = "Not Found" });
                return get;
            }
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var response = repository.Register(registerVM);
            if (response == 3)
            {
                var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Success" });
                return get;
            }
            else if (response == 2)
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to register(University tidak ditemukan)" });
                return get;
            }
            else if (response == 1)
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to register(Email sudah digunakan)" });
                return get;
            }
            else
            {
                var get = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to register(Nik sudah digunakan)" });
                return get;
            }
        }
    }
}
