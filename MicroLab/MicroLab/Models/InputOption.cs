using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Models
{
    public class InputOption : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int VariableId { get; set; }
    }
}