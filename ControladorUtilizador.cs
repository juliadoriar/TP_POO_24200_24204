using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que atua como controlador para operações relacionadas aos utilizadores
    /// </summary>
    public class ControladorUtilizador
    {
        #region Lista de Utilizadores
        /// <summary>
        /// Método estático para criar um objeto Utilizador
        /// </summary>
        /// <returns></returns>
        public static Utilizador CriarUtilizador()
        {
            Console.WriteLine("Por favor, forneça as informações do utilizador:");

            int ultimoId = Utilizador.GetUltimoId(); // Obter o último id de utilizador
            int utiId = ++ultimoId; // Incrementar a variável de classe com o último id do utilizador e atribuí-lo para o objeto 
            Utilizador.SetUltimoId(utiId); // Guardar o último id de utilizador

            Console.Write("Nome: ");
            string nomeUti = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Data de Nascimento (DD-MM-YYYY): ");
            DateTime dataNascimento;
            if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento)) // Verificar se a data de nascimento está no formato correto
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Formato de data inválido.");
            }

            Console.Write("Morada: ");
            string morada = Console.ReadLine();

            Console.Write("Código Postal: ");
            string codigoPostal = Console.ReadLine();

            Console.Write("Localidade: ");
            string localidade = Console.ReadLine();

            Console.Write("Contacto Telefone: ");
            string contactoTelefone = Console.ReadLine();

            Console.Write("Documento de Identificação: ");
            string docIdentificacao = Console.ReadLine();

            Console.Write("Tipo de Documento de Identificação: ");
            string tipoDocIdentificacao = Console.ReadLine();

            Console.Write("IBAN: ");
            string iban = Console.ReadLine();

            Console.Write("Tipo de Utilizador: ");
            string tipoUtilizador = Console.ReadLine();

            // Cria um novo objeto Utilizador
            Utilizador utilizador = new Utilizador(
                utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, tipoUtilizador);

            AdicionarUtilizador(utilizador); // Adicionar utilizador à lista de utilizadores
            return utilizador;
        }

        /// <summary>
        /// Método para adicionar utilizador à lista de utilizadores
        /// </summary>
        /// <param name="novoUtilizador"></param>
        public static void AdicionarUtilizador(Utilizador novoUtilizador)
        {
            // Verificar se o utilizador já existe na lista
            if (Utilizador.listaDeUtilizadores.Exists(Utilizador =>    //Função lambda
                novoUtilizador.GetDocIdentificacao() == Utilizador.GetDocIdentificacao() &&
                novoUtilizador.GetTipoDocIdentificacao() == Utilizador.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O utilizador já existe na lista.");
            }
            else
            {
                Utilizador.listaDeUtilizadores.Add(novoUtilizador);
                SalvarListaFicheiro("utilizador.json"); // Salvar lista de utilizadores no ficheiro
                Console.WriteLine("Utilizador adicionado com sucesso.");
                
                // Verificar o tipo de utilizador e criar o objeto correspondente
                if (novoUtilizador.GetTipoUtilizador() == "Morador" || novoUtilizador.GetTipoUtilizador() == "morador")
                {
                    ControladorMorador.CriarMorador(novoUtilizador);
                }
                /* A SER IMPLEMENTADO
                else if (novoUtilizador.GetTipoUtilizador() == "Funcionário")
                {
                    Funcionario.CriarFuncionario(novoUtilizador);
                }
                else if (novoUtilizador.GetTipoUtilizador() == "Gestor")
                {
                    Gestor.CriarGestor(novoUtilizador);
                }*/
            }

        }

        /// <summary>
        /// Método para imprimir a lista de utilizadores
        /// </summary>
        public static void ImprimirListaDeUtilizadores()
        {

            List<Utilizador> listaDeUtilizadoresAtual = CarregarListaDeUtilizadores("utilizador.json");

            if (listaDeUtilizadoresAtual == null)
            {
                Console.WriteLine("Não há utilizadores registados");
            }
            else
            {
                Console.WriteLine("Lista de Utilizadores");
                Console.WriteLine("--------------------------------------------");
                foreach (Utilizador utilizador in listaDeUtilizadoresAtual)
                {
                    Console.WriteLine($"ID: {utilizador.GetUtiId()}, Nome: {utilizador.GetNomeUti()}, Email: {utilizador.GetEmail()}, Data de Nascimento: {utilizador.GetDataNascimento():dd-MM-yyyy}, Morada: {utilizador.GetMorada()}, Código Postal: {utilizador.GetCodigoPostal()}, Localidade: {utilizador.GetLocalidade()}, Contacto Telefone: {utilizador.GetContactoTelefone()}, Documento de Identificação: {utilizador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {utilizador.GetTipoDocIdentificacao()}, IBAN: {utilizador.GetIBAN()}, Tipo de Utilizador: {utilizador.GetTipoUtilizador()}, Ativo: {utilizador.GetIsAtivo()} Data de Registo: {utilizador.GetDataRegisto():dd-MM-yyyy}");
                }
            }

        }
        #region Json
        /// <summary>
        /// Método para criar um ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public static void CriarFicheiroJson(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                // Se o ficheiro existir, limpa o conteúdo
                File.WriteAllText(caminhoArquivo, string.Empty);
            }
            else
            {
                // Se o ficheiro não existir, cria o arquivo vazio
                File.Create(caminhoArquivo).Close();
            }
        }

        /// <summary>
        /// Método para salvar a lista de utilizadores no ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public static void SalvarListaFicheiro(string caminhoArquivo)
        {
            string json = JsonConvert.SerializeObject(Utilizador.listaDeUtilizadores, Newtonsoft.Json.Formatting.Indented); // Serializar lista de utilizadores
            File.WriteAllText(caminhoArquivo, json); // Escrever no ficheiro
        }

        /// <summary>
        /// Método para carregar a lista de utilizadores do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static List<Utilizador> CarregarListaDeUtilizadores(string caminhoArquivo)
        {
            List<Utilizador> listaDeUtilizadoresAtual = new List<Utilizador>();
            if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro
                listaDeUtilizadoresAtual = JsonConvert.DeserializeObject<List<Utilizador>>(json); // Desserializar o ficheiro
                return listaDeUtilizadoresAtual; // Retornar a lista de utilizadores
            }
                        
            return new List<Utilizador>(); // Se o ficheiro não existir, retorna uma lista vazia
        }
        #endregion

        #endregion

    }
}
