using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    abstract class Operation : Token
    {
        public virtual string Symbol { get; protected set; }
        public virtual int Priority { get; protected set; }
        public abstract int RequiredOperands { get; }
        public abstract double Perform(params double[] operands);
    }

    internal class Addition : Operation
    {
        public override string Symbol => "+";
        public override int Priority => 1;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] + operands[1];
        }
    }

    internal class Subtraction : Operation
    {
        public override string Symbol => "-";
        public override int Priority => 1;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] - operands[1];
        }
    }

    internal class Multiplication : Operation
    {
        public override string Symbol => "*";
        public override int Priority => 2;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] * operands[1];
        }
    }

    internal class Division : Operation
    {
        public override string Symbol => "/";
        public override int Priority => 2;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] / operands[1];
        }
    }

    internal class Log : Operation
    {
        public override string Symbol => "log";
        public override int Priority => 3;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return Math.Log(operands[0], operands[1]);
        }

        public static bool IsLog(string str)
        {
            return str == "log";
        }
    }

    internal class Power : Operation
    {
        public override string Symbol => "^";
        public override int Priority => 3;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return Math.Pow(operands[1], operands[0]);
        }
    }


    internal class Sqrt : Operation
    {
        public override string Symbol => "sqrt";
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Sqrt(operands[0]);
        }

        public static bool IsSqrt(string str)
        {
            return str == "sqrt";
        }
    }

    internal class Rt : Operation
    {
        public override string Symbol => "rt";
        public override int Priority => 3;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return Math.Pow(operands[0], 1 / operands[1]);
        }

        public static bool IsRt(string str)
        {
            return str == "rt";
        }
    }

    internal class Sin : Operation
    {
        public override string Symbol => "sin";
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Sin(operands[0]);
        }

        public static bool IsSin(string str)
        {
            return str == "sin";
        }
    }

    internal class Cos : Operation
    {
        public override string Symbol => "cos";
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Cos(operands[0]);
        }

        public static bool IsCos(string str)
        {
            return str == "cos";
        }
    }

    internal class Tg : Operation
    {
        public override string Symbol => "tg";
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Tan(operands[0]);
        }

        public static bool IsTg(string str)
        {
            return str == "tg";
        }
    }

    internal class Ctg : Operation
    {
        public override string Symbol => "ctg";
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return 1.0 / Math.Tan(operands[0]);
        }

        public static bool IsCtg(string str)
        {
            return str == "ctg";
        }
    }
}
