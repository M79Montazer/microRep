using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Dto
{
    public class FormulaDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }

        public List<VariableDto> Variables { get; set; }
        //public Dictionary<int,double> Inputs { get; set; }
    }
}