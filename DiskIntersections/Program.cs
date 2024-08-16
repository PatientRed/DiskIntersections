internal class Program
{
    const int _limit = 10_000_000;

    private static void Main(string[] args)
    {
        //11
        int[] A = [1, 5, 2, 1, 4, 0];

        Console.WriteLine(new Solution().solution(A));

        Console.WriteLine($"Googled: {DiscIntersectionsGoogleEdition(A)}");

        Console.ReadKey();
    }

    //O(N) to O(N^2) time, potential O(N^2/2) memory, currently O(N^2) mem
    //O(N*M) where M = avg(radius) [= input.Average()]?
    public static int DiscIntersections(int[] input)
    {
        //can be packed into (input.Length**2)/2 bits structure if you need to consume less memory (now consumes (input.Length^2)*8 B)
        bool[,] cache = new bool[input.Length, input.Length];
        int count = 0;

        //O(N*(2radius-1)) === best O(N) to worst O(N^2) (though one N is assignments when the second N consists fully of bool checks (aka lowcoster))
        for (int i = 0; i < input.Length; i++)
        {
            count += UpdateIntersections(cache, i, input[i]);

            if (count > _limit)
                return -1;
        }

        return count;
    }

    //O(N) time, O(N) mem
    public static int DiscIntersectionsGoogleEdition(int[] input)
    {
        //https://stackoverflow.com/a/16814894

        int result = 0;
        //2*O(N) Mem
        int[] startsAt = new int[input.Length];
        int[] endsAt = new int[input.Length];

        //O(N)
        for (int i = 0, j = input.Length - 1; i < input.Length; i++)
        {
            int left = i > input[i] ? i - input[i] : 0;
            int right = j - i > input[i] ? i + input[i] : j;

            startsAt[left]++;
            endsAt[right]++;
        }

        for (int i = 0, current = 0; i < input.Length; i++)
        {
            if (startsAt[i] > 0)
            {
                //new starts intersects all of currently continuos circles
                result += current * startsAt[i];
                //intersections of new starts.
                //C(n, k), we need pairs, so k = 2.
                //C(startsAt[i], 2) = startsAt[i]! / (2! * (startsAt[i]-2)!).
                //See https://en.wikipedia.org/wiki/Combination#Number_of_k-combinations
                result += startsAt[i] * (startsAt[i] - 1) / 2;

                if (result > _limit)
                    return -1;

                //add new starts into active disks pool
                current += startsAt[i];
            }

            //remove finished at this point
            current -= endsAt[i];
        }

        return result;
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