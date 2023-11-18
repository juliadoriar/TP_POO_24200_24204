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

        #region Construtor
        public Reserva(
            int reservaId,
            int quartoId,
            int utiId,
            DateTime dataEntrada,
            DateTime dataSaida,
            float precoCaucao,
            bool isAtivo,
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
    }

}
