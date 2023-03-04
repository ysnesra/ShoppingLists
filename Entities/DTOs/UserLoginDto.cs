﻿using Core.Aspect.Autofac.Validation;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserLoginDto : IDto
    {
        public int UserId { get; set; }
        public string? Email { get; set; } 
        public string? Password { get; set; } 
        public string? Role { get; set; } 

    }
}
