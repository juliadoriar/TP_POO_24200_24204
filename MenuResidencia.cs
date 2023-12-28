using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// // Classe que representa o menu de interação com operações relacionadas a uma residência
    /// </summary>
    public class MenuResidencia
    {

        private ControladorUtilizador controladorUtilizador;

        public MenuResidencia()
        {
            controladorUtilizador = new ControladorUtilizador();
        }

        /// <summary>
        /// // Método estático para exibir o menu
        /// </summary>
        public void ExibirMenu()
        {
            bool continuar = true; // Variável para controlar a continuidade do loop

            while (continuar)// Loop para exibir o menu
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
                        Console.WriteLine("Opção 1 - Registar Utilizador");
                        //chamar método criar utilizador
                        controladorUtilizador.CriarUtilizador();
                        break;

                    case "2":
                        Console.WriteLine("Opção 2 - Alterar Registo de Utilizador");
                        break;

                    case "3":
                        Console.WriteLine("Opção 3 - Listar Utilizadores");
                        controladorUtilizador.ImprimirListaDeUtilizadores();
                        break;

                    case "4":
                        Console.WriteLine("Opção 4 - Listar Moradores");
                        ControladorMorador.ImprimirListaDeMoradores();
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
                        ControladorQuarto.ImprimirListaDeQuartos(Quarto.listaDeQuartos);
                        break;

                    case "9":
                        Console.WriteLine("Saindo...");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                if (continuar != false) // Aguardar o usuário pressionar Enter para limpar o ecrã e continuar
                {
                    Console.WriteLine("\nPressione Enter para voltar ao menu...");
                    Console.ReadLine();
                    Console.Clear();
                }

            }
        }
    }

}
