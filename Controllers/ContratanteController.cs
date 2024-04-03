using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.Repositories.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace EPT.Controllers
{
    [Authorize]
    public class ContratanteController : Controller
    {
        private readonly IContratante _contratanteRepository;
        private readonly AppDbContext _context;

        public ContratanteController(IContratante contratanteRepository, AppDbContext context)
        {
            _contratanteRepository = contratanteRepository;
            _context = context;

        }
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "contratante")
        {

            var lista = _context.Contratante.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                lista = lista.Where(p => p.contratante.Contains(filter));
            }

            var model = await PagingList.CreateAsync(lista, 30, pageindex, sort, "contratante");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Incluir(Contratante contratantes)
        {
            _contratanteRepository.Incluir(contratantes);
            return RedirectToAction("Index", "Contratante");
        }


        public IActionResult Alterar(string idContratante)
        {
            var contratante = _contratanteRepository.ContratantePorId(idContratante);
            return View(contratante);
        }

        [HttpPost]

        public IActionResult Alterar(Contratante contratanteAlterar, string contratanteId)
        {
            _contratanteRepository.Alterar(contratanteAlterar, contratanteId);
            return RedirectToAction("Index", "Contratante");
        }

     /* requisito 2*/

        public IActionResult Excluir(string idcontratante)
        {
            var excluir = _contratanteRepository.ContratantePorId(idcontratante);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult Excluir(string contratanteId, IFormCollection form)
        {
            _contratanteRepository.Excluir(contratanteId);
            return RedirectToAction("Index", "Contratante");
        }

    }
}
