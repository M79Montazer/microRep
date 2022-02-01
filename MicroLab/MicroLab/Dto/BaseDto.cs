using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Dto
{
    public class BaseDto
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool Deleted { get; set; }

    }
}