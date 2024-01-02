using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ControladorGestor
    {
        #region CRUD Gestor
        #region Criar Gestor
        /// <summary>
        /// Método para criar um novo gestor
        /// </summary>
        /// <param name="utilizador"></param>
        /// <returns></returns>
        public Gestor CriarGestor(Utilizador utilizador)
        {
            Gestor gestor = new Gestor(
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

            return gestor;
        }

        /// <summary>
        /// Método para adicionar um novo gestor à lista
        /// </summary>
        /// <param name="novoGestor"></param>
        /// <returns></returns>
        public bool AdicionarGestor(Gestor novoGestor)
        {
            List<Gestor> listaExistente = Gestor.CarregarListaDeGestores("gestor.json");
            
            // Verificar se o gestor já existe na lista através do documento de identificação
            if (!ValidarGestor(novoGestor, listaExistente))
            {
                return false;
            }
            else
            {
                listaExistente.Add(novoGestor); // Adicionar gestor à lista de gestores
                Gestor.SalvarListaFicheiro("gestor.json", listaExistente); // Salvar lista de gestores no ficheiro
                return true;
            }

        }
        #endregion

        #region Buscar Gestor
        /// <summary>
        /// Menu para buscar um gestor
        /// </summary>
        /// <returns></returns>
        public Predicate<Gestor> MenuBuscarGestor()
        {
            // Inicializa o predicado como nulo
            Predicate<Gestor> predicado = null;
            // Obtém a lista de gestores atual do controlador
            List<Gestor> listaDeGestoresAtual = Gestor.CarregarListaDeGestores("gestor.json");

            // Loop para escolher o critério de busca
            while (true)
            {
                Console.WriteLine("Escolha o critério de busca para gestor:");
                Console.WriteLine("1. Por ID do Gestor");
                Console.WriteLine("2. Por Nome do Gestor");
                Console.WriteLine("3. Por Email do Gestor");
                Console.WriteLine("4. Por Data de Nascimento do Gestor");
                Console.WriteLine("5. Por Localidade do Gestor");
                Console.WriteLine("6. Por Documento de Identificação do Gestor");
                Console.WriteLine("7. Por Atividade do Gestor");
                Console.WriteLine("8. Por Data de Registo do Gestor");

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
                        valorInteiro = Servico.LerInteiro("Digite o ID do Gestor:");
                        predicado = g => g.GetUtiId() == valorInteiro;
                        break;
                    case 2:
                        valorString = Servico.LerString("Digite o Nome do Gestor:");
                        predicado = g => g.GetNomeUti() == valorString;
                        break;
                    case 3:
                        valorString = Servico.LerString("Digite o Email do Gestor:");
                        predicado = g => g.GetEmail() == valorString;
                        break;
                    case 4:
                        valorData = Servico.LerData("Digite a Data de Nascimento do Gestor (dd-MM-yyyy):");
                        predicado = g => g.GetDataNascimento() == valorData;
                        break;
                    case 5:
                        valorString = Servico.LerString("Digite a Localidade do Gestor:");
                        predicado = g => g.GetLocalidade() == valorString;
                        break;
                    case 6:
                        valorString = Servico.LerString("Digite o Documento de Identificação do Gestor:");
                        predicado = g => g.GetDocIdentificacao() == valorString;
                        break;
                    case 7:
                        valorBool = Servico.LerBooleano("Digite o critério de atividade (true/false):");
                        predicado = g => g.GetIsAtivo() == valorBool;
                        break;
                    case 8:
                        valorData = Servico.LerData("Digite a Data de Registo do Gestor (dd-MM-yyyy):");
                        predicado = g => g.GetDataRegisto() == valorData;
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
                List<Gestor> listaGestoresSelecionados = BuscarGestor(predicado);
                ExibirDetalhesListaGestor(listaGestoresSelecionados);
            }

            return predicado;
        }

        /// <summary>
        /// Método para buscar gestores
        /// </summary>
        /// <param name="predicado"></param>
        /// <returns></returns>
        public List<Gestor> BuscarGestor(Predicate<Gestor> predicado)
        {
            List<Gestor> listaExistente = Gestor.CarregarListaDeGestores("gestor.json");

            if (predicado == null)
            {
                // Se o predicado for nulo, retorna null
                return null;
            }
            else
            {
                // Se o predicado não for nulo, retorna a lista de gestores que satisfazem o predicado
                List<Gestor> GestoresEncontrados = listaExistente.FindAll(predicado);
                return GestoresEncontrados;
            }
        }
        #endregion

        #region Editar Gestor
        /// <summary>
        /// Método para editar um gestor na lista
        /// </summary>
        /// <param name="gestor"></param>
        public void EditarGestor(Gestor gestor)
        {
            // Carregar a lista existente do arquivo
            List<Gestor> listaGestoresExistente = Gestor.CarregarListaDeGestores("gestor.json");

            // Se o gestor for válido, atualiza os campos na lista existente
            Gestor gestorExistente = listaGestoresExistente.FirstOrDefault(g => g.GetUtiId() == gestor.GetUtiId());

            if (gestorExistente != null)
            {
                // Atualiza os campos no gestor existente com os valores do gestor atualizado
                AtualizarCamposGestor(gestorExistente, gestor);
            }

            // Salva as alterações no arquivo
            Gestor.SalvarListaFicheiro("gestor.json", listaGestoresExistente);

            Console.WriteLine("Atualização do gestor realizada com sucesso.");
        }

        /// <summary>
        /// Método para atualizar os campos de um gestor
        /// </summary>
        /// <param name="existente"></param>
        /// <param name="atualizado"></param>
        public void AtualizarCamposGestor(Gestor existente, Gestor atualizado)
        {
            // Atualiza os campos específicos do gestor
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

        #region Excluir Gestor
        /// <summary>
        /// Método para excluir um gestor da lista
        /// </summary>
        /// <param name="gestor"></param>
        public void ExcluirGestor(Gestor gestor)
        {
            // Obtem a lista atual de gestores
            List<Gestor> listaExistente = Gestor.CarregarListaDeGestores("gestor.json");

            // Verifica se o gestor existe na lista
            if (!listaExistente.Any(g => g.GetUtiId() == gestor.GetUtiId()))
            {
                Console.WriteLine($"Gestor com ID {gestor.GetUtiId()} não encontrado. A exclusão não foi realizada.");
                return;
            }
            else
            {
                // Se o gestor for válido, exclui o gestor da lista existente
                listaExistente.RemoveAll(g => g.GetUtiId() == gestor.GetUtiId());

                // Salva as alterações no ficheiro
                Gestor.SalvarListaFicheiro("gestor.json", listaExistente);

                Console.WriteLine("Exclusão de gestor realizada com sucesso.");
            }
        }
        #endregion

        #region Imprimir Gestor
        /// <summary>
        /// Método para imprimir a lista de gestores
        /// </summary>
        public void ImprimirListaDeGestores()
        {
            List<Gestor> listaDeGestoresAtual = Gestor.CarregarListaDeGestores("gestor.json"); // Carregar lista de Gestores do ficheiro

            ExibirDetalhesListaGestor(listaDeGestoresAtual);

        }

        /// <summary>
        /// Método para imprimir os detalhes de uma lista de gestores
        /// </summary>
        /// <param name="listaDeGestoresAtual"></param>
        public void ExibirDetalhesListaGestor(List<Gestor> listaDeGestoresAtual)
        {
            if (listaDeGestoresAtual == null)
            {
                Console.WriteLine("Não há Gestores registados");
            }
            else
            {
                Console.WriteLine("Lista de Gestores");
                Console.WriteLine("--------------------------------------------");
                foreach (Gestor gestor in listaDeGestoresAtual)
                {
                    Console.WriteLine($"ID: {gestor.GetUtiId()}, Nome: {gestor.GetNomeUti()}, Email: {gestor.GetEmail()}, Data de Nascimento: {gestor.GetDataNascimento():dd-MM-yyyy}, Morada: {gestor.GetMorada()}, Código Postal: {gestor.GetCodigoPostal()}, Localidade: {gestor.GetLocalidade()}, Contacto Telefone: {gestor.GetContactoTelefone()}, Documento de Identificação: {gestor.GetDocIdentificacao()}, Tipo de Documento de Identificação: {gestor.GetTipoDocIdentificacao()}, IBAN: {gestor.GetIBAN()}, Tipo de Utilizador: {gestor.GetTipoUtilizador()}, Ativo: {gestor.GetIsAtivo()} Data de Registo: {gestor.GetDataRegisto():dd-MM-yyyy}");
                }
            }
        }

        /// <summary>
        /// Método para exibir os detalhes de um único gestor
        /// </summary>
        /// <param name="utilizador"></param>
        public void ExibirDetalhesGestor(Gestor gestor)
        {
            Console.WriteLine($"ID: {gestor.GetUtiId()}, Nome: {gestor.GetNomeUti()}, Email: {gestor.GetEmail()}, Data de Nascimento: {gestor.GetDataNascimento():dd-MM-yyyy}, Morada: {gestor.GetMorada()}, Código Postal: {gestor.GetCodigoPostal()}, Localidade: {gestor.GetLocalidade()}, Contacto Telefone: {gestor.GetContactoTelefone()}, Documento de Identificação: {gestor.GetDocIdentificacao()}, Tipo de Documento de Identificação: {gestor.GetTipoDocIdentificacao()}, IBAN: {gestor.GetIBAN()}, Tipo de Utilizador: {gestor.GetTipoUtilizador()}, Ativo: {gestor.GetIsAtivo()} Data de Registo: {gestor.GetDataRegisto():dd-MM-yyyy}");
        }
        #endregion
        #endregion

        #region Métodos Auxiliares
        /// <summary>
        /// Método para validar se um gestor já existe na lista
        /// </summary>
        /// <param name="novoGestor"></param>
        /// <param name="listaExistente"></param>
        /// <returns></returns>
        public bool ValidarGestor(Gestor novoGestor, List<Gestor> listaExistente)
        {

            if (listaExistente.Exists(gestor => //Função lambda
                   novoGestor.GetDocIdentificacao() == gestor.GetDocIdentificacao() &&
                   novoGestor.GetTipoDocIdentificacao() == gestor.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O gestor já existe na lista.");
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



