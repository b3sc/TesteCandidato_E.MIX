using Nancy.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebApplicationCEP.Models
{
    public class Cep
    {
        public int Id { get; set; }

        [Display(Name ="CEP")]
        public string CEP { get; set; }

        [Display(Name ="Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        
        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        
        [Display(Name = "UF")]
        public string UF { get; set; }

        [Display(Name = "SIAFI")]
        public string SIAFI { get; set; }

        [Display(Name = "GIA")]
        public string GIA { get; set; }

        [Display(Name = "IBGE")]
        public string IBGE { get; set; }


        public static Cep Busca(string cep)
        {
            var objCEP = new Cep();

            var viaCEPUrl = "https://viacep.com.br/ws/" + cep + "/json/";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(viaCEPUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                string jsonResponse = string.Empty;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonResponse = reader.ReadToEnd();
                }

                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                CepJsonObject cepJson = json_serializer.Deserialize<CepJsonObject>(jsonResponse);


                objCEP.CEP = cepJson.cep;
                objCEP.Logradouro = cepJson.logradouro;
                objCEP.Bairro = cepJson.bairro;
                objCEP.Localidade = cepJson.localidade;
                objCEP.Complemento = cepJson.complemento;
                objCEP.UF = cepJson.uf;
                objCEP.SIAFI = cepJson.siafi;
                objCEP.GIA = cepJson.gia;
                objCEP.IBGE = cepJson.ibge;

            }
            catch (Exception e)
            {

                new Exception($"{e}");
            }

            return objCEP;

        }

        public class CepJsonObject
        {
            public string cep { get; set; }

            public string logradouro { get; set; }

            public string bairro { get; set; }

            public string localidade { get; set; }

            public string complemento { get; set; }

            public string uf { get; set; }

            public string siafi { get; set; }

            public string gia { get; set; }

            public string ibge { get; set; }
        }

    }
}
