using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Dto
{
    public class BasiliskResponse<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Text { get; set; }
    }
}