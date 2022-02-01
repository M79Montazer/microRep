using MicroLab.Data;
using MicroLab.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroLab.Dto;

namespace MicroLab.Service
{
    public static class FormulaService
    {
        static readonly Context db = new Context();
        public static List<VariableDto> SolveFormula(int formulaId, Dictionary<int,double> inputs)
        {
            var formula = GetFormulaById(formulaId);
            foreach (var item in formula.Variables)
            {
                if (item.IsInput)
                {
                    foreach (var i in inputs)
                    {
                        if (i.Key == item.Id)
                        {
                            item.Value = i.Value;
                        }
                    }
                }
            } 

            var finalList = new List<VariableDto>();
            foreach (var item in formula.Variables)
            {
                if (item.Value == null)
                {
                    var firstVariable = formula.Variables.First(s => s.Id == item.FirstVariableId);
                    VariableDto secondVariable =null ;
                    if (item.SecondVariableId!=null)
                    {
                        secondVariable = formula.Variables.First(s => s.Id == item.SecondVariableId);
                    }

                    //if (firstVariable.Value==null || secondVariable.Value==null)
                    //{
                    //    throw new Exception();
                    //}
                    var secondValue = item.Const == null ? (secondVariable == null ? 0 : secondVariable.Value) : item.Const;
                    var firstVale = firstVariable.Value;
                    switch (item.Operation)
                    {
                        case Enums.Operation.Sum:
                            item.Value = firstVale + secondValue;
                            break;                             
                        case Enums.Operation.Subtract:         
                            item.Value = firstVale - secondValue;
                            break;
                        case Enums.Operation.Multiply:
                            item.Value = firstVale * secondValue;
                            break;
                        case Enums.Operation.Division:
                            item.Value = firstVale / secondValue;
                            break;
                        case Enums.Operation.Power:
                            item.Value = Math.Pow(firstVale ?? 0, secondValue ?? 0);
                            break;
                        case Enums.Operation.Sqrt:
                            secondValue = 1 / secondValue;
                            item.Value = Math.Pow(firstVale ?? 0, secondValue ?? 0);
                            break;
                        case Enums.Operation.Sin:
                            item.Value = Math.Sin(firstVale ?? 0);
                            break;
                        case Enums.Operation.Cos:
                            item.Value = Math.Cos(firstVale ?? 0);
                            break;
                        case Enums.Operation.Tan:
                            item.Value = Math.Tan(firstVale ?? 0);
                            break;
                        case Enums.Operation.Cot:
                            item.Value = Math.Tan((Math.PI/2)- firstVale ?? 0);
                            break;
                        case Enums.Operation.Abs:
                            item.Value = Math.Abs(firstVale ?? 0);
                            break;
                        default:
                            break;
                    }
                }
                if (item.IsFinal)
                {
                    finalList.Add(item);
                }
            }


            return finalList;
        }

