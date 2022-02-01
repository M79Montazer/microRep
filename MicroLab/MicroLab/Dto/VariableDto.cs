using MicroLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Dto
{
    public class VariableDto : BaseDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsInput { get; set; }
        public bool IsFinal { get; set; }
        public Operation Operation { get; set; }
        public int? FirstVariableId { get; set; }
        public VariableDto FirstVariable { get; set; }
        public string FirstVName { get; set; }
        public int? SecondVariableId { get; set; }
        public VariableDto SecondVariable { get; set; }
        public string SecondVName { get; set; }
        public int FormulaId { get; set; }
        public FormulaDto Formula { get; set; }
        public double? Value { get; set; }
        public double? Const { get; set; }
        public string Description { get; set; }
        public bool IsMultiOption { get; set; }
        public List<InputOptionDto> InputOptions { get; set; }
    }
}