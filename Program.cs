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
            ControladorMorador controladorMorador = new ControladorMorador();
            controlador.SetControladorMorador(controladorMorador);

            Servico.CriarFicheiroJson("utilizador.json");
            Servico.CriarFicheiroJson("morador.json");
            Servico.CriarFicheiroJson("gestor.json");
            Servico.CriarFicheiroJson("funcionario.json");
            Servico.CriarFicheiroJson("reserva.json");
            ControladorQuarto.CriarListaDeQuartos();

            //controlador.MenuEditarUtilizador();
            //controlador.ImprimirListaDeUtilizadores();
            //controladorMorador.ImprimirListaDeMoradores();
            //controladorMorador.MenuBuscarMorador();
            //controlador.MenuExcluirUtilizador();


            menuInicial.ExibirMenuInicial();
        }
    } 
}