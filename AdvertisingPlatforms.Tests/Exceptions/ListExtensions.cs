namespace AdvertisingPlatforms.Tests.Exceptions;

public static class ListExtensions
{
    public static bool IsContainsSameElements<T>(this List<T> leftList, List<T> rightList)
    {
        return leftList.OrderBy(value => value).SequenceEqual(rightList.OrderBy(value => value));
    }
}
