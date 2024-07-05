namespace TO.Core.Utils;

public static class Conversor
{
    public static string ApenasNumeros(string valor)
    {
        if (valor == null) return string.Empty;
        var onlyNumber = "";
        foreach (var s in valor)
        {
            if (char.IsDigit(s))
            {
                onlyNumber += s;
            }
        }
        return onlyNumber.Trim();
    }
}
