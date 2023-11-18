using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que representa um quarto da residência
    /// </summary>
    public class Quarto
    {
        public static List<Quarto> listaDeQuartos = new List<Quarto>(); // Variável que guarda a lista de quartos da residência
        
        [JsonProperty("quartoId")]
        protected int QuartoId; // Propriedade que indica o id do quarto
        [JsonProperty("tipoQuarto")]
        protected string TipoQuarto; // Propriedade que indica o tipo de quarto (individual, duplo, triplo, etc.)
        [JsonProperty("andar")]
        protected int Andar; // Propriedade que indica o andar onde se localiza o quarto
        [JsonProperty("capacidade")]
        protected int Capacidade; // Propriedade que indica a capacidade de pessoas do quarto
        [JsonProperty("precoRenda")]
        protected float PrecoRenda; // Propriedade que indica o preço da renda do quarto
        [JsonProperty("disponibilidade")]
        protected bool Disponibilidade; // Propriedade que indica se o quarto está disponível ou não


        #region Construtor
        /// <summary>
        /// Construtor da classe Quarto
        /// </summary>
        /// <param name="quartoId"></param>
        /// <param name="tipoQuarto"></param>
        /// <param name="andar"></param>
        /// <param name="capacidade"></param>
        /// <param name="precoRenda"></param>
        /// <param name="disponibilidade"></param>
        public Quarto(int quartoId, string tipoQuarto, int andar, int capacidade, float precoRenda, bool disponibilidade)
        {
            QuartoId = quartoId;
            TipoQuarto = tipoQuarto;
            Andar = andar;
            Capacidade = capacidade;
            PrecoRenda = precoRenda;
            Disponibilidade = disponibilidade;
        }
        #endregion
   
        /// <summary>
        /// Métodos de acesso aos atributos da classe Quarto
        /// </summary>
        /// <returns></returns>
        #region Getters e Setters
        public int GetQuartoId()
        {
            return QuartoId;
        }   
        private void SetQuartoId(int quartoId)
        {
            QuartoId = quartoId;
        }
        public string GetTipoQuarto()
        {
            return TipoQuarto;
        }           
        public void SetTipoQuarto(string tipoQuarto)
        {
            TipoQuarto = tipoQuarto;
        }
        public int GetAndar()
        {
            return Andar;
        }
        public void SetAndar(int andar)
        {
            Andar = andar;
        }
        public int GetCapacidade()
        {
            return Capacidade;
        }
        public void SetCapacidade(int capacidade)
        {
            Capacidade = capacidade;
        }
        public float GetPrecoRenda()
        {
            return PrecoRenda;
        }
        public void SetPrecoRenda(float precoRenda)
        {
            PrecoRenda = precoRenda;
        }
        public bool GetDisponibilidade()
        {
            return Disponibilidade;
        }
        public void SetDisponibilidade(bool disponibilidade)
        {
            Disponibilidade = disponibilidade;
        }
        #endregion

    }
}
