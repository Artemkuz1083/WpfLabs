using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    internal class Token
    {

    }

    internal class Varieble : Token
    {
        public bool CheckX;

        public Varieble(char symbol)
        {
            CheckX = IsVarieble(symbol);
        }

        private static bool IsVarieble(char symbol)
        {
            return symbol == 'x' || symbol == 'X';
        }
    }

    internal class Number : Token
    {
        public double Symbol;

        public Number(double num)
        {
            Symbol = num;
        }
    }

    internal class Brackets : Token
    {
        public char Symbol;
        public bool IsClosing;

        public Brackets(char symbol)
        {
            if (symbol != '(' && symbol != ')')
                throw new ArgumentException("This is not valid bracket");

            IsClosing = symbol == ')';
            Symbol = symbol;
        }
    }
}
