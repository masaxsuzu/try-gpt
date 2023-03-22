namespace TryGpt;

using System.Text;

public class Calculator
{
    private static readonly char[] Operators = { '+', '-', '*', '/' };

    public double Eval(string expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            throw new ArgumentException("式が空です。");
        }

        double result = 0;
        string[] tokens = Tokenize(expression);
        if (tokens.Length > 0)
        {
            result = Evaluate(tokens);
        }

        return result;
    }

    private string[] Tokenize(string expression)
    {
        List<string> tokens = new List<string>();
        StringBuilder buffer = new StringBuilder();

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (Char.IsDigit(c) || c == '.')
            {
                buffer.Append(c);
            }
            else if (Operators.Contains(c))
            {
                if (buffer.Length > 0)
                {
                    tokens.Add(buffer.ToString());
                    buffer.Clear();
                }

                if (c == '-' && (i == 0 || Operators.Contains(expression[i - 1])))
                {
                    buffer.Append(c);
                }
                else
                {
                    tokens.Add(c.ToString());
                }
            }
            else if (c == '(' || c == ')')
            {
                if (buffer.Length > 0)
                {
                    tokens.Add(buffer.ToString());
                    buffer.Clear();
                }

                tokens.Add(c.ToString());
            }
        }

        if (buffer.Length > 0)
        {
            tokens.Add(buffer.ToString());
        }

        return tokens.ToArray();
    }



    private double Evaluate(string[] tokens)
    {
        Stack<double> values = new Stack<double>();
        Stack<char> operators = new Stack<char>();

        for (int i = 0; i < tokens.Length; i++)
        {
            string token = tokens[i];

            if (double.TryParse(token, out double value))
            {
                values.Push(value);
            }
            else if (Operators.Contains(token[0]))
            {
                while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token[0]))
                {
                    EvaluateTop(values, operators);
                }

                operators.Push(token[0]);
            }
            else if (token == "(")
            {
                operators.Push('(');
            }
            else if (token == ")")
            {
                while (operators.Peek() != '(')
                {
                    EvaluateTop(values, operators);
                }

                operators.Pop();
            }
        }

        while (operators.Count > 0)
        {
            EvaluateTop(values, operators);
        }

        return values.Pop();
    }

    private int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            default:
                return 0;
        }
    }

    private void EvaluateTop(Stack<double> values, Stack<char> operators)
    {
        if (values.Count < 2)
        {
            throw new ArgumentException("無効な式です。");
        }

        char op = operators.Pop();
        double right = values.Pop();
        double left = values.Pop();

        switch (op)
        {
            case '+':
                values.Push(left + right);
                break;
            case '-':
                values.Push(left - right);
                break;
            case '*':
                values.Push(left * right);
                break;
            case '/':
                if (right == 0)
                {
                    throw new DivideByZeroException();
                }

                values.Push(left / right);
                break;
            default:
                throw new ArgumentException("無効な演算子です。");
        }
    }
}
