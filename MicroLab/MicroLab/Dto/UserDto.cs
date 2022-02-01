﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Dto
{
    public class UserDto : BaseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Credit { get; set; }
        public bool IsAdmin { get; set; }
    }
}