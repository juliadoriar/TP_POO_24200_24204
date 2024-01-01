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
        public static Gestor CriarGestor(Utilizador utilizador)
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

        public static void AdicionarGestor(Gestor novoGestor)
        {
            if (Gestor.listaDeGestores.Exists(Gestor => //Função lambda
                    novoGestor.GetDocIdentificacao() == Gestor.GetDocIdentificacao() &&
                    novoGestor.GetTipoDocIdentificacao() == Gestor.GetTipoDocIdentificacao()))
            {
                Console.WriteLine("O gestor já existe na lista.");
            }
            else
            {
                Gestor.listaDeGestores.Add(novoGestor); 
                SalvarListaFicheiro("gestor.json", Gestor.listaDeGestores);
            }
        }

    #region Json
    /// <summary>
    /// Método para salvar a lista de moradores no ficheiro JSON
    /// </summary>
    /// <param name="caminhoArquivo"></param>
    /// <param name="listaGestores"></param>
    public static void SalvarListaFicheiro(string caminhoArquivo, List<Gestor> listaGestores)
    {
        string json = JsonConvert.SerializeObject(listaGestores, Newtonsoft.Json.Formatting.Indented); // Serializar lista de gestores
        File.WriteAllText(caminhoArquivo, json); // Escrever no ficheiro
    }

    /// <summary>
    /// Método para carregar a lista de moradores do ficheiro JSON
    /// </summary>
    /// <param name="caminhoArquivo"></param>
    /// <returns></returns>
    public static List<Gestor> CarregarListaDeGestores(string caminhoArquivo)
    {
        if (File.Exists(caminhoArquivo)) // Verificar se o ficheiro existe
        {
            string json = File.ReadAllText(caminhoArquivo); // Ler o ficheiro

            if (!string.IsNullOrEmpty(json)) // Verificar se o JSON não está vazio
            {
                return JsonConvert.DeserializeObject<List<Gestor>>(json);
            }
        }

        return new List<Gestor>(); // Se o ficheiro não existir ou estiver vazio, retorna uma lista vazia
    }
        #endregion
    }

}



