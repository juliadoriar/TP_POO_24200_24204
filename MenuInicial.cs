﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    internal class MenuInicial
    {
        private ControladorUtilizador controladorUtilizador;

        public MenuInicial(ControladorUtilizador controladorUtilizador)
        {
            this.controladorUtilizador = controladorUtilizador;
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
                    ControladorUtilizador.Login();
                    Utilizador utilizadorLogado = ControladorUtilizador.Login();
                    ExibirMenuAposLogin(utilizadorLogado); // Exibe o menu apropriado com base no tipo de utilizador reconhecido no login
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
                    //new MenuMorador().ExibirMenu();
                    break;
                case "gestor":
                    //new MenuGestor().ExibirMenu();
                    break;
                case "funcionario":
                    //new MenuFuncionario().ExibirMenu();
                    break;
                default:
                    Console.WriteLine("Tipo de utilizador não reconhecido.");
                    break;
            }
        }

    }
}
