using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.ViewModels;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ReflectionIT.Mvc.Paging;

namespace EPT.Controllers
{
     [Authorize]
    public class AcervoController : Controller
    {
        private readonly IAcervo _acervoRepository;
        private readonly IProfissionais _profissionaisRepository;
        private readonly IFuncoes _funcoesRepository;
        private readonly IAtestado _atestadoRepository;
        private readonly AppDbContext _context;

        public AcervoController(IAcervo acervoRepository, IProfissionais profissionaisRepository, IFuncoes funcoesRepository, IAtestado atestadoRepository, AppDbContext context)
        {
            _acervoRepository = acervoRepository;
            _profissionaisRepository = profissionaisRepository;
            _funcoesRepository = funcoesRepository;
            _atestadoRepository = atestadoRepository;
            _context = context;
        }


       
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "AcervoId")
        {

            var lista = _context.Acervo.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                lista = lista.Where(p => p.AcervoId.ToString().Contains(filter));
            }
            var model = await PagingList.CreateAsync(lista, 30, pageindex, sort, "AcervoId");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Incluir(int? idAtestado)
        {

            List<Profissionais> profissionais = _profissionaisRepository.Listar().ToList();
            ViewBag.Profissionais = new SelectList(profissionais, "ProfissionaisId", "Nome");

            List<Funcoes> funcoes = _funcoesRepository.Listar().ToList();
            ViewBag.Funcoes = new SelectList(funcoes, "FuncoesId", "Funcao");

            List<Atestado> atestado = _atestadoRepository.Listar().ToList();
            ViewBag.Atestado = new SelectList(atestado, "AtestadoId", "AtestadoId");

            AcervoViewModel acervoViewModel = new AcervoViewModel();
            {
                acervoViewModel.dt_inicio = new DateTime(1900, 1, 1);
                acervoViewModel.dt_fim = new DateTime(1900, 1, 1);

            }
            if(idAtestado !=0 && idAtestado!=null)
            {
                acervoViewModel.SelectedAtestadoId = (int)idAtestado;
            }

            return View(acervoViewModel);

        }

        [HttpPost]
        public IActionResult Incluir(AcervoViewModel acervo)
        {
            Atestado atestadoSelecionado = _atestadoRepository.AtestadoPorId(acervo.SelectedAtestadoId.ToString());

            
            if (acervo.dt_inicio == new DateTime(1900, 1, 1) && acervo.dt_fim == new DateTime(1900, 1, 1))
            {
                acervo.dt_inicio = atestadoSelecionado.dt_inicio;
                acervo.dt_fim = atestadoSelecionado.dt_fim;
            }

            _acervoRepository.Incluir(acervo);

            return RedirectToAction("Index");
        }

        public IActionResult Alterar(string idacervo)
        {
            var acervo = _acervoRepository.AcervoPorId(idacervo);

            List<Profissionais> profissionais = _profissionaisRepository.Listar().ToList();
            ViewBag.Profissionais = new SelectList(profissionais, "ProfissionaisId", "Nome");

            List<Funcoes> funcoes = _funcoesRepository.Listar().ToList();
            ViewBag.Funcoes = new SelectList(funcoes, "FuncoesId", "Funcao");

            List<Atestado> atestado = _atestadoRepository.Listar().ToList();
            ViewBag.Atestado = new SelectList(atestado, "AtestadoId", "AtestadoId");

            AcervoViewModel alteraracervo = new();
            {
                alteraracervo.numero_acervo = acervo.numero_acervo;
                alteraracervo.numero_ept = acervo.numero_ept;
                alteraracervo.SelectedAtestadoId = acervo.AtestadoId;
                alteraracervo.SelectedProfissionaisId = acervo.ProfissionaisId;
                alteraracervo.SelectedFuncoesId= acervo.FuncoesId;
                string id = alteraracervo.SelectedProfissionaisId.ToString();
                Profissionais prof = _profissionaisRepository.ProfissionalPorId(id);
                alteraracervo.nome = prof.Nome;
                string idfuncao = alteraracervo.SelectedFuncoesId.ToString();
                Funcoes func = _funcoesRepository.FuncaoPorId(idfuncao);
                alteraracervo.funcao = func.Funcao;
                alteraracervo.AcervoId = Convert.ToInt32(idacervo);
                alteraracervo.dt_inicio = acervo.dt_inicio;
                alteraracervo.dt_fim = acervo.dt_fim;
                alteraracervo.Link = acervo.Link;
            }

            return View(alteraracervo);
        }

        [HttpPost]

        public IActionResult Alterar(AcervoViewModel acervoAlterar, string acervoId)
        {
            _acervoRepository.Alterar(acervoAlterar, acervoId);
            return RedirectToAction("Index");
        }

        public IActionResult Excluir(string idacervo)
        {
            var excluir = _acervoRepository.AcervoPorId(idacervo);
            AcervoViewModel acervo = new AcervoViewModel();
            {
                acervo.AcervoId=excluir.AcervoId;
                acervo.numero_acervo = excluir.numero_acervo;
                acervo.numero_ept = excluir.numero_ept;


            }
            return View(acervo);
        }

        [HttpPost]

        public IActionResult Excluir(string idacervo, IFormCollection form)
        {
            _acervoRepository.Excluir(idacervo);
            return RedirectToAction("Index");
        }
    }
}
