internal class Program
{
    private static void Main(string[] args)
    {
        //11
        int[] A = [1, 5, 2, 1, 4, 0];

        Console.WriteLine(new Solution().solution(A));

        Console.ReadKey();
    }

    public static int DiscIntersections(int[] input)
    {
        //can be packed into (input.Length**2)/2 bits structure if you need to consume less memory (now consumes (input.Length^2)*8 B)
        bool[,] cache = new bool[input.Length, input.Length];
        int count = 0;

        //O(N*(2radius-1)) === best O(N) to worst O(N^2) (though one N is assignments when the second N consists fully of bool checks (aka lowcoster))
        for (int i = 0; i < input.Length; i++)
            count += UpdateIntersections(cache, i, input[i]);

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

internal class Solution
{
    public int solution(int[] A) => Program.DiscIntersections(A);
}