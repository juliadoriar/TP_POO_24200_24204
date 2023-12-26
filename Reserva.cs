using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que representa uma reserva de um quarto
    /// </summary>
    public class Reserva
    {
        [JsonProperty("reservaId")]
        public int ReservaId { get; set; } // Propriedade que indica o id da reserva
        [JsonProperty("quartoId")]
        public int QuartoId { get; set; } // Propriedade que indica o id do quarto reservado
        [JsonProperty("utiId")]
        public int UtiId { get; set; } // Propriedade que indica o id do utilizador que fez a reserva
        [JsonProperty("dataEntrada")]
        public DateTime DataEntrada { get; set; } // Propriedade que indica a data de entrada na reserva
        [JsonProperty("dataSaida")]
        public DateTime DataSaida { get; set; } // Propriedade que indica a data de saída da reserva
        [JsonProperty("precoCaucao")]
        public float PrecoCaucao { get; set; } // Propriedade que indica o preço da caução da reserva
        [JsonProperty("isAtivo")]
        public bool IsAtivo { get; set; } // Propriedade que indica se a reserva está ativa ou não
        [JsonProperty("dataReserva")]
        public DateTime DataReserva { get; set; } // Propriedade que indica a data da reserva

        private static int ultimoIdReserva = 0; // Variável que guarda o último id de reserva

        #region Construtor
        public Reserva(
            int reservaId,
            int quartoId,
            int utiId,
            DateTime dataEntrada,
            DateTime dataSaida,
            float precoCaucao,
            bool isAtivo = true, // Por defeito, a reserva está ativa
            DateTime dataReserva = default(DateTime)) // A data da reserva é a data atual por defeito
        {
            ReservaId = reservaId;
            QuartoId = quartoId;
            UtiId = utiId;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            PrecoCaucao = precoCaucao;
            IsAtivo = isAtivo;
            DataReserva = (dataReserva == default(DateTime)) ? DateTime.Now : dataReserva; // Se a data da reserva não for especificada, é a data atual
        }
        #endregion

        public static int GetUltimoId()
        {
            return ultimoIdReserva;
        }
        public static void SetUltimoId(int ultimoId)
        {
            Reserva.ultimoIdReserva = ultimoId;
        }

        #region Métodos de acesso aos atributos da classe Reserva
        /// <summary>
        /// Método para criar a lista de reservas
        /// </summary>
        public void CriarListaReservas()
        {
            List<Reserva> listaDeReservas = new List<Reserva>();
            SalvarListaFicheiro("reserva.json", listaDeReservas);
        }

        /// <summary>
        /// Método para criar um ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public void CriarFicheiroJson(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                // Se o ficheiro existir, limpa o conteúdo
                File.WriteAllText(caminhoArquivo, string.Empty);
            }
            else
            {
                // Se o ficheiro não existir, cria o arquivo vazio
                File.Create(caminhoArquivo).Close();
            }
        }

        public void SalvarListaFicheiro(string caminhoArquivo, List<Reserva> listaDeReservas)
        {
            string json = JsonConvert.SerializeObject(listaDeReservas, Newtonsoft.Json.Formatting.Indented); // Serializar lista de utilizadores
            File.WriteAllText(caminhoArquivo, json); // Escrever no ficheiro
        }

        /// <summary>
        /// Método para carregar a lista de reservas do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public List<Reserva> CarregarListaDeReservas(string caminhoArquivo)
        {
            List<Reserva> listaDeReservasAtual = new List<Reserva>();
            if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro
                listaDeReservasAtual = JsonConvert.DeserializeObject<List<Reserva>>(json); // Desserializar o ficheiro
                return listaDeReservasAtual; // Retornar a lista de reservas
            }

            return new List<Reserva>(); // Se o ficheiro não existir, retorna uma lista vazia
        }

        #endregion
    }

}
