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
            ControladorReserva controladorReserva = new ControladorReserva();
            ViewReserva viewReservas = new ViewReserva();
            viewReservas.SetControladorReserva(controladorReserva);
            controladorReserva.SetViewReserva(viewReservas);

            ControladorUtilizador.CriarFicheiroJson("utilizador.json");
            ControladorUtilizador.CriarFicheiroJson("morador.json");
            ControladorUtilizador.CriarFicheiroJson("gestor.json");
            ControladorUtilizador.CriarFicheiroJson("funcionario.json");
            Reserva.CriarFicheiroJson("reserva.json");
            ControladorQuarto.CriarListaDeQuartos();

            //ControladorMorador.ImprimirListaDeMoradores();
            //viewReservas.MenuCriarReserva();
            //viewReservas.ImprimirListaReservaAtual();
            //viewReservas.MenuBuscarReserva();
            //viewReservas.MenuEditarReserva();
            //viewReservas.MenuExcluirReserva();
            //MenuResidencia.ExibirMenu();
            //viewReservas.ImprimirListaReservaAtual();

            menuInicial.ExibirMenuInicial();
        }
    } 
}