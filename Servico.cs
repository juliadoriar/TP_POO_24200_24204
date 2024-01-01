using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    internal class Servico
    {
        #region Leitura de dados
        /// <summary>
        /// Método para ler uma string
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static string LerString(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }

        /// <summary>
        /// Método para ler um inteiro
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Entrada inválida. Digite um número inteiro.");
                Console.Write(mensagem);
            }

            return valor;
        }

        /// <summary>
        /// Método para ler uma data
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static DateTime LerData(string mensagem)
        {
            DateTime data;
            Console.Write(mensagem);

            while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.WriteLine("Entrada inválida. Digite uma data no formato dd-MM-yyyy.");
                Console.Write(mensagem);
            }

            return data;
        }

        /// <summary>
        /// Método para ler um número real
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static float LerFloat(string mensagem)
        {
            float valor;
            Console.Write(mensagem);

            while (!float.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Entrada inválida. Digite um número real.");
                Console.Write(mensagem);
            }

            return valor;
        }

        /// <summary>
        /// Método para ler um valor booleano
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static bool LerBooleano(string mensagem)
        {
            bool valor;
            Console.Write(mensagem);

            while (!bool.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Entrada inválida. Digite um valor booleano (true/false).");
                Console.Write(mensagem);
            }

            return valor;
        }
        #endregion
    }
}
