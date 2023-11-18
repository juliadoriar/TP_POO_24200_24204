using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// Classe que atua como controlador para operações relacionadas aos quartos
    /// </summary>
    public class ControladorQuarto
    {
        #region Lista de Quartos
        /// <summary>
        /// Método estático para criar um objeto quarto
        /// </summary>
        /// <returns></returns>
        public static List<Quarto> CriarListaDeQuartos()
        {

            // Itera pelos andares (0 a 2) e pelos números dos quartos (1 a 19)
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 1; j < 20; j++)
                {
                    // Obtém informações aleatórias para criar um quarto
                    string tipoQuarto = ObterTipoQuartoAleatorio();
                    int capacidade = ObterCapacidadePorTipoQuarto(tipoQuarto);
                    float precoBase = ObterPrecoBasePorTipoQuarto(tipoQuarto);
                    float precoRenda = CalcularPrecoRenda(i, precoBase);
                    bool disponibilidadeAleatoria = ObterDisponibilidadeAleatoria();

                    // Cria um objeto Quarto com as informações obtidas
                    Quarto quarto = new Quarto(
                        j + i * 100, // ID único do quarto
                        tipoQuarto,
                        i, // Andar
                        capacidade,
                        precoRenda,
                        disponibilidadeAleatoria
                    );

                    Quarto.listaDeQuartos.Add(quarto); // Adiciona o quarto à lista de quartos
                }
            }

            return Quarto.listaDeQuartos; // Retorna a lista de quartos criada
        }

        /// <summary>
        /// Método para imprimir quarto à lista de quartos
        /// </summary>
        /// <param name="listaDeQuartos"></param>
        public static void ImprimirListaDeQuartos(List<Quarto> listaDeQuartos)
        {
            Console.WriteLine("Lista de Quartos");
            Console.WriteLine("--------------------------------------------");
            foreach (Quarto quarto in listaDeQuartos)
            {
                Console.WriteLine($"ID: {quarto.GetQuartoId().ToString("D3")}, Tipo: {quarto.GetTipoQuarto()}, Andar: {quarto.GetAndar()}, Capacidade: {quarto.GetCapacidade()}, Preço Renda: {quarto.GetPrecoRenda().ToString("F2")}, Disponibilidade: {quarto.GetDisponibilidade()}");
            }
        }
        #region Métodos auxiliares de Lista de Quartos
        /// <summary>
        /// Método para obter um tipo de quarto aleatório
        /// </summary>
        /// <returns></returns>
        private static string ObterTipoQuartoAleatorio()
        {
            string[] tiposQuarto = { "Individual", "Duplo", "Studio" }; // Array com os tipos de quarto
            Random random = new Random(); // Objeto Random para gerar números aleatórios
            int indiceAleatorio = random.Next(tiposQuarto.Length); // Gera um número aleatório entre 0 e o tamanho do array
            return tiposQuarto[indiceAleatorio]; // Retorna o tipo de quarto correspondente ao índice gerado
        }

        /// <summary>
        /// Método para obter a capacidade de um quarto por tipo de quarto
        /// </summary>
        /// <param name="tipoQuarto"></param>
        /// <returns></returns>
        private static int ObterCapacidadePorTipoQuarto(string tipoQuarto)
        {
            switch (tipoQuarto)
            {
                case "Individual":
                case "Studio":
                    return 1;
                case "Duplo":
                    return 2;
                default:
                    return 0; // Caso de fallback, caso seja um tipo de quarto desconhecido
            }
        }

        /// <summary>
        /// Método para obter o preço base de um quarto por tipo de quarto
        /// </summary>
        /// <param name="tipoQuarto"></param>
        /// <returns></returns>
        private static float ObterPrecoBasePorTipoQuarto(string tipoQuarto)
        {
            switch (tipoQuarto)
            {
                case "Individual":
                    return 220;
                case "Duplo":
                    return 400;
                case "Studio":
                    return 350;
                default:
                    return 0; // Caso de fallback, caso seja um tipo de quarto desconhecido
            }
        }

        /// <summary>
        /// Método para calcular o preço de renda de um quarto por andar
        /// </summary>
        /// <param name="andar"></param>
        /// <param name="precoBase"></param>
        /// <returns></returns>
        private static float CalcularPrecoRenda(int andar, float precoBase)
        {
            return precoBase * (1.0f + 0.05f * andar); // Retorna o preço de renda com base no preço base e no andar, com um acréscimo de 5% por andar
        }

        /// <summary>
        /// Método para obter a disponibilidade de um quarto aleatoriamente
        /// </summary>
        /// <returns></returns>
        private static bool ObterDisponibilidadeAleatoria()
        {
            Random random = new Random();
            return random.NextDouble() < 0.5; // Retorna true ou false aleatoriamente
        }
        #endregion
        #endregion
    }
}
