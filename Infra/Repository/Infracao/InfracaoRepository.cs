using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Infracao;
using Domain.Shared.Classes;
using Domain.Shared.Entidade;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infracao
{
    public class InfracaoRepository : IInfracaoRepository
    {
        private readonly DataContext _context;

        public InfracaoRepository(DataContext context)
        {
            _context = context;
        }

        public void Insert(InfracaoModel infracaoModel)
        {
            _context.Infracao.Add(infracaoModel);
            _context.SaveChanges();
        }


        public void Update(InfracaoModel infracaoModel)
        {
            _context.Entry(infracaoModel).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(InfracaoModel infracaoModel)
        {
            _context.Infracao.Remove(infracaoModel);
            _context.SaveChanges();
        }


        public InfracaoModel GetById(int id)
        {
            return _context.Infracao
                        .Include(g => g.Grupo)
                        .Include(n => n.Natureza)
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public InfracaoModel GetById(int id, string usuario)
        {
            return (InfracaoModel)_context.Infracao
                        .Include(g => g.Grupo)
                        .Include(n => n.Natureza)
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
        }


        public IEnumerable<InfracaoModel> GetAll(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Codigo", "Descricao", "AmparoLegal", "GrupoId", "NaturezaId" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return (IEnumerable<InfracaoModel>)_context.Infracao
                                    .Include(g => g.Grupo)
                                    .Include(n => n.Natureza)
                                    .Where(InfracaoExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .AsNoTracking()
                                    .Skip((pesquisa.Qtd * pesquisa.Pagina))
                                    .Take(pesquisa.Qtd)
                                    .OrderByDynamic(pesquisa.Campo, pesquisa.Ordem)
                                    .ToList();
        }


        public int GetTotalDeRegistros(Pesquisa pesquisa)
        {
            string[] camposPesquisa = { "Id", "Codigo", "Descricao", "AmparoLegal", "GrupoId", "NaturezaId" };

            if (Array.IndexOf(camposPesquisa, pesquisa.Campo) == -1)
            {
                pesquisa.Campo = "Id";
            }

            return _context.Infracao
                                    .Where(InfracaoExpressao.SetWhere(pesquisa.Campo, pesquisa.Filtro))
                                    .Count();
        }

        public IEnumerable<InfracaoModel> GetByCodigo(string codigo)
        {
            return _context.Infracao
                        .AsNoTracking()
                        .Where(x => x.Codigo == codigo)
                        .OrderBy(x => x.ValidadeInicio)
                        .ToList();
        }

        public InfracaoModel GetByCodigoEValidadeInicio(string codigo, DateTime validadeInicio)
        {
            return _context.Infracao
                        .AsNoTracking()
                        .Where(x =>
                            x.Codigo == codigo
                            && x.ValidadeInicio == validadeInicio
                        )
                        .FirstOrDefault();
        }

        public InfracaoModel GetByCodigoValidadeInicioMenor(int id, string codigo, DateTime validadeInicio)
        {
            return _context.Infracao
                        .Where(x =>
                            x.Id != id
                            && x.Codigo == codigo
                            && x.ValidadeInicio < validadeInicio
                        )
                        .OrderByDescending(x => x.ValidadeInicio)
                        .Take(1)
                        .FirstOrDefault();
        }


        public InfracaoModel GetByCodigoValidadeInicioMaior(int id, string codigo, DateTime validadeInicio)
        {
            return _context.Infracao
                        .Where(x =>
                            x.Id != id
                            && x.Codigo == codigo
                            && x.ValidadeInicio > validadeInicio
                        )
                        .OrderBy(x => x.ValidadeInicio)
                        .Take(1)
                        .FirstOrDefault();
        }

    }
}