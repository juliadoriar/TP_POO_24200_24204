using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TP_POO_24200_24204.ControladorReserva;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que atua como controlador para operações relacionadas aos utilizadores
    /// </summary>
    public class ControladorUtilizador
    {
        private ControladorMorador controladorMorador;
        private ControladorGestor controladorGestor;
        private ControladorFuncionario controladorFuncionario;
        

        /// <summary>
        /// Instancia um objeto de ControladorMorador
        /// </summary>
        /// <param name="controladorMorador"></param>
        public void SetControladorMorador(ControladorMorador controladorMorador)
        {
            this.controladorMorador = controladorMorador;
        }

        /// <summary>
        /// Instancia um objeto de ControladorFuncionario
        /// </summary>
        /// <param name="controladorGestor"></param>
        public void SetControladorGestor(ControladorGestor controladorGestor)
        {
            this.controladorGestor = controladorGestor;
        }

        /// <summary>
        /// Instancia um objeto de ControladorFuncionario
        /// </summary>
        /// <param name="controladorFuncionario"></param>
        public void SetControladorFuncionario(ControladorFuncionario controladorFuncionario)
        {
            this.controladorFuncionario = controladorFuncionario;
        }

        #region Lista de Utilizadores



        public event Action<Utilizador> UtilizadorCriado;
        #region CRUD Utilizador
        #region Criar Utilizador

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
                tipoUtilizador = Servico.LerString("Tipo de Utilizador (Morador, Funcionario, Gestor): ").ToLower(); // Converte para minúsculas para facilitar a comparação

                if (!ValidarTipoUtilizador(tipoUtilizador))
                {
                    Console.WriteLine("Tipo de utilizador inválido. Por favor, escolha entre Morador, Funcionario, ou Gestor.");
                }
            } while (!ValidarTipoUtilizador(tipoUtilizador));


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

               else if (novoUtilizador.GetTipoUtilizador() == "Funcionário" || novoUtilizador.GetTipoUtilizador() == "funcionário" || novoUtilizador.GetTipoUtilizador() == "funcionario" || novoUtilizador.GetTipoUtilizador() == "Funcionario")
               {
                   controladorFuncionario.CriarFuncionario(novoUtilizador);
               }
               else if (novoUtilizador.GetTipoUtilizador() == "Gestor" || novoUtilizador.GetTipoUtilizador() == "gestor")
               {
                   controladorGestor.CriarGestor(novoUtilizador);
               }

               return true;


            }
        }
        #endregion

        #region Buscar Utilizador
        public Predicate<Utilizador> MenuBuscarUtilizador()
        {
            // Inicializa o predicado como nulo
            Predicate<Utilizador> predicado = null;
            // Obtém a lista de utilizadores atual do controlador
            List<Utilizador> listaDeReservasAtual = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            // Loop para escolher o critério de busca
            while (true)
            {
                Console.WriteLine("Escolha o critério de busca:");
                Console.WriteLine("1. Por ID do Utilizador");
                Console.WriteLine("2. Por Nome do Utilizador");
                Console.WriteLine("3. Por Email do Utilizador");
                Console.WriteLine("4. Por Data de Nascimento do Utilizador");
                Console.WriteLine("5. Por Localidade do Utilizador");
                Console.WriteLine("6. Por Documento de Identificação do Utilizador");
                Console.WriteLine("7. Por Tipo de Utilizador");
                Console.WriteLine("8. Por Ativo do Utilizador");
                Console.WriteLine("9. Por Data de Registo do Utilizador");
                Console.WriteLine("0. Cancelar");

                // Entrada de opção do usuário
                int opcao = Servico.LerInteiro("");

                string valorString;
                int valorInteiro;
                DateTime valorData;
                bool valorBool;

                // Switch para definir o predicado com base na opção escolhida
                switch (opcao)
                {
                    case 1:
                        valorInteiro = Servico.LerInteiro("Digite o ID do Utilizador:");
                        predicado = u => u.GetUtiId() == valorInteiro;
                        break;
                    case 2:
                        valorString = Servico.LerString("Digite o Nome do Utilizador:");
                        predicado = u => u.GetNomeUti() == valorString;
                        break;
                    case 3:
                        valorString = Servico.LerString("Digite o Email do Utilizador:");
                        predicado = u => u.GetEmail() == valorString;
                        break;
                    case 4:
                        valorData = Servico.LerData("Digite a Data de Nascimento do Utilizador (dd-MM-yyyy):");
                        predicado = u => u.GetDataNascimento() == valorData;
                        break;
                    case 5:
                        valorString = Servico.LerString("Digite a Localidade do Utilizador:");
                        predicado = u => u.GetLocalidade() == valorString;
                        break;
                    case 6:
                        valorString = Servico.LerString("Digite o Documento de Identificação do Utilizador:");
                        predicado = u => u.GetDocIdentificacao() == valorString;
                        break;
                    case 7:
                        do
                        {
                            valorString = Servico.LerString("Digite o Tipo de Utilizador:").ToLower(); // Converte para minúsculas para facilitar a comparação

                            if (!ValidarTipoUtilizador(valorString))
                            {
                                Console.WriteLine("Tipo de utilizador inválido. Por favor, escolha entre Morador, Funcionario, ou Gestor.");
                            }
                        } while (!ValidarTipoUtilizador(valorString));

                        predicado = u => u.GetTipoUtilizador() == valorString;
                        break;
                    case 8:
                        valorBool = Servico.LerBooleano("Digite o Ativo do Utilizador:");
                        predicado = u => u.GetIsAtivo() == valorBool;
                        break;
                    case 9:
                        valorData = Servico.LerData("Digite a Data de Registo do Utilizador (dd-MM-yyyy):");
                        predicado = u => u.GetDataRegisto() == valorData;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                // Se uma opção válida foi escolhida, sai do loop
                if (opcao >= 1 && opcao <= 9)
                {
                    break;
                }
            }

            // Se um predicado foi definido, realiza a busca e exibe os resultados
            if (predicado != null)
            {
                List<Utilizador> listaUtilizadoresSelecionados = BuscarUtilizador(predicado);
                ExibirDetalhesListaUtilizador(listaUtilizadoresSelecionados);
            }

            return predicado;
        }

        public List<Utilizador> BuscarUtilizador(Predicate<Utilizador> predicado)
        {
            List<Utilizador> listaExistente = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            if (predicado == null)
            {
                // Se o predicado for nulo, retorna null
                return null;
            }
            else
            {
                // Se o predicado não for nulo, retorna a lista de reservas que satisfazem o predicado
                List<Utilizador> UtilizadoresEncontrados = listaExistente.FindAll(predicado);
                return UtilizadoresEncontrados;
            }
        }
        #endregion

        #region Editar Utilizador
        /// <summary>
        /// Menu para editar um utilizador
        /// </summary>
        public void MenuEditarUtilizador()
        {
            // Obtém o predicado para buscar os utilizadores
            Predicate<Utilizador> predicado = MenuBuscarUtilizador();

            // Busca os utilizadores com base no predicado
            List<Utilizador> utilizadoresEncontrados = BuscarUtilizador(predicado);

            if (utilizadoresEncontrados != null && utilizadoresEncontrados.Count > 0)
            {
                // Solicita ao usuário que escolha o ID do utilizador a ser atualizado
                int idEscolhido = Servico.LerInteiro("Escolha o ID do utilizador que deseja atualizar: ");

                // Busca o utilizador escolhido
                Utilizador utilizadorEscolhido = utilizadoresEncontrados.FirstOrDefault(u => u.GetUtiId() == idEscolhido);

                if (utilizadorEscolhido != null)
                {
                    // Exibe detalhes do utilizador escolhido
                    ExibirDetalhesUtilizador(utilizadorEscolhido);

                    // Chama o controlador para editar o utilizador
                    EditarUtilizador(utilizadorEscolhido);
                }
                else
                {
                    Console.WriteLine($"Utilizador com ID {idEscolhido} não encontrado. A atualização não foi realizada.");
                }
            }
            else
            {
                Console.WriteLine("Utilizador não encontrado. A atualização não foi realizada.");
            }
        }

        /// <summary>
        /// Menu para escolher o TipoCampo a ser atualizado
        /// </summary>
        /// <returns></returns>
        public TipoCampo MenuSelecionarCampo()
        {
            TipoCampo tipoCampo = TipoCampo.Nulo; // Valor padrão

            while (true)
            {
                Console.WriteLine("Escolha o campo que deseja atualizar:");
                Console.WriteLine("1. Nome do Utilizador");
                Console.WriteLine("2. Email");
                Console.WriteLine("3. Password");
                Console.WriteLine("4. Data de Nascimento");
                Console.WriteLine("5. Morada");
                Console.WriteLine("6. Código Postal");
                Console.WriteLine("7. Localidade");
                Console.WriteLine("8. Contacto Telefone");
                Console.WriteLine("9. Documento de Identificação");
                Console.WriteLine("10. Tipo de Documento de Identificação");
                Console.WriteLine("11. IBAN");
                Console.WriteLine("12. Tipo de Utilizador");
                Console.WriteLine("13. Ativo");


                // Entrada de opção do usuário
                int opcaoCampo = Servico.LerInteiro("");

                switch (opcaoCampo)
                {
                    case 1:
                        tipoCampo = TipoCampo.NomeUti;
                        break;
                    case 2:
                        tipoCampo = TipoCampo.Email;
                        break;
                    case 3:
                        tipoCampo = TipoCampo.Password;
                        break;
                    case 4:
                        tipoCampo = TipoCampo.DataNascimento;
                        break;
                    case 5:
                        tipoCampo = TipoCampo.Morada;
                        break;
                    case 6:
                        tipoCampo = TipoCampo.CodigoPostal;
                        break;
                    case 7:
                        tipoCampo = TipoCampo.Localidade;
                        break;
                    case 8:
                        tipoCampo = TipoCampo.ContactoTelefone;
                        break;
                    case 9:
                        tipoCampo = TipoCampo.DocIdentificacao;
                        break;
                    case 10:
                        tipoCampo = TipoCampo.TipoDocIdentificacao;
                        break;
                    case 11:
                        tipoCampo = TipoCampo.IBAN;
                        break;
                    case 12:
                        tipoCampo = TipoCampo.TipoUtilizador;
                        break;
                    case 13:
                        tipoCampo = TipoCampo.IsAtivo;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }

                // Se uma opção válida foi escolhida, sai do loop
                if (opcaoCampo >= 1 && opcaoCampo <= 13)
                {
                    break;
                }
            }

            return tipoCampo;
        }

        /// <summary>
        /// Obtém o novo valor para o campo escolhido
        /// </summary>
        /// <param name="utilizadorEscolhido"></param>
        /// <param name="tipoCampo"></param>
        /// <returns></returns>
        public Utilizador ObterNovoValorUtilizador(Utilizador utilizadorEscolhido, TipoCampo tipoCampo)
        {
            switch (tipoCampo)
            {
                case TipoCampo.NomeUti:
                    // Solicita ao usuário o novo valor para o campo NomeUti
                    utilizadorEscolhido.SetNomeUti(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.Email:
                    // Solicita ao usuário o novo valor para o campo Email
                    utilizadorEscolhido.SetEmail(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.Password:
                    // Solicita ao usuário o novo valor para o campo Password
                    utilizadorEscolhido.SetPassword(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.DataNascimento:
                    // Solicita ao usuário o novo valor para o campo DataNascimento
                    utilizadorEscolhido.SetDataNascimento(Servico.LerData($"Digite o novo valor para o campo {tipoCampo} (dd-MM-yyyy): "));
                    break;
                case TipoCampo.Morada:
                    // Solicita ao usuário o novo valor para o campo Morada
                    utilizadorEscolhido.SetMorada(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.CodigoPostal:
                    // Solicita ao usuário o novo valor para o campo CodigoPostal
                    utilizadorEscolhido.SetCodigoPostal(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.Localidade:
                    // Solicita ao usuário o novo valor para o campo Localidade
                    utilizadorEscolhido.SetLocalidade(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.ContactoTelefone:
                    // Solicita ao usuário o novo valor para o campo ContactoTelefone
                    utilizadorEscolhido.SetContactoTelefone(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.DocIdentificacao:
                    // Solicita ao usuário o novo valor para o campo DocIdentificacao
                    utilizadorEscolhido.SetDocIdentificacao(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.TipoDocIdentificacao:
                    // Solicita ao usuário o novo valor para o campo TipoDocIdentificacao
                    utilizadorEscolhido.SetTipoDocIdentificacao(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.IBAN:
                    // Solicita ao usuário o novo valor para o campo IBAN
                    utilizadorEscolhido.SetIBAN(Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                case TipoCampo.TipoUtilizador:
                    string tipoUtilizador;
                    do
                    {
                        // Solicita ao usuário o novo valor para o campo TipoUtilizador
                        tipoUtilizador = Servico.LerString($"Digite o novo valor para o campo {tipoCampo}: (Morador, Funcionario, Gestor): ").ToLower(); // Converte para minúsculas para facilitar a comparação

                        if (!ValidarTipoUtilizador(tipoUtilizador))
                        {
                            Console.WriteLine("Tipo de utilizador inválido. Por favor, escolha entre Morador, Funcionario, ou Gestor.");
                        }
                    } while (!ValidarTipoUtilizador(tipoUtilizador));

                    utilizadorEscolhido.SetTipoUtilizador(tipoUtilizador);
                    break;
                case TipoCampo.IsAtivo:
                    // Solicita ao usuário o novo valor para o campo IsAtivo
                    utilizadorEscolhido.SetIsAtivo(Servico.LerBooleano($"Digite o novo valor para o campo {tipoCampo}: "));
                    break;
                default:
                    Console.WriteLine("Campo não suportado. A atualização não foi realizada.");
                    break;
            }

            return utilizadorEscolhido;
        }


        /// <summary>
        /// Método para editar um utilizador
        /// </summary>
        /// <param name="utilizador"></param>
        public void EditarUtilizador(Utilizador utilizador)
        {
            // Obtem a lista atual de utilizadores
            List<Utilizador> listaExistente = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            // Cria uma cópia da lista existente sem o utilizador escolhido para fins de validação
            List<Utilizador> listaValidacao = listaExistente.Where(u => u.GetUtiId() != utilizador.GetUtiId()).ToList();

            // Selecionar campo para edição
            TipoCampo tipoCampo = MenuSelecionarCampo();

            // Obtém o novo valor e atualiza o utilizador escolhido
            Utilizador utilizadorAtualizado = ObterNovoValorUtilizador(utilizador, tipoCampo);

            // Valida utilizador
            if (ValidarUtilizador(utilizadorAtualizado, listaValidacao))
            {
                // Se o utilizador for válido, atualiza os campos na lista existente
                Utilizador utilizadorExistente = listaExistente.FirstOrDefault(u => u.GetUtiId() == utilizador.GetUtiId());
                if (utilizadorExistente != null)
                {
                    // Atualiza os campos no utilizador existente com os valores do utilizador atualizado
                    AtualizarCamposUtilizador(utilizadorExistente, utilizadorAtualizado);
                }

                // Salva as alterações no arquivo
                Utilizador.SalvarListaFicheiro("utilizador.json", listaExistente);
                Console.WriteLine("Atualização realizada com sucesso.");
            }
            else
            {
                Console.WriteLine("O utilizador não é válido. A atualização não foi realizada.");
            }
        }

        /// <summary>
        /// Método para atualizar os campos de um utilizador
        /// </summary>
        /// <param name="existente"></param>
        /// <param name="atualizado"></param>
        public void AtualizarCamposUtilizador(Utilizador existente, Utilizador atualizado)
        {
            existente.SetNomeUti(atualizado.GetNomeUti());
            existente.SetEmail(atualizado.GetEmail());
            existente.SetPassword(atualizado.GetPassword());
            existente.SetDataNascimento(atualizado.GetDataNascimento());
            existente.SetMorada(atualizado.GetMorada());
            existente.SetCodigoPostal(atualizado.GetCodigoPostal());
            existente.SetLocalidade(atualizado.GetLocalidade());
            existente.SetContactoTelefone(atualizado.GetContactoTelefone());
            existente.SetDocIdentificacao(atualizado.GetDocIdentificacao());
            existente.SetTipoDocIdentificacao(atualizado.GetTipoDocIdentificacao());
            existente.SetIBAN(atualizado.GetIBAN());
            existente.SetTipoUtilizador(atualizado.GetTipoUtilizador());
            existente.SetIsAtivo(atualizado.GetIsAtivo());
        }

        #endregion

        #region Excluir Utilizador
        /// <summary>
        /// Menu para excluir um utilizador
        /// </summary>
        public void MenuExcluirUtilizador()
        {
            // Obtém um predicado para buscar os utilizadores com base nos critérios escolhidos pelo usuário
            Predicate<Utilizador> predicado = MenuBuscarUtilizador();

            // Busca os utilizadores com base no predicado
            List<Utilizador> utilizadoresEncontrados = BuscarUtilizador(predicado);

            if (utilizadoresEncontrados != null && utilizadoresEncontrados.Count > 0)
            {
                // Solicita ao usuário que escolha o ID do utilizador a ser excluído
                int idEscolhido = Servico.LerInteiro("Escolha o ID do utilizador que deseja excluir: ");

                // Busca o utilizador escolhido
                Utilizador utilizadorEscolhido = utilizadoresEncontrados.FirstOrDefault(u => u.GetUtiId() == idEscolhido);


                if (utilizadorEscolhido != null)
                {
                    // Exibe detalhes do utilizador escolhido
                    ExibirDetalhesUtilizador(utilizadorEscolhido);

                    // Chama o controlador para excluir o utilizador
                    ExcluirUtilizador(utilizadorEscolhido);
                }
                else
                {
                    // Se o utilizador escolhido não foi encontrado, exibe uma mensagem indicando que a exclusão não foi realizada
                    Console.WriteLine($"Utilizador com ID {idEscolhido} não encontrado. A exclusão não foi realizada.");
                }
            }
            else
            {
                // Se nenhum utilizador foi encontrado com base nos critérios escolhidos, exibe uma mensagem indicando que a exclusão não foi realizada
                Console.WriteLine("Utilizador não encontrado. A exclusão não foi realizada.");
            }
        }

        /// <summary>
        /// Método para excluir utilizador da lista
        /// </summary>
        /// <param name="utilizador"></param>
        public void ExcluirUtilizador(Utilizador utilizador)
        {
            // Obtem a lista atual de utilizadores
            List<Utilizador> listaExistente = Utilizador.CarregarListaDeUtilizadores("utilizador.json");

            // Verifica se o utilizador existe na lista
            if (!listaExistente.Any(u => u.GetUtiId() == utilizador.GetUtiId()))
            {
                Console.WriteLine($"Utilizador com ID {utilizador.GetUtiId()} não encontrado. A exclusão não foi realizada.");
                return;
            }
            else
            {
                // Se o utilizador for válido, exclui o utilizador da lista existente
                listaExistente.RemoveAll(u => u.GetUtiId() == utilizador.GetUtiId());

                // Salva as alterações no ficheiro
                Utilizador.SalvarListaFicheiro("utilizador.json", listaExistente);
                Console.WriteLine("Exclusão realizada com sucesso.");
            }
        }

        #endregion


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
        public void ExibirDetalhesUtilizador(Utilizador utilizador)
        {
            Console.WriteLine($"ID: {utilizador.GetUtiId()}, Nome: {utilizador.GetNomeUti()}, Email: {utilizador.GetEmail()}, Data de Nascimento: {utilizador.GetDataNascimento():dd-MM-yyyy}, Morada: {utilizador.GetMorada()}, Código Postal: {utilizador.GetCodigoPostal()}, Localidade: {utilizador.GetLocalidade()}, Contacto Telefone: {utilizador.GetContactoTelefone()}, Documento de Identificação: {utilizador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {utilizador.GetTipoDocIdentificacao()}, IBAN: {utilizador.GetIBAN()}, Tipo de Utilizador: {utilizador.GetTipoUtilizador()}, Ativo: {utilizador.GetIsAtivo()} Data de Registo: {utilizador.GetDataRegisto():dd-MM-yyyy}");
        }
        #endregion

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
        /// <summary>
        /// Método de validação para evitar duplicidade de utilizadores
        /// </summary>
        /// <param name="novoUtilizador"></param>
        /// <param name="listaExistente"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método de validação para o tipo de utilizador
        /// </summary>
        /// <param name="tipoUtilizador"></param>
        /// <returns></returns>
        public bool ValidarTipoUtilizador(string tipoUtilizador)
        {
            return tipoUtilizador.Equals("morador", StringComparison.OrdinalIgnoreCase) ||
                   tipoUtilizador.Equals("funcionario", StringComparison.OrdinalIgnoreCase) ||
                   tipoUtilizador.Equals("gestor", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Define os campos que podem ser editados
        /// </summary>
        public enum TipoCampo
        {
            UtiId,
            NomeUti,
            Email,
            Password,
            DataNascimento,
            Morada,
            CodigoPostal,
            Localidade,
            ContactoTelefone,
            DocIdentificacao,
            TipoDocIdentificacao,
            IBAN,
            TipoUtilizador,
            IsAtivo,
            DataRegisto,
            Nulo
        }
        #endregion

        #endregion

    }
}
