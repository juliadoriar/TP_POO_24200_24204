using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace TP_POO_24200_24204
{
    public class Morador : Utilizador
    {
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
        DateTime dataRegisto,
        bool isAdimplente)
        : base(utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, "Morador", false, dataRegisto)
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

        #region Métodos

            #region Lista de Moradores
            public static Morador CriarMorador()
            {
                Console.WriteLine("Por favor, forneça as informações do morador:");

                Console.Write("ID do Morador: ");
                int utiId = int.Parse(Console.ReadLine());

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

                DateTime dataRegisto = DateTime.Now;

                bool isAdimplente = true;

                // Cria um novo objeto Morador
                Morador morador = new Morador(
                    utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, dataRegisto, isAdimplente);

                return morador;   

            }

            public static List<Morador> CriarListaDeMoradores()
            {
                List<Morador> listaDeMoradores = new List<Morador>();

                return listaDeMoradores;
            }

            // Add morador à lista de moradores
            public static void AdicionarMorador(List<Morador> listaDeMoradores)
            {
                Morador novoMorador = CriarMorador();

               //Verificar se o morador já existe na lista
                foreach (Morador morador in listaDeMoradores)
                {
                    if (novoMorador.GetDocIdentificacao() == morador.GetDocIdentificacao() && novoMorador.GetTipoDocIdentificacao() == morador.GetTipoDocIdentificacao())
                    {
                        Console.WriteLine("O morador já existe na lista.");
                        novoMorador = null;
                    }
                    else
                    {
                        listaDeMoradores.Add(novoMorador);
                    }
                }
            }

            // Imprimir lista de moradores
            public static void ImprimirListaDeMoradores(List<Morador> listaDeMoradores)
        {
                foreach (Morador morador in listaDeMoradores)
            {
                    Console.WriteLine($"ID: {morador.GetUtiId()}, Nome: {morador.GetNomeUti()}, Email: {morador.GetEmail()}, Data de Nascimento: {morador.GetDataNascimento():dd-MM-yyyy}, Morada: {morador.GetMorada()}, Código Postal: {morador.GetCodigoPostal()}, Localidade: {morador.GetLocalidade()}, Contacto Telefone: {morador.GetContactoTelefone()}, Documento de Identificação: {morador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {morador.GetTipoDocIdentificacao()}, IBAN: {morador.GetIBAN()}, Tipo de Utilizador: {morador.GetTipoUtilizador()}, Ativo: {morador.GetIsAtivo()} Data de Registo: {morador.GetDataRegisto():dd-MM-yyyy}, Adimplente: {morador.GetIsAdimplente()}");
                }
            }
            #endregion

        #endregion
    }
}
