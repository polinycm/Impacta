using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace EPT.Controllers
{
    [Authorize]

    public class ItensController : Controller
    {
        private readonly IItens _itensRepository;
        private readonly AppDbContext _context;


        public ItensController(IItens itensRepository, AppDbContext context)
        {
            _itensRepository = itensRepository;
            _context = context;

        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Item")
        {

            var lista = _context.Itens.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                lista = lista.Where(p => p.Item.Contains(filter));
            }
            var model = await PagingList.CreateAsync(lista, 30, pageindex, sort, "Item");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Incluir(Itens itens)
        {
            _itensRepository.Incluir(itens);
            return RedirectToAction("Index", "Itens");
        }


        public IActionResult Alterar(string iditem)
        {
            var funcao = _itensRepository.ItemPorId(iditem);
            return View(funcao);

        }

        [HttpPost]

        public IActionResult Alterar(Itens itemAlterar, string itemId)
        {
            _itensRepository.Alterar(itemAlterar, itemId);
            return RedirectToAction("Index", "Itens");
        }

        public IActionResult Excluir(string iditem)
        {
            var excluir = _itensRepository.ItemPorId(iditem);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult Excluir(string itemId, IFormCollection form)
        {
            _itensRepository.Excluir(itemId);
            return RedirectToAction("Index", "Itens");
        }
    }
}
