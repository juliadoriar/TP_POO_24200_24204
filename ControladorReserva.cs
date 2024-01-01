using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ControladorReserva
    {
        private Reserva modelReserva;
        private ViewReserva viewReserva;

        public void SetViewReserva(ViewReserva viewReserva)
        {
            this.viewReserva = viewReserva;
        }

        public void SetModelReserva (Reserva modelReserva)
        {
            this.modelReserva = modelReserva;
        }


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

            Reserva novaReserva = new Reserva(
                reservaId,
                quartoId,
                utiId,
                dataEntrada,
                dataSaida,
                precoCaucao);

            if (AdicionarReserva(novaReserva))
            {
                Reserva.AtualizarUltimoIdNoJson(reservaId, "reserva.json");
                return novaReserva;
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
            List<Reserva> listaExistente = ObterListaAtual();

            // Validação da reserva
            if (!ValidarReserva(novaReserva, listaExistente))
            {
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

        // Validação da reserva
        public bool ValidarReserva(Reserva novaReserva, List<Reserva> listaExistente)
        {

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
                && ((novaReserva.DataEntrada >= r.DataEntrada && novaReserva.DataEntrada < r.DataSaida) ||
                (novaReserva.DataSaida > r.DataEntrada && novaReserva.DataSaida <= r.DataSaida) ||
                (novaReserva.DataEntrada <= r.DataEntrada && novaReserva.DataSaida >= r.DataSaida))
                && r.IsAtivo == true))
            {
                Console.WriteLine("O quarto já está ocupado nessas datas.");
                return false;
            }
            // Verificar se o utilizador já tem uma reserva nas mesmas datas
            else if (listaExistente.Exists(r =>
                novaReserva.UtiId == r.UtiId
                && ((novaReserva.DataEntrada >= r.DataEntrada && novaReserva.DataEntrada < r.DataSaida) ||
                (novaReserva.DataSaida > r.DataEntrada && novaReserva.DataSaida <= r.DataSaida) ||
                (novaReserva.DataEntrada <= r.DataEntrada && novaReserva.DataSaida >= r.DataSaida))
                && r.IsAtivo == true))

            {
                Console.WriteLine("O utilizador já tem outra reserva nessas datas.");
                return false;
            }
            else
            {
                return true;
            }
        }



        // Método para buscar reservas com base em um predicado
        public List<Reserva> BuscarReserva(Predicate<Reserva> predicado)
        {
            List<Reserva> listaExistente = ObterListaAtual();

            if (predicado == null)
            {
                // Se o predicado for nulo, retorna null
                return null;
            }
            else
            {
                // Se o predicado não for nulo, retorna a lista de reservas que satisfazem o predicado
                List<Reserva> reservasEncontradas = listaExistente.FindAll(predicado);
                return reservasEncontradas;
            }
        }

        public void EditarReserva(Reserva reserva)
        {
            // Obtem a lista atual de reservas
            List<Reserva> listaExistente = ObterListaAtual();

            // Cria uma cópia da lista existente sem a reserva escolhida para fins de validação
            List<Reserva> listaValidacao = listaExistente.Where(r => r.ReservaId != reserva.ReservaId).ToList();

            // Selecionar campo para edição
            TipoCampo tipoCampo = viewReserva.MenuSelecionarCampo();

            // Obtém o novo valor e atualiza a reserva escolhida
            Reserva reservaAtualizada = viewReserva.ObterNovoValorReserva(reserva, tipoCampo);

            // Valida reserva
            if (ValidarReserva(reservaAtualizada, listaValidacao))
            {
                // Se a reserva for válida, atualiza os campos na lista existente
                Reserva reservaExistente = listaExistente.FirstOrDefault(r => r.ReservaId == reserva.ReservaId);
                if (reservaExistente != null)
                {
                    // Atualiza os campos na reserva existente com os valores da reserva atualizada
                    AtualizarCamposReserva(reservaExistente, reservaAtualizada);
                }

                // Salva as alterações no arquivo
                Reserva.SalvarListaFicheiro("reserva.json", listaExistente);
                Console.WriteLine("Atualização realizada com sucesso.");
            }
            else
            {
                Console.WriteLine("A reserva não é válida. A atualização não foi realizada.");
            }
        }

        public void AtualizarCamposReserva(Reserva existente, Reserva atualizada)
        {
            existente.QuartoId = atualizada.QuartoId;
            existente.UtiId = atualizada.UtiId;
            existente.DataEntrada = atualizada.DataEntrada;
            existente.DataSaida = atualizada.DataSaida;
            existente.IsAtivo = atualizada.IsAtivo;
        }


        /*// Método para editar uma reserva
        public void EditarReserva()
            {
                Predicate<Reserva> predicado = viewReserva.MenuBuscarReserva();
                List<Reserva> reservasEncontradas = BuscarReserva(predicado);

                if (reservasEncontradas != null && reservasEncontradas.Count > 0)
                {
                    Console.Write("Escolha o ID da reserva que deseja atualizar: ");
                    if (int.TryParse(Console.ReadLine(), out int idEscolhido))
                    {
                        Reserva reservaEscolhida = reservasEncontradas.FirstOrDefault(r => r.ReservaId == idEscolhido);

                        if (reservaEscolhida != null)
                        {
                            // Obtem a lista atual de reservas
                            List<Reserva> listaExistente = ObterListaAtual();

                            // Cria uma cópia da lista existente sem a reserva escolhida para fins de validação
                            List<Reserva> listaValidacao = listaExistente.Where(r => r.ReservaId != idEscolhido).ToList();

                            Console.WriteLine("Detalhes da reserva selecionada:");
                            Console.WriteLine($"ID: {reservaEscolhida.ReservaId}, ID do Quarto: {reservaEscolhida.QuartoId}, ID do Utilizador: {reservaEscolhida.UtiId}, Data de Entrada: {reservaEscolhida.DataEntrada:dd-MM-yyyy}, Data de Saída: {reservaEscolhida.DataSaida:dd-MM-yyyy}, Preço da Caução: {reservaEscolhida.PrecoCaucao}, Ativo: {reservaEscolhida.IsAtivo}, Data da Reserva: {reservaEscolhida.DataReserva:dd-MM-yyyy}");

                            TipoCampo tipoCampo = viewReserva.MenuSelecionarCampo();

                            // Obtém o novo valor e atualiza a reserva escolhida
                            Reserva reservaAtualizada = viewReserva.ObterNovoValorReserva(reservaEscolhida, tipoCampo);

                            // Valida reserva
                            if (ValidarReserva(reservaAtualizada, listaValidacao))
                            {
                                // Se a reserva for válida, atualiza os campos na lista existente
                                Reserva reservaExistente = listaExistente.FirstOrDefault(r => r.ReservaId == idEscolhido);
                                if (reservaExistente != null)
                                {
                                    // Atualiza os campos na reserva existente com os valores da reserva atualizada
                                    reservaExistente.QuartoId = reservaAtualizada.QuartoId;
                                    reservaExistente.UtiId = reservaAtualizada.UtiId;
                                    reservaExistente.DataEntrada = reservaAtualizada.DataEntrada;
                                    reservaExistente.DataSaida = reservaAtualizada.DataSaida;
                                    reservaExistente.IsAtivo = reservaAtualizada.IsAtivo;
                                }

                                // Salva as alterações no arquivo
                                Reserva.SalvarListaFicheiro("reserva.json", listaExistente);
                                Console.WriteLine("Atualização realizada com sucesso.");
                            }
                            else
                            {
                                Console.WriteLine("A reserva não é válida. A atualização não foi realizada.");
                            }                     
                        }
                        else
                        {
                            Console.WriteLine($"Reserva com ID {idEscolhido} não encontrada. A atualização não foi realizada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. A atualização não foi realizada.");
                    }
                }
                else
                {
                    Console.WriteLine("Reserva não encontrada. A atualização não foi realizada.");
                }
            }*/

        public enum TipoCampo
        {
            ReservaId,
            QuartoId,
            UtiId,
            DataEntrada,
            DataSaida,
            IsAtivo,
            DataReserva,
            Nulo
        }

        public List<Reserva> ObterListaAtual()
        {
            return Reserva.CarregarListaDeReservas("reserva.json");
        }
    }
}
