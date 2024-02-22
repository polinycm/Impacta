

using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.Repositories.Repository;
using EPT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Drawing;

namespace EPT.Controllers
{
    [Authorize]
    public class AtestadoController : Controller
    {
        private readonly IAtestado _atestadoRepository;
        private readonly IAtestadoItem _atestadoItemRepository;
        private readonly IItens _itensRepository;
        private readonly IContratante _contratanteRepository;
        private readonly ISubItens _subitensRepository;
        private readonly AppDbContext _context;
        public AtestadoController(ISubItens subitensRepository, IAtestado atestadoRepository, IItens itensRepository, IAtestadoItem atestadoItemRepository,IContratante contratanteRepository, AppDbContext context)
        {
            _subitensRepository = subitensRepository;
            _atestadoRepository = atestadoRepository;
            _itensRepository = itensRepository;
            _atestadoItemRepository = atestadoItemRepository;
            _contratanteRepository = contratanteRepository;
            _context = context;
        }

        
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "AtestadoId")
        {

            var lista = _context.Atestado.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                lista = lista.Where(p => p.AtestadoId.ToString().Contains(filter));
            }
            var model = await PagingList.CreateAsync(lista, 30, pageindex, sort, "AtestadoId");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public IActionResult Incluir()
        {
            List<Contratante> contratante = _contratanteRepository.Listar().ToList();
            ViewBag.Contratante = new SelectList(contratante, "ContratanteId", "contratante");
            
            return View();

        }

        [HttpPost]
        public IActionResult Incluir(AtestadoViewModel atestados)
        {

            _atestadoRepository.Incluir(atestados);

            return RedirectToAction("Index", "Atestado");
        }

        public IActionResult IncluirItem(int idatestado)
        {
            List<Itens> itens = _itensRepository.Listar().ToList();
            ViewBag.Itens = new SelectList(itens, "ItensId", "Item", null);

            AtestadoItemViewModel viewModel = new AtestadoItemViewModel();
            {
                IEnumerable<AtestadoItem> listaItem = _atestadoItemRepository.Listar(idatestado);
                viewModel.listaAtestadoItem = listaItem;
                viewModel.AtestadoId = idatestado;
                viewModel.SubItensId = 0;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult IncluirItem(AtestadoItemViewModel viewmodel, int atestadoId)
        {
            if(viewmodel.SelectedItemId == 0 || viewmodel.AtestadoItem.quantidade==null)
            {
                if(viewmodel.SelectedItemId == 0)
                {
                    ViewBag.ErrorMessage = "Por favor, selecione um item.";
                }
                List<Itens> itens = _itensRepository.Listar().ToList();
                ViewBag.Itens = new SelectList(itens, "ItensId", "Item", null);

                AtestadoItemViewModel viewModel = new AtestadoItemViewModel();
                {
                    IEnumerable<AtestadoItem> listaItem = _atestadoItemRepository.Listar(atestadoId);
                    viewModel.listaAtestadoItem = listaItem;
                    viewModel.AtestadoId = atestadoId;
                    viewModel.SubItensId = 0;
                }

                return View(viewModel);

            }
            else
            {   _atestadoItemRepository.Incluir(viewmodel);
               
                var viewModel = new AtestadoItemViewModel();
                {
                    List<Itens> itens = _itensRepository.Listar().ToList();
                    ViewBag.Itens = new SelectList(itens, "ItensId", "Item", null);

                    
                    viewModel.AtestadoId = atestadoId;
                    IEnumerable<AtestadoItem> listaItem = _atestadoItemRepository.Listar(atestadoId);
                    viewModel.listaAtestadoItem = listaItem;
                    viewModel.AtestadoId = atestadoId;

                    AtestadoItem qtd = new AtestadoItem()
                    {
                        quantidade = "0",
                        unidade = null

                    };

                    viewModel.AtestadoItem = qtd;

                    ViewBag.Limpar = "Limpar";
                }

                

                return View("IncluirItem",viewModel);

            }

        }

        [HttpPost]
        public JsonResult RecuperarSubItem(int idItem)
        {
            var lista= _atestadoItemRepository.ListarSubItens(idItem: idItem);
            return Json(lista);

        }



        public IActionResult Alterar(string idatestado)
        {
            var atestado = _atestadoRepository.AtestadoPorId(idatestado);
            List<Contratante> contratante = _contratanteRepository.Listar().ToList();
            ViewBag.Contratante = new SelectList(contratante, "ContratanteId", "contratante");

            AtestadoViewModel atestados = new();
            {
                atestados.atestado=atestado;
                atestados.atestado.Link=atestado.Link;
                atestados.SelectedContratanteId = atestado.ContratanteId;
                atestados.Contratante = _contratanteRepository.ContratantePorId(atestado.ContratanteId.ToString());
                atestados.SelectedMoeda = atestado.moeda;
            }

            return View(atestados);
        }

        [HttpPost]

        public IActionResult Alterar(AtestadoViewModel atestadoAlterar, string atestadoId)
        {

            _atestadoRepository.Alterar(atestadoAlterar, atestadoId);
            return RedirectToAction("Index", "Atestado");
        }

        public IActionResult AlterarItem(string idItem)
        {
            var item =_atestadoItemRepository.ItemPorId(idItem);

            return View(item);
        }

        [HttpPost]

        public IActionResult AlterarItem(AtestadoItem itemAlterar, string atestadoItemId, string AtestadoId)
        {
            _atestadoItemRepository.AlterarItem(itemAlterar,atestadoItemId);
            var item = _atestadoItemRepository.Listar(Convert.ToInt32(AtestadoId));

            List<Itens> itens = _itensRepository.Listar().ToList();
            ViewBag.Itens = new SelectList(itens, "ItensId", "Item", null);

            AtestadoItemViewModel viewModel = new AtestadoItemViewModel();
            {
                viewModel.listaAtestadoItem = item;
            }


            return View("IncluirItem", viewModel);
        }




        public IActionResult Excluir(string idatestado)
        {
            var excluir = _atestadoRepository.AtestadoPorId(idatestado);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult Excluir(string idatestado, IFormCollection form)
        {
            _atestadoRepository.Excluir(idatestado);
            return RedirectToAction("Index", "Atestado");
        }


        public IActionResult ExcluirItem(string iditem)
        {
            var excluir = _atestadoItemRepository.ItemPorId(iditem);
            return View(excluir);
        }

        [HttpPost]

        public IActionResult ExcluirItem(string iditem, string AtestadoId)
        {
            _atestadoItemRepository.ExcluirItem(iditem);

            var itens = _atestadoItemRepository.Listar(Convert.ToInt32(AtestadoId));
            AtestadoItemViewModel viewModel = new AtestadoItemViewModel();
            {
                viewModel.listaAtestadoItem = itens;
                viewModel.AtestadoId = Convert.ToInt32(AtestadoId);

                IEnumerable<AtestadoItem> listaItem = _atestadoItemRepository.Listar(Convert.ToInt32(AtestadoId));
                viewModel.listaAtestadoItem = listaItem;
                viewModel.AtestadoId = Convert.ToInt32(AtestadoId);
            }
                        
            return View("IncluirItem",viewModel);
        }

    }
}
