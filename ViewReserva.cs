using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TP_POO_24200_24204.ControladorReserva;

namespace TP_POO_24200_24204
{
    public class ViewReserva
    {
        #region Instâncias de ViewReserva
        private ControladorReserva controladorReserva;

        /// <summary>
        /// Instancia um objeto de ControladorReserva
        /// </summary>
        /// <param name="controladorReserva"></param>
        public void SetControladorReserva(ControladorReserva controladorReserva)
        {
            this.controladorReserva = controladorReserva;
        }
        #endregion

        #region CRUD Reserva
        #region Criar Reserva
        /// <summary>
        /// Cria uma reserva
        /// </summary>
        public void MenuCriarReserva()
        {
            // Título e cabeçalho da operação
            Console.WriteLine("Criar Reserva");
            Console.WriteLine("-------------");
            Console.WriteLine();

            // Entrada de dados para criar uma nova reserva
            int quartoId = Servico.LerInteiro("Insira o ID do quarto: ");
            int utiId = Servico.LerInteiro("Insira o ID do utilizador: ");
            DateTime dataEntrada = Servico.LerData("Insira a data de entrada (dd-MM-yyyy): ");
            DateTime dataSaida = Servico.LerData("Insira a data de saída (dd-MM-yyyy): ");
            float precoCaucao = Servico.LerFloat("Insira o preço da caução: ");

            // Chama o controlador para criar a reserva
            controladorReserva.CriarReserva(quartoId, utiId, dataEntrada, dataSaida, precoCaucao);
        }
        #endregion

