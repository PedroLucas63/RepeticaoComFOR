//Nomeação do Projeto de Urna:
namespace Urna
{
    //Criação da classe da aplicação:
    class App
    {
        //Criação do método principal da aplicação:
        public static void Main(string[] args)
        { 

            //Incremento da função que gera a lista de candidatos:
            var candidatos = CriarCandidatos();

            //Incremento da função que vai ocorrer os votos e retornar o resultado:
            var votos = Votacao(candidatos);

            //Exibição do resultado:
            Resultado(votos);

        }

        //Criação da função que criar e retorna uma lista de candidatos:
        public static List<Candidato> CriarCandidatos()
        {
            //Criação de uma lista com vários candidatos:
            var candidatos = new List<Candidato>();

            //Variáveis dos dados de cada candidato:
            string id, nome, disciplina;
            int idade;

            //Variável que verifica se um candidato tem o ID de outro:
            bool IDigual;

            //Variável que vai determinar se deve ficar repetindo a criação de novos candidatos:
            char continuar;

            //Sistema de repetição para ficar adicionar candidatos:
            do
            {
                //Começa com o candidato não sendo igual:
                IDigual = false;

                //Cabeçalho do registro de candidatos:
                Console.WriteLine($"{new String('=', 20)}REGISTRO DE CANDIDATOS{new String('=', 20)}");

                //Pedido dos dados do Candidato:
                Console.Write("\nID: ");
                id = Console.ReadLine();

                Console.Write("Nome: ");
                nome = Console.ReadLine();

                Console.Write("Disciplina: ");
                disciplina = Console.ReadLine();

                Console.Write("Idade: ");
                idade = int.Parse(Console.ReadLine());

                //Criação do novo candidato que deve ser adicionado a lista:
                var novo_candidato = new Candidato(id, nome, disciplina, idade);

                //Verifica se o ID corresponde apenas a números e faz as determinadas funções:
                if (!id.All(char.IsDigit))
                {
                    Console.WriteLine("\nO candidato não foi registrado pois o ID não é composto apenas de números!");
                }
                else
                {
                    //Verifica se já tem algum candidato com o ID:
                    foreach (var candidato in candidatos)
                    {
                        if (candidato.ID == novo_candidato.ID)
                        {
                            IDigual = true;
                        }
                    }

                    //Faz as determinadas funções dependendo se já tem ou não o ID:
                    if (IDigual == true)
                    {
                        Console.WriteLine("\nCandidato não foi registrado pois o ID já está em uso!");
                    }
                    else if (int.Parse(id) > 99999 || int.Parse(id) < 0)
                    {
                        Console.WriteLine("\nO ID do candidato precisa estar entre 1 e 99999, incluindo!");
                    }
                    else
                    {
                        //Incremento do novo candidato na lista de candidatos:
                        candidatos.Add(novo_candidato);
                        Console.WriteLine("\nCandidato registrado com sucesso!");
                    }
                }

                //Pede se a pessoa deseja continuar:
                Console.Write("\nDeseja registrar mais um candidato ('s' para sim)? ");
                continuar = char.Parse(Console.ReadLine());

                //Executa a limpeza da tela:
                Console.Clear();

            } while (char.ToLower(continuar) == 's');

            //Adiciona os candidatos gerais (Branco e Nulo):
            candidatos.Add(new Candidato("0", "Branco", "", 0));
            candidatos.Add(new Candidato("", "Nulo", "", 0));

            //Retorna da lista de candidatos:
            return candidatos;
        }

