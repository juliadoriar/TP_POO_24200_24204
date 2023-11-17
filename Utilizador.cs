using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace TP_POO_24200_24204
{
    public class Utilizador
    {
        protected static List<Utilizador> listaDeUtilizadores = new List<Utilizador>();

        protected int UtiId;
        protected string NomeUti;
        protected string Email;
        protected string Password;
        protected DateTime DataNascimento;
        protected string Morada;
        protected string CodigoPostal;
        protected string Localidade;
        protected string ContactoTelefone;
        protected string DocIdentificacao;
        protected string TipoDocIdentificacao;
        protected string IBAN;
        protected string TipoUtilizador;
        protected bool IsAtivo;
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

        /*[JsonConstructor]
        private Utilizador(int utiId, string nomeUti, string email, string password, DateTime dataNascimento, string morada, string codigoPostal, string localidade, string contactoTelefone, string docIdentificacao, string tipoDocIdentificacao, string iban, string tipoUtilizador, bool isAtivo, DateTime dataRegisto)
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
            DataRegisto = dataRegisto;
        }*/
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

        public static void SalvarListaEmArquivo(string caminhoArquivo)
        {
            string json = JsonSerializer.Serialize(listaDeUtilizadores, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true });
            //string json = JsonSerializer.Serialize(listaDeUtilizadores);

            File.WriteAllText(caminhoArquivo, json);
        }
        // Carregar lista de utilizadores
        public static void CarregarListaDeUtilizadores(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                listaDeUtilizadores = JsonSerializer.Deserialize<List<Utilizador>>(json);
            }
        }

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
            SalvarListaEmArquivo("utilizador.json");
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
                //SalvarListaEmArquivo("utilizador.json");
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

            CarregarListaDeUtilizadores("utilizador.json");
            foreach (Utilizador utilizador in listaDeUtilizadores)
            {
                Console.WriteLine($"ID: {utilizador.GetUtiId()}, Nome: {utilizador.GetNomeUti()}, Email: {utilizador.GetEmail()}, Data de Nascimento: {utilizador.GetDataNascimento():dd-MM-yyyy}, Morada: {utilizador.GetMorada()}, Código Postal: {utilizador.GetCodigoPostal()}, Localidade: {utilizador.GetLocalidade()}, Contacto Telefone: {utilizador.GetContactoTelefone()}, Documento de Identificação: {utilizador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {utilizador.GetTipoDocIdentificacao()}, IBAN: {utilizador.GetIBAN()}, Tipo de Utilizador: {utilizador.GetTipoUtilizador()}, Ativo: {utilizador.GetIsAtivo()} Data de Registo: {utilizador.GetDataRegisto():dd-MM-yyyy}");
            }
        }
        #region Json

        #endregion

        #endregion
    }


}
