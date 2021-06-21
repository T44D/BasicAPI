﻿using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        public AccountController(AccountRepository repository) : base(repository) {}
    }
}
