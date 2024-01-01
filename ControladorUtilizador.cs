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

        private List<Utilizador> listaDeUtilizadores;

        public ControladorUtilizador()
        {
            listaDeUtilizadores = new List<Utilizador>();
        }

        public event Action<Utilizador> UtilizadorCriado;
        /// <summary>
        /// Método estático para criar um objeto Utilizador
        /// </summary>
        /// <returns></returns>
        public Utilizador CriarUtilizador()
        {
            Console.WriteLine("Por favor, forneça as informações do utilizador:");

            int ultimoId = LerUltimoIdDoJson("utilizador.json");// Obter o último id de utilizador
            int utiId = ++ultimoId; // Incrementar a variável de classe com o último id do utilizador e atribuí-lo para o objeto 
            Utilizador.SetUltimoId(utiId); // Guardar o último id de utilizador

            Console.Write("Nome: ");
            string nomeUti = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            DateTime dataNascimento;
            while (true)
            {
                Console.Write("Data de Nascimento (DD-MM-YYYY): ");
                string inputDataNascimento = Console.ReadLine();

                if (DateTime.TryParseExact(inputDataNascimento, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
                {
                    break; // Sai do loop se a data for válida
                }
                else
                {
                    Console.WriteLine("Formato de data inválido. Tente novamente no formato DD-MM-YYYY.");
                }
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

            string tipoUtilizador;
            do
            {
                Console.Write("Tipo de Utilizador (Morador, Funcionario, Gestor): ");
                tipoUtilizador = Console.ReadLine().ToLower(); // Converta para minúsculas para facilitar a comparação

                if (!tipoUtilizador.Equals("morador", StringComparison.OrdinalIgnoreCase) &&
                    !tipoUtilizador.Equals("funcionario", StringComparison.OrdinalIgnoreCase) &&
                    !tipoUtilizador.Equals("gestor", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Tipo de utilizador inválido. Por favor, escolha entre Morador, Funcionario, ou Gestor.");
                }
            } while (!tipoUtilizador.Equals("morador", StringComparison.OrdinalIgnoreCase) &&
         !tipoUtilizador.Equals("funcionario", StringComparison.OrdinalIgnoreCase) &&
         !tipoUtilizador.Equals("gestor", StringComparison.OrdinalIgnoreCase));


            // Cria um novo objeto Utilizador
            Utilizador utilizador = new Utilizador(
                utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, tipoUtilizador);

            AdicionarUtilizador(utilizador); // Adicionar utilizador à lista de utilizadores
            AtualizarUltimoIdNoJson(utiId);

            Console.WriteLine("Utilizador criado com sucesso!");

            MenuInicial menuInicial = new MenuInicial(this);
            menuInicial.ExibirMenuInicial();

            return utilizador;
        }


        /// <summary>
        ///     
        /// </summary>
        /// <param name="novoUtilizador"></param>
        public void AdicionarUtilizador(Utilizador novoUtilizador)
        {
            // Carregar a lista existente do arquivo
            List<Utilizador> listaExistente = CarregarListaDeUtilizadores("utilizador.json");

            // Verificar se o utilizador já existe na lista
            if (listaExistente.Exists(u =>
                novoUtilizador.GetDocIdentificacao() == u.GetDocIdentificacao() &&
                novoUtilizador.GetTipoDocIdentificacao() == u.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O utilizador já existe na lista.");
            }
            else
            {
                listaExistente.Add(novoUtilizador);

                // Salvar a lista atualizada no arquivo
                SalvarListaFicheiro("utilizador.json", listaExistente);

                Utilizador.listaDeUtilizadores = listaExistente; // Atualizar a lista estática

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
        public void ImprimirListaDeUtilizadores()
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

        public static Utilizador AutenticarUtilizador(string email, string senha)
        {
            // Carregar a lista de utilizadores do arquivo JSON
            List<Utilizador> listaDeUtilizadores = CarregarListaDeUtilizadores("utilizador.json");

            // Verificar se as credenciais correspondem a algum usuário na lista
            return listaDeUtilizadores.FirstOrDefault(u => u.GetEmail() == email && u.GetPassword() == senha);
        }


        public static Utilizador Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            Utilizador utilizador = AutenticarUtilizador(email, senha);

            if (utilizador != null)
            {
                Console.WriteLine($"Bem-vindo, {utilizador.GetNomeUti()}!");
                return utilizador;
            }

            else
            {
                return null;
            }
        }

        #region Json
        /// <summary>
        /// Método para criar um ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public static void CriarFicheiroJson(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                File.Create(caminhoArquivo).Close();
            }
        }

        /// <summary>
        /// Método para salvar a lista de utilizadores no ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        public static void SalvarListaFicheiro(string caminhoArquivo, List<Utilizador> listaUtilizadores)
        {
            string json = JsonConvert.SerializeObject(new { UltimoId = LerUltimoIdDoJson(caminhoArquivo), Utilizadores = listaUtilizadores }, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
        }

        /// <summary>
        /// Método para carregar a lista de utilizadores do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        /// 
        public static List<Utilizador> CarregarListaDeUtilizadores(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
            {
                string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro

                if (!string.IsNullOrEmpty(json)) // Verificar se o JSON não está vazio
                {
                    // Desserializar o JSON em um objeto anônimo que contém a propriedade "Utilizadores"
                    var jsonData = JsonConvert.DeserializeAnonymousType(json, new { Utilizadores = new List<Utilizador>() });

                    // Retornar a lista de utilizadores da propriedade anônima
                    return jsonData.Utilizadores;
                }
            }

            return new List<Utilizador>(); // Se o ficheiro não existir ou estiver vazio, retorna uma lista vazia
        }

        /// <summary>
        /// Método que lê o último id de utilizador do ficheiro JSON
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns></returns>
        public static int LerUltimoIdDoJson(string caminhoArquivo)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(File.ReadAllText(caminhoArquivo));

            if (jsonData != null && jsonData.UltimoId != null)
            {
                return (int)jsonData.UltimoId;
            }

            return 0; // Valor padrão se o arquivo ou conteúdo não existir
        }

        /// <summary>
        /// Método que atualiza o último id de utilizador no ficheiro JSON
        /// </summary>
        /// <param name="novoUltimoId"></param>
        public static void AtualizarUltimoIdNoJson(int novoUltimoId)
        {
            string caminhoArquivo = "utilizador.json";
            string json = JsonConvert.SerializeObject(new { UltimoId = novoUltimoId, Utilizadores = Utilizador.listaDeUtilizadores }, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
        }

        #endregion

        #endregion

    }
}
