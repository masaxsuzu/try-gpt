namespace TryGpt;
public class Calculator
{
    public int Eval(string expression)
    {
        int result = 0;
        if (string.IsNullOrEmpty(expression))
        {
            throw new ArgumentException("式が空です。");
        }

        try
        {
            result = (int)new DataTable().Compute(expression, null);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("式の評価に失敗しました。", ex);
        }

        return result;
    }
}
