using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace EPT.Controllers
{
    [Authorize]
    public class FuncoesController : Controller
    {

        private readonly IFuncoes _funcoesRepository;
        private readonly AppDbContext _context;

        public FuncoesController(IFuncoes funcoesRepository, AppDbContext context)
        {
            _funcoesRepository = funcoesRepository;
            _context = context;

        }
       
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Funcao")
        {

            var lista = _context.Funcoes.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                lista = lista.Where(p => p.Funcao.Contains(filter));
            }

            var model = await PagingList.CreateAsync(lista, 30, pageindex, sort, "Funcao");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }


        public IActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Incluir(Funcoes funcoes)
        {
            _funcoesRepository.Incluir(funcoes);
            return RedirectToAction("Index", "Funcoes");
        }


        public IActionResult Alterar(string idfuncao)
        {
            var funcao = _funcoesRepository.FuncaoPorId(idfuncao);
            return View(funcao);
        }

        [HttpPost]

        public IActionResult Alterar(Funcoes funcaoAlterar, string funcaoId)
        {
            _funcoesRepository.Alterar(funcaoAlterar, funcaoId);
            return RedirectToAction("Index", "Funcoes");
        }

        public IActionResult Excluir(string idfuncao)
        {
            var excluir=_funcoesRepository.FuncaoPorId(idfuncao);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult Excluir(string funcaoId, IFormCollection form)
        {
            _funcoesRepository.Excluir(funcaoId);
            return RedirectToAction("Index", "Funcoes");
        }

    }
}
