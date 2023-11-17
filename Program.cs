using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Quarto> listaDeQuartos = Quarto.CriarListaDeQuartos();

            Quarto.ImprimirListaDeQuartos(listaDeQuartos);

            List<Utilizador> listaDeUtilizadores = Utilizador.CriarListaDeUtilizadores();
           Utilizador.CriarUtilizador(listaDeUtilizadores);
            Utilizador.ImprimirListaDeUtilizadores(listaDeUtilizadores);

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
            }

            List<Morador> listaDeMoradores = Morador.CriarListaDeMoradores();
            Morador.AdicionarMorador(listaDeMoradores);*/
            Morador.ImprimirListaDeMoradores();


        }

    } 
}