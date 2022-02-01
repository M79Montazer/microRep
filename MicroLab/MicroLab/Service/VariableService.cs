using MicroLab.Data;
using MicroLab.Dto;
using MicroLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLab.Service
{
    public static class VariableService
    {
        static readonly Context db = new Context();
        
    //    public static bool AddVariable(VariableDto variableDto)
    //    {
    //        try
    //        {
    //            var firsId = db.
    //            var variable = new Variable
    //            {
    //                CreateDate=DateTime.Now,
    //                Deleted=false,
    //                FirstVariableId=variableDto.FirstVariableId,
    //                FormulaId=variableDto.FormulaId,
    //                IsFinal=variableDto.IsFinal,
    //                IsInput=variableDto.IsInput,
    //                Name=variableDto.Name,
    //                Operation=variableDto.Operation,
    //                Priority=variableDto.Priority,
    //                SecondVariableId=variableDto.SecondVariableId,
    //                Description=variableDto.Description,
    //                Const=variableDto.Const,
                    
    //            };
    //            db.Variables.Add(variable);
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    }
}