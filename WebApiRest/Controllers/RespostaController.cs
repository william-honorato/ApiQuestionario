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
        public RespostaModel Resposta(uint idResposta)
        {
            var resposta = listaRespostas.Find(f => f.Id == idResposta);
            return resposta;
        }

        // GET: api/resposta/pergunta/idPergunta
        [HttpGet]
        [Route("pergunta/{idPergunta}")]
        public RespostaModel PerguntaRespostas(uint idPergunta)
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
                    return $"Erro ao adicionar resposta, a pergunta com id {resposta.IdPergunta} não existe";
                }

                resposta.Id = resposta.ProximoId();
                listaRespostas.Add(resposta);
                return $"Resposta id {resposta.Id} adicionada com sucesso";
            }

            return "Erro ao adicionar resposta";
        }

        // PUT: api/resposta/id
        [HttpPut]
        [Route("")]
        public string AtualizarResposta([FromBody]RespostaModel resposta)
        {
            var obj = listaRespostas.FirstOrDefault(x => x.Id == resposta.Id);
            if (obj != null)
            {
                listaRespostas.Remove(obj);
                listaRespostas.Add(resposta);
                listaRespostas = listaRespostas.OrderBy(o => o.Id).ToList();
                return "Resposta atualizada com suceso";
            }

            return "Resposta não encontrada";
        }

        // DELETE: api/resposta/id
        [HttpDelete]
        [Route("{idResposta}")]
        public string DeletarResposta(uint idResposta)
        {
            var obj = listaRespostas.FirstOrDefault(x => x.Id == idResposta);
            if (obj != null)
            {
                listaRespostas.Remove(obj);
                return "Resposta excluida com suceso";
            }

            return "Resposta não encontrada";
        }

        public static List<RespostaModel> Respostas(uint idPergunta)
        {
            List<RespostaModel> listaRetorno = null;

            if (idPergunta > 0)
            {
                listaRetorno = listaRespostas.FindAll(f => f.IdPergunta == idPergunta);
            }

            return listaRetorno;
        }
    }
}
