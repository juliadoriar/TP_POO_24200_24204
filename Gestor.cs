using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO_24200_24204
{
    public class Gestor : Utilizador
    {
        public static List<Gestor> listaDeGestores = new List<Gestor>();

        public Gestor(
            int utiId,
            string nomeUti,
            string email,
            string password,
            DateTime dataNascimento,
            string morada,
            string codigoPostal,
            string localidade,
            string contactoTelefone,
            string docIdentificacao,
            string tipoDocIdentificacao,
            string iban,
            bool isAtivo,
            DateTime dataRegisto)
            : base(utiId, nomeUti, email, password, dataNascimento, morada, codigoPostal, localidade, contactoTelefone, docIdentificacao, tipoDocIdentificacao, iban, "Gestor", isAtivo, dataRegisto)
        {
           
        }


    }




}
