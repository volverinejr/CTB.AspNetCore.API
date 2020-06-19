using Infra.Contexts;
using System.Linq;
using System.Collections.Generic;
using Domain.Modulos.TaxaSelic;
using Domain.Shared.Classes;
using Microsoft.EntityFrameworkCore;
using Domain.Shared.Entidade;
using System;

namespace Infra.Repository
{
    public class TaxaSelicRepository : ITaxaSelicRepository
    {
        private readonly DataContext _context;

        public TaxaSelicRepository(DataContext context)
        {
            _context = context;
        }


        public void Insert(TaxaSelicModel taxaSelicModel)
        {
            _context.TaxaSelic.Add(taxaSelicModel);
            _context.SaveChanges();
        }


        public void Update(TaxaSelicModel taxaSelicModel)
        {
            _context.Entry(taxaSelicModel).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(TaxaSelicModel taxaSelicModel)
        {
            _context.TaxaSelic.Remove(taxaSelicModel);
            _context.SaveChanges();
        }


        public TaxaSelicModel GetById(int id)
        {
            return _context.TaxaSelic
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public TaxaSelicModel GetById(int id, string usuario)
        {
            return (TaxaSelicModel)_context.TaxaSelic
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public IEnumerable<TaxaSelicModel> GetAll(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Ano" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }


            return (IEnumerable<TaxaSelicModel>)_context.TaxaSelic
                                    .Where(TaxaSelicExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .AsNoTracking()
                                    .Skip((pesquisa.Qtd * pesquisa.Pagina))
                                    .Take(pesquisa.Qtd)
                                    .OrderByDynamic(pesquisa.Campo, pesquisa.Ordem)
                                    .ToList();


        }

        public int GetTotalDeRegistros(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Ano" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return _context.TaxaSelic
                                    .Where(TaxaSelicExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .Count();
        }

        public TaxaSelicModel GetByAnoeMes(int ano, int mes)
        {
            return _context.TaxaSelic
                       .AsNoTracking()
                       .Where(x =>
                                   x.Ano == ano
                                   && x.Mes == mes
                           )
                       .FirstOrDefault();
        }
    }
}