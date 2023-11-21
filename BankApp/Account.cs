using System.Security.Cryptography;
using System.Text;

namespace BankApp;

public class Account {
    private double _balance;
    public string HashedPassword { get; }
    public int Number { get; set; }
    public string OwnerName { get; set; }
    public int OwnerAge { get; set; }

    public Account(string ownerName, string password, int age, int accountNumber) {
        OwnerName = ownerName;
        OwnerAge = age;
        HashedPassword = Utils.HashPassword(password);
        _balance = 0.00;
        Number = accountNumber;
    }

    public double GetBalance() {
        return _balance;
    }

    public void Deposit(double depositAmmount) {
        _balance += depositAmmount;
    }

    public void Withdraw(double withdrawAmmount) {
        if (withdrawAmmount <= _balance) {
            _balance -= withdrawAmmount;

            Console.WriteLine($"\nSacado R${withdrawAmmount}. O valor foi retirado do seu saldo atual.");
            Thread.Sleep(2000);
        }
        else {
            Console.WriteLine("\nERRO: Você não possui esse valor em conta.\n");
            Thread.Sleep(2000);
        }
    }

    public void Transfer(double transferAmmount, int? destinationAccountNumber) {
        _balance -= transferAmmount;
    }

    public void ReceiveTransfer(double transferAmmount, int? destinationAccountNumber) {
        _balance += transferAmmount;
    }

    public bool CheckPasswordHash(string password) {
        if (Utils.HashPassword(password) == HashedPassword) {
            return true;
        }

        return false;
    }
}