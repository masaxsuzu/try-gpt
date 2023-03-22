namespace TryGpt.Tests.Calculator;

using Xunit;

public class TestCalculator
{
    [Fact]
    public void Eval_Addition_Success()
    {
        Calculator calculator = new Calculator();
        int result = calculator.Eval("1+2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Eval_Subtraction_Success()
    {
        Calculator calculator = new Calculator();
        int result = calculator.Eval("5-2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Eval_Multiplication_Success()
    {
        Calculator calculator = new Calculator();
        int result = calculator.Eval("3*4");
        Assert.Equal(12, result);
    }

    [Fact]
    public void Eval_Division_Success()
    {
        Calculator calculator = new Calculator();
        int result = calculator.Eval("10/5");
        Assert.Equal(2, result);
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
