﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class ChangePasswordVM
    {
        public string NIK { get; set; }
        public string Email { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}
