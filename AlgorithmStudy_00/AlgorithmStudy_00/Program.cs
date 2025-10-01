using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy_00
{
    class Program
    {
        static void Main(string[] args)
        {
            //BackTracking.Example00 example = new BackTracking.Example00(8, 4);
            //BackTracking.Example01 example01 = new BackTracking.Example01(4);

            //int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //int m = 4;
            //List<List<int>> result = new List<List<int>>();

            //Combination(nums, m, 0, new List<int>(), result);

            //foreach (List<int> comb in result)
            //    Console.WriteLine(string.Join(", ", comb));

            // Console.ReadLine();


            #region DivideAndConquer
            //List<float[]> sample1 = new List<float[]>();
            //List<float[]> sample2 = new List<float[]>();
            //Random random = new Random();


            //for (int i = 0; i < 1000; i++)
            //{
            //    float[] one = new float[] { (float)random.Next(10000) / 100, (float)random.Next(10000) / 100 };
            //    sample1.Add(one);
            //    sample2.Add((float[])one.Clone());

            //    if (sample1[i][0] != sample2[i][0]) Console.WriteLine($"{i}");
            //}

            //DivideAndConquer.FindingClosestPointProblem findingClosest = new DivideAndConquer.FindingClosestPointProblem();
            //Console.WriteLine(findingClosest.FindClosestDistance(sample1));
            //Console.WriteLine(findingClosest.BruteDistanceFind(sample2));
            #endregion

            int[] answer = GraphProblem.MinEdgesPerTypeSolver.GetMinEdgesPerType_ArrayDSU(4, 5, 2, new List<int> { 3, 0, 0, 2, 1 }, new List<int> { 2, 3, 2, 1, 3 }, new List<int> { 1, 0, 0, 1, 1 });

            for (int i = 0; i < answer.Length; i++)
            {
                Console.WriteLine($"{answer[i]}");
            }
        }

        public static void Combination<T>(T[] arr, int m, int start, List<T> chosen, List<List<T>> result)
        {
            if (chosen.Count == m)
            {
                result.Add(new List<T>(chosen));
                return;
            }

            for (int i = start; i < arr.Length; i++)
            {
                chosen.Add(arr[i]);
                Combination(arr, m, i + 1, chosen, result);
                chosen.RemoveAt(chosen.Count - 1);
            }
        }

        public class MinHeap<T> where T : IComparable<T>
        {
            private List<T> _data = new List<T>();

            public int Count { get { return _data.Count; } }

            public void Push(T newItem)
            {
                _data.Add(newItem);
                int index = _data.Count - 1;

                while (index > 0)
                {
                    int parentIndex = (index - 1) / 2;
                    if (_data[index].CompareTo(_data[parentIndex]) >= 0) break;

                    (_data[index], _data[parentIndex]) = (_data[parentIndex], _data[index]);
                    index = parentIndex;
                }
            }

            public T Pop()
            {
                if (_data.Count == 0) throw new Exception();

                T root = _data[0];
                _data[0] = _data[_data.Count - 1];
                _data.RemoveAt(_data.Count - 1);

                int index = 0;

                while (true)
                {
                    int left = 2 * index + 1, right = 2 * index + 2, smallest = index;
                    if (left < _data.Count && _data[left].CompareTo(_data[smallest]) < 0) smallest = left;
                    if (right < _data.Count && _data[right].CompareTo(_data[smallest]) < 0) smallest = right;
                    if (smallest == index) break;

                    (_data[index], _data[smallest]) = (_data[smallest], _data[index]);
                    index = smallest;
                }

                return root;

            }

            public T Peek() { return _data[0]; }
        }


    }

    public class BackTracking
    {
        //https://blog.encrypted.gg/945

        public class Example00
        {
            int answer;
            int N;
            int M;
            string print = string.Empty;

            int[] arr = new int[8];
            bool[] isUsed = new bool[9];

            /// <summary>
            /// 
            /// </summary>
            /// <param Last Number="N"></param>
            /// <param Number to Pick="M"></param>
            public Example00(int _N, int _M)
            {
                N = _N;
                M = _M;
                answer = 0;
                Pick(0);
                Console.WriteLine(answer);
            }

            public void Pick(int k)
            {
                if (k == M)
                {
                    print = string.Empty;

                    for (int j = 0; j < M; j++)
                    {
                        print = string.Concat(print, " ", arr[j]);
                    }
                    answer++;
                    Console.WriteLine(print);
                    return;
                }

                for (int i = 1; i <= N; i++)
                {
                    if (!isUsed[i])
                    {
                        arr[k] = i;
                        isUsed[i] = true;
                        Pick(k + 1);
                        isUsed[i] = false;
                    }
                }

            }
        }

        public class Example01
        {
            int answer;
            int N;
            int[,] chessBoard;
            bool[] isQueenXs;
            int[] isQueenSlash; // / = same y + x
            int[] isQueenBackslsh; // | = same y - x
            string print = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            /// <param ChessBoardSize="N"></param>
            public Example01(int _N)
            {
                N = _N;
                isQueenXs = new bool[N];
                isQueenSlash = new int[N];
                isQueenBackslsh = new int[N];

                for (int i = 0; i < N; i++)
                {
                    isQueenSlash[i] = -2 * N;
                    isQueenBackslsh[i] = -2 * N;
                }

                chessBoard = new int[N, N];

                SetQueen(0);

                Console.WriteLine(answer);
            }

            public void SetQueen(int k)
            {
                if (k == N)
                {

                    for (int i = 0; i < N; i++)
                    {
                        print = string.Empty;
                        for (int j = 0; j < N; j++)
                        {
                            print = string.Concat(print, chessBoard[i, j], " ");
                        }
                        Console.WriteLine(print);
                    }
                    Console.WriteLine("\n");
                    answer++;

                    return;
                }

                for (int x = 0; x < N; x++)
                {
                    if (isQueenXs[x]) continue;

                    bool diagonalCheck = false;

                    for (int v = 0; v < k; v++)
                    {
                        if (isQueenSlash[v] == k + x) { diagonalCheck = true; break; }
                        if (isQueenBackslsh[v] == k - x) { diagonalCheck = true; break; }
                    }
                    if (diagonalCheck) continue;

                    isQueenXs[x] = true;
                    isQueenSlash[k] = k + x;
                    isQueenBackslsh[k] = k - x;
                    chessBoard[k, x] = 1;

                    SetQueen(k + 1);

                    isQueenXs[x] = false;
                    isQueenSlash[k] = -2 * N;
                    isQueenBackslsh[k] = -2 * N;
                    chessBoard[k, x] = 0;

                }

            }
        }
    }

    public class DivideAndConquer
    {
        public class FindingClosestPointProblem
        {
            public float FindClosestDistance(List<float[]> points)
            {
                points.Sort((a, b) => a[0].CompareTo(b[0]));
                return (float)Math.Sqrt((double)DivideAndConquer(points, 0, points.Count));
            }

            public float BruteDistanceFind(List<float[]> points)
            {
                float d2 = float.MaxValue;
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        float dx = points[i][0] - points[j][0];
                        float dy = points[i][1] - points[j][1];
                        float distSqr = dx * dx + dy * dy;

                        d2 = distSqr < d2 ? distSqr : d2;
                    }
                }
                return (float)Math.Sqrt(d2);
            }

            public float DivideAndConquer(List<float[]> points, int leftIn, int rightEx)
            {
                int n = rightEx - leftIn;
                if (n <= 3)
                {
                    float best = float.MaxValue;

                    for (int i = leftIn; i < rightEx; i++)
                    {
                        for (int j = i + 1; j < rightEx; j++)
                        {
                            float dx = (points[i][0]) - points[j][0];
                            float dy = (points[i][1]) - points[j][1];
                            float distSqr = dx * dx + dy * dy;

                            best = distSqr < best ? distSqr : best;
                        }
                    }

                    points.Sort(leftIn, n, Comparer<float[]>.Create((a, b) => a[1].CompareTo(b[1])));
                    return best;
                }


                int middle = (rightEx + leftIn) / 2;
                float middleX = points[middle][0];

                float d2L = DivideAndConquer(points, leftIn, middle);
                float d2R = DivideAndConquer(points, middle, rightEx);
                float d2 = d2L < d2R ? d2L : d2R;

                List<float[]> temp = new List<float[]>();
                int leftIndex = leftIn, rightIndex = middle;
                while (leftIndex < middle && rightIndex < rightEx)
                {
                    if (points[leftIndex][1] <= points[rightIndex][1]) temp.Add(points[leftIndex++]);
                    else temp.Add(points[rightIndex++]);
                }
                while (leftIndex < middle) temp.Add(points[leftIndex++]);
                while (rightIndex < rightEx) temp.Add(points[rightIndex++]);

                for (int i = 0; i < temp.Count; i++)
                {
                    points[leftIn + i] = temp[i];
                }

                List<float[]> strip = new List<float[]>();
                for (int i = leftIn; i < rightEx; i++)
                {
                    float dx = points[i][0] - middleX;
                    if (dx * dx < d2) strip.Add(points[i]);
                }

                for (int i = 0; i < strip.Count; i++)
                {
                    for (int j = i + 1; j < strip.Count && j <= i + 7; j++)
                    {
                        float dy = strip[i][1] - strip[j][1];
                        if (dy * dy > d2) break;

                        float dx = strip[i][0] - strip[j][0];
                        float distSqr = dx * dx + dy * dy;
                        d2 = distSqr < d2 ? distSqr : d2;
                    }
                }

                return d2;
            }
        }
    }

    public class GraphProblem
    {
        public sealed class DSU
        {
            private readonly int[] parent;
            private readonly int[] size;

            public int N => parent.Length;


            public DSU(int n)
            {
                parent = new int[n];
                size = new int[n];

                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                    size[i] = 1;
                }
            }

            private int Find(int x)
            {
                // route compression
                if (parent[x] != x)
                {
                    parent[x] = Find(parent[x]);
                }
                return parent[x];
            }

            public bool Union(int a, int b)
            {
                int ra = Find(a);
                int rb = Find(b);
                // Already same union
                if (ra == rb) return false;

                if (size[ra] < size[rb])
                    (ra, rb) = (rb, ra);

                // a <= b
                parent[rb] = ra;
                size[ra] += size[rb];
                return true;
            }

            public int FindRoot(int x) => Find(x);
        }

        public static class MinEdgesPerTypeSolver
        {
            // n: region count, m: edge count, typeCount: type count
            // startRegion: from, endRegion: to, type: type of that edge

            public static int[] GetMinEdgesPerType_ArrayDSU(int n, int m, int typeCount, IList<int> startRegion, IList<int> endRegion, IList<int> edgeType)
            {
                List<int>[] edgesByType = new List<int>[typeCount];
                for (int t = 0; t < typeCount; t++) edgesByType[t] = new List<int>();
                for (int i = 0; i < m; i++) edgesByType[edgeType[i]].Add(i);

                int[] answer = new int[typeCount];

                for (int t = 0; t < typeCount; t++)
                {
                    List<int> edges = edgesByType[t];
                    if (edges.Count == 0)
                    {
                        answer[t] = 0;
                        continue;
                    }

                    DSU dsu = new DSU(n);
                    bool[] used = new bool[n];

                    foreach (int edgeIndex in edges)
                    {
                        int u = startRegion[edgeIndex], v = endRegion[edgeIndex];
                        if (u == v)
                        {
                            // self roof case
                            used[u] = true;
                            continue;
                        }
                        used[u] = true; used[v] = true;
                        dsu.Union(u, v);
                    }

                    int nodes = 0;
                    HashSet<int> roots = new HashSet<int>();
                    for (int i = 0; i < n; i++)
                    {
                        // Pass if not used
                        if (!used[i]) continue;

                        nodes++;
                        roots.Add(dsu.FindRoot(i));
                    }

                    int comps = roots.Count;

                    // Min edges = nodes - comps

                    answer[t] = nodes - comps;
                }

                return answer;

            }
        }

    }

}
