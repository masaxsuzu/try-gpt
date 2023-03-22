namespace TryGpt.Tests.Calculator;

using TryGpt;
using Xunit;

public class TestCalculator
{
    [Theory]
    [InlineData("1+2", 3)]
    [InlineData("0+0", 0)]
    [InlineData("100+200", 300)]
    [InlineData("-1+2", 1)]
    [InlineData("1.5+2.5", 4)]
    public void Eval_Addition_Success(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        var result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1-2", -1)]
    [InlineData("0-0", 0)]
    [InlineData("200-100", 100)]
    [InlineData("-1-2", -3)]
    [InlineData("1.5-2.5", -1)]
    public void Eval_Subtraction_Success(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        var result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2*3", 6)]
    [InlineData("0*100", 0)]
    [InlineData("0*-100", 0)]
    [InlineData("-1*2", -2)]
    [InlineData("1.5*2.5", 3.75)]
    public void Eval_Multiplication_Success(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        var result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("10/5", 2)]
    [InlineData("10/4", 2.5)]
    [InlineData("-10/5", -2)]
    [InlineData("1/3", 1.0 / 3)]
    public void Eval_Division_Success(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        var result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("10/4", 2.5)]
    public void Eval_Division_ReturnsCorrectResult(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        double result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1+2*3+4", 11)]
    [InlineData("(1+2)*3+4", 13)]
    [InlineData("(1+2)*(3+4)", 21)]
    [InlineData("10/5*2+6", 10)]
    [InlineData("10/(5*2)+6", 7)]
    [InlineData("10.5/4+3.2", 5.8250000000000002)]
    [InlineData("10+2.5*3", 17.5)]
    [InlineData("10-3.5*2", 3)]
    public void Eval_OperatorPrecedence_Success(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        double result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("(1+2)*3+4", 13)]
    [InlineData("(1+2)*(3+4)", 21)]
    [InlineData("10/(5*2)+6", 7)]
    public void Eval_Parentheses_Success(string expression, double expected)
    {
        Calculator calculator = new Calculator();
        double result = calculator.Eval(expression);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Eval_EmptyExpression_ExceptionThrown()
    {
        Calculator calculator = new Calculator();
        Assert.Throws<ArgumentException>(() => calculator.Eval(""));
    }

    [Fact]
    public void Eval_InvalidExpression_ExceptionThrown()
    {
        Calculator calculator = new Calculator();
        Assert.Throws<ArgumentException>(() => calculator.Eval("1+2+"));
    }

}
