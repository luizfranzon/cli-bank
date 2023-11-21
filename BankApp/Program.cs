using System.Globalization;
using BankApp;

Console.Clear();

Account[] accounts = new Account[10];
var accountsQuantity = 0;
var isUserLogged = false;
Account? loggedUserData = null;
int? userLoggedAccountNumber = null;

while (true) {
    ShowLogo();

    if (!isUserLogged) {
        Console.WriteLine("----------=@=----------");
        Console.WriteLine("[1]- Acessar conta");
        Console.WriteLine("[2]- Criar conta");
        Console.WriteLine("\n[0]- Sair");
        Console.WriteLine("----------=@=----------");

        Console.WriteLine("Escolha o número da opção desejada.\n");
        Console.Write("> ");
        string selectedOption = Console.ReadLine() ?? "";

        switch (selectedOption) {
            case "1":
                AccessAccount();
                break;
            case "2":
                CreateAccount();
                break;
            case "0":
                Console.WriteLine("Tchau tchau! 👋");
                Thread.Sleep(2000);
                Environment.Exit(0);
                break;
            case "100":
                Debug();
                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente. \n");
                break;
        }
    }
    else {
        Console.Clear();
        ShowLogo();

        Console.WriteLine($"Seja bem-vindo, {loggedUserData!.OwnerName.Split(" ")[0]}");
        Console.WriteLine(
            "Saldo em conta: R$" + accounts[(int)(userLoggedAccountNumber - 1)!].GetBalance()
                .ToString("F2", CultureInfo.InvariantCulture));

        Console.WriteLine("\n----------=@=----------");
        Console.WriteLine("[1]- Depositar");
        Console.WriteLine("[2]- Sacar");
        Console.WriteLine("[3]- Transferir");
        Console.WriteLine("\n[0]- Sair da conta");
        Console.WriteLine("----------=@=----------");

        string selectedOption = Console.ReadLine() ?? "";

        switch (selectedOption) {
            case "1":
                DepositToAccount();
                break;
            case "2":
                WithdrawFromAccount();
                break;
            case "3":
                TransferToOtherAccount();
                break;
            case "0":
                Console.Clear();
                isUserLogged = false;
                loggedUserData = null;
                Console.WriteLine("Você deslogou da sua conta. tchau, tchau! \n");
                break;

                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente. \n");
                break;
        }
    }
}

static void ShowLogo() {
    Console.WriteLine(@"
█▀▀ █░░ █ █▄▄ ▄▀█ █▄░█ █▄▀
█▄▄ █▄▄ █ █▄█ █▀█ █░▀█ █░█
");
}

void AccessAccount() {
    Console.Clear();
    Console.WriteLine("Insira os seus dados de login: ");
    Console.Write("Número da conta: ");
    var typedLoginAccountNumber = int.Parse(Console.ReadLine()!) - 1;

    if (accounts[typedLoginAccountNumber] != null) {
        Console.Write("Insira a sua senha: ");
        var typedLoginAccountPassword = Console.ReadLine();

        if (accounts[typedLoginAccountNumber].HashedPassword == Utils.HashPassword(typedLoginAccountPassword!)) {
            isUserLogged = true;
            loggedUserData = accounts[typedLoginAccountNumber];
            userLoggedAccountNumber = loggedUserData.Number;
        }
        else {
            Console.WriteLine("Senha incorreta!");
        }
    }
    else {
        Console.Clear();
        Console.WriteLine("ERRO: Essa conta não existe!");
    }
}

void CreateAccount() {
    Console.Clear();

    Console.WriteLine("Para criar sua conta, insira os seus dados abaixo: ");

    Console.Write("Nome completo: ");
    string typedName = Console.ReadLine()!;

    Console.Write("Crie uma senha: ");
    string typedPassword = Console.ReadLine()!;

    Console.Write("Insira a sua idade: ");
    int typedAge = int.Parse(Console.ReadLine()!);

    accounts[accountsQuantity] = new Account(typedName, typedPassword, typedAge, accountsQuantity + 1);
    accountsQuantity += 1;

    Console.Clear();
    Console.WriteLine($"\nParabéns, {typedName.Split(" ")[0]}! Sua conta foi criada com sucesso!");
    Console.WriteLine("Anote o número da sua conta: " + accountsQuantity);
}

void DepositToAccount() {
    Console.Clear();
    Console.WriteLine("Quantos R$ (reais) deseja depositar?");
    Console.Write("Valor: R$");
    double depositAmmount = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

    accounts[loggedUserData.Number - 1].Deposit(depositAmmount);
}

void WithdrawFromAccount() {
    Console.Clear();
    Console.WriteLine("Quantos R$ (reais) deseja sacar?");
    Console.Write("Valor: R$");
    double withdrawAmmount = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

    accounts[loggedUserData.Number - 1].Withdraw(withdrawAmmount);
}

void TransferToOtherAccount() {
    Console.Clear();
    Console.WriteLine("Digite o número da conta que irá receber a transferencia");
    Console.Write("Conta: ");
    int destinationAccountNumber = int.Parse(Console.ReadLine()!);

    Console.Write("Valor: R$");
    double transferAmmount = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

    if (accounts[destinationAccountNumber - 1] != null) {
        if (accounts[(int)(userLoggedAccountNumber - 1)!].GetBalance() >= transferAmmount) {
            if (destinationAccountNumber != userLoggedAccountNumber) {
                accounts[(int)(userLoggedAccountNumber - 1)!].Transfer(transferAmmount, null);
                accounts[destinationAccountNumber - 1].ReceiveTransfer(transferAmmount, null);
            }
            else {
                Console.Clear();
                Console.WriteLine("ERRO: Você não pode transferir um valor para você mesmo!\n");
                Thread.Sleep(3000);
            }
        }
        else {
            Console.Clear();
            Console.WriteLine("ERRO: Valor em conta insuficiente para transferir");
            Thread.Sleep(3000);
        }
    }
    else {
        Console.Clear();
        Console.WriteLine("ERRO: A conta digitada não existe");
        Thread.Sleep(3000);
    }
}

//eu sei que eu deveria remover isso daqui, mas é intencional, deixa aqui mesmo
void Debug() {
    Console.WriteLine("Quantidade de contas criadas: " + accountsQuantity + "\n");
    if (accounts.Length != 0) {
        for (int i = 0; i < accountsQuantity; i++) {
            Console.WriteLine($"Conta {i + 1}# | Nome: {accounts[i].OwnerName}");
        }
    }

    Thread.Sleep(5000);
    Console.Clear();
}