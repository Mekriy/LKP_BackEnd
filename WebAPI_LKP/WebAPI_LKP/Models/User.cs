
﻿using Microsoft.AspNetCore.Identity;
using WebAPI_LKP.Models.Enums;

namespace WebAPI_LKP.Models
{
    public class User : IdentityUser<Guid>
    {
        public Roles Role { get; set; } = Roles.User;
        public List<Order> Orders { get; set; }
    }
}