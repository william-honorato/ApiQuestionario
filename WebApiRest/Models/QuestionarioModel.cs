using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class QuestionarioModel
    {
        private static uint idQuestionario = 0;

        public QuestionarioModel()
        {
            this.Id = ++idQuestionario;
        }

        public uint Id { get; set; }
        public string Nome { get; set; }
        public List<PerguntaModel> listaPerguntas { get; set; }
    }
}