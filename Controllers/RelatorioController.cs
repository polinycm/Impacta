using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.Repositories.Repository;
using EPT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Authentication.ExtendedProtection;

namespace EPT.Controllers
{
    [Authorize]

    public class RelatorioController : Controller
    {
        private readonly IRelatorio _relatorioRepository;
        private readonly IAtestado _atestadoRepository;
        private readonly IAtestadoItem _atestadoItemRepository;
        private readonly IAcervo _acervoRepository;
        private readonly IFuncoes _funcoesRepository;
        private readonly IProfissionais _profissionaisRepository;
        private readonly IItens _itensRepository;

        public RelatorioController(IRelatorio relatorioRepository, IAtestado atestadoRepository, IAtestadoItem atestadoItemRepository, IAcervo acervoRepository,IFuncoes funcoesRepository, IProfissionais profissionaisRepository, IItens itensRepository)
        {
            _relatorioRepository = relatorioRepository;
            _atestadoRepository = atestadoRepository;
            _atestadoItemRepository = atestadoItemRepository;
            _acervoRepository = acervoRepository;
            _funcoesRepository = funcoesRepository;
            _profissionaisRepository = profissionaisRepository;
            _itensRepository = itensRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Acervo1(int NomeId, int FuncaoId, int AtestadoId)
        {
            var acervos = _relatorioRepository.Acervo();

            List<Profissionais> profissionais = _profissionaisRepository.Listar().ToList();
            ViewBag.Profissionais = new SelectList(profissionais, "ProfissionaisId", "Nome");

            List<Funcoes> funcoes = _funcoesRepository.Listar().ToList();
            ViewBag.Funcoes = new SelectList(funcoes, "FuncoesId", "Funcao");

            List<Atestado> atestado = _atestadoRepository.Listar().ToList();
            ViewBag.Atestado = new SelectList(atestado, "AtestadoId", "AtestadoId");

            if (NomeId != 0 || AtestadoId != 0 || FuncaoId != 0)
            {
                 acervos = _relatorioRepository.AcervoFiltro1(NomeId, AtestadoId, FuncaoId);

            }

            return View(acervos);
        }

        public IActionResult Detalhe(string idAtestado, string n_ept, string n_acervo, string profissional, string funcao)
        {
            RelatorioAcervoDetalheViewModel viewModel = new();
            {
                Atestado atestado = new Atestado();
                {
                    atestado = _atestadoRepository.AtestadoPorId(idAtestado);
                }
                List<AtestadoItem> atestadoitem =new List<AtestadoItem>();
                {
                    int id = Convert.ToInt32(idAtestado);
                    var lista = _atestadoItemRepository.Listar(id);
                    atestadoitem=lista.ToList();
                }

                Acervo acervo = new Acervo();
                {
                    acervo.AtestadoId = Convert.ToInt32(idAtestado);
                    acervo.numero_ept = n_ept;
                    acervo.numero_acervo = n_acervo;
                    acervo.dt_inicio = _acervoRepository.AcervoPorN_Acervo(n_acervo).dt_inicio;
                    acervo.dt_fim = _acervoRepository.AcervoPorN_Acervo(n_acervo).dt_fim;

                }
                viewModel.atestado = atestado;
                viewModel.n_ept= n_ept;
                viewModel.n_acervo= n_acervo;
                viewModel.funcao= funcao;
                viewModel.profissional  = profissional;
                viewModel.itens = atestadoitem;
                viewModel.dt_inicio = acervo.dt_inicio;
                viewModel.dt_fim = acervo.dt_fim;

            }


            return View(viewModel);
        }

        
    }
}
