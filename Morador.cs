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
        public void SetIsAdimplente(bool isAdimplente)
        {
            IsAdimplente = isAdimplente;
        }
        #endregion

        #region Json
        /// <summary>
        /// Método para salvar a lista de moradores no ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <param name="listaMoradores"></param>
        public static void SalvarListaFicheiro(string caminhoArquivo, List<Morador> listaMoradores)
        {
            string json = JsonConvert.SerializeObject(listaMoradores, Newtonsoft.Json.Formatting.Indented); // Serializar lista de moradores
            File.WriteAllText(caminhoArquivo, json); // Escrever no ficheiro
        }    

        /// <summary>
        /// Método para carregar a lista de moradores do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static List<Morador> CarregarListaDeMoradores(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro

                if (!string.IsNullOrEmpty(json)) // Verificar se o JSON não está vazio
                {
                    return JsonConvert.DeserializeObject<List<Morador>>(json);
                }
            }

            return new List<Morador>(); // Se o ficheiro não existir ou estiver vazio, retorna uma lista vazia
        }
        #endregion
    }
}
