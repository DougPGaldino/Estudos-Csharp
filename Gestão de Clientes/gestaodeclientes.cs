using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();
        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }

        static void Main(string[] args)
        {
            Carregar(); // Carregar dados ao iniciar o programa

            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de gestão de Clientes - Seja bem vindo!");
                Console.WriteLine("1 - Listagem\n2- Adicionar\n3- Remover\n4- Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }

                Console.Clear();
            }
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("Cadastro concluído, aperte ENTER para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes");
                int i = 1;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado no sistema.");
            }

            Console.WriteLine("Aperte ENTER para sair.");
            Console.ReadLine();
        }

        static void Remover()
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente para remover.");
                Console.WriteLine("Aperte ENTER para sair.");
                Console.ReadLine();
                return;
            }

            // Exibir clientes diretamente na função Remover
            Console.WriteLine("Lista de clientes:");
            int i = 1;
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine($"ID: {i}");
                Console.WriteLine($"Nome: {cliente.nome}");
                Console.WriteLine($"Email: {cliente.email}");
                Console.WriteLine($"CPF: {cliente.cpf}");
                Console.WriteLine();
                i++;
            }

            Console.WriteLine("Digite o ID do cliente que deseja remover:");
            int id = int.Parse(Console.ReadLine());

            // Verificar se o ID está no intervalo correto
            if (id > 0 && id <= clientes.Count)
            {
                // Exibir detalhes do cliente que será removido
                Cliente cliente = clientes[id - 1];
                Console.WriteLine($"Você deseja remover o seguinte cliente?\nNome: {cliente.nome}\nEmail: {cliente.email}\nCPF: {cliente.cpf}");
                Console.WriteLine("Digite 'S' para confirmar ou qualquer outra tecla para cancelar.");
                string confirmacao = Console.ReadLine();

                if (confirmacao.ToUpper() == "S")
                {
                    clientes.RemoveAt(id - 1); // Remover o cliente
                    Salvar(); // Salvar a lista atualizada no arquivo após a remoção
                    Console.WriteLine("Cliente removido com sucesso! Aperte ENTER para sair.");
                }
                else
                {
                    Console.WriteLine("Remoção cancelada. Aperte ENTER para sair.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Aperte ENTER para sair.");
            }

            Console.ReadLine();
        }




        static void Salvar()
        {
            using (StreamWriter writer = new StreamWriter("clients.txt"))
            {
                foreach (Cliente cliente in clientes)
                {
                    writer.WriteLine($"{cliente.nome},{cliente.email},{cliente.cpf}");
                }
            }
        }

        static void Carregar()
        {
            if (File.Exists("clients.txt"))
            {
                using (StreamReader reader = new StreamReader("clients.txt"))
                {
                    string linha;
                    while ((linha = reader.ReadLine()) != null)
                    {
                        string[] dados = linha.Split(',');
                        Cliente cliente = new Cliente
                        {
                            nome = dados[0],
                            email = dados[1],
                            cpf = dados[2]
                        };
                        clientes.Add(cliente);
                    }
                }
            }
        }
    }
}
