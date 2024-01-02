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

            AdicionarGestor(gestor);
            return gestor;
        }

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

        #region Imprimir Gestor
        /// <summary>
        /// Método para imprimir a lista de gestores
        /// </summary>
        public void ImprimirListaDeGestores()
        {
            List<Gestor> listaDeGestoresAtual = Gestor.CarregarListaDeGestores("gestor.json"); // Carregar lista de Gestores do ficheiro

            ExibirDetalhesListaGestor(listaDeGestoresAtual);

        }

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

        #region Métodos Auxiliares
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



