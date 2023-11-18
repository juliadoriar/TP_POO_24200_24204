using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    public class Morador : Utilizador
    {
        public static List<Morador> listaDeMoradores = new List<Morador>();
        
        [JsonProperty("isAdimplente")]
        protected bool IsAdimplente;

        #region Construtor
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
        bool isAdimplente = true)
        : base(utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, "Morador", isAtivo, dataRegisto)
        {
            IsAdimplente = isAdimplente;
        }
        #endregion

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
