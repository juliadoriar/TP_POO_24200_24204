using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    internal class MenuInicial
    {
        private ControladorUtilizador controladorUtilizador;
        private ControladorMorador controladorMorador;
        private ControladorGestor controladorGestor;
        private ControladorFuncionario controladorFuncionario;

        public MenuInicial(ControladorUtilizador controladorUtilizador)
        {
            this.controladorUtilizador = controladorUtilizador;
            controladorMorador = new ControladorMorador();
            controladorGestor = new ControladorGestor();
            controladorFuncionario = new ControladorFuncionario();
            controladorUtilizador.SetControladorMorador(controladorMorador);
            controladorUtilizador.SetControladorGestor(controladorGestor);
            controladorUtilizador.SetControladorFuncionario(controladorFuncionario);
        }
        public void ExibirMenuInicial()
        {
            Console.WriteLine("Bem-vindo! Escolha uma opção:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Registrar");
            Console.WriteLine("3. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Utilizador utilizadorLogado = ControladorUtilizador.Login();

                    if (utilizadorLogado != null)
                    {
                        ExibirMenuAposLogin(utilizadorLogado); // Exibe o menu com base no tipo de utilizador reconhecido no login
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Credenciais inválidas. Tente novamente.");
                        Console.WriteLine("\nPressione Enter para voltar ao menu inicial...");
                        Console.ReadLine();
                        Console.Clear();
                        ExibirMenuInicial();
                    }
                    break;
                case "2":
                    controladorUtilizador.CriarUtilizador();
                    break;
                case "3":
                    Console.WriteLine("Até logo!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    ExibirMenuInicial();
                    break;
            }
        }

        private void ExibirMenuAposLogin(Utilizador utilizador)
        {
            // Verifica o tipo de utilizador e exibe o menu apropriado
            switch (utilizador.GetTipoUtilizador().ToLower())
            {
                case "morador":
                    new MenuMorador(utilizador).ExibirMenu();
                    break;
                case "gestor":
                    new MenuGestor(utilizador).ExibirMenu();
                    break;
                case "funcionario":
                    new MenuFuncionario(utilizador).ExibirMenu();
                    break;
                default:
                    Console.WriteLine("Tipo de utilizador não reconhecido."); //opção para identificar algum utilizador que por algum erro tenha sido salvo com um tipo não reconhecido
                    break;
            }
        }

    }
}
