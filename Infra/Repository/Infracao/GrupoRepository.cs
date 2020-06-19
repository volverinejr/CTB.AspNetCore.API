using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Modulos.Infracao.Grupo;
using Domain.Shared.Classes;
using Domain.Shared.Entidade;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infracao
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly DataContext _context;

        public GrupoRepository(DataContext context)
        {
            _context = context;
        }

        public void Insert(GrupoModel grupoModel)
        {
            _context.InfracaoGrupo.Add(grupoModel);
            _context.SaveChanges();
        }


        public void Update(GrupoModel grupoModel)
        {
            _context.Entry(grupoModel).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(GrupoModel grupoModel)
        {
            _context.InfracaoGrupo.Remove(grupoModel);
            _context.SaveChanges();
        }


        public GrupoModel GetById(int id)
        {
            return _context.InfracaoGrupo
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public GrupoModel GetById(int id, string usuario)
        {
            return (GrupoModel)_context.InfracaoGrupo
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public IEnumerable<GrupoModel> GetAll(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Nome" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return (IEnumerable<GrupoModel>)_context.InfracaoGrupo
                                    .Where(GrupoExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .AsNoTracking()
                                    .Skip((pesquisa.Qtd * pesquisa.Pagina))
                                    .Take(pesquisa.Qtd)
                                    .OrderByDynamic(pesquisa.Campo, pesquisa.Ordem)
                                    .ToList();
        }


        public int GetTotalDeRegistros(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Nome" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return _context.InfracaoGrupo
                                    .Where(GrupoExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .Count();
        }
    }
}