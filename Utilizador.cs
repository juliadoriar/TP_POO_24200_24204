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
        protected static List<Utilizador> listaDeUtilizadores = new List<Utilizador>();

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
        #endregion

        #region Lista de Utilizadores
        public static Utilizador CriarUtilizador()
        {
            Console.WriteLine("Por favor, forneça as informações do utilizador:");

            int utiId = ++Utilizador.ultimoId;

            Console.Write("Nome: ");
            string nomeUti = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Data de Nascimento (DD-MM-YYYY): ");
            DateTime dataNascimento;
            if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
            {
                Console.WriteLine($"Data de Nascimento: {dataNascimento:dd-MM-yyyy}");
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

            AdicionarUtilizador(utilizador);
            return utilizador;

        }

        public static void AdicionarUtilizador(Utilizador novoUtilizador)
        {
            // Verificar se o utilizador já existe na lista
            if (listaDeUtilizadores.Exists(Utilizador =>    //Função lambda
                novoUtilizador.GetDocIdentificacao() == Utilizador.GetDocIdentificacao() &&
                novoUtilizador.GetTipoDocIdentificacao() == Utilizador.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O utilizador já existe na lista.");
            }
            else
            {
                listaDeUtilizadores.Add(novoUtilizador);
                SalvarListaFicheiro("utilizador.json");
                Console.WriteLine("Utilizador adicionado com sucesso.");
                if (novoUtilizador.GetTipoUtilizador() == "Morador" || novoUtilizador.GetTipoUtilizador() == "morador")
                {
                    Morador.CriarMorador(novoUtilizador);
                }
                /*else if (novoUtilizador.GetTipoUtilizador() == "Funcionário")
                {
                    Funcionario.CriarFuncionario(novoUtilizador);
                }
                else if (novoUtilizador.GetTipoUtilizador() == "Gestor")
                {
                    Gestor.CriarGestor(novoUtilizador);
                }*/
            }

        }

        // Imprimir lista de utilizadores
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
        //Criar ficheiro Json
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
        
        // Salvar lista de utilizadores
        public static void SalvarListaFicheiro(string caminhoArquivo)
        {
            string json = JsonConvert.SerializeObject(listaDeUtilizadores, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
        }
        // Carregar lista de utilizadores
        public static List<Utilizador> CarregarListaDeUtilizadores(string caminhoArquivo)
        {
            List<Utilizador> listaDeUtilizadoresAtual = new List<Utilizador>();
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                listaDeUtilizadoresAtual = JsonConvert.DeserializeObject<List<Utilizador>>(json);
                return listaDeUtilizadoresAtual;
            }
            
            // Se o ficheiro não existir, retorna uma lista vazia
            return new List<Utilizador>();

        }
        #endregion

        #endregion
    }


}
