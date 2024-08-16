internal class Program
{
    private static void Main(string[] args)
    {
    }

    public static int DiscIntersections(int[] input)
    {
        //can be packed into (input.Length**2)/2 bits structure if you need to consume less memory
        bool[,] cache = new bool[input.Length, input.Length];
        int count = 0;

        return count;
    }

    public static int DiscIntersectionsGoogleEdition(int[] input)
    {
        return 0;
    }

    //O(2*radius-1) === [O(1), O(N)]
    private static int UpdateIntersections(bool[,] cache, int center, int radius)
    {
        int total = 0;
        int size = cache.GetLength(1);

        //O(2*radius-1)
        for (int i = center - radius; i <= center + radius; i++)
        {
            if (i == center || i < 0 || i >= size)
                continue;

            if (i < center)
            {
                if (!cache[i, center])
                {
                    cache[i, center] = true;
                    total++;
                }
            }
            else if (!cache[center, i])
            {
                cache[center, i] = true;
                total++;
            }
        }

        return total;
    }
}