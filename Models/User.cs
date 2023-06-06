using System;
using System.Collections.Generic;

namespace ApiRESTWithNet6.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}
