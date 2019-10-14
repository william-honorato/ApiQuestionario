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
        public PerguntaModel Pergunta(int idPergunta)
        {
            var pergunta = listaPerguntas.Find(f => f.ID == idPergunta);
            return pergunta;
        }

        // POST: api/pergunta
        [HttpPost]
        [Route("")]
        public string AdicionarPergunta([FromBody]PerguntaModel pergunta)
        {
            if(pergunta != null)
            {
                if(listaPerguntas.Find(f => f.ID == pergunta.ID) != null)
                {
                    return $"O id {pergunta.ID} já está cadastrado em outra pergunta";
                }

                listaPerguntas.Add(pergunta);
                return "Pergunta adicionada com sucesso";
            }

            return "Erro ao adicionar pergunta";
        }

        // PUT: api/pergunta/id
        [HttpPut]
        [Route("{idPergunta}")]
        public string AtualizarPergunta(int idPergunta, [FromBody]PerguntaModel pergunta)
        {
            var obj = listaPerguntas.FirstOrDefault(x => x.ID == idPergunta);
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
        public string DeletarPergunta(int idPergunta)
        {
            var obj = listaPerguntas.FirstOrDefault(x => x.ID == idPergunta);
            if (obj != null)
            {
                listaPerguntas.Remove(obj);
                return "Pergunta excluida com suceso";
            }

            return "Pergunta não encontrada";
        }

        public bool PerguntaExiste(int idPergunta)
        {
            return listaPerguntas.Find(f => f.ID == idPergunta) != null;
        }
    }
}
