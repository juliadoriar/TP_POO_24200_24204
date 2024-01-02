using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que atua como controlador para operações relacionadas aos moradores
    /// </summary>
    public class ControladorMorador
    {
        #region Lista de Moradores

        #region CRUD Morador
        #region Criar Morador
        /// <summary>
        /// Método para criar um objeto morador
        /// </summary>
        /// <param name="utilizador"></param>
        /// <returns></returns>
        public Morador CriarMorador(Utilizador utilizador)
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
            return morador;

        }

        /// <summary>
        ///  Método para adicionar morador à lista de moradores
        /// </summary>
        /// <param name="novoMorador"></param>
        public bool AdicionarMorador(Morador novoMorador)
        {
            // Carregar a lista existente do arquivo
            List<Morador> listaExistente = Morador.CarregarListaDeMoradores("morador.json");

            // Verificar se o morador já existe na lista através do documento de identificação
            if (!ValidarMorador(novoMorador, listaExistente))
            {
                return false;
            }
            else
            {
                listaExistente.Add(novoMorador); // Adicionar morador à lista de moradores
                Morador.SalvarListaFicheiro("morador.json", listaExistente); // Salvar lista de moradores no ficheiro
                return true;
            }

        }
        #endregion

        #region Buscar Morador
        /// <summary>
        /// Menu para buscar um morador
        /// </summary>
        /// <returns></returns>
        public Predicate<Morador> MenuBuscarMorador()
        {
            // Inicializa o predicado como nulo
            Predicate<Morador> predicado = null;
            // Obtém a lista de moradores atual do controlador
            List<Morador> listaDeMoradoresAtual = Morador.CarregarListaDeMoradores("morador.json");

            // Loop para escolher o critério de busca
            while (true)
            {
                Console.WriteLine("Escolha o critério de busca para morador:");
                Console.WriteLine("1. Por ID do Morador");
                Console.WriteLine("2. Por Nome do Morador");
                Console.WriteLine("3. Por Email do Morador");
                Console.WriteLine("4. Por Data de Nascimento do Morador");
                Console.WriteLine("5. Por Localidade do Morador");
                Console.WriteLine("6. Por Documento de Identificação do Morador");
                Console.WriteLine("7. Por Atividade do Morador");
                Console.WriteLine("8. Por Adimplência do Morador");
                Console.WriteLine("9. Por Data de Registo do Morador");

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
                        valorInteiro = Servico.LerInteiro("Digite o ID do Morador:");
                        predicado = m => m.GetUtiId() == valorInteiro;
                        break;
                    case 2:
                        valorString = Servico.LerString("Digite o Nome do Morador:");
                        predicado = m => m.GetNomeUti() == valorString;
                        break;
                    case 3:
                        valorString = Servico.LerString("Digite o Email do Morador:");
                        predicado = m => m.GetEmail() == valorString;
                        break;
                    case 4:
                        valorData = Servico.LerData("Digite a Data de Nascimento do Morador (dd-MM-yyyy):");
                        predicado = m => m.GetDataNascimento() == valorData;
                        break;
                    case 5:
                        valorString = Servico.LerString("Digite a Localidade do Morador:");
                        predicado = m => m.GetLocalidade() == valorString;
                        break;
                    case 6:
                        valorString = Servico.LerString("Digite o Documento de Identificação do Morador:");
                        predicado = m => m.GetDocIdentificacao() == valorString;
                        break;
                    case 7:
                        valorBool = Servico.LerBooleano("Digite o critério de atividade (true/false):");
                        predicado = m => m.GetIsAtivo() == valorBool;
                        break;
                    case 8:
                        valorBool = Servico.LerBooleano("Digite o critério de adimplência (true/false):");
                        predicado = m => m.GetIsAdimplente() == valorBool;
                        break;
                    case 9:
                        valorData = Servico.LerData("Digite a Data de Registo do Morador (dd-MM-yyyy):");
                        predicado = m => m.GetDataRegisto() == valorData;
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
                List<Morador> listaMoradoresSelecionados = BuscarMorador(predicado);
                ExibirDetalhesListaMorador(listaMoradoresSelecionados);
            }

            return predicado;
        }

        /// <summary>
        /// Método para buscar moradores
        /// </summary>
        /// <param name="predicado"></param>
        /// <returns></returns>
        public List<Morador> BuscarMorador(Predicate<Morador> predicado)
        {
            List<Morador> listaExistente = Morador.CarregarListaDeMoradores("morador.json");

            if (predicado == null)
            {
                // Se o predicado for nulo, retorna null
                return null;
            }
            else
            {
                // Se o predicado não for nulo, retorna a lista de moradores que satisfazem o predicado
                List<Morador> MoradoresEncontrados = listaExistente.FindAll(predicado);
                return MoradoresEncontrados;
            }
        }
        #endregion

        #region Editar Morador
        /// <summary>
        /// Método para editar um morador na lista
        /// </summary>
        /// <param name="morador"></param>
        public void EditarMorador(Morador morador)
        {
 
            // Carregar a lista existente do arquivo
            List<Morador> listaMoradoresExistente = Morador.CarregarListaDeMoradores("morador.json");

            // Se o morador for válido, atualiza os campos na lista existente
            Morador moradorExistente = listaMoradoresExistente.FirstOrDefault(m => m.GetUtiId() == morador.GetUtiId());
            
            if (moradorExistente != null)
            {
                // Atualiza os campos no morador existente com os valores do morador atualizado
                AtualizarCamposMorador(moradorExistente, morador);
            }

            // Salva as alterações no arquivo
            Morador.SalvarListaFicheiro("morador.json", listaMoradoresExistente);

            Console.WriteLine("Atualização do morador realizada com sucesso.");
        }

        /// <summary>
        /// Método para atualizar os campos de um morador
        /// </summary>
        /// <param name="existente"></param>
        /// <param name="atualizado"></param>
        public void AtualizarCamposMorador(Morador existente, Morador atualizado)
        {
            // Atualiza os campos específicos do morador, exceto IsAdimplente
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

        /// <summary>
        /// Método para editar a adimplência de um morador
        /// </summary>
        public void EditarAdimplenciaMorador()
        {
            // Solicitar ao usuário o UtiID do morador a ser editado
            int utiId = Servico.LerInteiro("Digite o ID do morador:");

            // Carregar a lista existente do arquivo
            List<Morador> listaMoradoresExistente = Morador.CarregarListaDeMoradores("morador.json");

            // Encontrar o morador na lista pelo UtiID
            Morador moradorExistente = listaMoradoresExistente.FirstOrDefault(m => m.GetUtiId() == utiId);

            if (moradorExistente != null)
            {
                // Atualizar o campo IsAdimplente no morador existente
                AtualizarAdimplenciaMorador(moradorExistente, novoStatusAdimplente: !moradorExistente.GetIsAdimplente()); // Inverte o valor atual

                // Salvar as alterações no arquivo
                Morador.SalvarListaFicheiro("morador.json", listaMoradoresExistente);

                Console.WriteLine("Atualização da adimplência do morador realizada com sucesso.");
            }
            else
            {
                Console.WriteLine($"Morador com UtiID {utiId} não encontrado. A atualização da adimplência não foi realizada.");
            }
        }

        /// <summary>
        /// Método para atualizar a adimplência de um morador
        /// </summary>
        /// <param name="moradorExistente"></param>
        /// <param name="novoStatusAdimplente"></param>
        public void AtualizarAdimplenciaMorador(Morador moradorExistente, bool novoStatusAdimplente)
        {
            // Atualiza o campo IsAdimplente
            moradorExistente.SetIsAdimplente(novoStatusAdimplente);
        }
        #endregion

        #region Excluir Morador
        public void ExcluirMorador(Morador morador)
        {
            // Obtem a lista atual de moradores
            List<Morador> listaExistente = Morador.CarregarListaDeMoradores("morador.json");

            // Verifica se o utilizador existe na lista
            if (!listaExistente.Any(m => m.GetUtiId() == morador.GetUtiId()))
            {
                Console.WriteLine($"Morador com ID {morador.GetUtiId()} não encontrado. A exclusão não foi realizada.");
                return;
            }
            else
            {
                // Se o utilizador for válido, exclui o utilizador da lista existente
                listaExistente.RemoveAll(u => u.GetUtiId() == morador.GetUtiId());

                // Salva as alterações no ficheiro
                Morador.SalvarListaFicheiro("morador.json", listaExistente);

                Console.WriteLine("Exclusão realizada com sucesso.");
            }
        }

        #endregion

        #region Imprimir Morador
        /// <summary>
        /// Método para imprimir a lista de moradores
        /// </summary>
        public void ImprimirListaDeMoradores()
        {
            List<Morador> listaDeMoradoresAtual = Morador.CarregarListaDeMoradores("morador.json"); // Carregar lista de moradores do ficheiro

            ExibirDetalhesListaMorador(listaDeMoradoresAtual);

        }

        public void ExibirDetalhesListaMorador(List<Morador> listaDeMoradoresAtual)
        {
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

        /// <summary>
        /// Método para exibir os detalhes de um único morador
        /// </summary>
        /// <param name="utilizador"></param>
        public void ExibirDetalhesMorador(Morador morador)
        {
            Console.WriteLine($"ID: {morador.GetUtiId()}, Nome: {morador.GetNomeUti()}, Email: {morador.GetEmail()}, Data de Nascimento: {morador.GetDataNascimento():dd-MM-yyyy}, Morada: {morador.GetMorada()}, Código Postal: {morador.GetCodigoPostal()}, Localidade: {morador.GetLocalidade()}, Contacto Telefone: {morador.GetContactoTelefone()}, Documento de Identificação: {morador.GetDocIdentificacao()}, Tipo de Documento de Identificação: {morador.GetTipoDocIdentificacao()}, IBAN: {morador.GetIBAN()}, Tipo de Utilizador: {morador.GetTipoUtilizador()}, Ativo: {morador.GetIsAtivo()} Data de Registo: {morador.GetDataRegisto():dd-MM-yyyy}, Adimplente: {morador.GetIsAdimplente()}");
        }
        #endregion
        
        #endregion

        #region Métodos Auxiliares
        public bool ValidarMorador(Morador novoMorador, List<Morador> listaExistente)
        {

            if (listaExistente.Exists(morador => //Função lambda
                   novoMorador.GetDocIdentificacao() == morador.GetDocIdentificacao() &&
                   novoMorador.GetTipoDocIdentificacao() == morador.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O morador já existe na lista.");
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
