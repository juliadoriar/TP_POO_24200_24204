using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    public class Morador : Utilizador
    {
        private static List<Morador> listaDeMoradores = new List<Morador>();
        
        [JsonProperty("isAdimplente")]
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
        bool isAtivo,
        DateTime dataRegisto,
        bool isAdimplente = true)
        : base(utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, "Morador", isAtivo, dataRegisto)
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
            AdicionarMorador(morador);
            return morador;

        }
        // Add morador à lista de moradores
        public static void AdicionarMorador(Morador novoMorador)
        {
            // Verificar se o morador já existe na lista
            if (listaDeMoradores.Exists(morador =>
                    novoMorador.GetDocIdentificacao() == morador.GetDocIdentificacao() &&
                    novoMorador.GetTipoDocIdentificacao() == morador.GetTipoDocIdentificacao()))
                {
                    Console.WriteLine("O morador já existe na lista.");
            }
            else
            {
                listaDeMoradores.Add(novoMorador);
                SalvarListaFicheiro("morador.json");
            }

        }

        // Imprimir lista de moradores
        public static void ImprimirListaDeMoradores()
            {
                List<Morador> listaDeMoradoresAtual = CarregarListaDeMoradores("morador.json");

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
        // Salvar lista de moradores
        public static void SalvarListaFicheiro(string caminhoArquivo)
        {
            string json = JsonConvert.SerializeObject(listaDeMoradores, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
        }
        // Carregar lista de utilizadores
        public static List<Morador> CarregarListaDeMoradores(string caminhoArquivo)
        {
            List<Morador> listaDeMoradoresAtual = new List<Morador>();

            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                listaDeMoradoresAtual = JsonConvert.DeserializeObject<List<Morador>>(json);
                return listaDeMoradoresAtual;
            }

            // Se o ficheiro não existir, retorna uma lista vazia
            return new List<Morador>();
        }
        #endregion
        #endregion

        #endregion
    }
}
