using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    /// <summary>
    /// 
    /// </summary>
    public class Quarto
    {
        protected int QuartoId;
        protected string TipoQuarto;
        protected int Andar;
        protected int Capacidade;
        protected float PrecoRenda;
        protected bool Disponibilidade;


        #region Construtor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quartoId"></param>
        /// <param name="tipoQuarto"></param>
        /// <param name="andar"></param>
        /// <param name="capacidade"></param>
        /// <param name="precoRenda"></param>
        /// <param name="disponibilidade"></param>
        public Quarto(int quartoId, string tipoQuarto, int andar, int capacidade, float precoRenda, bool disponibilidade)
        {
            QuartoId = quartoId;
            TipoQuarto = tipoQuarto;
            Andar = andar;
            Capacidade = capacidade;
            PrecoRenda = precoRenda;
            Disponibilidade = disponibilidade;
        }
        #endregion
   
        #region Getters e Setters
        public int getQuartoId()
        {
            return QuartoId;
        }   
        private void setQuartoId(int quartoId)
        {
            QuartoId = quartoId;
        }
        public string getTipoQuarto()
        {
            return TipoQuarto;
        }           
        public void setTipoQuarto(string tipoQuarto)
        {
            TipoQuarto = tipoQuarto;
        }
        public int getAndar()
        {
            return Andar;
        }
        public void setAndar(int andar)
        {
            Andar = andar;
        }
        public int getCapacidade()
        {
            return Capacidade;
        }
        public void setCapacidade(int capacidade)
        {
            Capacidade = capacidade;
        }
        public float getPrecoRenda()
        {
            return PrecoRenda;
        }
        public void setPrecoRenda(float precoRenda)
        {
            PrecoRenda = precoRenda;
        }
        public bool getDisponibilidade()
        {
            return Disponibilidade;
        }
        public void setDisponibilidade(bool disponibilidade)
        {
            Disponibilidade = disponibilidade;
        }
        #endregion

        #region Métodos

            #region Lista de Quartos
            public static List<Quarto> CriarListaDeQuartos()
            {
                List<Quarto> listaDeQuartos = new List<Quarto>();

                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 1; j < 20; j++)
                    {
                        string tipoQuarto = ObterTipoQuartoAleatorio();
                        int capacidade = ObterCapacidadePorTipoQuarto(tipoQuarto);
                        float precoBase = ObterPrecoBasePorTipoQuarto(tipoQuarto);
                        float precoRenda = CalcularPrecoRenda(i, precoBase);
                        bool disponibilidadeAleatoria = ObterDisponibilidadeAleatoria();

                        Quarto quarto = new Quarto(
                            j + i * 100,
                            tipoQuarto,
                            i,
                            capacidade,
                            precoRenda,
                            disponibilidadeAleatoria
                        );

                        listaDeQuartos.Add(quarto);
                    }
                }

                return listaDeQuartos;
            }
            public static void ImprimirListaDeQuartos(List<Quarto> listaDeQuartos)
            {
                    foreach (Quarto quarto in listaDeQuartos)
                    {
                        Console.WriteLine($"ID: {quarto.getQuartoId().ToString("D3")}, Tipo: {quarto.getTipoQuarto()}, Andar: {quarto.getAndar()}, Capacidade: {quarto.getCapacidade()}, Preço Renda: {quarto.getPrecoRenda().ToString("F2")}, Disponibilidade: {quarto.getDisponibilidade()}");
                    }
            }
                #region Métodos auxiliares de Lista de Quartos
                private static string ObterTipoQuartoAleatorio()
                {
                    string[] tiposQuarto = { "Individual", "Duplo", "Studio" };
                    Random random = new Random();
                    int indiceAleatorio = random.Next(tiposQuarto.Length);
                    return tiposQuarto[indiceAleatorio];
                }
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

                private static float CalcularPrecoRenda(int andar, float precoBase)
                {
                    return precoBase * (1.0f + 0.05f * andar);
                }

                private static bool ObterDisponibilidadeAleatoria()
                {
                    Random random = new Random();
                    return random.NextDouble() < 0.5;
                }
                #endregion
            #endregion
        #endregion

    }
}
