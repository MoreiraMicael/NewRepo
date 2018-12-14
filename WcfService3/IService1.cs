using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;

namespace WcfService3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Refeicoes")]
        [Description("Gets all the Refeicoes.")]
        List<Refeicao> GetRefeicoes();

        // ADD Refeicao
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddRefeicao")]
        [Description("Adds a Refeicao to Refeicoes.")]
        void AddRefeicao(Refeicao refeicao);


        /*[OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Refeicoes")]
        [Description("Post a Refeicao in Refeicoes.")]
        List<Refeicao> PostRefeicoes();*/

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/PesoIdeal?genero={genero}&altura={altura}")]
        [Description("Gets Peso Ideal.")]
        double CalculadoraPesoIdeal(string genero, int altura);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Calorias?idade={idade}&genero={genero}&altura={altura}&peso={peso}&actividade={actividade}")]
        [Description("Gets Calorias.")]
        List<Calorias> CalculadoraCalorias(int idade, string genero, double altura, double peso, string actividade);
    }

    [DataContract]
    public class DadosPessoais
    {
        public DadosPessoais()
        {
        }

        public DadosPessoais(int Idade, int Altura, double Peso, string ActividadeFisica, string Genero)
        {
            this.Idade = Idade;
            this.Altura = Altura;
            this.Peso = Peso;
            this.ActividadeFisica = ActividadeFisica;
            this.Genero = Genero;
        }

        [DataMember]
        public int Idade { get; set; }
        [DataMember]
        public int Altura { get; set; }
        [DataMember]
        public double Peso { get; set; }
        [DataMember]
        public string ActividadeFisica { get; set; }
        [DataMember]
        public string Genero { get; set; }

        public override string ToString()
        {
            string DadosPessoais = string.Empty;
            DadosPessoais += "Idade: " + Idade + Environment.NewLine;
            DadosPessoais += "Item: " + Altura + Environment.NewLine;
            DadosPessoais += "Quantidade: " + Peso + Environment.NewLine;
            DadosPessoais += "Calorias: " + ActividadeFisica + Environment.NewLine;
            DadosPessoais += "Calorias: " + Genero + Environment.NewLine;
            return DadosPessoais;
        }
    }

    [DataContract]
    public class Refeicao
    {
        public Refeicao()
        {
        }

        public Refeicao(string Restaurante, string Item, string Quantidade, string Calorias)
        {
            this.Restaurante = Restaurante;
            this.Item = Item;
            this.Quantidade = Quantidade;
            this.Calorias = Calorias;
        }

        [DataMember]
        public string Restaurante { get; set; }
        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public string Quantidade { get; set; }
        [DataMember]
        public string Calorias { get; set; }

        public override string ToString()
        {
            string Refeicao = string.Empty;
            Refeicao += "Restaurante: " + Restaurante + Environment.NewLine;
            Refeicao += "Item: " + Item + Environment.NewLine;
            Refeicao += "Quantidade: " + Quantidade + Environment.NewLine;
            Refeicao += "Calorias: " + Calorias + Environment.NewLine;
            return Refeicao;
        }
    }

    [DataContract]
    public class Calorias
    {
        public Calorias()
        {
        }

        public Calorias(double caloriasTotal, double menosMeio, double menosUm, double maisMeio, double maisUm)
        {
            this.caloriasTotal = caloriasTotal;
            this.menosMeio = menosMeio;
            this.menosUm = menosUm;
            this.maisMeio = maisMeio;
            this.maisUm = maisUm;
        }

        [DataMember]
        public double caloriasTotal { get; set; }
        [DataMember]
        public double menosMeio { get; set; }
        [DataMember]
        public double menosUm { get; set; }
        [DataMember]
        public double maisMeio { get; set; }
        [DataMember]
        public double maisUm { get; set; }

        public override string ToString()
        {
            string Calorias = string.Empty;
            Calorias += "Calorias que deve Ingerir: " + caloriasTotal + Environment.NewLine;
            Calorias += "Calorias para perder meio kilo: " + menosMeio + Environment.NewLine;
            Calorias += "Calorias para perder um kilo: " + menosUm + Environment.NewLine;
            Calorias += "Calorias para ganhar meio kilo: " + maisMeio + Environment.NewLine;
            Calorias += "Calorias para ganhar um kilo: " + maisUm + Environment.NewLine;
            return Calorias;
        }
    }
}
