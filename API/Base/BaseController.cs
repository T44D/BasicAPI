using API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var response = repository.Get();
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

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var response = repository.Get(key);
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
        public ActionResult Insert(Entity entity)
        {
            var response = repository.Insert(entity);
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

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var response = repository.Delete(key);
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

        [HttpPut("{key}")]
        public ActionResult Update(Entity entity, Key key)
        {
            var find = repository.Get(key);
            if (find != null)
            {
                var response = repository.Update(entity, key);
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
