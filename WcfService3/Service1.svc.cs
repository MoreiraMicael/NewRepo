using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Xml;

namespace WcfService3
{
    public class Service1 : IService1
    {
        private static string FILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data",
            "refeicoes1.xml");

        public object JsonConvert { get; private set; }

        public double CalculadoraCalorias(int idade, string genero, double altura, double peso, string actividade)
        {

            double sedent = 1.2;
            double light = 1.375;
            double moderate = 1.55;
            double very = 1.725;
            double extra = 1.9;

            int caloriasM = ((int)(10 * peso + 6.25 * altura - 5 * idade + 5));

            int caloriasF = ((int)(10 * peso + 6.25 * altura - 5 * idade - 161));

            int caloriasZero = 0;

            if (genero == "Masculino")
            {
                if (actividade == "Sedentario")
                {
                    double caloriasMasc = ((caloriasM * sedent));
                    return caloriasMasc;
                    //double menosMeio = ((caloriasM * sedent) - 500);
                    //double menosUm = ((caloriasM * sedent) - 1000);
                    //double maisMeio = ((caloriasM * sedent) + 500);
                    //double maisUm = ((caloriasM * sedent) + 1000);
                }
                else
                {
                    return caloriasZero;
                }
            }
            return caloriasZero;
        }

        public double CalculadoraPesoIdeal(string genero, int altura)
        {
            double alturaInches = altura * 0.39370079;
            double alturaExtra = alturaInches - 60;

            if (genero == "Masculino")
            {
                double pesoExtra = alturaExtra * 1.9;
                double pesoIdealM = ((double)(52 + pesoExtra));
                return pesoIdealM;
            }
            else
            {
                double pesoExtra = alturaExtra * 1.7;
                double pesoIdealF = ((double)(49 + pesoExtra));
                return pesoIdealF;
            }
        }

        public List<Refeicao> GetRefeicoes()
        {
                XmlDocument doc = new XmlDocument();
                doc.Load(FILEPATH);
                List<Refeicao> refeicoes = new List<Refeicao>();
                XmlNodeList refeicaoNodes = doc.SelectNodes("/Refeicoes/Refeicao");
                foreach (XmlNode refeicaoNode in refeicaoNodes)
                {
                    XmlNode restauranteNode = refeicaoNode.SelectSingleNode("Restaurante");
                    XmlNode itemNode = refeicaoNode.SelectSingleNode("Item");
                    XmlNode quantidadeNode = refeicaoNode.SelectSingleNode("Quantidade");
                    XmlNode caloriasNode = refeicaoNode.SelectSingleNode("Calorias");
                    
                    Refeicao refeicao = new Refeicao(
                    restauranteNode.InnerText,
                    itemNode.InnerText,
                    quantidadeNode.InnerText,
                    caloriasNode.InnerText
                    );
                    refeicoes.Add(refeicao);
                }
                return refeicoes;
            }

        public List<Refeicao> PostRefeicoes()
        {
            {
                //  string FILEPATH = Path.Combine(HttpContext.Current.Server.MapPath("/App_Data/calorias_restaurantes_1.txt"));
                List<Refeicao> refeicaos = new List<Refeicao>();

                var txt = string.Empty;

                using (StreamReader stream = new StreamReader("..\\..\\App_Data\\calorias_restaurantes_1.txt"))
                {
                    txt = stream.ReadToEnd();
                }

                string[] linhas = txt.Split(new[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);

                XmlDocument xmldoc;
                XmlElement Refeicoes;
                XmlElement Refeicao;
                XmlElement restaurante;
                XmlElement Item;
                XmlElement Quantidade;
                XmlElement Calorias;

                //if(File.Exists())

                xmldoc = new XmlDocument();
                Refeicoes = xmldoc.CreateElement("Refeicoes");

                for (int i = 1; i < linhas.Length; i++)
                {
                    string[] campos = linhas[i].Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    Refeicao = xmldoc.CreateElement("Refeicao");
                    restaurante = xmldoc.CreateElement("Restaurante");
                    restaurante.InnerText = campos[0];
                    Refeicao.AppendChild(restaurante);
                    Item = xmldoc.CreateElement("Item");
                    Item.InnerText = campos[1];
                    Refeicao.AppendChild(Item);
                    Quantidade = xmldoc.CreateElement("Quantidade");
                    Quantidade.InnerText = campos[2];
                    Refeicao.AppendChild(Quantidade);
                    Calorias = xmldoc.CreateElement("Calorias");
                    Calorias.InnerText = campos[3];
                    Refeicao.AppendChild(Calorias);
                    Refeicoes.AppendChild(Refeicao);
                }

                UnicodeEncoding unicode = new UnicodeEncoding();

                /*using (var stream = File.OpenText("..\\..\\App_Data\\calorias_restaurantes_2.txt"))
                {
                    txt = stream.ReadToEnd();
                }
                String unicodeString = txt;
                Byte[] encodedBytes = unicode.GetBytes(unicodeString);

                String decodedString = unicode.GetString(encodedBytes);
                linhas = decodedString.Split( '�' );
                for (int i = 1; i < linhas.Length; i++)
                {
                    string[] campos = linhas[i].Split('|' );
                    Refeicao = xmldoc.CreateElement("Refeicao");
                    restaurante = xmldoc.CreateElement("Restaurante");
                    restaurante.InnerText = campos[0];
                    Refeicao.AppendChild(restaurante);
                    Item = xmldoc.CreateElement("Item");
                    Item.InnerText = campos[1];
                    Refeicao.AppendChild(Item);
                    Quantidade = xmldoc.CreateElement("Quantidade");
                    Quantidade.InnerText = campos[2];
                    Refeicao.AppendChild(Quantidade);
                    Calorias = xmldoc.CreateElement("Calorias");
                    Calorias.InnerText = campos[3];
                    Refeicao.AppendChild(Calorias);
                    Refeicoes.AppendChild(Refeicao);
                }
                using (StreamReader stream = new StreamReader("..\\..\\App_Data\\calorias_restaurantes_3.json"))
                {
                    txt = stream.ReadToEnd();
                }

                //string resultadoTexto = txt;
                List<Refeicao> listaRefeiceoes = JsonConvert.DeserializeObject<List<Refeicao>>(txt);
                for (int i = 1; i < listaRefeiceoes.Count; i++)
                {
                    Refeicao = xmldoc.CreateElement("Refeicao");
                    restaurante = xmldoc.CreateElement("Restaurante");
                    restaurante.InnerText = listaRefeiceoes[i].Restaurante;
                    Refeicao.AppendChild(restaurante);
                    Item = xmldoc.CreateElement("Item");
                    Item.InnerText = listaRefeiceoes[i].Item;
                    Refeicao.AppendChild(Item);
                    Quantidade = xmldoc.CreateElement("Quantidade");
                    Quantidade.InnerText = listaRefeiceoes[i].Quantidade;
                    Refeicao.AppendChild(Quantidade);
                    Calorias = xmldoc.CreateElement("Calorias");
                    Calorias.InnerText = listaRefeiceoes[i].Calorias;
                    Refeicao.AppendChild(Calorias);
                    Refeicoes.AppendChild(Refeicao);
                }
                */
                xmldoc.AppendChild(Refeicoes);

                xmldoc.Save(Path.ChangeExtension("\\App_Data\\calorias_restaurantes_1XML", ".xml"));
                return refeicaos;
            }
        }

    }
}
