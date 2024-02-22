using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.Repositories.Repository;
using EPT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPT.Controllers
{
    [Authorize]

    public class SubItensController : Controller
    {
        private readonly ISubItens _subitensRepository;

        public SubItensController(ISubItens subitensRepository)
        {
            _subitensRepository = subitensRepository;

        }
        public IActionResult Index(int itemId)
        {
            var itens = _subitensRepository.Listar(itemId);
            SubItemViewModel subitem = new SubItemViewModel();
            {
                subitem.ItemId = itemId.ToString();
                subitem.SubItems=itens.ToList();
            }
            return View(subitem);
        }

        public IActionResult Incluir()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Incluir(SubItemViewModel subItens)
        {
            
            _subitensRepository.Incluir(subItens);
            return RedirectToAction("Index", "SubItens", new { itemId=subItens.ItemId});
        }


        public IActionResult Alterar(string subitemId)
        {
            var funcao = _subitensRepository.SubItemPorId(subitemId);
            return View(funcao);

        }

        [HttpPost]

        public IActionResult Alterar(SubItens subitemAlterar, string subitemId)
        {
            _subitensRepository.AlterarSubItem(subitemAlterar, subitemId);
            return RedirectToAction("Index", "SubItens", new { itemId = subitemAlterar.ItensId});
        }

        public IActionResult Excluir(string subitemId)
        {
            var excluir = _subitensRepository.SubItemPorId(subitemId);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult Excluir(string subitemId, IFormCollection form)
        {
            var excluir = _subitensRepository.SubItemPorId(subitemId);

            _subitensRepository.ExcluirSubItem(subitemId);
            return RedirectToAction("Index", "SubItens", new { itemId = excluir.ItensId });
        }
    }
}
