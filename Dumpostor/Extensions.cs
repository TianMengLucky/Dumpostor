using System;

namespace Dumpostor;

public static class Extensions
{
    public static T[] GetValues<T>() where T : Enum
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    public static string Capitalize(this string word)
    {
        return char.ToUpper(word[0]) + word[1..];
    }

    public static string RemoveSuffix(this string s, string suffix)
    {
        if (s.EndsWith(suffix))
        {
            return s[..^suffix.Length];
        }

        return s;
    }
}
