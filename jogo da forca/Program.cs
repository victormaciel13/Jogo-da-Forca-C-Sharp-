using System;
using System.Collections.Generic;
using System.Linq;

class ForcaTemas
{
    static void Main()
    {
        ExibirTelaBoasVindas();

        Dictionary<string, List<string>> temasOriginais = new Dictionary<string, List<string>>
        {
            { "1", new List<string> { "cachorro", "gato", "elefante", "tigre", "girafa", "leao" } },
            { "2", new List<string> { "pizza", "hamburguer", "lasanha", "sushi", "feijoada", "churrasco" } },
            { "3", new List<string> { "palmeiras", "flamengo", "corinthians", "gremio", "vasco", "santos" } },
            { "4", new List<string> { "camiseta", "calca", "jaqueta", "tenis", "vestido", "blusa" } }
        };

        Dictionary<string, string> nomesTemas = new Dictionary<string, string>
        {
            { "1", "Animais" },
            { "2", "Comidas" },
            { "3", "Times de Futebol" },
            { "4", "Peças de Roupa" }
        };

        Dictionary<string, List<string>> palavrasRestantes = temasOriginais.ToDictionary(
            entry => entry.Key,
            entry => new List<string>(entry.Value)
        );

        Random rand = new Random();
        bool continuarJogando = true;
        int pontuacaoTotal = 0;
        int totalAcertos = 0;
        int totalErros = 0;

        while (continuarJogando)
        {
            Console.Clear();
            Console.WriteLine("=== JOGO DA FORCA COM TEMAS ===");
            Console.WriteLine($"Pontuação atual: {pontuacaoTotal} pontos");
            Console.WriteLine("Escolha um tema:");

            foreach (var tema in nomesTemas)
                Console.WriteLine($"{tema.Key} - {tema.Value}");

            Console.Write("Digite o número do tema: ");
            string escolha = Console.ReadLine();

            if (!palavrasRestantes.ContainsKey(escolha))
            {
                Console.WriteLine("Tema inválido. Pressione qualquer tecla para tentar novamente.");
                Console.ReadKey();
                continue;
            }

            if (palavrasRestantes[escolha].Count == 0)
            {
                Console.WriteLine("Você já usou todas as palavras desse tema! Escolha outro tema.");
                Console.ReadKey();
                continue;
            }

            List<string> palavrasDisponiveis = palavrasRestantes[escolha];
            int indice = rand.Next(palavrasDisponiveis.Count);
            string palavraEscolhida = palavrasDisponiveis[indice];
            palavrasDisponiveis.RemoveAt(indice);

            string nomeTema = nomesTemas[escolha];
            char[] letrasDescobertas = new string('_', palavraEscolhida.Length).ToCharArray();
            List<char> letrasErradas = new List<char>();

            const int maxTentativas = 15;
            int tentativasRestantes = maxTentativas;

            while (tentativasRestantes > 0 && new string(letrasDescobertas) != palavraEscolhida)
            {
                Console.Clear();
                Console.WriteLine($"Tema: {nomeTema}");
                DesenharForca(maxTentativas - tentativasRestantes);
                Console.WriteLine("Palavra: " + string.Join(" ", letrasDescobertas));
                Console.WriteLine($"Tentativas restantes: {tentativasRestantes}");
                Console.WriteLine("Letras erradas: " + string.Join(", ", letrasErradas));
                Console.WriteLine($"Pontuação atual: {pontuacaoTotal}");

                Console.Write("Digite uma letra: ");
                char tentativa = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (!char.IsLetter(tentativa))
                {
                    Console.WriteLine("Digite uma letra válida!");
                    Console.ReadKey();
                    continue;
                }

                if (palavraEscolhida.Contains(tentativa))
                {
                    for (int i = 0; i < palavraEscolhida.Length; i++)
                        if (palavraEscolhida[i] == tentativa)
                            letrasDescobertas[i] = tentativa;
                }
                else
                {
                    if (!letrasErradas.Contains(tentativa))
                    {
                        letrasErradas.Add(tentativa);
                        tentativasRestantes--;
                    }
                }
            }

            Console.Clear();
            DesenharForca(maxTentativas - tentativasRestantes);
            if (new string(letrasDescobertas) == palavraEscolhida)
            {
                Console.WriteLine($"🎉 Parabéns! Você acertou a palavra: {palavraEscolhida}");
                pontuacaoTotal += 10;
                totalAcertos++;
            }
            else
            {
                Console.WriteLine($"😢 Game Over! A palavra era: {palavraEscolhida}");
                totalErros++;
            }

            Console.WriteLine($"\nSua pontuação: {pontuacaoTotal} pontos");

            Console.Write("\nDeseja jogar novamente? (s/n): ");
            string resposta = Console.ReadLine().ToLower();

            if (resposta != "s")
            {
                continuarJogando = false;
                Console.WriteLine("\n===== PLACAR FINAL =====");
                Console.WriteLine($"🏁 Pontuação total: {pontuacaoTotal} pontos");
                Console.WriteLine($"✔️ Acertos: {totalAcertos}");
                Console.WriteLine($"❌ Erros: {totalErros}");
                Console.WriteLine("Obrigado por jogar! Até a próxima. 👋");
            }
        }
    }

    static void ExibirTelaBoasVindas()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("=======================================================");
        Console.WriteLine("                   BEM-VINDO AO JOGO!");
        Console.WriteLine("                   FORCA EM C#");
        Console.WriteLine("=======================================================");
        Console.WriteLine("         Escolha um tema para começar o jogo.");
        Console.WriteLine("=======================================================");
        Console.ResetColor();
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    static void DesenharForca(int erros)
    {
        string cabeca = erros >= 1 ? "O" : " ";
        string bracoEsq = erros >= 3 ? "\\" : " ";
        string tronco = erros >= 2 ? "|" : " ";
        string bracoDir = erros >= 4 ? "/" : " ";
        string pernaEsq = erros >= 5 ? "/" : " ";
        string pernaDir = erros >= 6 ? "\\" : " ";

        Console.WriteLine("  _______     ");
        Console.WriteLine(" |/      |    ");
        Console.WriteLine($" |      {cabeca}    ");
        Console.WriteLine($" |     {bracoEsq}{tronco}{bracoDir}   ");
        Console.WriteLine($" |     {pernaEsq} {pernaDir}    ");
        Console.WriteLine(" |            ");
        Console.WriteLine("_|___         ");
    }
}