        //Criação da função que pede os votos e retorna o resultado:
        public static List<Candidato> Votacao(List<Candidato> candidatos)
        {

            //Criação da variável que vai dizer se deve continuar ou não:
            char continuar;

            //Recebe o voto:
            string voto;

            //Faz a verificação se já houve voto da pessoa:
            bool votado;

            //Variável que vai funcionar para indicar o menu:
            string menu = $"{new String('=', 20)}CANDIDATOS{new String('=', 20)}\n";

            //Percorre a lista de candidatos adicionando cada um no menu:
            foreach(var candidato in candidatos)
            {
                //Verifica se o candidato é o Branco ou Nulo:
                if(candidato.ID != "" && candidato.ID != "0")
                {
                    menu += $"{String.Format("{0,5}", candidato.ID)} {new String('.', 25)} {candidato.Nome}\n";
                }
            }

            //Adiciona os dados do branco ou nulo no final, juntamente com a pergunta do voto:
            menu += $"{String.Format("{0, 5}", "0")} {new String('.', 25)} Branco\n" +
                $"{String.Format("{0, 5}", "")} {new String('.', 25)} Nulo\n" +
                $"\nDigite o seu voto: ";

            //Sistema de repetição que vai executar até que a variável continuar ser diferente de S:
            do
            {

                //Faz a limpeza da tela do console:
                Console.Clear();

                //Informa que a pessoa atual ainda não votou:
                votado = false;

                //Variável que confirma o voto:
                char confirmar_voto;

                //Escreve o menu com os candidatos e recebe o voto:
                Console.Write(menu);
                voto = Console.ReadLine();

                //Faz a verificação do voto:
                foreach(var candidato in candidatos)
                {
                    //Evita fazer a contagem do voto branco e nulo:
                    if (candidato.ID != "0" && candidato.ID != "")
                    {
                        //Verifica se o voto é igual ao ID de algum candidato:
                        if(voto == candidato.ID)
                        {
                            //Pergunta se a pessoa deseja confirmar o voto:
                            Console.Write($"\nTem certeza que deseja votar no candidato {candidato.Nome}, professor da disciplina {candidato.Disciplina} ('s' para sim)? ");
                            confirmar_voto = char.Parse(Console.ReadLine());

                            if(char.ToLower(confirmar_voto) == 's')
                            {
                                //Contabiliza um voto para o candidato:
                                candidato.Votar();

                                //Informa que já foi votado:
                                Console.WriteLine("\nVoto computado!");
                            }
                            else
                            {
                                //Informa que o voto não foi contabilizado:
                                Console.WriteLine("\nVoto cancelado!");
                            }
                            votado = true;
                            //Faz a quebra do sistema de repetição:
                            break;
                        }
                    }
                }

                //Faz a verificação se já teve o voto:
                if(votado == false)
                {
                    //Verifica se o voto foi em branco:
                    if(voto == "0")
                    {
                        //Pede para fazer a confirmação do voto:
                        Console.Write("\nTem certeza que deseja votar em Branco ('s' para sim)? ");
                        confirmar_voto = char.Parse(Console.ReadLine());

                        //Verifica se o voto foi confirmado:
                        if(char.ToLower(confirmar_voto) == 's')
                        {
                            //Contabiliza um voto para branco:
                            candidatos[candidatos.Count-2].Votar();

                            //Informa que já foi votado:
                            Console.WriteLine("\nVoto computado!");
                        }
                        else
                        {
                            //Informa que o voto não foi contabilizado:
                            Console.WriteLine("\nVoto cancelado!");
                        }
                    }
                    else
                    {
                        //Pede para fazer a confirmação do voto:
                        Console.Write("\nTem certeza que deseja votar em Nulo ('s' para sim)? ");
                        confirmar_voto = char.Parse(Console.ReadLine());

                        //Verifica se o voto foi confirmado:
                        if (char.ToLower(confirmar_voto) == 's')
                        {
                            //Contabiliza do voto para nulo:
                            candidatos.Last().Votar();

                            //Informa que o voto foi computado:
                            Console.WriteLine("\nVoto computado!");
                        }
                        else
                        {
                            //Informa que o voto foi cancelado:
                            Console.WriteLine("\nVoto cancelado!");
                        }
                    }
                }

                //Pergunta se ainda vão votar mais uma vez e lê a resposta:
                Console.Write("\nDeseja registrar mais um voto ('s' para sim)? ");
                continuar = char.Parse(Console.ReadLine());

            } while (char.ToLower(continuar) == 's');

            //Retorna a lista de candidatos com os votos contabilizados:
            return candidatos;
        }

        //Criação da função que retorna o resultado:
        public static void Resultado(List<Candidato> votos)
        {
            //Faz a ordenação dos candidatos pela idade e depois pela quantidade de votos:
            var votosOrdenados = votos.OrderByDescending(candidato => candidato.Idade).ToList();
            votosOrdenados = votosOrdenados.OrderByDescending(candidato => candidato.Votos).ToList();

            //Faz a limpeza da tela:
            Console.Clear();

            int votos_gerais = 0, votos_validos = 0;

            //Faz a verificação da quantidade dos votos totais e válidos;
            foreach(var candidato in votosOrdenados)
            {
                //Verifica se é um candidato não branco e não nulo:
                if(candidato.ID != "0" && candidato.ID != "")
                {
                    //Contabiliza os votos válidos:
                    votos_validos += candidato.Votos;
                }

                //Contabiliza todos os votos:
                votos_gerais += candidato.Votos;
            }

            //Exibe um cabeçalho para os votos:
            Console.WriteLine($"{new String('=', 30)}RESULTADO{new String('=', 30)}\n");

            //Indica a ordem de vitória:
            int ordem = 1;

            //Percorre mostrando os ganhadores:
            foreach(var candidato in votosOrdenados)
            {
                //Faz a eliminação dos votos em nulo e em branco:
                if(candidato.ID != "0" && candidato.ID != "")
                {
                    //Informa a posição, o nome, a porcentagem de votos válidos e gerais:
                    Console.WriteLine($"{String.Format("{0, 5}", ordem)} - {String.Format("{0, 20}", candidato.Nome)} - Votos Gerais: {Math.Round((float)candidato.Votos * 100 / votos_gerais, 2)}% - Votos Válidos: {Math.Round((float) candidato.Votos * 100 / votos_validos, 2)}%");

                    //Incrementa mais um na ordem:
                    ordem++;
                }
            }

            //Informa os dados dos votos em Nulo e Branco:
            Console.WriteLine($"{String.Format("{0, 5}", "")} - {String.Format("{0, 20}", "Branco")} - Votos Gerais: {Math.Round((float)votos[votos.Count-2].Votos * 100 / votos_gerais, 2)}%");
            Console.WriteLine($"{String.Format("{0, 5}", "")} - {String.Format("{0, 20}", "Nulo")} - Votos Gerais: {Math.Round((float)votos.Last().Votos * 100 / votos_gerais, 2)}%");
        }

    }

    //Criação da classe dos candidatos:
    public class Candidato
    {
        //Implementação de todas as propriedades:
        public string ID { get; }
        public string Nome { get; }
        public string Disciplina { get; }
        public int Idade { get; }
        public int Votos { get; set; }

        //Método de construção do candidato com ID,nome, disciplina, idade e a quantidade de votos:
        public Candidato(string id, string nome, string disciplina, int idade)
        {
            ID = id;
            Nome = nome;
            Disciplina = disciplina;
            Idade = idade;
            Votos = 0;
        }

        //Método para adicionar um voto:
        public void Votar()
        {
            Votos++;
        }

        public override string ToString()
        {
            return $"Candidato {ID}" +
                $"\nNome: {Nome}" +
                $"\nDisciplina: {Disciplina}" +
                $"\nIdade: {Idade}" +
                $"\nVotos: {Votos}";
        }
    }
}