namespace AdvertisingPlatforms.IntegrationTests.Utilities.Exceptions;

public static class ListExtensions
{
    public static bool IsContainsSameElements<T>(this List<T> leftList, List<T> rightList)
    {
        if (leftList.Count != rightList.Count)
        {
            return false;
        }

        var leftHashSet = new HashSet<T>(leftList);
        var rightHashSet = new HashSet<T>(rightList);

        return leftHashSet.SetEquals(rightHashSet);
    }
}