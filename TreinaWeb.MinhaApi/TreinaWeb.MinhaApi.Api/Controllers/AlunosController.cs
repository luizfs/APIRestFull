using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TreinaWeb.MinhaApi.AcessoDados.Entity.Context;
using TreinaWeb.MinhaApi.Api.AutoMapper;
using TreinaWeb.MinhaApi.Api.DTOs;
using TreinaWeb.MinhaApi.Api.Filters;
using TreinaWeb.MinhaApi.Dominio;
using TreinaWeb.MinhaApi.Repositorios.Entity;
using TreinaWeb.MinhaApi.Repositorios.Interfaces;

namespace TreinaWeb.MinhaApi.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Alunos")]
    public class AlunosController : ApiController
    {
        private IRepositorioTreinaWeb<Aluno, int> _repositorioAlunos
            = new RepositorioAlunos(new MinhaApiDbContext());


        public IHttpActionResult Get()
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar();
            List<AlunoDTO> dtos = AutoMapperManager.Instace.Mapper.Map<List<Aluno>, List<AlunoDTO>>(alunos);
            return Ok(dtos);
        }

        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
            if (aluno == null)
                return NotFound();
            AlunoDTO dto = AutoMapperManager.Instace.Mapper.Map<Aluno, AlunoDTO>(aluno);
            return Content(HttpStatusCode.OK, dto);

        }


        [Route("por-nome/{nomeAluno}")]
        public IHttpActionResult Get(string nomeAluno)
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar(s => s.Nome.ToLower().Contains(nomeAluno.ToLower()));
            List<AlunoDTO> dtos = AutoMapperManager.Instace.Mapper.Map<List<Aluno>, List<AlunoDTO>>(alunos);
            return Ok(dtos);
        }

        [ApplyModelValidation]
        public IHttpActionResult Post([FromBody] AlunoDTO dto)
        {

            try
            {
                Aluno aluno = AutoMapperManager.Instace.Mapper.Map<AlunoDTO, Aluno>(dto);
                _repositorioAlunos.Inserir(aluno);
                return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] AlunoDTO dto)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();
                Aluno aluno = AutoMapperManager.Instace.Mapper.Map<AlunoDTO, Aluno>(dto);
                aluno.Id = id.Value;
                _repositorioAlunos.Atualizar(aluno);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();
                Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
                if (aluno == null)
                    return NotFound();

                _repositorioAlunos.ExcluirPorId(id.Value);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
