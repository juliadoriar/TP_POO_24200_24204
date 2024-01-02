using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ControladorFuncionario
    {
        #region CRUD Funcionário
        #region Criar Funcionário
        /// <summary>
        /// Método para criar um novo funcionário
        /// </summary>
        /// <param name="utilizador"></param>
        /// <returns></returns>
        public Funcionario CriarFuncionario(Utilizador utilizador)
        {
            Funcionario funcionario = new Funcionario(
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

            return funcionario;
        }

        /// <summary>
        /// Método para adicionar um novo funcionário à lista
        /// </summary>
        /// <param name="novoFuncionario"></param>
        /// <returns></returns>
        public bool AdicionarFuncionario(Funcionario novoFuncionario)
        {
            List<Funcionario> listaExistente = Funcionario.CarregarListaDeFuncionarios("funcionario.json");

            // Verificar se o funcionário já existe na lista através do documento de identificação
            if (!ValidarFuncionario(novoFuncionario, listaExistente))
            {
                return false;
            }
            else
            {
                listaExistente.Add(novoFuncionario); // Adicionar funcionário à lista de funcionários
                Funcionario.SalvarListaFicheiro("funcionario.json", listaExistente); // Salvar lista de funcionários no ficheiro
                return true;
            }
        }
        #endregion

        #region Buscar Funcionário
        /// <summary>
        /// Menu para buscar um funcionário
        /// </summary>
        /// <returns></returns>
        public Predicate<Funcionario> MenuBuscarFuncionario()
        {
            // Inicializa o predicado como nulo
            Predicate<Funcionario> predicado = null;

            // Obtém a lista de funcionários atual do controlador
            List<Funcionario> listaDeFuncionariosAtual = Funcionario.CarregarListaDeFuncionarios("funcionario.json");

            // Loop para escolher o critério de busca
            while (true)
            {
                Console.WriteLine("Escolha o critério de busca para funcionário:");
                Console.WriteLine("1. Por ID do Funcionário");
                Console.WriteLine("2. Por Nome do Funcionário");
                Console.WriteLine("3. Por Email do Funcionário");
                Console.WriteLine("4. Por Data de Nascimento do Funcionário");
                Console.WriteLine("5. Por Localidade do Funcionário");
                Console.WriteLine("6. Por Documento de Identificação do Funcionário");
                Console.WriteLine("7. Por Atividade do Funcionário");
                Console.WriteLine("8. Por Data de Registo do Funcionário");

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
                        valorInteiro = Servico.LerInteiro("Digite o ID do Funcionário:");
                        predicado = f => f.GetUtiId() == valorInteiro;
                        break;
                    case 2:
                        valorString = Servico.LerString("Digite o Nome do Funcionário:");
                        predicado = f => f.GetNomeUti() == valorString;
                        break;
                    case 3:
                        valorString = Servico.LerString("Digite o Email do Funcionário:");
                        predicado = f => f.GetEmail() == valorString;
                        break;
                    case 4:
                        valorData = Servico.LerData("Digite a Data de Nascimento do Funcionário (dd-MM-yyyy):");
                        predicado = f => f.GetDataNascimento() == valorData;
                        break;
                    case 5:
                        valorString = Servico.LerString("Digite a Localidade do Funcionário:");
                        predicado = f => f.GetLocalidade() == valorString;
                        break;
                    case 6:
                        valorString = Servico.LerString("Digite o Documento de Identificação do Funcionário:");
                        predicado = f => f.GetDocIdentificacao() == valorString;
                        break;
                    case 7:
                        valorBool = Servico.LerBooleano("Digite o critério de atividade (true/false):");
                        predicado = f => f.GetIsAtivo() == valorBool;
                        break;
                    case 8:
                        valorData = Servico.LerData("Digite a Data de Registo do Funcionário (dd-MM-yyyy):");
                        predicado = f => f.GetDataRegisto() == valorData;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                // Se uma opção válida foi escolhida, sai do loop
                if (opcao >= 1 && opcao <= 8)
                {
                    break;
                }
            }

            // Se um predicado foi definido, realiza a busca e exibe os resultados
            if (predicado != null)
            {
                List<Funcionario> listaFuncionariosSelecionados = BuscarFuncionario(predicado);
                ExibirDetalhesListaFuncionario(listaFuncionariosSelecionados);
            }

            return predicado;
        }

        /// <summary>
        /// Método para buscar funcionários
        /// </summary>
        /// <param name="predicado"></param>
        /// <returns></returns>
        public List<Funcionario> BuscarFuncionario(Predicate<Funcionario> predicado)
        {
            List<Funcionario> listaExistente = Funcionario.CarregarListaDeFuncionarios("funcionario.json");

            if (predicado == null)
            {
                // Se o predicado for nulo, retorna null
                return null;
            }
            else
            {
                // Se o predicado não for nulo, retorna a lista de funcionários que satisfazem o predicado
                List<Funcionario> FuncionariosEncontrados = listaExistente.FindAll(predicado);
                return FuncionariosEncontrados;
            }
        }
        #endregion

        #region Editar Funcionário
        /// <summary>
        /// Método para editar um funcionário na lista
        /// </summary>
        /// <param name="funcionario"></param>
        public void EditarFuncionario(Funcionario funcionario)
        {
            // Carregar a lista existente do arquivo
            List<Funcionario> listaFuncionariosExistente = Funcionario.CarregarListaDeFuncionarios("funcionario.json");

            // Se o funcionário for válido, atualiza os campos na lista existente
            Funcionario funcionarioExistente = listaFuncionariosExistente.FirstOrDefault(f => f.GetUtiId() == funcionario.GetUtiId());

            if (funcionarioExistente != null)
            {
                // Atualiza os campos no funcionário existente com os valores do funcionário atualizado
                AtualizarCamposFuncionario(funcionarioExistente, funcionario);
            }

            // Salva as alterações no arquivo
            Funcionario.SalvarListaFicheiro("funcionario.json", listaFuncionariosExistente);

            Console.WriteLine("Atualização do funcionário realizada com sucesso.");
        }

        /// <summary>
        /// Método para atualizar os campos de um funcionário
        /// </summary>
        /// <param name="existente"></param>
        /// <param name="atualizado"></param>
        public void AtualizarCamposFuncionario(Funcionario existente, Funcionario atualizado)
        {
            // Atualiza os campos específicos do funcionário
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

        #region Excluir Funcionario
        /// <summary>
        /// Método para excluir um funcionário da lista
        /// </summary>
        /// <param name="funcionario"></param>
        public void ExcluirFuncionario(Funcionario funcionario)
        {
            // Obtem a lista atual de funcionários
            List<Funcionario> listaExistente = Funcionario.CarregarListaDeFuncionarios("funcionario.json");

            // Verifica se o funcionário existe na lista
            if (!listaExistente.Any(f => f.GetUtiId() == funcionario.GetUtiId()))
            {
                Console.WriteLine($"Funcionário com ID {funcionario.GetUtiId()} não encontrado. A exclusão não foi realizada.");
                return;
            }
            else
            {
                // Se o funcionário for válido, exclui o funcionário da lista existente
                listaExistente.RemoveAll(f => f.GetUtiId() == funcionario.GetUtiId());

                // Salva as alterações no ficheiro
                Funcionario.SalvarListaFicheiro("funcionario.json", listaExistente);

                Console.WriteLine("Exclusão de funcionário realizada com sucesso.");
            }
        }
        #endregion

        #region Imprimir Funcionário
        /// <summary>
        /// Método para imprimir a lista de funcionários
        /// </summary>
        public void ImprimirListaDeFuncionarios()
        {
            List<Funcionario> listaDeFuncionariosAtual = Funcionario.CarregarListaDeFuncionarios("funcionario.json"); // Carregar lista de Funcionários do ficheiro

            ExibirDetalhesListaFuncionario(listaDeFuncionariosAtual);
        }

        public void ExibirDetalhesListaFuncionario(List<Funcionario> listaDeFuncionariosAtual)
        {
            if (listaDeFuncionariosAtual == null)
            {
                Console.WriteLine("Não há Funcionários registados");
            }
            else
            {
                Console.WriteLine("Lista de Funcionários");
                Console.WriteLine("--------------------------------------------");
                foreach (Funcionario funcionario in listaDeFuncionariosAtual)
                {
                    Console.WriteLine($"ID: {funcionario.GetUtiId()}, Nome: {funcionario.GetNomeUti()}, Email: {funcionario.GetEmail()}, Data de Nascimento: {funcionario.GetDataNascimento():dd-MM-yyyy}, Morada: {funcionario.GetMorada()}, Código Postal: {funcionario.GetCodigoPostal()}, Localidade: {funcionario.GetLocalidade()}, Contacto Telefone: {funcionario.GetContactoTelefone()}, Documento de Identificação: {funcionario.GetDocIdentificacao()}, Tipo de Documento de Identificação: {funcionario.GetTipoDocIdentificacao()}, IBAN: {funcionario.GetIBAN()}, Tipo de Utilizador: {funcionario.GetTipoUtilizador()}, Ativo: {funcionario.GetIsAtivo()} Data de Registo: {funcionario.GetDataRegisto():dd-MM-yyyy}");
                }
            }
        }

        /// <summary>
        /// Método para exibir os detalhes de um único funcionário
        /// </summary>
        /// <param name="utilizador"></param>
        public void ExibirDetalhesFuncionario(Funcionario funcionario)
        {
            Console.WriteLine($"ID: {funcionario.GetUtiId()}, Nome: {funcionario.GetNomeUti()}, Email: {funcionario.GetEmail()}, Data de Nascimento: {funcionario.GetDataNascimento():dd-MM-yyyy}, Morada: {funcionario.GetMorada()}, Código Postal: {funcionario.GetCodigoPostal()}, Localidade: {funcionario.GetLocalidade()}, Contacto Telefone: {funcionario.GetContactoTelefone()}, Documento de Identificação: {funcionario.GetDocIdentificacao()}, Tipo de Documento de Identificação: {funcionario.GetTipoDocIdentificacao()}, IBAN: {funcionario.GetIBAN()}, Tipo de Utilizador: {funcionario.GetTipoUtilizador()}, Ativo: {funcionario.GetIsAtivo()} Data de Registo: {funcionario.GetDataRegisto():dd-MM-yyyy}");
        }
        #endregion
        #endregion

        #region Métodos Auxiliares
        /// <summary>
        /// Método para validar se o funcionário já existe na lista
        /// </summary>
        /// <param name="novoFuncionario"></param>
        /// <param name="listaExistente"></param>
        /// <returns></returns>
        public bool ValidarFuncionario(Funcionario novoFuncionario, List<Funcionario> listaExistente)
        {
            if (listaExistente.Exists(funcionario =>
                   novoFuncionario.GetDocIdentificacao() == funcionario.GetDocIdentificacao() &&
                   novoFuncionario.GetTipoDocIdentificacao() == funcionario.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O funcionário já existe na lista.");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }

}
