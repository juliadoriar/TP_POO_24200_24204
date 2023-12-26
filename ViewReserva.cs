using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ViewReserva
    {
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


            ControladorReserva.CriarReserva(quartoId, utiId, dataEntrada, dataSaida, precoCaucao);
        }

        /*public static void ListarReservas()
        {
            Console.WriteLine("Listar Reservas");
            Console.WriteLine("---------------");
            Console.WriteLine();

            foreach (Reserva reserva in Reserva.listaDeReservas)
            {
                Console.WriteLine(reserva.ToString());
            }
        }

        public static void AtualizarReserva()
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
