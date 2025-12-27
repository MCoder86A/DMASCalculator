using System.Collections.Generic;

namespace Utility
{
    public class DMASEvaluator
    {
        //PUBLIC API FOR EVALUATION
        public static double Evaluate(string expression)
        {
            List<string> tokens = Tokenize(expression);
            return Evaluate(tokens);
        }

        //CONVERT EXPRESSION INTO A LIST OF STRING CONTAINING TERMS AND OPERATOR
        static List<string> Tokenize(string expression)
        {
            List<string> tokens = new ();
            string num = "";

            foreach (char c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                    num += c;
                else if ("+-*/".Contains(c))
                {
                    if (num.Length > 0)
                    {
                        tokens.Add(num);
                        num = "";
                    }
                    tokens.Add(c.ToString());
                }
            }

            if (num.Length > 0)
                tokens.Add(num);

            return tokens;
        }

        //USED STACK TO EVALUATE EXPRESSIONS WITH OPERATOR PRECEDENCE
        static double Evaluate(List<string> tokens)
        {
            Stack<double> stack = new();
            char op = '+';

            //ALL MULTIPLICATION AND DIVISION ARE EVALUATED IN THE FIRST PASS
            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    if (op == '+') stack.Push(num);
                    else if (op == '-') stack.Push(-num);
                    else if (op == '*') stack.Push(stack.Pop() * num);
                    else if (op == '/') stack.Push(stack.Pop() / num);
                }
                else
                {
                    op = token[0];
                }
            }

            double result = 0;
            //REMAINING ADDITION AND SUBTRACTION ARE EVALUATED IN THE SECOND PASS
            while (stack.Count > 0)
                result += stack.Pop();

            return result;
        }
    }
}
