using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSharp.WPF.MVVM.Models.Users
{
    public class UserInfo
    {
        public long id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
    }
}
