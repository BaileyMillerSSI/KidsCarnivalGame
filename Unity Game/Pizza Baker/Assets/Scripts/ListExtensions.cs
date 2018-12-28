

using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    private static System.Random prng = new System.Random();

    public static T  GetRandom<T>(this IEnumerable<T> list)
    {
        return list.ElementAt(prng.Next(list.Count()));
    }
}
