using System.Collections.Generic;
using System.Linq;
using Domain.Shared.Entidade;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using Domain.Modulos.Infracao.Natureza;
using Domain.Shared.Classes;

namespace Infracao
{
    public class NaturezaRepository : INaturezaRepository
    {
        private readonly DataContext _context;

        public NaturezaRepository(DataContext context)
        {
            _context = context;
        }

        public void Insert(NaturezaModel naturezaModel)
        {
            _context.InfracaoNatureza.Add(naturezaModel);
            _context.SaveChanges();
        }


        public void Update(NaturezaModel naturezaModel)
        {
            _context.Entry(naturezaModel).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(NaturezaModel naturezaModel)
        {
            _context.InfracaoNatureza.Remove(naturezaModel);
            _context.SaveChanges();
        }


        public NaturezaModel GetById(int id)
        {
            return _context.InfracaoNatureza
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public NaturezaModel GetById(int id, string usuario)
        {
            return (NaturezaModel)_context.InfracaoNatureza
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public IEnumerable<NaturezaModel> GetAll(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Nome", "Valor" };

            //Console.WriteLine(pesquisa);

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return (IEnumerable<NaturezaModel>)_context.InfracaoNatureza
                                    .Where(NaturezaExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .AsNoTracking()
                                    .Skip((pesquisa.Qtd * pesquisa.Pagina))
                                    .Take(pesquisa.Qtd)
                                    .OrderByDynamic(pesquisa.Campo, pesquisa.Ordem)
                                    .ToList();

        }


        public int GetTotalDeRegistros(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Nome", "Valor" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return _context.InfracaoNatureza
                                    .Where(NaturezaExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .Count();
        }

    }
}