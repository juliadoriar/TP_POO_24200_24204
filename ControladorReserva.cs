using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ControladorReserva
    {
        public Reserva CriarReserva(int quartoId, int utiId, DateTime dataEntrada, DateTime dataSaida, float precoCaucao)
        {
            int ultimoId = Reserva.GetUltimoId(); // Obter o último id de utilizador
            int reservaId = ++ultimoId; // Incrementar a variável de classe com o último id do utilizador e atribuí-lo para o objeto 
            Reserva.SetUltimoId(reservaId); // Guardar o último id de utilizador

            Reserva reserva = new Reserva(
                reservaId,
                quartoId,
                utiId,
                dataEntrada,
                dataSaida,
                precoCaucao);
            //AdicionarReserva(reserva);
            return reserva;
        }
    }
}
