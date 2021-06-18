using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var response = employeeRepository.Get();
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

        [HttpGet("{nik}")]
        public ActionResult Get(string nik)
        {
            var response = employeeRepository.Get(nik);
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

        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            var response = employeeRepository.Insert(employee);
            if (response > 0)
            {
                var insert = Ok(new { status = HttpStatusCode.OK, result = response, message = "Insert successful" });
                return insert;
            }
            else
            {
                var insert = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to insert" });
                return insert;
            }
        }

        [HttpDelete("{nik}")]
        public ActionResult Delete(string nik)
        {
            var response = employeeRepository.Delete(nik);
            if (response > 0)
            {
                var delete = Ok(new { status = HttpStatusCode.OK, result = response, message = "Delete successful" });
                return delete;
            }
            else
            {
                var delete = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to delete" });
                return delete;
            }
        }

        [HttpPut("{nik}")]
        public ActionResult Update(Employee employee, string nik)
        {
            var find = employeeRepository.Get(nik);
            if (find != null)
            {
                var response = employeeRepository.Update(employee, nik);
                if (response > 0)
                {
                    var update = Ok(new { status = HttpStatusCode.OK, result = response, message = "Update successful" });
                    return update;
                }
                else
                {
                    var update = BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Fail to update" });
                    return update;
                }
            }
            else
            {
                var update = NotFound(new { status = HttpStatusCode.NotFound, result = find, message = "Not Found" });
                return update;
            }
        }
    }
}
