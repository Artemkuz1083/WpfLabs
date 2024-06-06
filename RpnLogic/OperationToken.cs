using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpn.Logic
{
    abstract class Operation : Token
    {
        public virtual int Priority { get; }
        public abstract int RequiredOperands { get; }
        public abstract double Perform(params double[] operands);
    }

    internal class Addition : Operation
    {
        public override int Priority => 1;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] + operands[1];
        }
    }

    internal class Subtraction : Operation
    {
        public override int Priority => 1;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] - operands[1];
        }
    }

    internal class Multiplication : Operation
    {
        public override int Priority => 2;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] * operands[1];
        }
    }

    internal class Division : Operation
    {
        public override int Priority => 2;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return operands[0] / operands[1];
        }
    }

    internal class Log : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return Math.Log(operands[1], operands[0]);
        }
    }

    internal class Power : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return Math.Pow(operands[0], operands[1]);
        }
    }


    internal class Sqrt : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Sqrt(operands[0]);
        }
    }

    internal class Rt : Operation
    {        
        public override int Priority => 3;
        public override int RequiredOperands => 2;

        public override double Perform(params double[] operands)
        {
            return Math.Pow(operands[0], 1 / operands[1]);
        }
    }

    internal class Sin : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Sin(operands[0]);
        }
    }

    internal class Cos : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Cos(operands[0]);
        }
    }

    internal class Tg : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return Math.Tan(operands[0]);
        }
    }

    internal class Ctg : Operation
    {
        public override int Priority => 3;
        public override int RequiredOperands => 1;

        public override double Perform(params double[] operands)
        {
            return 1.0 / Math.Tan(operands[0]);
        }
    }
}
