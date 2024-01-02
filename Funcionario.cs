using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    public class Funcionario : Utilizador
    {
        public Funcionario(
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
            DateTime dataRegisto)
            : base(utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, "Funcionario", isAtivo, dataRegisto)
        {
        }

        #region JsonFuncionario
        /// <summary>
        /// Método para salvar a lista de funcionários no ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <param name="listaFuncionarios"></param>
        public static void SalvarListaFicheiro(string caminhoArquivo, List<Funcionario> listaFuncionarios)
        {
            string json = JsonConvert.SerializeObject(listaFuncionarios, Newtonsoft.Json.Formatting.Indented); // Serializar lista de funcionários
            File.WriteAllText(caminhoArquivo, json); // Escrever no ficheiro
        }

        /// <summary>
        /// Método para carregar a lista de funcionários do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static List<Funcionario> CarregarListaDeFuncionarios(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro

                if (!string.IsNullOrEmpty(json)) // Verificar se o JSON não está vazio
                {
                    return JsonConvert.DeserializeObject<List<Funcionario>>(json);
                }
            }

            return new List<Funcionario>(); // Se o ficheiro não existir ou estiver vazio, retorna uma lista vazia
        }
        #endregion
    }

}
