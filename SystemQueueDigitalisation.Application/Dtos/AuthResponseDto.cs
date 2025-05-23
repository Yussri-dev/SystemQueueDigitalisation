﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemQueueDigitalisation.Application.Dtos
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string ErrorMessage { get; set; }
    }
}
