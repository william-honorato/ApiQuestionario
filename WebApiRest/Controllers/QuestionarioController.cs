using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRest.Controllers;
using WebApiRest.Models;

namespace WebApiRest.Controllers
{
    [RoutePrefix("api/questionario")]
    public class QuestionarioController : ApiController
    {        
        private static List<QuestionarioModel> listaQuestionarios = new List<QuestionarioModel>();
       
        // GET: api/questionario
        [HttpGet]
        [Route("")]
        public IEnumerable<QuestionarioModel> Questionarios()
        {
            return listaQuestionarios;
        }

        // GET: api/questionario/id
        [HttpGet]
        [Route("{idQuestionario}")]
        public QuestionarioModel Questionario(uint idQuestionario)
        {
            QuestionarioModel questionario = null;

            if (idQuestionario > 0)
            {
                questionario = listaQuestionarios.Find(f => f.Id == idQuestionario);
                questionario.listaPerguntas = PerguntaController.PerguntasRespostas(idQuestionario);
            }

            return questionario;
        }

        // POST: api/questionario
        [HttpPost]
        [Route("")]
        public string AdicionarQuestionario([FromBody]QuestionarioModel questionario)
        {
            if (questionario != null)
            {
                if (listaQuestionarios.Find(f => f.Id == questionario.Id) != null)
                {
                    return $"O id {questionario.Id} já está cadastrado em outro questionario";
                }

                listaQuestionarios.Add(questionario);
                return $"Questionario id {questionario.Id} adicionado com sucesso";
            }

            return "Erro ao adicionar questionario";
        }

        // PUT: api/questionario/id
        [HttpPut]
        [Route("")]
        public string Atualizarquestionario([FromBody]QuestionarioModel questionario)
        {
            var obj = listaQuestionarios.FirstOrDefault(x => x.Id == questionario.Id);

            if (obj != null)
            {
                obj = questionario;
                return "Questionario atualizada com suceso";
            }

            return "Questionario não encontrada";
        }

        // DELETE: api/questionario/id
        [HttpDelete]
        [Route("{idQuestionario}")]
        public string Deletarquestionario(uint idQuestionario)
        {
            var obj = listaQuestionarios.FirstOrDefault(x => x.Id == idQuestionario);

            if (obj != null)
            {
                listaQuestionarios.Remove(obj);
                return "Questionario excluida com suceso";
            }

            return "Questionario não encontrada";
        }

        public static bool QuestionarioExiste(uint idQuestionario)
        {
            return listaQuestionarios.Find(f => f.Id == idQuestionario) != null;
        }
    }
}