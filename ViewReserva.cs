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
        private ControladorReserva controladorReserva;

        public void SetControladorReserva(ControladorReserva controladorReserva)
        {
            this.controladorReserva = controladorReserva;
        }

        #region CRUD Reserva
        #region Criar Reserva
        public void CriarReserva()
        {
            Console.WriteLine("Criar Reserva");
            Console.WriteLine("-------------");
            Console.WriteLine();

            int quartoId = LerInteiro("Insira o ID do quarto: ");
            int utiId = LerInteiro("Insira o ID do utilizador: ");
            DateTime dataEntrada = LerData("Insira a data de entrada (dd-MM-yyyy): ");
            DateTime dataSaida = LerData("Insira a data de saída (dd-MM-yyyy): ");
            float precoCaucao = LerFloat("Insira o preço da caução: ");

            controladorReserva.CriarReserva(quartoId, utiId, dataEntrada, dataSaida, precoCaucao);
        }
        #endregion

        #region Buscar Reserva
        public Predicate<Reserva> MenuBuscarReserva()
        {
            Predicate<Reserva> predicado = null;
            List<Reserva> listaDeReservasAtual = controladorReserva.ObterListaAtual();

            while (true)
            {
                Console.WriteLine("Escolha o critério de busca:");
                Console.WriteLine("1. Por ID da Reserva");
                Console.WriteLine("2. Por ID do Quarto");
                Console.WriteLine("3. Por ID do Utilizador");
                Console.WriteLine("4. Por Data de Entrada");
                Console.WriteLine("5. Por Data de Saída");
                Console.WriteLine("6. Por Data da Reserva");

                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    int valorInteiro;
                    DateTime valorData;

                    switch (opcao)
                    {
                        case 1:
                            valorInteiro = LerInteiro("Digite o ID da Reserva:");
                            predicado = r => r.ReservaId == valorInteiro;
                            break;
                        case 2:
                            valorInteiro = LerInteiro("Digite o ID do Quarto:");
                            predicado = r => r.QuartoId == valorInteiro;
                            break;
                        case 3:
                            valorInteiro = LerInteiro("Digite o ID do Utilizador:");
                            predicado = r => r.UtiId == valorInteiro;
                            break;
                        case 4:
                            valorData = LerData("Digite a Data de Entrada (dd-MM-yyyy):");
                            predicado = r => r.DataEntrada == valorData;
                            break;
                        case 5:
                            valorData = LerData("Digite a Data de Saída (dd-MM-yyyy):");
                            predicado = r => r.DataSaida == valorData;
                            break;
                        case 6:
                            valorData = LerData("Digite a Data da Reserva (dd-MM-yyyy):");
                            predicado = r => r.DataReserva == valorData;
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }

                    if (opcao >= 1 && opcao <= 6)
                    {
                        // Se uma opção válida foi escolhida, sai do loop
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("Entrada inválida.");
                }
            }

            if (predicado != null)
            {
                List<Reserva> listaDeReservasSelecionadas = controladorReserva.BuscarReserva(predicado);
                ExibirDetalhesListaReserva(listaDeReservasSelecionadas);
            }

            return predicado;
        }
        #endregion

        #region Editar Reserva
        public void MenuEditarReserva()
        {
            Predicate<Reserva> predicado = MenuBuscarReserva();
            List<Reserva> reservasEncontradas = controladorReserva.BuscarReserva(predicado);

            if (reservasEncontradas != null && reservasEncontradas.Count > 0)
            {

                int idEscolhido = LerInteiro("Escolha o ID da reserva que deseja atualizar: ");


                Reserva reservaEscolhida = reservasEncontradas.FirstOrDefault(r => r.ReservaId == idEscolhido);

                // Exibir detalhes da reserva
                ExibirDetalhesReserva(reservaEscolhida);

                if (reservaEscolhida != null)
                {
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
    
        // Menu para escolher o TipoCampo a ser atualizado
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

                if (int.TryParse(Console.ReadLine(), out int opcaoCampo))
                {
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
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, escolha uma opção válida.");
                }
            }

            return tipoCampo;
        }

        public Reserva ObterNovoValorReserva(Reserva reservaEscolhida, TipoCampo tipoCampo)
        {

            switch (tipoCampo)
            {
                case TipoCampo.QuartoId:
                    reservaEscolhida.QuartoId = LerInteiro($"Digite o novo valor para o campo {tipoCampo}: ");
                    break;

                case TipoCampo.UtiId:
                    reservaEscolhida.UtiId = LerInteiro($"Digite o novo valor para o campo {tipoCampo}: ");
                    break;

                case TipoCampo.DataEntrada:
                    reservaEscolhida.DataEntrada = LerData($"Digite o novo valor para o campo {tipoCampo} (dd-MM-yyyy): ");
                    break;

                case TipoCampo.DataSaida:
                    reservaEscolhida.DataSaida = LerData($"Digite o novo valor para o campo {tipoCampo} (dd-MM-yyyy): ");
                    break;

                case TipoCampo.IsAtivo:
                    reservaEscolhida.IsAtivo = LerBooleano($"Digite o novo valor para o campo {tipoCampo} (true/false): ");
                    break;

                default:
                    Console.WriteLine("Campo não suportado. A atualização não foi realizada.");
                    break;
            }

            return reservaEscolhida;
        }
        #endregion

        #region Impressão de reservas
        /// <summary>
        /// Método para imprimir a lista de reservas
        /// </summary>
        public void ImprimirListaReservaAtual()
        {

            List<Reserva> listaDeReservasAtual = controladorReserva.ObterListaAtual();

            ExibirDetalhesListaReserva(listaDeReservasAtual);
        }

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

        public void ExibirDetalhesReserva(Reserva reserva)
        {
            Console.WriteLine($"ID: {reserva.ReservaId}, ID do Quarto: {reserva.QuartoId}, ID do Utilizador: {reserva.UtiId}, Data de Entrada: {reserva.DataEntrada:dd-MM-yyyy}, Data de Saída: {reserva.DataSaida:dd-MM-yyyy}, Preço da Caução: {reserva.PrecoCaucao}, Ativo: {reserva.IsAtivo}, Data da Reserva: {reserva.DataReserva:dd-MM-yyyy}");
        }
        #endregion

        #region Excluir Reserva
        // A implementar
        #endregion

        #endregion

        #region Leitura de dados
        public string LerString(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }

        public int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Entrada inválida. Digite um número inteiro.");
                Console.Write(mensagem);
            }

            return valor;
        }

        public DateTime LerData(string mensagem)
        {
            DateTime data;
            Console.Write(mensagem);

            while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.WriteLine("Entrada inválida. Digite uma data no formato dd-MM-yyyy.");
                Console.Write(mensagem);
            }

            return data;
        }

        public float LerFloat(string mensagem)
        {
            float valor;
            Console.Write(mensagem);

            while (!float.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Entrada inválida. Digite um número real.");
                Console.Write(mensagem);
            }

            return valor;
        }

        public bool LerBooleano(string mensagem)
        {
            bool valor;
            Console.Write(mensagem);

            while (!bool.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Entrada inválida. Digite um valor booleano (true/false).");
                Console.Write(mensagem);
            }

            return valor;
        }
        #endregion
    }

}
