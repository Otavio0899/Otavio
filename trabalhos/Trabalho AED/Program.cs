using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Trabalho_AED;

class Program
{
    static List<Funcionario> funcionarios = new List<Funcionario>();
    static Dictionary<string, string> users = new Dictionary<string, string>();
    static bool isLoggedIn = false;
    static ArvoreBinaria arvore = new ArvoreBinaria();
    static string dataFilePath = "data.json";
    
    static void Main(string[] args)
    {
        LoadData();

        while (true)
        {
            if (isLoggedIn)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Cadastrar funcionário");
                Console.WriteLine("2 - Visualizar funcionários");
                Console.WriteLine("3 - Editar funcionário");
                Console.WriteLine("4 - Excluir funcionário");
                Console.WriteLine("5 - Ordenar por ordem alfabética");
                Console.WriteLine("6 - Ordenar por salário");
                Console.WriteLine("7 - Ordenar por código");
                Console.WriteLine("8 - Sair");

                string opcao = Console.ReadLine();
                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarFuncionario();
                        break;
                    case "2":
                        VisualizarFuncionarios();
                        break;
                    case "3":
                        EditarFuncionario();
                        break;
                    case "4":
                        ExcluirFuncionario();
                        break;
                    case "5":
                        OrdenarPorOrdemAlfabetica();
                        break;
                    case "6":
                        OrdenarPorSalario();
                        break;
                    case "7":
                        OrdenarPorCodigo();
                        break;
                    case "8":
                        Logout();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Digite novamente.");
                        break;
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Registrar");
                Console.WriteLine("3 - Sair");

                string opcao = Console.ReadLine();
                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        Login();
                        Console.ReadKey();
                        break;
                    case "2":
                        Register();
                        Console.ReadKey();
                        break;
                    case "3":
                        SaveData();
                        Environment.Exit(0);
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Digite novamente.");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
                Console.WriteLine();                
            }
        }
    }

    static void CadastrarFuncionario()
    {
        Console.Write("Digite o nome do funcionário: ");
        string nome = Console.ReadLine();

        if (!VerificarApenasLetras(nome))
        {
            Console.WriteLine("O nome do funcionário deve conter apenas letras.");
            return;
        }

        Console.Write("Digite a função do funcionário: ");
        string funcao = Console.ReadLine();

        if (!VerificarApenasLetras(funcao))
        {
            Console.WriteLine("A função do funcionário deve conter apenas letras.");
            return;
        }

        Console.Write("Digite o salário do funcionário: ");

        double salario;

        if (!double.TryParse(Console.ReadLine(), out salario) || salario <= 0)
        {
            Console.WriteLine("Salário inválido. Certifique-se de inserir um número válido.");

            return; 
        }
        int codigo = 1;

        codigo = GerarCodigoUnico();

        Funcionario funcionario = new Funcionario(nome, funcao, salario, codigo);
        funcionarios.Add(funcionario);

        Console.WriteLine("Funcionário cadastrado com sucesso!");

    }
    static bool VerificarApenasLetras(string input)
    {
        foreach (char c in input)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                return false;
            }
        }
        return true;
    }   

    static void VisualizarFuncionarios()
    {
        if (funcionarios.Count == 0)
        {
            Console.WriteLine("Nenhum funcionário cadastrado.");
            return;
        }

        foreach (Funcionario funcionario in funcionarios)
        {
            Console.WriteLine(funcionario.ToString());
        }
    }

    static void EditarFuncionario()
    {
        Console.Write("Digite o código do funcionário a ser editado: ");
        int codigo = int.Parse(Console.ReadLine());

        Funcionario funcionario = funcionarios.Find(f => f.Codigo == codigo);

        if (funcionario == null)
        {
            Console.WriteLine("Funcionário não encontrado.");
            return;
        }

        Console.WriteLine("Dados atuais do funcionário:");

        Console.WriteLine(funcionario.ToString());

        Console.WriteLine("Digite os novos dados do funcionário:");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Função: ");
        string funcao = Console.ReadLine();

        Console.Write("Salário: ");
        double salario;
        if (!double.TryParse(Console.ReadLine(), out salario) || salario <= 0)
        {
            Console.WriteLine("Salário inválido. Certifique-se de inserir um número válido.");
            return;
        }

        funcionario.Nome = nome;
        funcionario.Funcao = funcao;
        funcionario.Salario = salario;

        Console.WriteLine("Funcionário atualizado com sucesso!");
    }

    static void ExcluirFuncionario()
    {
        Console.Write("Digite o código do funcionário a ser excluído: ");
        int codigo = int.Parse(Console.ReadLine());

        Funcionario funcionario = funcionarios.Find(f => f.Codigo == codigo);

        if (funcionario == null)
        {
            Console.WriteLine("Funcionário não encontrado.");
            return;
        }

        funcionarios.Remove(funcionario);

        // Remover o código da árvore binária
        arvore.Remover(codigo);

        Console.WriteLine("Funcionário excluído com sucesso!");
    }

    static void OrdenarPorOrdemAlfabetica()
    {
        funcionarios.Sort((f1, f2) => string.Compare(f1.Nome, f2.Nome));

        Console.WriteLine("Funcionários ordenados por ordem alfabética:");
        VisualizarFuncionarios();
    }

    static void OrdenarPorSalario()
    {
        funcionarios.Sort((f1, f2) => f1.Salario.CompareTo(f2.Salario));

        Console.WriteLine("Funcionários ordenados por salário:");
        VisualizarFuncionarios();
    }

    static void OrdenarPorCodigo()
    {
        funcionarios.Sort((f1, f2) => f1.Codigo.CompareTo(f2.Codigo));

        Console.WriteLine("Funcionários ordenados por código:");
        VisualizarFuncionarios();
    }

    static void Login()
    {
        Console.Write("Digite o nome de usuário: ");
        string username = Console.ReadLine();

        Console.Write("Digite a senha: ");
        string password = Console.ReadLine();

        if (users.ContainsKey(username) && users[username] == password)
        {
            isLoggedIn = true;
            Console.WriteLine("Login bem-sucedido!");
        }
        else
        {
            Console.WriteLine("Usuário ou senha incorretos.");
        }
    }

    static void Register()
    {
        Console.Write("Digite o nome de usuário: ");
        string username = Console.ReadLine();

        if (users.ContainsKey(username))
        {
            Console.WriteLine("Nome de usuário já existe. Tente outro.");
            return;
        }

        Console.Write("Digite a senha: ");
        string password = Console.ReadLine();

        users.Add(username, password);

        Console.WriteLine("Registro concluído com sucesso!");
    }

    static void Logout()
    {
        isLoggedIn = false;
        Console.WriteLine("Logout realizado com sucesso!");
    }

    static void LoadData()
    {
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

            if (data.ContainsKey("funcionarios"))
                funcionarios = JsonConvert.DeserializeObject<List<Funcionario>>(data["funcionarios"].ToString());

            if (data.ContainsKey("users"))
                users = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["users"].ToString());
        }
    }

    static void SaveData()
    {
        var data = new Dictionary<string, object>
        {
            { "funcionarios", funcionarios },
            { "users", users }
        };

        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(dataFilePath, jsonData);
    }

    static int GerarCodigoUnico()
    {
        int codigo = 1;

        while (funcionarios.Exists(f => f.Codigo == codigo))
        {
            codigo++;
        }

        return codigo;
    }
}
