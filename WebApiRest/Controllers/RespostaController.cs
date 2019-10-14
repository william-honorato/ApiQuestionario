using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiRest.Models;

namespace WebApiRest.Controllers
{
    [RoutePrefix("api/resposta")]
    public class RespostaController : ApiController
    {
        private static List<RespostaModel> listaRespostas = new List<RespostaModel>();

        // GET: api/resposta
        [HttpGet]
        [Route("")]
        public IEnumerable<RespostaModel> Respostas()
        {
            return listaRespostas;
        }

        // GET: api/resposta/id
        [HttpGet]
        [Route("{idResposta}")]
        public RespostaModel Resposta(int idResposta)
        {
            var resposta = listaRespostas.Find(f => f.ID == idResposta);
            return resposta;
        }

        // GET: api/resposta/pergunta/idPergunta
        [HttpGet]
        [Route("pergunta/{idPergunta}")]
        public RespostaModel PerguntaRespostas(int idPergunta)
        {
            var resposta = listaRespostas.Find(f => f.IdPergunta == idPergunta);
            return resposta;
        }

        // POST: api/resposta
        [HttpPost]
        [Route("")]
        public string AdicionarResposta([FromBody]RespostaModel resposta)
        {
            if (resposta != null)
            {
                var p = new PerguntaController();
                if (!p.PerguntaExiste(resposta.IdPergunta))
                {
                    return $"O id {resposta.IdPergunta} não existe cadastrado para nenhuma pergunta";
                }

                if (listaRespostas.Find(f => f.ID == resposta.ID) != null)
                {
                    return $"O id {resposta.ID} já está cadastrado em outra resposta";
                }

                listaRespostas.Add(resposta);
                return "Resposta adicionada com sucesso";
            }

            return "Erro ao adicionar resposta";
        }

        // PUT: api/resposta/id
        [HttpPut]
        [Route("{idResposta}")]
        public string AtualizarResposta(int idResposta, [FromBody]RespostaModel resposta)
        {
            var obj = listaRespostas.FirstOrDefault(x => x.ID == idResposta);
            if (obj != null)
            {
                obj = resposta;
                return "Resposta atualizada com suceso";
            }

            return "Resposta não encontrada";
        }

        // DELETE: api/resposta/id
        [HttpDelete]
        [Route("{idResposta}")]
        public string DeletarResposta(int idResposta)
        {
            var obj = listaRespostas.FirstOrDefault(x => x.ID == idResposta);
            if (obj != null)
            {
                listaRespostas.Remove(obj);
                return "Resposta excluida com suceso";
            }

            return "Resposta não encontrada";
        }
    }
}
