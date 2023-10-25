using System.Text;

namespace Final.Domain.Extention;

public static class StringExtention
{
    public static string Join(this List<string> words)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < words.Count; i++)
        {
            sb.Append($"{i + 1}: {words[i]} ");
        }

        return sb.ToString();
    }
}
