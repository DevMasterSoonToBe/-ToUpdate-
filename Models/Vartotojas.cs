using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class Vartotojas : IdentityUser<Guid>
    {
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
    }
}
