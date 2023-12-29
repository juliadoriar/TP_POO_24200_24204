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
            ControladorUtilizador controlador = new ControladorUtilizador();
            MenuInicial menuInicial = new MenuInicial(controlador);
            ViewReserva viewReserva = new ViewReserva();

            ControladorUtilizador.CriarFicheiroJson("utilizador.json");
            ControladorUtilizador.CriarFicheiroJson("morador.json");
            Reserva.CriarFicheiroJson("reserva.json");
            ControladorQuarto.CriarListaDeQuartos();


            viewReserva.CriarReserva();
            viewReserva.ImprimirListaDeReservas();
            //MenuResidencia.ExibirMenu();
            menuInicial.ExibirMenuInicial();
        }
    } 
}