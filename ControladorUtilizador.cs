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
        private ControladorMorador controladorMorador;

        /// <summary>
        /// Instancia um objeto de ControladorMorador
        /// </summary>
        /// <param name="controladorMorador"></param>
        public void SetControladorMorador(ControladorMorador controladorMorador)
        {
            this.controladorMorador = controladorMorador;
        }

        #region Lista de Utilizadores


        public event Action<Utilizador> UtilizadorCriado;

        /// <summary>
        /// Método para criar um objeto Utilizador
        /// </summary>
        /// <returns></returns>
        public Utilizador CriarUtilizador()
        {
            Console.WriteLine("Por favor, forneça as informações do utilizador:");

            int ultimoId = Utilizador.LerUltimoIdDoJson("utilizador.json");// Obter o último id de utilizador
            int utiId = ++ultimoId; // Incrementar a variável de classe com o último id do utilizador e atribuí-lo para o objeto 
 

            string nomeUti = Servico.LerString("Nome: ");
            string email = Servico.LerString("Email: ");
            string password = Servico.LerString("Password: ");
            DateTime dataNascimento = Servico.LerData("Data de Nascimento (DD-MM-YYYY): ");
            string morada = Servico.LerString("Morada: ");
            string codigoPostal = Servico.LerString("Código Postal: ");
            string localidade = Servico.LerString("Localidade: ");
            string contactoTelefone = Servico.LerString("Contacto Telefone: ");
            string docIdentificacao = Servico.LerString("Documento de Identificação: ");
            string tipoDocIdentificacao = Servico.LerString("Tipo de Documento de Identificação: ");
            string iban = Servico.LerString("IBAN: ");


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
            Utilizador.AtualizarUltimoIdNoJson(utiId);

            Console.WriteLine("Utilizador criado com sucesso!");

            MenuInicial menuInicial = new MenuInicial(this);
            menuInicial.ExibirMenuInicial();

            return utilizador;
        }


        /// <summary>
        /// Método para adicionar utilizador à lista de utilizadores
        /// </summary>
        /// <param name="novoUtilizador"></param>
        public bool AdicionarUtilizador(Utilizador novoUtilizador)
        {
            // Carregar a lista existente do arquivo
            List<Utilizador> listaExistente = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            // Verificar se o utilizador já existe na lista
            if (!ValidarUtilizador(novoUtilizador, listaExistente))
            {
                return false;
            }
            else
            {
                listaExistente.Add(novoUtilizador);

                // Salvar a lista atualizada no arquivo
                Utilizador.SalvarListaFicheiro("utilizador.json", listaExistente);

                // Verificar o tipo de utilizador e criar o objeto correspondente
                if (novoUtilizador.GetTipoUtilizador() == "Morador" || novoUtilizador.GetTipoUtilizador() == "morador")
                {
                    controladorMorador.CriarMorador(novoUtilizador);
                }

               //else if (novoUtilizador.GetTipoUtilizador() == "Funcionário")
               //{
               //    Funcionario.CriarFuncionario(novoUtilizador);
               //}
               else if (novoUtilizador.GetTipoUtilizador() == "Gestor")
               {
                   ControladorGestor.CriarGestor(novoUtilizador);
               }

               return true;


            }
        }



        #region Imprimir Utilizador
        /// <summary>
        /// Método para imprimir a lista de utilizadores
        /// </summary>
        public void ImprimirListaDeUtilizadores()
        {

            List<Utilizador> listaDeUtilizadoresAtual = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            ExibirDetalhesListaUtilizador(listaDeUtilizadoresAtual);

        }

        /// <summary>
        /// Método para exibir os detalhes de uma lista de utilizadores
        /// </summary>
        /// <param name="listaDeUtilizadoresAtual"></param>
        public void ExibirDetalhesListaUtilizador(List<Utilizador> listaDeUtilizadoresAtual)
        {
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

        /// <summary>
        /// Método para exibir os detalhes de um utilizador único
        /// </summary>
        /// <param name="utilizador"></param>
        public void ExibirDetalhesReserva(Utilizador utilizador)
        {
            Console.WriteLine($"ID: {utilizador.GetUtiId()}, Nome: {utilizador.GetNomeUti()}, Email: {utilizador.GetEmail()}, Data de Nascimento: {utilizador.GetDataNascimento():dd-MM-yyyy}, Morada: {utilizador.GetMorada()}, Código Postal: {utilizador.GetCodigoPostal()}, Localidade: {utilizador.GetLocalidade()}, Contacto Telefone: {utilizador.GetContactoTelefone()}, Documento de Identificação: {utilizador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {utilizador.GetTipoDocIdentificacao()}, IBAN: {utilizador.GetIBAN()}, Tipo de Utilizador: {utilizador.GetTipoUtilizador()}, Ativo: {utilizador.GetIsAtivo()} Data de Registo: {utilizador.GetDataRegisto():dd-MM-yyyy}");
        }
        #endregion

        #region Autenticação
        public static Utilizador AutenticarUtilizador(string email, string senha)
        {
            // Carregar a lista de utilizadores do arquivo JSON
            List<Utilizador> listaDeUtilizadores = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            // Verificar se as credenciais correspondem a algum usuário na lista
            return listaDeUtilizadores.FirstOrDefault(u => u.GetEmail() == email && u.GetPassword() == senha);
        }

        public static Utilizador Login()
        {

            string email = Servico.LerString("Email: ");
            string senha = Servico.LerString("Senha: ");

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
        #endregion

        #region Métodos Auxiliares
        public bool ValidarUtilizador(Utilizador novoUtilizador, List<Utilizador> listaExistente)
        {
            // Verificar se o utilizador já existe na lista
            if (listaExistente.Exists(u =>
                           novoUtilizador.GetDocIdentificacao() == u.GetDocIdentificacao() &&
                                          novoUtilizador.GetTipoDocIdentificacao() == u.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O utilizador já existe na lista.");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #endregion

    }
}
