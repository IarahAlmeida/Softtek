using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public readonly int Numero;
        public string Titular { get; set; }
        public double Saldo { get; private set; }
        private const double _taxa = 3.5;

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {
            if (depositoInicial >= 0)
            {
                Saldo = depositoInicial;
            }
            else
            {
                throw new Exception("Deposito inicial não pode ser menor que 0.");
            }
        }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            Saldo -= (quantia + _taxa);
        }

        public override string ToString()
        {
            return $"Conta {Numero.ToString()}, Titular: {Titular.ToString()}, Saldo: $ {Saldo.ToString("0.00")}";
        }
    }
}