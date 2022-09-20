using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pi
{
    public class NumeroPi
    {
        public string __casasDecimais { get; set; } // Numero do pi
        public string NumeroMatch { get; set; } // Sequencia encontrada
        public int TamanhoNumeroMatch { get; set; }
        public uint CasaDecimalAtualAPI { get; set; } // Casa decimal q ta na consulta
        public uint CasaDecimalAtualPercorrendo { get; set; } // Casa decimal que esta percorrendo a string

        public HttpClient client = new HttpClient();

        public NumeroPi(int tamanhoNumeroMatch)
        {
            TamanhoNumeroMatch = tamanhoNumeroMatch;
            this.client = client;
            __casasDecimais = "";
            CasaDecimalAtualAPI = 0;
            CasaDecimalAtualPercorrendo = 0;
        }

        //public async void AliemntarCasasDecimaisPiAsync()
        //{
        //    HttpResponseMessage response = await client.GetAsync($"https://api.pi.delivery/v1/pi?start={CasaDecimalAtualAPI}&numberOfDigits=1000");
        //    response.EnsureSuccessStatusCode();
        //    string responseBody = await response.Content.ReadAsStringAsync();

        //    CasasDecimais += JsonSerializer.Deserialize<Json>(responseBody).content;

        //    PercorrerPi();
        //}

        public void PercorrerPi()
        {
            string numeroAtual = "";

            Console.WriteLine($"Verificando {__casasDecimais.Length} apos a Casa decimal: {CasaDecimalAtualPercorrendo}");
            for (int i = 0; i <= __casasDecimais.Length - TamanhoNumeroMatch; i++)
            {
                Task.Delay(300).Wait();
                CasaDecimalAtualPercorrendo++;
                numeroAtual = __casasDecimais.Substring(i, TamanhoNumeroMatch);
                if (VerificaNumeroPrimoPalimetro(numeroAtual))
                {
                    NumeroMatch = numeroAtual;
                    Console.WriteLine("Numero encontrado: " + NumeroMatch);
                    StreamWriter x;
                    string CaminhoNome = "C:\\Users\\Paulo\\Desktop\\arq01.txt";
                    x = File.CreateText(CaminhoNome);
                    x.WriteLine(NumeroMatch);
                    x.Close();
                    break;
                }
            }
            Console.WriteLine("Cabo de percorrer");
            return;
        }

        public void PercorrerPi2(string _casasDecimais)
        {
            string numeroAtual = "";
            for (int i = 0; i <= __casasDecimais.Length - TamanhoNumeroMatch; i++)
            {
                CasaDecimalAtualPercorrendo++;
                numeroAtual = __casasDecimais.Substring(i, TamanhoNumeroMatch);
                if (VerificaNumeroPrimoPalimetro(numeroAtual))
                {
                    NumeroMatch = numeroAtual;
                    Console.WriteLine("Numero encontrado: " + NumeroMatch);
                    StreamWriter x;
                    string CaminhoNome = "C:\\Users\\Paulo\\Desktop\\arq01.txt";
                    x = File.CreateText(CaminhoNome);
                    x.WriteLine(NumeroMatch);
                    x.Close();
                    break;
                }
            }
            Console.WriteLine("Cabo de percorrer");
            return;
        }


        bool VerificaNumeroPrimoPalimetro(string numeroString)
        {
            if (!VerificaNumeroPalimetro(numeroString)) return false;
            if (!VerificaNumeroPrimo(Decimal.Parse(numeroString))) return false;
            return true;
        }

        bool VerificaNumeroPrimo(decimal numero)
        {
            if (numero == 0) return false;
            for (int i = 2; i * i <= numero; i++)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }

        bool VerificaNumeroPalimetro(string numeroString)
        {
            numeroString = String.Join("", numeroString);
            for (int i = 0; i <= numeroString.Length / 2; i++)
            {
                if (numeroString[i] != numeroString[numeroString.Length - 1 - i]) return false;
            }
            return true;
        }

    }
}