        #region Buscar Reserva
        /// <summary>
        /// Busca uma reserva
        /// </summary>
        /// <returns></returns>
        public Predicate<Reserva> MenuBuscarReserva()
        {
            // Inicializa o predicado como nulo
            Predicate<Reserva> predicado = null;
            // Obtém a lista de reservas atual do controlador
            List<Reserva> listaDeReservasAtual = controladorReserva.ObterListaAtual();

            // Loop para escolher o critério de busca
            while (true)
            {
                Console.WriteLine("Escolha o critério de busca:");
                Console.WriteLine("1. Por ID da Reserva");
                Console.WriteLine("2. Por ID do Quarto");
                Console.WriteLine("3. Por ID do Utilizador");
                Console.WriteLine("4. Por Data de Entrada");
                Console.WriteLine("5. Por Data de Saída");
                Console.WriteLine("6. Por Data da Reserva");

                // Entrada de opção do usuário
                int opcao = Servico.LerInteiro("");

                int valorInteiro;
                DateTime valorData;

                // Switch para definir o predicado com base na opção escolhida
                switch (opcao)
                {
                    case 1:
                        valorInteiro = Servico.LerInteiro("Digite o ID da Reserva:");
                        predicado = r => r.ReservaId == valorInteiro;
                        break;
                    case 2:
                        valorInteiro = Servico.LerInteiro("Digite o ID do Quarto:");
                        predicado = r => r.QuartoId == valorInteiro;
                        break;
                    case 3:
                        valorInteiro = Servico.LerInteiro("Digite o ID do Utilizador:");
                        predicado = r => r.UtiId == valorInteiro;
                        break;
                    case 4:
                        valorData = Servico.LerData("Digite a Data de Entrada (dd-MM-yyyy):");
                        predicado = r => r.DataEntrada == valorData;
                        break;
                    case 5:
                        valorData = Servico.LerData("Digite a Data de Saída (dd-MM-yyyy):");
                        predicado = r => r.DataSaida == valorData;
                        break;
                    case 6:
                        valorData = Servico.LerData("Digite a Data da Reserva (dd-MM-yyyy):");
                        predicado = r => r.DataReserva == valorData;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                // Se uma opção válida foi escolhida, sai do loop
                if (opcao >= 1 && opcao <= 6)
                {
                    break;
                }
            }

            // Se um predicado foi definido, realiza a busca e exibe os resultados
            if (predicado != null)
            {
                List<Reserva> listaDeReservasSelecionadas = controladorReserva.BuscarReserva(predicado);
                ExibirDetalhesListaReserva(listaDeReservasSelecionadas);
            }

            return predicado;
        }
        #endregion


        #region Editar Reserva
        /// <summary>
        /// Edita uma reserva
        /// </summary>
        public void MenuEditarReserva()
        {
            // Obtém o predicado para buscar as reservas
            Predicate<Reserva> predicado = MenuBuscarReserva();

            // Busca as reservas com base no predicado
            List<Reserva> reservasEncontradas = controladorReserva.BuscarReserva(predicado);

            if (reservasEncontradas != null && reservasEncontradas.Count > 0)
            {
                // Solicita ao usuário que escolha o ID da reserva a ser atualizada
                int idEscolhido = Servico.LerInteiro("Escolha o ID da reserva que deseja atualizar: ");

                // Busca a reserva escolhida
                Reserva reservaEscolhida = reservasEncontradas.FirstOrDefault(r => r.ReservaId == idEscolhido);

                // Exibe detalhes da reserva escolhida
                ExibirDetalhesReserva(reservaEscolhida);

                if (reservaEscolhida != null)
                {
                    // Chama o controlador para editar a reserva
                    controladorReserva.EditarReserva(reservaEscolhida);
                }
                else
                {
                    Console.WriteLine($"Reserva com ID {idEscolhido} não encontrada. A atualização não foi realizada.");
                }
            }
            else
            {
                Console.WriteLine("Reserva não encontrada. A atualização não foi realizada.");
            }
        }

        /// <summary>
        /// Menu para escolher o TipoCampo a ser atualizado
        /// </summary>
        /// <returns></returns>
        public TipoCampo MenuSelecionarCampo()
        {
            TipoCampo tipoCampo = TipoCampo.Nulo; // Valor padrão

            while (true)
            {
                Console.WriteLine("Escolha o campo que deseja atualizar:");
                Console.WriteLine("1. ID do Quarto");
                Console.WriteLine("2. ID do Utilizador");
                Console.WriteLine("3. Data de Entrada");
                Console.WriteLine("4. Data de Saída");
                Console.WriteLine("5. Ativo");

                // Entrada de opção do usuário
                int opcaoCampo = Servico.LerInteiro("");

                    switch (opcaoCampo)
                    {
                        case 1:
                            tipoCampo = TipoCampo.QuartoId;
                            break;
                        case 2:
                            tipoCampo = TipoCampo.UtiId;
                            break;
                        case 3:
                            tipoCampo = TipoCampo.DataEntrada;
                            break;
                        case 4:
                            tipoCampo = TipoCampo.DataSaida;
                            break;
                        case 5:
                            tipoCampo = TipoCampo.IsAtivo;
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                            break;
                    }

                    // Se uma opção válida foi escolhida, sai do loop
                    if (opcaoCampo >= 1 && opcaoCampo <= 5)
                    {
                        break;
                    }
            }

            return tipoCampo;
        }

        /// <summary>
        /// Obtém o novo valor para o campo escolhido
        /// </summary>
        /// <param name="reservaEscolhida"></param>
        /// <param name="tipoCampo"></param>
        /// <returns></returns>
        public Reserva ObterNovoValorReserva(Reserva reservaEscolhida, TipoCampo tipoCampo)
        {
            switch (tipoCampo)
            {
                case TipoCampo.QuartoId:
                    // Solicita ao usuário o novo valor para o campo QuartoId
                    reservaEscolhida.QuartoId = Servico.LerInteiro($"Digite o novo valor para o campo {tipoCampo}: ");
                    break;

                case TipoCampo.UtiId:
                    // Solicita ao usuário o novo valor para o campo UtiId
                    reservaEscolhida.UtiId = Servico.LerInteiro($"Digite o novo valor para o campo {tipoCampo}: ");
                    break;

                case TipoCampo.DataEntrada:
                    // Solicita ao usuário a nova data para o campo DataEntrada
                    reservaEscolhida.DataEntrada = Servico.LerData($"Digite o novo valor para o campo {tipoCampo} (dd-MM-yyyy): ");
                    break;

                case TipoCampo.DataSaida:
                    // Solicita ao usuário a nova data para o campo DataSaida
                    reservaEscolhida.DataSaida = Servico.LerData($"Digite o novo valor para o campo {tipoCampo} (dd-MM-yyyy): ");
                    break;

                case TipoCampo.IsAtivo:
                    // Solicita ao usuário o novo valor booleano para o campo IsAtivo
                    reservaEscolhida.IsAtivo = Servico.LerBooleano($"Digite o novo valor para o campo {tipoCampo} (true/false): ");
                    break;

                default:
                    Console.WriteLine("Campo não suportado. A atualização não foi realizada.");
                    break;
            }

            return reservaEscolhida;
        }
        #endregion


        #region Imprimir Reserva
        /// <summary>
        /// Método para imprimir a lista de reservas
        /// </summary>
        public void ImprimirListaReservaAtual()
        {

            List<Reserva> listaDeReservasAtual = controladorReserva.ObterListaAtual();

            ExibirDetalhesListaReserva(listaDeReservasAtual);
        }

        /// <summary>
        /// Método para exibir os detalhes de uma lista de reservas
        /// </summary>
        /// <param name="listaDeReservasAtual"></param>
        public void ExibirDetalhesListaReserva(List<Reserva> listaDeReservasAtual)
        {
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

        /// <summary>
        /// Método para exibir os detalhes de uma reserva única
        /// </summary>
        /// <param name="reserva"></param>
        public void ExibirDetalhesReserva(Reserva reserva)
        {
            Console.WriteLine($"ID: {reserva.ReservaId}, ID do Quarto: {reserva.QuartoId}, ID do Utilizador: {reserva.UtiId}, Data de Entrada: {reserva.DataEntrada:dd-MM-yyyy}, Data de Saída: {reserva.DataSaida:dd-MM-yyyy}, Preço da Caução: {reserva.PrecoCaucao}, Ativo: {reserva.IsAtivo}, Data da Reserva: {reserva.DataReserva:dd-MM-yyyy}");
        }
        #endregion

        #region Excluir Reserva
        /// <summary>
        /// Método para excluir uma reserva
        /// </summary>
        public void MenuExcluirReserva()
        {
            // Obtém um predicado para buscar as reservas com base nos critérios escolhidos pelo usuário
            Predicate<Reserva> predicado = MenuBuscarReserva();

            // Busca as reservas com base no predicado
            List<Reserva> reservasEncontradas = controladorReserva.BuscarReserva(predicado);

            if (reservasEncontradas != null && reservasEncontradas.Count > 0)
            {
                // Solicita ao usuário que escolha o ID da reserva a ser excluída
                int idEscolhido = Servico.LerInteiro("Escolha o ID da reserva que deseja excluir: ");

                // Busca a reserva escolhida
                Reserva reservaEscolhida = reservasEncontradas.FirstOrDefault(r => r.ReservaId == idEscolhido);

                // Exibe detalhes da reserva escolhida
                ExibirDetalhesReserva(reservaEscolhida);

                if (reservaEscolhida != null)
                {
                    // Chama o controlador para excluir a reserva
                    controladorReserva.ExcluirReserva(reservaEscolhida);
                }
                else
                {
                    // Se a reserva escolhida não foi encontrada, exibe uma mensagem indicando que a exclusão não foi realizada
                    Console.WriteLine($"Reserva com ID {idEscolhido} não encontrada. A exclusão não foi realizada.");
                }
            }
            else
            {
                // Se nenhuma reserva foi encontrada com base nos critérios escolhidos, exibe uma mensagem indicando que a exclusão não foi realizada
                Console.WriteLine("Reserva não encontrada. A exclusão não foi realizada.");
            }
        }

        #endregion

        #endregion
    }

}
