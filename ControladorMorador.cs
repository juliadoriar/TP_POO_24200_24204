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
            AdicionarMorador(morador); // Adicionar morador à lista de moradores
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
