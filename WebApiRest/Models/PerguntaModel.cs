using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class PerguntaModel
    {
        private static uint idPergunta = 0;

        public uint ProximoId()
        {
            return ++idPergunta;
        }

        public uint Id { get; set; }
        public uint IdQuestionario { get; set; }
        public string TextoPergunta { get; set; }
        public List<RespostaModel> listaRespostas { get; set; }
    }
}