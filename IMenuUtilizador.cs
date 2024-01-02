using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Interface que apresenta o método ExibirMenu()
    /// </summary>
    public interface IMenuUtilizador
    {
        void ExibirMenu();
    }

    /// <summary>
    /// Classe que representa o menu de interação com operações relacionadas a cada tipo de utilizador
    /// </summary>
    public abstract class MenuUtilizadorBase : IMenuUtilizador
    {
        protected Utilizador UtilizadorAtual;

        public MenuUtilizadorBase(Utilizador utilizador)
        {
            UtilizadorAtual = utilizador;
        }

        public abstract void ExibirMenu();


    }

    /// <summary>
    /// Menu específico para moradores
    /// </summary>
    public class MenuMorador : MenuUtilizadorBase
    {
        private ControladorUtilizador controladorUtilizador;
        private ViewReserva viewReserva;
        public MenuMorador(Utilizador utilizador) : base(utilizador)
        {
            controladorUtilizador = new ControladorUtilizador();
            viewReserva = new ViewReserva();
        }

        public override void ExibirMenu()
        {
            bool continuar = true; // Variável para controlar a continuidade do loop

            while (continuar)// Loop para exibir o menu
            {
                Console.WriteLine("Menu Morador:");
                Console.WriteLine("1. Realizar Reserva");
                Console.WriteLine("2. Alterar Reserva");
                Console.WriteLine("3. Cancelar Reserva");
                Console.WriteLine("4. Buscar Reserva");
                Console.WriteLine("5. Alterar Cadastro");
                Console.WriteLine("6. Voltar ao Menu Inicial");
                Console.WriteLine("7. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("Opção 1 - Realizar Reserva");
                        viewReserva.MenuCriarReserva();
                        break;

                    case "2":
                        Console.WriteLine("Opção 2 - Alterar Reserva");
                        viewReserva.MenuEditarReserva();
                        break;

                    case "3":
                        Console.WriteLine("Opção 3 - Cancelar Reserva");
                        viewReserva.MenuExcluirReserva();
                        break;

                    case "4":
                        Console.WriteLine("Opção 4 - Buscar Reserva");
                        viewReserva.MenuBuscarReserva();
                        break;

                    case "5":
                        Console.WriteLine("Opção 5 - Alterar Cadastro");
                        controladorUtilizador.MenuEditarUtilizador();
                        break;

                    case "6":
                        Console.WriteLine("Opção 6 - Voltar ao Menu Inicial");
                        Console.Clear();
                        MenuInicial menuInicial = new MenuInicial(controladorUtilizador);
                        menuInicial.ExibirMenuInicial();
                        continuar = false;
                        break;
                    case "7":
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
        /// <summary>
        /// Menu específico para gestores
        /// </summary>
        public class MenuGestor : MenuUtilizadorBase
        {
            private ControladorUtilizador controladorUtilizador;
            private ControladorMorador controladorMorador;
            private ViewReserva viewReserva;
       
            public MenuGestor(Utilizador utilizador) : base(utilizador)
            {
                controladorUtilizador = new ControladorUtilizador();
                viewReserva = new ViewReserva();
                controladorMorador = new ControladorMorador();
            }

            public override void ExibirMenu()
            {

                bool continuar = true; // Variável para controlar a continuidade do loop

                while (continuar)  // Loop para exibir o menu
                {

                    Console.WriteLine("Menu Gestor:");
                    Console.WriteLine("1. Realizar Reserva");
                    Console.WriteLine("2. Alterar Reserva");
                    Console.WriteLine("3. Cancelar Reserva");
                    Console.WriteLine("4. Buscar Reserva");
                    Console.WriteLine("5. Alterar Cadastro");
                    Console.WriteLine("6. Listar Moradores");
                    Console.WriteLine("7. Listar Utilizadores");
                    Console.WriteLine("8. Listar Quartos");
                    Console.WriteLine("9. Voltar ao Menu Inicial");
                    Console.WriteLine("10. Sair");

                    Console.Write("Escolha uma opção: ");
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            Console.WriteLine("Opção 1 - Realizar Reserva");
                            viewReserva.MenuCriarReserva();
                            break;

                        case "2":
                            Console.WriteLine("Opção 2 - Alterar Reserva");
                            viewReserva.MenuEditarReserva();
                            break;

                        case "3":
                            Console.WriteLine("Opção 3 - Cancelar Reserva");
                            viewReserva.MenuExcluirReserva();
                            break;

                        case "4":
                            Console.WriteLine("Opção 4 - Buscar Reserva");
                            viewReserva.MenuBuscarReserva();
                            break;

                        case "5":
                            Console.WriteLine("Opção 5 - Alterar Cadastro");
                            controladorUtilizador.MenuEditarUtilizador();
                            break;

                        case "6":
                            Console.WriteLine("Opção 6 - Listar Moradores");
                            controladorMorador.ImprimirListaDeMoradores();
                            break;

                        case "7":
                            Console.WriteLine("Opção 7 - Listar Utilizadores");
                            controladorUtilizador.ImprimirListaDeUtilizadores();
                            break;

                        case "8":
                            Console.WriteLine("Opção 8 - Listar Quartos");
                            ControladorQuarto.ImprimirListaDeQuartos(Quarto.listaDeQuartos);
                            break;

                        case "9":
                            Console.WriteLine("Opção 9 - Voltar ao Menu Inicial");
                            Console.Clear();
                            MenuInicial menuInicial = new MenuInicial(controladorUtilizador);
                            menuInicial.ExibirMenuInicial();
                            continuar = false;
                            break;

                        case "10":
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

        /// <summary>
        /// Menu específico para funcionários
        /// </summary>    
        public class MenuFuncionario : MenuUtilizadorBase
        {
            private ControladorUtilizador controladorUtilizador;
            private ViewReserva viewReserva;
            private ControladorMorador controladorMorador;
            public MenuFuncionario(Utilizador utilizador) : base(utilizador)
            {
                controladorUtilizador = new ControladorUtilizador();
                viewReserva = new ViewReserva();
                controladorMorador = new ControladorMorador();
        }

            public override void ExibirMenu()
            {
                bool continuar = true; // Variável para controlar a continuidade do loop

                while (continuar)  // Loop para exibir o menu
                {
                    Console.WriteLine("Menu Funcionário:");
                    Console.WriteLine("1. Realizar Reserva");
                    Console.WriteLine("2. Alterar Reserva");
                    Console.WriteLine("3. Cancelar Reserva");
                    Console.WriteLine("4. Alterar Cadastro");
                    Console.WriteLine("5. Listar Moradores");
                    Console.WriteLine("6. Alterar Adimplência de um Morador");
                    Console.WriteLine("7. Listar Quartos");
                    Console.WriteLine("8. Voltar ao Menu Inicial");
                    Console.WriteLine("9. Sair");

                    Console.Write("Escolha uma opção: ");
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            Console.WriteLine("Opção 1 - Realizar Reserva");
                            viewReserva.MenuCriarReserva();
                            break;

                        case "2":
                            Console.WriteLine("Opção 2 - Alterar Reserva");
                            viewReserva.MenuEditarReserva();
                            break;

                        case "3":
                            Console.WriteLine("Opção 3 - Cancelar Reserva");
                            viewReserva.MenuExcluirReserva();
                            break;

                        case "4":
                            Console.WriteLine("Opção 4 - Alterar Cadastro");
                            controladorUtilizador.MenuEditarUtilizador();
                            break;

                        case "5":
                            Console.WriteLine("Opção 5 - Listar Moradores");
                            controladorMorador.ImprimirListaDeMoradores();
                            break;

                        case "6":
                            Console.WriteLine("Opção 6 - Alterar Adimplência de um Morador");
                            controladorMorador.EditarAdimplenciaMorador();
                            break;

                        case "7":
                            Console.WriteLine("Opção 7 - Listar Quartos");
                            ControladorQuarto.ImprimirListaDeQuartos(Quarto.listaDeQuartos);
                            break;

                        case "8":
                            Console.WriteLine("Opção 8 - Voltar ao Menu Inicial");
                            Console.Clear();
                            MenuInicial menuInicial = new MenuInicial(controladorUtilizador);
                            menuInicial.ExibirMenuInicial();
                            continuar = false;
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
