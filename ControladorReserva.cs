using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ControladorReserva
    {
        /// <summary>
        /// Método que cria uma reserva
        /// </summary>
        /// <param name="quartoId"></param>
        /// <param name="utiId"></param>
        /// <param name="dataEntrada"></param>
        /// <param name="dataSaida"></param>
        /// <param name="precoCaucao"></param>
        /// <returns></returns>
        public Reserva CriarReserva(int quartoId, int utiId, DateTime dataEntrada, DateTime dataSaida, float precoCaucao)
        {
            int ultimoId = Reserva.LerUltimoIdReserva("reserva.json"); // Obter o último id da reserva
            int reservaId = ++ultimoId; // Incrementar a variável de classe com o último id da reserva e atribuí-lo para o objeto 

            Reserva reserva = new Reserva(
                reservaId,
                quartoId,
                utiId,
                dataEntrada,
                dataSaida,
                precoCaucao);

            if (AdicionarReserva(reserva))
            {
                Reserva.AtualizarUltimoIdNoJson(reservaId, "reserva.json");
                return reserva;
            }

            return null; // ou algum indicador de que a reserva não foi adicionada com sucesso
        }

        /// <summary>
        /// Método que adiciona uma reserva à lista de reservas
        /// </summary>
        /// <param name="novaReserva"></param>
        /// <returns>True se a reserva foi adicionada com sucesso, False caso contrário.</returns>
        public bool AdicionarReserva(Reserva novaReserva)
        {
            // Carregar a lista existente do arquivo
            List<Reserva> listaExistente = Reserva.CarregarListaDeReservas("reserva.json");

            if (novaReserva.DataEntrada < DateTime.Today)
            {
                Console.WriteLine("A data de entrada não pode ser anterior à data atual.");
                return false;
            }
            else if (novaReserva.DataSaida <= novaReserva.DataEntrada)
            {
                Console.WriteLine("A data de saída não pode ser anterior ou igual à data de entrada.");
                return false;
            }
            // Verificar se o quarto está ocupado naquelas datas
            else if (listaExistente.Exists(r =>
                novaReserva.QuartoId == r.QuartoId
                && novaReserva.DataEntrada >= r.DataEntrada
                && novaReserva.DataEntrada < r.DataSaida
                && novaReserva.DataSaida > r.DataEntrada))
            {
                Console.WriteLine("O quarto já está ocupado nessas datas.");
                return false;
            }
            // Verificar se o utilizador já tem uma reserva nas mesmas datas
            else if (listaExistente.Exists(r =>
                novaReserva.UtiId == r.UtiId
                && novaReserva.DataEntrada >= r.DataEntrada
                && novaReserva.DataEntrada < r.DataSaida
                && novaReserva.DataSaida > r.DataEntrada))
            {
                Console.WriteLine("O utilizador já tem outra reserva nessas datas.");
                return false;
            }
            else
            {
                listaExistente.Add(novaReserva);

                // Salvar a lista atualizada no arquivo
                Reserva.SalvarListaFicheiro("reserva.json", listaExistente);
                return true;
            }
        }

    }
}
