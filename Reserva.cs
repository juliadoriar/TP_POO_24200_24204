using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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

        #region Métodos de acesso aos atributos da classe Reserva
        /// <summary>
        /// Método para criar a lista de reservas
        /// </summary>
        public void CriarListaReservas()
        {
            List<Reserva> listaDeReservas = new List<Reserva>();
            SalvarListaFicheiro("reserva.json", listaDeReservas);
        }

        #region JSON
        /// <summary>
        /// Método para criar um ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public static void CriarFicheiroJson(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                File.Create(caminhoArquivo).Close();
            }
        }

        /// <summary>
        /// Método para salvar a lista de reservas no ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <param name="listaDeReservas"></param>
        public static void SalvarListaFicheiro(string caminhoArquivo, List<Reserva> listaDeReservas)
        {
            string json = JsonConvert.SerializeObject(new { UltimoIdReserva = LerUltimoIdReserva(caminhoArquivo), Reservas = listaDeReservas }, Newtonsoft.Json.Formatting.Indented); // Serializar lista de utilizadores

            File.WriteAllText(caminhoArquivo, json); // Escreve no ficheiro
        }

        /// <summary>
        /// Método para carregar a lista de reservas do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static List<Reserva> CarregarListaDeReservas(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo)) // Verifica se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Lê o ficheiro

                if (!string.IsNullOrEmpty(json)) // Verifica se o JSON não está vazio
                {
                    // Desserializa o JSON em um objeto anônimo que contém a propriedade "Reservas"
                    var jsonData = JsonConvert.DeserializeAnonymousType(json, new { Reservas = new List<Reserva>() });

                    // Retorna a lista de reservas da propriedade anônima
                    return jsonData.Reservas;
                }
            }

            return new List<Reserva>(); // Se o ficheiro não existir, retorna uma lista vazia
        }

      /// <summary>
        /// Método que lê o último id de reserva do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static int LerUltimoIdReserva(string caminhoArquivo)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(File.ReadAllText(caminhoArquivo));

            if (jsonData != null && jsonData.UltimoIdReserva != null)
            {
                return (int)jsonData.UltimoIdReserva;
            }

            return 0; // Valor padrão se o ficheiro ou conteúdo não existir
        }

        /// <summary>
        /// Método que atualiza o último id de reserva no ficheiro JSON
        /// </summary>
        /// <param name="novoUltimoId"></param>
        public static void AtualizarUltimoIdNoJson(int novoUltimoId, string caminhoArquivo)
        {
            List<Reserva> listaExistente = CarregarListaDeReservas(caminhoArquivo);

            string json = JsonConvert.SerializeObject(new { UltimoIdReserva = novoUltimoId, Reservas = listaExistente }, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
        }
        #endregion
        #endregion
    }

}
