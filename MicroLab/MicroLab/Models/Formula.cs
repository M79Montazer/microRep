using MicroLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Models
{
    public class Formula : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public ICollection<Variable> Variables { get; set; }
    }
}