        public static List<FormulaDto> GetAllFormulas()
        {
            try
            {
                var f = db.Formulas.Where(s => !s.Deleted).ToList();
                var res = new List<FormulaDto>();
                foreach (var item in f)
                {
                    res.Add(new FormulaDto {
                        Id=item.Id,
                        CreateDate=item.CreateDate,
                        Name=item.Name,
                        Cost=item.Cost,
                    });
                }
                return res;   
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static FormulaDto GetFormulaById(int id)
        {
            var f = db.Formulas.Where(s => s.Id == id && !s.Deleted).Include(s => s.Variables).Include(s=>s.Variables.Select(q=>q.InputOptions)).ToList();
            if (f.Count != 1)
            {
                throw new Exception();
            }
            var b = f.First();
            b.Variables.OrderBy(s => s.Priority);
            var variableDtoList = new List<VariableDto>();
            foreach (var item in b.Variables)
            {
                if (item.Deleted)
                {
                    continue;
                }
                var inputOptions = new List<InputOptionDto>();
                foreach (var inputOpt in item.InputOptions)
                {
                    if (inputOpt.Deleted)
                    {
                        continue;
                    }
                    var opt = new InputOptionDto
                    {
                        Id = inputOpt.Id,
                        CreateDate = inputOpt.CreateDate,
                        Deleted = inputOpt.Deleted,
                        ModifyDate = inputOpt.ModifyDate,
                        Name=inputOpt.Name,
                        Value=inputOpt.Value,
                        VariableId = inputOpt.VariableId,
                    };
                    inputOptions.Add(opt);
                }





                var v1 = item.FirstVariableId!=null?variableDtoList.First(s => s.Id == item.FirstVariableId):null;
                var v2 = item.SecondVariableId!=null?variableDtoList.First(s => s.Id == item.SecondVariableId):null;
                var newVDto = new VariableDto
                {
                    Id = item.Id,
                    FirstVariableId = item.FirstVariableId,
                    FirstVariable = v1,
                    SecondVariableId = item.SecondVariableId,
                    SecondVariable = v2,
                    FormulaId = item.FormulaId,
                    IsFinal = item.IsFinal,
                    IsInput = item.IsInput,
                    Name = item.Name,
                    Operation = item.Operation,
                    Priority = item.Priority,
                    Value = null,
                    Description = item.Description,
                    Const = item.Const,
                    IsMultiOption=item.IsMultiOption,
                    InputOptions= inputOptions.Count!=0?inputOptions:null,
                };
                variableDtoList.Add(newVDto);
            }
            variableDtoList.OrderBy(o => o.Priority);

            var formula = new FormulaDto
            {
                Id = b.Id,
                Variables = variableDtoList,
                //Inputs = inputs,
                Name = b.Name,
                Deleted = b.Deleted,
                Cost = b.Cost,
            };
            return formula;
        }

        public static bool AddFormula(FormulaDto formulaDto)
        {
            try
            {
                var formula = new Formula
                {
                    CreateDate = DateTime.Now,
                    Deleted = false,
                    Name = formulaDto.Name,
                    Cost = formulaDto.Cost,
                };
                db.Formulas.Add(formula);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddVariable(VariableDto v)
        {

            try
            {
                int? firstVId = null;
                int? secondVId = null;
                try
                {

                    firstVId = db.Formulas.Where(s => s.Id == v.FormulaId && !s.Deleted).Include(s => s.Variables)
                        .First().Variables.Where(s => s.Name == v.FirstVName && !s.Deleted).Select(s => s.Id).First();
                    secondVId = db.Formulas.Where(s => s.Id == v.FormulaId && !s.Deleted).Include(s => s.Variables)
                        .First().Variables.Where(s => s.Name == v.SecondVName && !s.Deleted).Select(s => s.Id).First();
                }
                catch (Exception e)
                {

                }

                var variable = new Variable
                {
                    CreateDate = DateTime.Now,
                    Deleted = false,
                    Name = v.Name,
                    Description = v.Description,
                    FirstVariableId = firstVId,
                    FormulaId = v.FormulaId,
                    IsFinal = v.IsFinal,
                    IsInput = v.IsInput,
                    Operation = v.Operation,
                    Priority = v.Priority,
                    SecondVariableId = secondVId,
                    Const = v.Const,
                    IsMultiOption = v.IsMultiOption,
                };
                db.Variables.Add(variable);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool DeleteVariable(int id)
        {

            try
            {
                var variable = db.Variables.First(s => s.Id == id);
                variable.Deleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteFormula(int id)
        {

            try
            {
                var formula = db.Formulas.First(s => s.Id == id);
                formula.Deleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddInputOption(InputOptionDto inputOption)
        {

            try
            {
                var opt = new InputOption
                {
                    CreateDate = DateTime.Now,
                    Deleted = false,
                    Name = inputOption.Name,
                    Value = inputOption.Value,
                    VariableId = inputOption.VariableId,
                };
                db.InputOptions.Add(opt);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DeleteInputOption(int id)
        {

            try
            {
                var opt = db.InputOptions.First(s => s.Id == id);
                opt.Deleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<InputOptionDto> GetInputOptions(int vid)
        {

            try
            {
                var v = db.Variables.Where(s => !s.Deleted && s.Id==vid).Include(s=>s.InputOptions).First();
                var res = new List<InputOptionDto>();
                foreach (var item in v.InputOptions)
                {
                    if (item.Deleted)
                    {
                        continue;
                    }
                    res.Add(new InputOptionDto
                    {
                        Id = item.Id,
                        CreateDate = item.CreateDate,
                        Name = item.Name,
                        Value = item.Value,
                        VariableId=item.VariableId,
                    });
                }
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}