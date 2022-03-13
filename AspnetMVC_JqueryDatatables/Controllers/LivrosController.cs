using CRUD_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_EntityFramework.Controllers
{
    public class LivrosController : Controller
    {
        MeuContexto _meuContexto;

        public LivrosController()
        {
            _meuContexto = new MeuContexto();
        }

        // GET: Livros
        public ActionResult Index()
        {
            return View(_meuContexto.Livros.ToList());
        }

        public ActionResult Adicionar()
        {
            CarregarCondicoesLivros();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _meuContexto.Livros.Add(livro);
                _meuContexto.SaveChanges();

                return RedirectToAction("Index");
            }

            CarregarCondicoesLivros();

            return View(livro);
        }

        public ActionResult Editar(int id)
        {
            var oLivro = _meuContexto.Livros.Find(id);

            if (oLivro == null)
                return HttpNotFound();

            CarregarCondicoesLivros();

            return View(oLivro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _meuContexto.Entry(livro).State = System.Data.Entity.EntityState.Modified;
                _meuContexto.SaveChanges();

                return RedirectToAction("Index");
            }

            CarregarCondicoesLivros();

            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var oLivro = _meuContexto.Livros.Find(id);

            if (oLivro != null)
            {
                _meuContexto.Livros.Remove(oLivro);
                _meuContexto.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public void CarregarCondicoesLivros()
        {
            ViewData["CondicoesLivros"] = new SelectList(_meuContexto.CondicaoLivros.ToList(), "CondicaoId", "Descricao");
        }

        [HttpPost]
        public ActionResult ObterListaLivros()
        {
            JsonResult oJsonResult = new JsonResult();

            try
            {
                string sSearch = Request.Form.GetValues("search[value]")[0];
                string sDraw = Request.Form.GetValues("draw")[0];
                string sOrder = Request.Form.GetValues("order[0][column]")[0];
                string sOrderDir = Request.Form.GetValues("order[0][dir]")[0];
                int iStartRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int iPageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
                var oLivroList = _meuContexto.Livros.ToList();
                int iTotalRecords = oLivroList.Count;

                if (!string.IsNullOrEmpty(sSearch) && !string.IsNullOrWhiteSpace(sSearch))
                {
                    oLivroList = oLivroList.Where(x => x.LivroId.ToString().Contains(sSearch.ToLower()) ||
                        x.Nome.ToLower().StartsWith(sSearch.ToLower()) ||
                        x.NumeroEdicao.ToLower().Contains(sSearch.ToLower()) ||
                        x.Autor.ToLower().StartsWith(sSearch.ToLower()) ||
                        x.Editora.ToLower().StartsWith(sSearch.ToLower()) ||
                        x.PrecoVenda.ToString().Contains(sSearch.ToLower())
                     ).ToList();
                }
                
                oLivroList = OrdenarListaLivros(sOrder, sOrderDir, oLivroList);

                int iRegistrosFiltrados = oLivroList.Count;
                
                oLivroList = oLivroList.Skip(iStartRec).Take(iPageSize).ToList();
                
                var oLivroListDTO = oLivroList.Select(x =>
                    new
                    {
                        x.LivroId,
                        x.Nome,
                        x.NumeroEdicao,
                        x.Autor,
                        x.Editora,
                        x.PrecoVenda,
                    });

                oJsonResult = this.Json(new
                {
                    draw = Convert.ToInt32(sDraw),
                    recordsTotal = iTotalRecords,
                    recordsFiltered = iRegistrosFiltrados,
                    data = oLivroListDTO
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return oJsonResult;
        }

        private List<Livro> OrdenarListaLivros(string order, string orderDir, List<Livro> data)
        {
            var oLivroList = new List<Livro>();

            try
            {
                switch (order)
                {
                    case "0":
                        oLivroList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LivroId).ToList(): data.OrderBy(p => p.LivroId).ToList();
                        break;
                    case "1":
                        oLivroList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Nome).ToList(): data.OrderBy(p => p.Nome).ToList();
                        break;
                    case "2":
                        oLivroList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Autor).ToList(): data.OrderBy(p => p.Autor).ToList();
                        break;
                    case "3":
                        oLivroList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PrecoVenda).ToList() : data.OrderBy(p => p.PrecoVenda).ToList();
                        break;
                    case "4":
                        oLivroList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.NumeroEdicao).ToList(): data.OrderBy(p => p.NumeroEdicao).ToList();
                        break;
                    default:
                        oLivroList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LivroId).ToList(): data.OrderBy(p => p.LivroId).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return oLivroList;
        }
    }
}
