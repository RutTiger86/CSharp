﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Efcore.Migrations.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
