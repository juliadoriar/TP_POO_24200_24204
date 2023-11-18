using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Subclasse de Utilizador, quando o utilizador é morador da residência
    /// </summary>
    public class Morador : Utilizador
    {   
        public static List<Morador> listaDeMoradores = new List<Morador>(); // Variável que guarda a lista de moradores
        
        [JsonProperty("isAdimplente")]
        protected bool IsAdimplente; // Propriedade que indica se o morador está adimplente ou não

        #region Construtor 
        /// <summary>
        /// Construtor da classe Morador que chama o construtor da classe base (Utilizador)
        /// </summary>
        /// <param name="utiId"></param>
        /// <param name="nomeUti"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="dataNascimento"></param>
        /// <param name="morada"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="localidade"></param>
        /// <param name="contactoTelefone"></param>
        /// <param name="docIdentificacao"></param>
        /// <param name="tipoDocIdentificacao"></param>
        /// <param name="iban"></param>
        /// <param name="isAtivo"></param>
        /// <param name="dataRegisto"></param>
        /// <param name="isAdimplente"></param>
        public Morador(
        int utiId,
        string nomeUti,
        string email,
        string password,
        DateTime dataNascimento,
        string morada,
        string codigoPostal,
        string localidade,
        string contactoTelefone,
        string docIdentificacao,
        string tipoDocIdentificacao,
        string iban,
        bool isAtivo,
        DateTime dataRegisto,
        bool isAdimplente = true) // O morador é adimplente por defeito
        : base(utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, "Morador", isAtivo, dataRegisto)
        {
            IsAdimplente = isAdimplente;
        }
        #endregion

        /// <summary>
        /// Métodos de acesso aos atributos da classe Morador
        /// </summary>
        /// <returns></returns>
        #region Getters e Setters
        public bool GetIsAdimplente()
        {
            return IsAdimplente;
        }       
        public void SetAdimplente(bool isAdimplente)
        {
            IsAdimplente = isAdimplente;
        }
        #endregion
    }
}
