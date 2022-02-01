using MicroLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Models
{
    public class Variable : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public double Value { get; set; }
        public int Priority { get; set; }
        public bool IsInput { get; set; }
        public bool IsFinal { get; set; }
        public Operation Operation { get; set; }
        public int? FirstVariableId { get; set; }
        //public Variable FirstVariable { get; set; }
        public int? SecondVariableId { get; set; }
        //public Variable SecondVariable { get; set; }
        public int FormulaId { get; set; }
        public Formula  Formula { get; set; }
        public string Description { get; set; }
        public double? Const { get; set; }
        public bool IsMultiOption { get; set; }
        public ICollection<InputOption> InputOptions { get; set; }
    }
}