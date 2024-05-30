using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    class Token
    {

    }

    class Number : Token
    {
        public double Symbol;

        public Number(double num)
        {
            Symbol = num;
        }
    }

    class Operation : Token
    {
        public char Symbol;
        public int Priorety;
        public Operation(char symbol)
        {
            Symbol = symbol;
            Priorety = GetPriorety(symbol);
        }

        private int GetPriorety(char symbol)
        {
            Dictionary<object, int> prioretyDictionary = new Dictionary<object, int>
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2},
            {'(', 0},
            {')', 5},
        };
            return prioretyDictionary[symbol];
        }
    }

    class Brackets : Token
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

    public class RpnCalculator
    {
        public string result;
        public RpnCalculator(string expression)
        {
            expression = expression.Replace(" ", string.Empty);
            var tokens = GetToken(expression);
            var rpn = RPN(tokens);
            result = string.Join("", Result(rpn));
        }
        private static List<Token> GetToken(string str)
        {
            List<Token> tokens = new List<Token>();
            string num = string.Empty;

            static void CheckEmpty(List<Token> tokens, string num)
            {
                if (num != string.Empty)
                {
                    tokens.Add(new Number(double.Parse(num)));
                }
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == ',')
                {
                    num += str[i];
                }
                else if (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/')
                {
                    CheckEmpty(tokens, num);
                    num = string.Empty;
                    tokens.Add(new Operation(str[i]));
                }
                else if (str[i] == '(' || str[i] == ')')
                {
                    CheckEmpty(tokens, num);
                    num = string.Empty;
                    tokens.Add(new Brackets(str[i]));
                }
            }
            CheckEmpty(tokens, num);
            return tokens;
        }

        private static List<Token> RPN(List<Token> tokens)
        {
            List<Token> prn = new List<Token>();
            Stack<Token> stack = new Stack<Token>();
            foreach (Token token in tokens)
            {
                if (stack.Count == 0 && !(token is Number))
                {
                    stack.Push(token);
                    continue;
                }
                if (token is Operation)
                {
                    if (stack.Peek() is Brackets)
                    {
                        stack.Push(token);
                        continue;
                    }

                    Operation oper = (Operation)token;
                    Operation oper2 = (Operation)stack.Peek();
                    if (oper.Priorety > oper2.Priorety)
                    {
                        stack.Push(token);
                    }
                    else if (oper.Priorety <= oper2.Priorety)
                    {
                        while (stack.Count > 0 && !(token is Brackets))
                        {
                            prn.Add(stack.Pop());
                        }
                        stack.Push(token);
                    }
                }
                else if (token is Brackets)
                {
                    if (((Brackets)token).IsClosing)
                    {
                        while (!(stack.Peek() is Brackets))
                        {
                            prn.Add(stack.Pop());
                        }
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(token);
                    }
                }
                else if (token is Number)
                {
                    prn.Add(token);
                }
            }
            while (stack.Count > 0)
            {
                prn.Add(stack.Pop());
            }
            return prn;
        }

        private static Stack<double> Result(List<Token> expression)
        {
            Stack<double> stack = new Stack<double>();
            foreach (Token token in expression)
            {
                if (token is Number number)
                {
                    number = (Number)token;
                    stack.Push(number.Symbol);
                }
                else
                {
                    var second = stack.Pop();
                    var first = stack.Pop();
                    stack.Push(Calculate(token, first, second));
                }
            }
            return stack;
        }

        private static double Calculate(Token op, double first, double second)
        {
            var oper = (Operation)op;
            switch (oper.Symbol)
            {
                case '*': return first * second;
                case '/': return first / second;
                case '+': return first + second;
                case '-': return first - second;
                default: return double.NaN;
            }
        }

    }

}