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

    public class ProfissionaisController : Controller
    {
        private readonly IProfissionais _profissionaisRepository;
        private readonly AppDbContext _context;
        public ProfissionaisController(IProfissionais profissionaisRepository, AppDbContext context)
        {
            _profissionaisRepository = profissionaisRepository;
            _context = context;
        }
        public async Task<IActionResult> Index(string filter, int pageindex=1, string sort="Nome")
        {

            var lista = _context.Profissionais.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                lista = lista.Where(p => p.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(lista, 30, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Incluir(Profissionais profissional)
        {
            _profissionaisRepository.Incluir(profissional);
            return RedirectToAction("Index", "Profissionais");
        }


        public IActionResult Alterar(string idprofissional)
        {
            var funcao = _profissionaisRepository.ProfissionalPorId(idprofissional);
            return View(funcao);
        }

        [HttpPost]

        public IActionResult Alterar(Profissionais profissionalAlterar, string idprofissional)
        {
            _profissionaisRepository.Alterar(profissionalAlterar, idprofissional);
            return RedirectToAction("Index", "Profissionais");
        }

        public IActionResult Excluir(string profissionalId)
        {
            var excluir = _profissionaisRepository.ProfissionalPorId(profissionalId);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult Excluir(string idprofissional, IFormCollection form)
        {
            _profissionaisRepository.Excluir(idprofissional);
            return RedirectToAction("Index", "Profissionais");
        }
    }
}
