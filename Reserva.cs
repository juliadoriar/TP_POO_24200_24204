using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    public class Reserva
    {
        [JsonProperty("reservaId")]
        public int ReservaId { get; set; }
        [JsonProperty("quartoId")]
        public int QuartoId { get; set; }
        [JsonProperty("utiId")]
        public int UtiId { get; set; }
        [JsonProperty("dataEntrada")]
        public DateTime DataEntrada { get; set; }
        [JsonProperty("dataSaida")]
        public DateTime DataSaida { get; set; }
        [JsonProperty("precoCaucao")]
        public float PrecoCaucao { get; set; }
        [JsonProperty("isAtivo")]
        public bool IsAtivo { get; set; }
        [JsonProperty("dataReserva")]
        public DateTime DataReserva { get; set; }

        #region Construtor
        public Reserva(
            int reservaId,
            int quartoId,
            int utiId,
            DateTime dataEntrada,
            DateTime dataSaida,
            float precoCaucao,
            bool isAtivo,
            DateTime dataReserva = default(DateTime))
        {
            ReservaId = reservaId;
            QuartoId = quartoId;
            UtiId = utiId;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            PrecoCaucao = precoCaucao;
            IsAtivo = isAtivo;
            DataReserva = (dataReserva == default(DateTime)) ? DateTime.Now : dataReserva;
        }
        #endregion
    }

}
