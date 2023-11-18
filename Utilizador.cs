using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que representa um utilizador da aplicação
    /// </summary>
    public class Utilizador
    {
        public static List<Utilizador> listaDeUtilizadores = new List<Utilizador>(); // Variável que guarda a lista de utilizadores

        [JsonProperty("UtiId")]
        protected int UtiId;  // Propriedade que guarda o id do utilizador
        [JsonProperty("NomeUti")]
        protected string NomeUti; // Propriedade que guarda o nome do utilizador
        [JsonProperty("Email")]
        protected string Email; // Propriedade que guarda o email do utilizador
        [JsonProperty("Password")]
        protected string Password; // Propriedade que guarda a password do utilizador
        [JsonProperty("DataNascimento")]
        protected DateTime DataNascimento; // Propriedade que guarda a data de nascimento do utilizador
        [JsonProperty("Morada")]
        protected string Morada; // Propriedade que guarda a morada do utilizador
        [JsonProperty("CodigoPostal")]
        protected string CodigoPostal; // Propriedade que guarda o código postal do utilizador
        [JsonProperty("Localidade")]
        protected string Localidade; // Propriedade que guarda a localidade do utilizador
        [JsonProperty("ContactoTelefone")]
        protected string ContactoTelefone; // Propriedade que guarda o contacto telefónico do utilizador
        [JsonProperty("DocIdentificacao")]
        protected string DocIdentificacao; // Propriedade que guarda o documento de identificação do utilizador
        [JsonProperty("TipoDocIdentificacao")]
        protected string TipoDocIdentificacao; // Propriedade que guarda o tipo de documento de identificação do utilizador
        [JsonProperty("IBAN")]
        protected string IBAN; // Propriedade que guarda o IBAN do utilizador
        [JsonProperty("TipoUtilizador")]
        protected string TipoUtilizador; // Propriedade que guarda o tipo de utilizador
        [JsonProperty("IsAtivo")]
        protected bool IsAtivo; // Propriedade que indica se o utilizador está ativo ou não
        [JsonProperty("DataRegisto")]
        protected DateTime DataRegisto; // Propriedade que guarda a data de registo do utilizador

        protected static int ultimoId = 0; // Variável que guarda o último id de utilizador

        #region Construtor
        /// <summary>
        /// Construtor da classe Utilizador
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
        /// <param name="tipoUtilizador"></param>
        /// <param name="isAtivo"></param>
        /// <param name="dataRegisto"></param>
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
                          bool isAtivo = false, // Por defeito, o utilizador não está ativo
                          DateTime dataRegisto = default(DateTime)) // Por defeito, a data de registo é a data atual

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
            DataRegisto = (dataRegisto == default(DateTime)) ? DateTime.Now : dataRegisto; // Se a data de registo não for definida, assume a data atual
        }
        #endregion
        /// <summary>
        /// Métodos de acesso aos atributos da classe Utilizador
        /// </summary>
        /// <returns></returns>
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
