using System;
using System.Collections.Generic;
using System.IO;
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

        public List<Calorias> CalculadoraCalorias(int idade, string genero, double altura, double peso, string actividade)//Return de todos os valores ?
        {
            int caloriasM = ((int)(10 * peso + 6.25 * altura - 5 * idade + 5));
            int caloriasF = ((int)(10 * peso + 6.25 * altura - 5 * idade - 161));
            double caloriasBase = 0;
            double actividadeValor = 0;

            if (genero == "Masculino")
            {
                caloriasBase = caloriasM;
            }
            if (genero == "Feminino")
            {
                caloriasBase = caloriasF;
            }

            if (actividade == "Sedentario")
            {
                actividadeValor = 1.2;
            }
            if (actividade == "Leve")
            {
                actividadeValor = 1.375;
            }
            if (actividade == "Moderada")
            {
                actividadeValor = 1.55;
            }
            if (actividade == "Alta")
            {
                actividadeValor = 1.725;
            }
            if (actividade == "Extra")
            {
                actividadeValor = 1.9;
            }

            
            double caloriasTotal = ((caloriasBase * actividadeValor));
            //return caloriasTotal;
            double menosMeio = (caloriasTotal - 500);
            double menosUm = (caloriasTotal - 1000);
            double maisMeio = (caloriasTotal + 500);
            double maisUm = (caloriasTotal + 1000);

            List<Calorias> calorias = new List<Calorias>();
            calorias.Add(new Calorias() { caloriasTotal = caloriasTotal, menosMeio = menosMeio, menosUm = menosUm, maisMeio = maisMeio, maisUm = maisUm });

            return calorias;
           
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
            if (genero == "Feminino")
            {
                double pesoExtra = alturaExtra * 1.7;
                double pesoIdealF = ((double)(49 + pesoExtra));
                return pesoIdealF;
            }
            else
                throw new System.ArgumentException("Parametro Errado", "Genero: Masculino ou Feminino");
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

        public void AddRefeicao(Refeicao refeicao)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);
            XmlNode refeicoesNode = doc.SelectSingleNode("/Refeicoes");
            XmlElement refeicaoNode = doc.CreateElement("Refeicao");
            XmlElement restauranteNode = doc.CreateElement("Restaurante");
            restauranteNode.InnerText = refeicao.Restaurante;
            refeicaoNode.AppendChild(restauranteNode);
            XmlElement itemNode = doc.CreateElement("Item");
            itemNode.InnerText = refeicao.Item;
            refeicaoNode.AppendChild(itemNode);
            XmlElement quantidadeNode = doc.CreateElement("Quantidade");
            quantidadeNode.InnerText = refeicao.Quantidade;
            refeicaoNode.AppendChild(quantidadeNode);
            XmlElement caloriasNode = doc.CreateElement("Calorias");
            caloriasNode.InnerText = refeicao.Calorias;
            refeicaoNode.AppendChild(caloriasNode);
            refeicoesNode.AppendChild(refeicaoNode);
            doc.Save(FILEPATH);
        }

        /*public List<Refeicao> PostRefeicoes()
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
                XmlElement Restaurante;
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
                    Refeicao.AppendChild(Restaurante);
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
                
                xmldoc.AppendChild(Refeicoes);

                xmldoc.Save(Path.ChangeExtension("\\App_Data\\calorias_restaurantes_1XML", ".xml"));
                return refeicaos;
            }
        }*/
    }
}
