using MicroLab.Dto;
using MicroLab.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroLab.Controllers
{
    public class ComputingController : Controller
    {
        // GET: Computing
        public ActionResult Index()
        {

            ViewBag.user = UserService.GetUser(Session["userId"]).Result;
            ViewBag.Access = UserService.Authenticate(Session["userId"]).Result;
            var a = FormulaService.GetAllFormulas();
            ViewBag.formulaList = a;
            return View();
        }

        public ActionResult EditFormula(int? f)
        {
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            var a = FormulaService.GetFormulaById(f ?? 0);
            ViewBag.formula = a;
            return View();
        }

        public JsonResult AddOrUpdateFormula(FormulaDto f)
        {
            FormulaService.AddFormula(f);
            return new JsonResult { Data = true };
        }
        public JsonResult AddOrUpdateVariabe(VariableDto v)
        {
            FormulaService.AddVariable(v);
            return new JsonResult { Data = true };
        }
        public JsonResult DeleteVariable(int id)
        {
            FormulaService.DeleteVariable(id);
            return new JsonResult { Data = true };
        }

        public JsonResult DeleteFormula(int id)
        {
            FormulaService.DeleteFormula(id);
            return new JsonResult { Data = true };
        }


        public ActionResult SolveFormula(int f)
        {
            var a = FormulaService.GetFormulaById(f);
            ViewBag.formula = a;
            return View();
        }
        public ActionResult InputOptions(int f)
        {
            var a = FormulaService.GetInputOptions(f);
            ViewBag.options = a;
            ViewBag.vid = f;
            return View();
        }
        public JsonResult AddOrUpdateOption(InputOptionDto v)
        {
            FormulaService.AddInputOption(v);
            return new JsonResult { Data = true };
        }

        public JsonResult DeleteOption(int id)
        {
            FormulaService.DeleteInputOption(id);
            return new JsonResult { Data = true };
        }
        public string Solve(int f, string input)
        {
            var formula = FormulaService.GetFormulaById(f);
            var user = UserService.GetUser(Session["userId"]).Result;
            if (user.Credit < formula.Cost)
            {
                return "{\"Description\":\"اعتبار\",\"Value\":\"کافی نیست\"}";
            }
            UserService.AddCredit(Session["userId"] ,- 1 * formula.Cost);
            var inputs = JsonConvert.DeserializeObject<Dictionary<int, double>>(input);
            var a = FormulaService.SolveFormula(f, inputs);
            var res = JsonConvert.SerializeObject(a);
            return res;
        }

    }
}