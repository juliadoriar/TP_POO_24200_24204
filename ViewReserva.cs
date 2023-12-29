using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ViewReserva
    {
        private ControladorReserva controladorReserva; // Adiciona um campo para a instância do ControladorReserva

        public ViewReserva()
        {
            controladorReserva = new ControladorReserva(); // Inicializa a instância no construtor
        }
        public void CriarReserva()
        {
            Console.WriteLine("Criar Reserva");
            Console.WriteLine("-------------");
            Console.WriteLine();

            Console.WriteLine("Insira o id do quarto: ");
            int quartoId = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira o id do utilizador: ");
            int utiId = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira a data de entrada: ");
            DateTime dataEntrada = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Insira a data de saída: ");
            DateTime dataSaida = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Insira o preço da caução: ");
            float precoCaucao = float.Parse(Console.ReadLine());


            controladorReserva.CriarReserva(quartoId, utiId, dataEntrada, dataSaida, precoCaucao);
        }

        /// <summary>
        /// Método para imprimir a lista de reservas
        /// </summary>
        public void ImprimirListaDeReservas()
        {

            List<Reserva> listaDeReservasAtual = Reserva.CarregarListaDeReservas("reserva.json");

            if (listaDeReservasAtual == null)
            {
                Console.WriteLine("Não há reservas registadas");
            }
            else
            {
                Console.WriteLine("Lista de Reservas");
                Console.WriteLine("--------------------------------------------");
                foreach (Reserva reserva in listaDeReservasAtual)
                {
                    Console.WriteLine($"ID: {reserva.ReservaId}, ID do Quarto: {reserva.QuartoId}, ID do Utilizador: {reserva.UtiId}, Data de Entrada: {reserva.DataEntrada:dd-MM-yyyy}, Data de Saída: {reserva.DataSaida:dd-MM-yyyy}, Preço da Caução: {reserva.PrecoCaucao}, Ativo: {reserva.IsAtivo}, Data da Reserva: {reserva.DataReserva:dd-MM-yyyy}");
                }
            }

        }

        /*public static void AtualizarReserva()
        {
            Console.WriteLine("Atualizar Reserva");
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Insira o id da reserva: ");
            int reservaId = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira o id do quarto: ");
            int quartoId = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira o id do utilizador: ");
            int utiId = int.Parse(Console.ReadLine());

            Console.WriteLine("Insira a data de entrada: ");
            DateTime dataEntrada = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Insira a data de saída: ");
            DateTime dataSaida = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Insira o preço da caução: ");
            float precoCaucao = float.Parse(Console.ReadLine());

            Console.WriteLine("Insira se a reserva está ativa ou não: ");
            bool isAtivo = bool.Parse(Console.ReadLine());
        }*/
    }
}
