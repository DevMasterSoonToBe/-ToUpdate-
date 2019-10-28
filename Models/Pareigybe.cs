using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class Pareigybe : IdentityRole<Guid>
    {
        public string role { get; set; }

    }
}