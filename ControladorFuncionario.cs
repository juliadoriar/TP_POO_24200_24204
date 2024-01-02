using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class ControladorFuncionario
    {
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

        #region Métodos Auxiliares
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
