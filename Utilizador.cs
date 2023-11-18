using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    public class Utilizador
    {
        public static List<Utilizador> listaDeUtilizadores = new List<Utilizador>();

        [JsonProperty("UtiId")]
        protected int UtiId;
        [JsonProperty("NomeUti")]
        protected string NomeUti;
        [JsonProperty("Email")]
        protected string Email;
        [JsonProperty("Password")]
        protected string Password;
        [JsonProperty("DataNascimento")]
        protected DateTime DataNascimento;
        [JsonProperty("Morada")]
        protected string Morada;
        [JsonProperty("CodigoPostal")]
        protected string CodigoPostal;
        [JsonProperty("Localidade")]
        protected string Localidade;
        [JsonProperty("ContactoTelefone")]
        protected string ContactoTelefone;
        [JsonProperty("DocIdentificacao")]
        protected string DocIdentificacao;
        [JsonProperty("TipoDocIdentificacao")]
        protected string TipoDocIdentificacao;
        [JsonProperty("IBAN")]
        protected string IBAN;
        [JsonProperty("TipoUtilizador")]
        protected string TipoUtilizador;
        [JsonProperty("IsAtivo")]
        protected bool IsAtivo;
        [JsonProperty("DataRegisto")]
        protected DateTime DataRegisto;

        private static int ultimoId = 0;

        #region Construtor
        public Utilizador(int utiId,
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
                          string tipoUtilizador,
                          bool isAtivo = false,
                          DateTime dataRegisto = default(DateTime))

        {
            UtiId = utiId;
            NomeUti = nomeUti;
            Email = email;
            Password = password;
            DataNascimento = dataNascimento;
            Morada = morada;
            CodigoPostal = codigoPostal;
            Localidade = localidade;
            ContactoTelefone = contactoTelefone;
            DocIdentificacao = docIdentificacao;
            TipoDocIdentificacao = tipoDocIdentificacao;
            IBAN = iban;
            TipoUtilizador = tipoUtilizador;
            IsAtivo = isAtivo;
            DataRegisto = (dataRegisto == default(DateTime)) ? DateTime.Now : dataRegisto;
        }
        #endregion

        #region Getters e Setters
        public int GetUtiId()
        {
            return UtiId;
        }

        public void SetUtiId(int utiId)
        {
            UtiId = utiId;
        }

        public string GetNomeUti()
        {
            return NomeUti;
        }

        public void SetNomeUti(string nomeUti)
        {
            NomeUti = nomeUti;
        }

        public string GetEmail()
        {
            return Email;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public DateTime GetDataNascimento()
        {
            return DataNascimento;
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        public string GetMorada()
        {
            return Morada;
        }

        public void SetMorada(string morada)
        {
            Morada = morada;
        }

        public string GetCodigoPostal()
        {
            return CodigoPostal;
        }

        public void SetCodigoPostal(string codigoPostal)
        {
            CodigoPostal = codigoPostal;
        }

        public string GetLocalidade()
        {
            return Localidade;
        }

        public void SetLocalidade(string localidade)
        {
            Localidade = localidade;
        }

        public string GetContactoTelefone()
        {
            return ContactoTelefone;
        }

        public void SetContactoTelefone(string contactoTelefone)
        {
            ContactoTelefone = contactoTelefone;
        }

        public string GetDocIdentificacao()
        {
            return DocIdentificacao;
        }

        public void SetDocIdentificacao(string docIdentificacao)
        {
            DocIdentificacao = docIdentificacao;
        }

        public string GetTipoDocIdentificacao()
        {
            return TipoDocIdentificacao;
        }

        public void SetTipoDocIdentificacao(string tipoDocIdentificacao)
        {
            TipoDocIdentificacao = tipoDocIdentificacao;
        }

        public string GetIBAN()
        {
            return IBAN;
        }

        public void SetIBAN(string iban)
        {
            IBAN = iban;
        }

        public string GetTipoUtilizador()
        {
            return TipoUtilizador;
        }

        public void SetTipoUtilizador(string tipoUtilizador)
        {
            TipoUtilizador = tipoUtilizador;
        }

        public bool GetIsAtivo()
        {
            return IsAtivo;
        }

        public void SetIsAtivo(bool isAtivo)
        {
            IsAtivo = isAtivo;
        }

        public DateTime GetDataRegisto()
        {
            return DataRegisto;
        }

        public void SetDataRegisto(DateTime dataRegisto)
        {
            DataRegisto = dataRegisto;
        }

        public static int GetUltimoId()
        {
            return ultimoId;
        }
        public static void SetUltimoId(int ultimoId)
        {
            Utilizador.ultimoId = ultimoId;
        }
        #endregion

    }


}
