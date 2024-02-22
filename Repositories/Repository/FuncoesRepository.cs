using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EPT.Repositories.Repository
{
    public class FuncoesRepository :IFuncoes
    {
        private readonly AppDbContext _context;

        public FuncoesRepository(AppDbContext context)
        {
            _context = context;
        }



        public void Alterar(Funcoes funcoes, string funcaoId)
        {

            var funcaoalterar = FuncaoPorId(funcaoId);

            if (funcaoalterar != null) 
            {
                funcaoalterar.Funcao = funcoes.Funcao;
                _context.SaveChanges();
            }

        }

        public void Excluir(string funcaoId)
        {
            var excluir = FuncaoPorId(funcaoId);
            _context.Funcoes.Remove(excluir);
            _context.SaveChanges();
        }

        public Funcoes FuncaoPorId(string idfuncao)
        {
            return _context.Funcoes.FirstOrDefault(f=>f.FuncoesId.ToString() == idfuncao);
        }

        public void Incluir(Funcoes funcoes)
        {
            _context.Funcoes.Add(funcoes);
            _context.SaveChanges();
        }

        public IEnumerable<Funcoes> Listar()
        {
            return _context.Funcoes;
        }
    }
}
