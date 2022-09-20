// https://api.pi.delivery/v1/pi?start=0&numberOfDigits=9
using Pi;
using System.Text.Json;

// 320827080
// 323667960


//string numeroCerto, CasasDecimaisAPI = "", numeroAtual;
//int QuantidadeNumeros = 21;
//// 210000000
//int QuantidadeDigitosPi = 840;
//HttpClient client = new HttpClient();
//bool numberValido = false;
//uint count = 321100920; // 321100920 parou aqi o padrao
//uint maxcount = 642201840;
//// 963.302.760
//Console.WriteLine("Numero inicial:");
//count = (uint)Int64.Parse(Console.ReadLine());

//Console.WriteLine("Numero Final:");
//maxcount = (uint)Int64.Parse(Console.ReadLine());



//while (!numberValido)
//{
//    CasasDecimaisAPI += PegarPiAsync(count, QuantidadeDigitosPi);

//    PercorrerPi(CasasDecimaisAPI);

//    count += (uint) QuantidadeDigitosPi;
//}

//void PercorrerPi(string casasDecimaisAPI)
//{
//    Console.WriteLine($"Tentando da casa decimal {count} até {count + QuantidadeDigitosPi}");
//    for (int i = 0; i <= CasasDecimaisAPI.Length - QuantidadeNumeros; i++)
//    {
//        numeroAtual = CasasDecimaisAPI.Substring(i, QuantidadeNumeros);
//        if (VerificaNumeroPrimoPalimetro(numeroAtual))
//        {
//            numeroCerto = numeroAtual;
//            numberValido = true;
//            Console.WriteLine(numeroCerto);
//            StreamWriter x;
//            string CaminhoNome = "C:\\Users\\Paulo\\Desktop\\arq01.txt";
//            x = File.CreateText(CaminhoNome);
//            x.WriteLine(numeroCerto);
//            x.Close();
//            break;
//        }
//    }
//}

//async Task<string> PegarPiAsync(uint casaDecimalAtual, int quantidadeDigitosPi)
//{
//    HttpResponseMessage response = await client.GetAsync($"https://api.pi.delivery/v1/pi?start={casaDecimalAtual}&numberOfDigits={quantidadeDigitosPi}");
//    response.EnsureSuccessStatusCode();
//    string responseBody = await response.Content.ReadAsStringAsync();

//    var JSON = JsonSerializer.Deserialize<Json>(responseBody);
//    return JSON.content;
//}

//bool VerificaNumeroPrimoPalimetro(string numeroString)
//{
//    if (!VerificaNumeroPalimetro(numeroString)) return false;
//    if (!VerificaNumeroPrimo(Decimal.Parse(numeroString))) return false;
//    return true;
//}

//bool VerificaNumeroPrimo(decimal numero){
//    if(numero == 0) return false;
//    for (int i = 2; i * i <= numero; i++)
//    {
//        if (numero % i == 0) return false;
//    }
//    return true;
//}

//bool VerificaNumeroPalimetro(string numeroString)
//{
//    numeroString = String.Join("", numeroString);
//    for(int i = 0; i <= numeroString.Length/2; i++)
//    {
//        if (numeroString[i] != numeroString[numeroString.Length - 1 - i]) return false;
//    }
//    return true;
//}




var pi = new NumeroPi(21);
//var t1 = new Action(pi.PercorrerPi);

//Parallel.Invoke(
//    new Action(pi.AliemntarCasasDecimaisPiAsync),
//    new Action(pi.PercorrerPi)
//);
//pi.AliemntarCasasDecimaisPiAsync();

int QuantidadeCasasPorRequest = 1000 - pi.TamanhoNumeroMatch;
bool teste = true;
while (true)
{
    ConsultaAsync(QuantidadeCasasPorRequest, pi.CasaDecimalAtualAPI);
    pi.CasaDecimalAtualAPI += (uint) QuantidadeCasasPorRequest;
    pi.CasaDecimalAtualAPI -= (uint) pi.TamanhoNumeroMatch * 2;
}
Console.ReadLine();

async void ConsultaAsync(int _qtdDigits, uint _casaDecimalAtualAPI)
{
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync($"https://api.pi.delivery/v1/pi?start={_casaDecimalAtualAPI}&numberOfDigits={_qtdDigits}");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    PercorrerPi(JsonSerializer.Deserialize<Json>(responseBody).content, pi.TamanhoNumeroMatch);

    return;
    //pi.CasaDecimalAtualAPI += (uint)_qtdDigits;
    //Console.WriteLine(pi.CasaDecimalAtualAPI);
    //pi.CasasDecimais += JsonSerializer.Deserialize<Json>(responseBody).content;

    
    //pi.CasasDecimais = "";
    //pi.CasaDecimalAtualAPI -= (uint)QuantidadeCasasPorRequest;
    //teste = false;
}


void PercorrerPi(string _casasDecimais, int TamanhoNumeroMatch)
{
    string numeroAtual = "";
    for (int i = 0; i <= _casasDecimais.Length - TamanhoNumeroMatch; i++)
    {
        numeroAtual = _casasDecimais.Substring(i, TamanhoNumeroMatch);
        if (VerificaNumeroPrimoPalimetro(numeroAtual))
        {
            Console.WriteLine("Numero encontrado: " + numeroAtual);
            StreamWriter x;
            string CaminhoNome = "C:\\Users\\Paulo\\Desktop\\arq01.txt";
            x = File.CreateText(CaminhoNome);
            x.WriteLine(numeroAtual);
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