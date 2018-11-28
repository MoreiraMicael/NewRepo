﻿using System;
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

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Refeicoes")]
        [Description("Post a Refeicao in Refeicoes.")]
        List<Refeicao> PostRefeicoes();

        [OperationContract]
        double CalculadoraPesoIdeal(string genero, int altura);

        [OperationContract]
        double CalculadoraCalorias(int idade, string genero, double altura, double peso, string actividade);
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
            string Refeicao = string.Empty;
            Refeicao += "Idade: " + Idade + Environment.NewLine;
            Refeicao += "Item: " + Altura + Environment.NewLine;
            Refeicao += "Quantidade: " + Peso + Environment.NewLine;
            Refeicao += "Calorias: " + ActividadeFisica + Environment.NewLine;
            Refeicao += "Calorias: " + Genero + Environment.NewLine;
            return Refeicao;
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

   
}