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
            Utilizador.CriarFicheiroJson("utilizador.json");
            Utilizador.CriarFicheiroJson("morador.json");
            Quarto.CriarListaDeQuartos();
            MenuResidencia.ExibirMenu();


        }

    } 
}