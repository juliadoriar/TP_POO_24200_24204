using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class MenuResidencia
    {
        public static void ExibirMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("Menu Residência:");
                Console.WriteLine("1. Registar Utilizador");
                Console.WriteLine("2. Alterar Registo de Utilizador");
                Console.WriteLine("3. Listar Utilizadores");
                Console.WriteLine("4. Listar Moradores");
                Console.WriteLine("5. Criar Reserva");
                Console.WriteLine("6. Alterar Reserva");
                Console.WriteLine("7. Cancelar Reserva");
                Console.WriteLine("8. Listar Quartos");
                Console.WriteLine("9. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Utilizador.CriarUtilizador();
                        Console.WriteLine("Opção 1 - Registar Utilizador");
                        break;

                    case "2":
                        Console.WriteLine("Opção 2 - Alterar Registo de Utilizador");
                        break;

                    case "3":
                        Console.WriteLine("Opção 3 - Listar Utilizadores");
                        Utilizador.ImprimirListaDeUtilizadores();
                        break;

                    case "4":
                        Console.WriteLine("Opção 4 - Listar Moradores");
                        Morador.ImprimirListaDeMoradores();
                        break;

                    case "5":
                        Console.WriteLine("Opção 5 - Criar Reserva");
                        break;

                    case "6":
                        Console.WriteLine("Opção 6 - Alterar Reserva");
                        break;

                    case "7":
                        Console.WriteLine("Opção 7 - Cancelar Reserva");
                        break;

                    case "8":
                        Console.WriteLine("Opção 8 - Listar Quartos");
                        Quarto.ImprimirListaDeQuartos(Quarto.listaDeQuartos);
                        break;

                    case "9":
                        Console.WriteLine("Saindo...");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                // Aguardar o usuário pressionar Enter para limpar a tela e continuar
                Console.WriteLine("Pressione Enter para continuar...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

}
