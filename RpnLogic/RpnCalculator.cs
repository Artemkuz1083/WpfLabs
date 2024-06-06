using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
namespace Rpn.Logic
{
    public class RpnCalculator
    {
        public string result;

        public RpnCalculator(string expression, double varX)
        {
            expression = expression.Replace(" ", string.Empty);
            var tokens = GetToken(expression);
            var rpn = RPN(tokens);
            result = string.Join("", Result(rpn, varX));
        }

        private static List<Token> GetToken(string str)
        {
            List<Token> tokens = new List<Token>();
            string num = string.Empty;

            static void CheckEmpty(List<Token> tokens, string num)
            {
                if (num != string.Empty)
                {
                    tokens.Add(new Number(double.Parse(num, CultureInfo.InvariantCulture)));
                }
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == '.')
                {
                    num += str[i];
                }
                else if (str[i] == ',')
                {
                    if (new Varieble(str[i]).CheckX)
                    {
                        tokens.Add(new Varieble(str[i]));
                    }
                    else if(num != string.Empty)
                    {
                        tokens.Add(new Number(double.Parse(num, CultureInfo.InvariantCulture)));
                        num = string.Empty;
                    }
                }
                else if (new Varieble(str[i]).CheckX)
                {
                    tokens.Add(new Varieble(str[i]));
                }
                else if (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/' || str[i] == '^')
                {
                    CheckEmpty(tokens, num);
                    num = string.Empty;
                    switch (str[i])
                    {
                        case '*': tokens.Add(new Multiplication());
                        continue;
                        case '/': tokens.Add(new Division());
                        continue;
                        case '+': tokens.Add(new Addition());
                        continue;
                        case '-': tokens.Add(new Subtraction());
                        continue;
                        case '^': tokens.Add(new Power());
                        continue;
                    }

                }
                else if (str[i] == '(' || str[i] == ')')
                {
                    CheckEmpty(tokens, num);
                    num = string.Empty;
                    tokens.Add(new Brackets(str[i]));
                }
                else if (char.IsLetter(str[i]))
                {
                    string func = WhtIsFunc(i, str);
                    switch (func)
                    {
                        case "log":
                            tokens.Add(new Log()); i+=2;
                            continue;
                        case "sqrt":
                            tokens.Add(new Sqrt()); i += 3;
                            continue;
                        case "rt":
                            tokens.Add(new Rt()); i += 1;
                            continue;
                        case "sin":
                            tokens.Add(new Sin()); i += 2;
                            continue;
                        case "cos":
                            tokens.Add(new Cos()); i += 2;
                            continue;
                        case "tg":
                            tokens.Add(new Tg()); i += 1;
                            continue;
                        case "ctg":
                            tokens.Add(new Ctg()); i += 2;
                            continue;
                    }
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
                if (stack.Count == 0 && !(token is Number) && !(token is Varieble))
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
                    if (oper.Priority > oper2.Priority)
                    {
                        stack.Push(token);
                    }
                    else if (oper.Priority <= oper2.Priority)
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
                else if (token is Number || token is Varieble)
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

        private static Stack<double> Result(List<Token> expression, double varX)
        {
            Stack<double> stack = new Stack<double>();
            foreach (Token token in expression)
            {
                if (token is Number number)
                {
                    number = (Number)token;
                    stack.Push(number.Symbol);
                }
                else if (token is Varieble)
                {
                    stack.Push(varX);
                }
                else
                {
                    int requiredOperands = ((Operation)token).RequiredOperands;

                    double[] operands = new double[requiredOperands];
                    for (int i = operands.Length - 1; i >= 0; i--)
                    {
                        operands[i] = stack.Pop();
                    }

                    double result = ((Operation)token).Perform(operands);
                    stack.Push(result);
                }
            }
            return stack;
        }

        private static string WhtIsFunc(int i, string expression)
        {
            string func = string.Empty;

            while (expression[i] != '(')
            {
                func += expression[i];
                i++;
            }
            return func.ToLower();
        }
    }
}