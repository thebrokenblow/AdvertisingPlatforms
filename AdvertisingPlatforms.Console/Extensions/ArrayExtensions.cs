namespace AdvertisingPlatforms.Console.Extensions;

public static class ArrayExtensions
{
    public static T Second<T>(this T[] array)
    {
        if (array == null || array.Length < 2)
        {
            throw new ArgumentException("Массив должен содержать как минимум два элемента.");
        }
        return array[1];
    }
}