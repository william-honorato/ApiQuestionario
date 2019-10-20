using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRest.Models;

namespace WebApiRest.Controllers
{
    [RoutePrefix ("api/pergunta")]
    public class PerguntaController : ApiController
    {
        private static List<PerguntaModel> listaPerguntas = new List<PerguntaModel>();

        // GET: api/pergunta
        [HttpGet]
        [Route("")]
        public IEnumerable<PerguntaModel> Perguntas()
        {
            return listaPerguntas;
        }

        // GET: api/pergunta/id
        [HttpGet]
        [Route("{idPergunta}")]
        public PerguntaModel Pergunta(uint idPergunta)
        {
            var pergunta = listaPerguntas.Find(f => f.Id == idPergunta);
            pergunta.listaRespostas = RespostaController.Respostas(idPergunta);
            return pergunta;
        }

        // POST: api/pergunta
        [HttpPost]
        [Route("")]
        public string AdicionarPergunta([FromBody]PerguntaModel pergunta)
        {
            if(pergunta != null)
            {
                if (QuestionarioController.QuestionarioExiste(pergunta.IdQuestionario))
                {
                    pergunta.Id = pergunta.ProximoId();
                    listaPerguntas.Add(pergunta);
                    return $"Pergunta id {pergunta.Id} adicionada com sucesso";
                }
                else
                {
                    return $"Erro ao adicionar pergunta, o questionario com id {pergunta.IdQuestionario} não existe";
                }
            }

            return "Erro ao adicionar pergunta";
        }

        // PUT: api/pergunta/id
        [HttpPut]
        [Route("")]
        public string AtualizarPergunta([FromBody]PerguntaModel pergunta)
        {
            var obj = listaPerguntas.FirstOrDefault(x => x.Id == pergunta.Id);
            if (obj != null)
            {
                obj = pergunta;
                return "Pergunta atualizada com suceso";
            }

            return "Pergunta não encontrada";
        }

        // DELETE: api/pergunta/id
        [HttpDelete]
        [Route("{idPergunta}")]
        public string DeletarPergunta(uint idPergunta)
        {
            var obj = listaPerguntas.FirstOrDefault(x => x.Id == idPergunta);
            if (obj != null)
            {
                listaPerguntas.Remove(obj);
                return "Pergunta excluida com suceso";
            }

            return "Pergunta não encontrada";
        }

        public bool PerguntaExiste(uint idPergunta)
        {
            return listaPerguntas.Find(f => f.Id == idPergunta) != null;
        }

        public static List<PerguntaModel> PerguntasRespostas(uint idQuestionario)
        {
            List<PerguntaModel> listaRetorno = null;

            if (idQuestionario > 0)
            {
                listaRetorno = listaPerguntas.FindAll(f => f.IdQuestionario == idQuestionario);

                foreach (var item in listaRetorno)
                {
                    item.listaRespostas = RespostaController.Respostas(item.Id);
                }
            }

            return listaRetorno;
        }
    }
}