﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class Quarto
    {
        protected int QuartoId { get; set; }
        protected string TipoQuarto { get; set; }
        protected string Andar { get; set; }
        protected int Capacidade { get; set; }
        protected float PrecoRenda { get; set; }
        protected bool Disponibilidade { get; set; }
    }
}
