using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class RespostaModel
    {
        public int ID { get; set; }
        public int IdPergunta { get; set; }
        public string TextoResposta { get; set; }
        public bool RespostaCorreta { get; set; }
    }
}