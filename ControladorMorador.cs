using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que atua como controlador para operações relacionadas aos moradores
    /// </summary>
    public class ControladorMorador
    {
        #region Lista de Moradores
        /// <summary>
        /// Método estático para criar um objeto morador
        /// </summary>
        /// <param name="utilizador"></param>
        /// <returns></returns>
        public static Morador CriarMorador(Utilizador utilizador)
        {
            Morador morador = new Morador(
                        utilizador.GetUtiId(),
                        utilizador.GetNomeUti(),
                        utilizador.GetEmail(),
                        utilizador.GetPassword(),
                        utilizador.GetDataNascimento(),
                        utilizador.GetMorada(),
                        utilizador.GetCodigoPostal(),
                        utilizador.GetLocalidade(),
                        utilizador.GetContactoTelefone(),
                        utilizador.GetDocIdentificacao(),
                        utilizador.GetTipoDocIdentificacao(),
                        utilizador.GetIBAN(),
                        utilizador.GetIsAtivo(),
                        utilizador.GetDataRegisto());
            AdicionarMorador(morador); // Adicionar morador à lista de moradores
            return morador;

        }

        /// <summary>
        ///  Método para adicionar morador à lista de moradores
        /// </summary>
        /// <param name="novoMorador"></param>
        public static void AdicionarMorador(Morador novoMorador)
        {
            // Verificar se o morador já existe na lista através do documento de identificação
            if (Morador.listaDeMoradores.Exists(morador => //Função lambda
                    novoMorador.GetDocIdentificacao() == morador.GetDocIdentificacao() &&
                    novoMorador.GetTipoDocIdentificacao() == morador.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O morador já existe na lista.");
            }
            else
            {
                Morador.listaDeMoradores.Add(novoMorador); // Adicionar morador à lista de moradores
                SalvarListaFicheiro("morador.json"); // Salvar lista de moradores no ficheiro
            }

        }

        /// <summary>
        /// Método para imprimir a lista de moradores
        /// </summary>
        public static void ImprimirListaDeMoradores()
        {
            List<Morador> listaDeMoradoresAtual = CarregarListaDeMoradores("morador.json"); // Carregar lista de moradores do ficheiro

            if (listaDeMoradoresAtual == null)
            {
                Console.WriteLine("Não há moradores registados");
            }
            else
            {
                Console.WriteLine("Lista de Moradores");
                Console.WriteLine("--------------------------------------------");
                foreach (Morador morador in listaDeMoradoresAtual)
                {
                    Console.WriteLine($"ID: {morador.GetUtiId()}, Nome: {morador.GetNomeUti()}, Email: {morador.GetEmail()}, Data de Nascimento: {morador.GetDataNascimento():dd-MM-yyyy}, Morada: {morador.GetMorada()}, Código Postal: {morador.GetCodigoPostal()}, Localidade: {morador.GetLocalidade()}, Contacto Telefone: {morador.GetContactoTelefone()}, Documento de Identificação: {morador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {morador.GetTipoDocIdentificacao()}, IBAN: {morador.GetIBAN()}, Tipo de Utilizador: {morador.GetTipoUtilizador()}, Ativo: {morador.GetIsAtivo()} Data de Registo: {morador.GetDataRegisto():dd-MM-yyyy}, Adimplente: {morador.GetIsAdimplente()}");
                }
            }

        }
        #region Json
        /// <summary>
        /// Método para salvar a lista de moradores no ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public static void SalvarListaFicheiro(string caminhoArquivo)
        {
            string json = JsonConvert.SerializeObject(Morador.listaDeMoradores, Newtonsoft.Json.Formatting.Indented); // Serializar lista de moradores
            File.WriteAllText(caminhoArquivo, json); // Escrever no ficheiro
        }

        /// <summary>
        /// Método para carregar a lista de moradores do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static List<Morador> CarregarListaDeMoradores(string caminhoArquivo)
        {
            List<Morador> listaDeMoradoresAtual = new List<Morador>();

            if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro
                listaDeMoradoresAtual = JsonConvert.DeserializeObject<List<Morador>>(json); // Desserializar o ficheiro
                return listaDeMoradoresAtual; // Retornar a lista de moradores
            }
            
            return new List<Morador>(); // Se o ficheiro não existir, retorna uma lista vazia
        }
        #endregion
        #endregion

    }
}
