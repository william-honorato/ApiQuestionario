using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class RespostaModel
    {
        private static uint idResposta = 0;

        public uint ProximoId()
        {
            return ++idResposta;
        }

        public uint Id { get; set; }
        public uint IdPergunta { get; set; }
        public string TextoResposta { get; set; }
        public bool RespostaCorreta { get; set; }
    }
}