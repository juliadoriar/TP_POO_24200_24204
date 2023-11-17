using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;


namespace TP_POO_24200_24204
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Quarto> listaDeQuartos = Quarto.CriarListaDeQuartos();

            Quarto.ImprimirListaDeQuartos(listaDeQuartos);

            Utilizador.SalvarListaEmArquivo("utilizador.json");
            Utilizador.CriarUtilizador();
            Utilizador.ImprimirListaDeUtilizadores();

            /*if (novoUtilizador.GetTipoUtilizador() == "Morador")
            {
                Morador.CriarMorador(novoUtilizador);
            }
            else if (novoUtilizador.GetTipoUtilizador() == "Funcionário")
            {
                Funcionario.AdicionarFuncionario(novoUtilizador);
            }
            else if (novoUtilizador.GetTipoUtilizador() == "Administrador")
            {
                Gestor.AdicionarGestor(novoUtilizador);
            }*/

            Morador.ImprimirListaDeMoradores();


        }

    } 
}