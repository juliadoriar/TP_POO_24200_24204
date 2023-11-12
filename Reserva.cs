using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class Reserva
    {
        protected int ReservaId { get; set; }
        protected int QuartoId { get; set; }
        protected int UtiId { get; set; }
        protected DateTime DataEntrada { get; set; }
        protected DateTime DataSaida { get; set; }
        protected float PrecoCaucao { get; set; }
        protected bool Estado { get; set; }
        protected DateTime DataReserva { get; set; }
    }
}
