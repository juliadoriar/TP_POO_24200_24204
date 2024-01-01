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
        #region Instâncias de ControladorReserva
        private Reserva modelReserva;
        private ViewReserva viewReserva;

        /// <summary>
        /// Instancia um objeto de ViewReserva
        /// </summary>
        /// <param name="viewReserva"></param>
        public void SetViewReserva(ViewReserva viewReserva)
        {
            this.viewReserva = viewReserva;
        }

        /// <summary>
        /// Instancia um objeto de Reserva
        /// </summary>
        /// <param name="modelReserva"></param>
        public void SetModelReserva (Reserva modelReserva)
        {
            this.modelReserva = modelReserva;
        }
        #endregion

        #region CRUD Reserva
        #region Criar Reserva
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
            // Obtém o último ID de reserva armazenado no arquivo JSON
            int ultimoId = Reserva.LerUltimoIdReserva("reserva.json");

            // Incrementa o último ID para obter um novo ID único para a nova reserva
            int reservaId = ++ultimoId;

            // Cria uma nova instância de Reserva com os parâmetros fornecidos
            Reserva novaReserva = new Reserva(
                reservaId,
                quartoId,
                utiId,
                dataEntrada,
                dataSaida,
                precoCaucao);

            // Adiciona a nova reserva à lista existente e verifica se a adição foi bem-sucedida
            if (AdicionarReserva(novaReserva))
            {
                // Atualiza o último ID no arquivo JSON com o novo ID da reserva criada
                Reserva.AtualizarUltimoIdNoJson(reservaId, "reserva.json");

                // Retorna a nova reserva criada
                return novaReserva;
            }

            // Retorna null em caso de falha na adição da reserva
            return null;
        }


        /// <summary>
        /// Método que adiciona uma reserva à lista de reservas
        /// </summary>
        /// <param name="novaReserva"></param>
        /// <returns>True se a reserva foi adicionada com sucesso, False caso contrário.</returns>
        public bool AdicionarReserva(Reserva novaReserva)
        {
            // Carrega a lista existente do ficheiro
            List<Reserva> listaExistente = ObterListaAtual();

            // Validação da reserva
            if (!ValidarReserva(novaReserva, listaExistente))
            {
                return false;
            }
            else
            {
                listaExistente.Add(novaReserva);

                // Salva a lista atualizada no ficheiro
                Reserva.SalvarListaFicheiro("reserva.json", listaExistente);
                return true;                
            }

        }
        #endregion

        #region Buscar Reserva
        /// <summary>
        /// Método para buscar reservas com base em um predicado
        /// </summary>
        /// <param name="predicado"></param>
        /// <returns></returns>
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
        #endregion

        #region Editar Reserva
        /// <summary>
        /// Método para editar uma reserva
        /// </summary>
        /// <param name="reserva"></param>
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

        /// <summary>
        /// Método para atualizar os campos de uma reserva
        /// </summary>
        /// <param name="existente"></param>
        /// <param name="atualizada"></param>
        public void AtualizarCamposReserva(Reserva existente, Reserva atualizada)
        {
            existente.QuartoId = atualizada.QuartoId;
            existente.UtiId = atualizada.UtiId;
            existente.DataEntrada = atualizada.DataEntrada;
            existente.DataSaida = atualizada.DataSaida;
            existente.IsAtivo = atualizada.IsAtivo;
        }
        #endregion

        #region Excluir Reserva
        /// <summary>
        /// Método para excluir reserva da lista
        /// </summary>
        /// <param name="reserva"></param>
        public void ExcluirReserva(Reserva reserva)
        {
            // Obtem a lista atual de reservas
            List<Reserva> listaExistente = ObterListaAtual();

            // Verifica se a reserva existe na lista
            if (!listaExistente.Any(r => r.ReservaId == reserva.ReservaId))
            {
                Console.WriteLine($"Reserva com ID {reserva.ReservaId} não encontrada. A exclusão não foi realizada.");
                return;
            }
            else
            {
                // Se a reserva for válida, exclui a reserva da lista existente
                listaExistente.RemoveAll(r => r.ReservaId == reserva.ReservaId);


                // Salva as alterações no ficheiro
                Reserva.SalvarListaFicheiro("reserva.json", listaExistente);
                Console.WriteLine("Exclusão realizada com sucesso.");
            }

        }
        #endregion

        #endregion

        #region Métodos Auxiliares
        /// <summary>
        /// Método para validar criação ou alteração de uma reserva
        /// </summary>
        /// <param name="novaReserva"></param>
        /// <param name="listaExistente"></param>
        /// <returns></returns>
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
            // Verifica se o quarto está ocupado naquelas datas
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
            // Verifica se o utilizador já tem uma reserva nas mesmas datas
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

        /// <summary>
        /// Define os campos que podem ser editados
        /// </summary>
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

        /// <summary>
        /// Método para obter a lista atual de reservas
        /// </summary>
        /// <returns></returns>
        public List<Reserva> ObterListaAtual()
        {
            return Reserva.CarregarListaDeReservas("reserva.json");
        }
        #endregion
    }
}
