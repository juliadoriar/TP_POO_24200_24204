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

            Servico.CriarFicheiroJson("utilizador.json");
            Servico.CriarFicheiroJson("morador.json");
            Servico.CriarFicheiroJson("gestor.json");
            Servico.CriarFicheiroJson("funcionario.json");
            Servico.CriarFicheiroJson("reserva.json");
            ControladorQuarto.CriarListaDeQuartos();

            menuInicial.ExibirMenuInicial();
        }
    } 
}