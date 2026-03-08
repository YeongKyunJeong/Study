using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Security.Principal;
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

            #region DSU
            //int[] answer = GraphProblem.MinEdgesPerTypeSolver.GetMinEdgesPerType_ArrayDSU(4, 5, 2, new List<int> { 3, 0, 0, 2, 1 }, new List<int> { 2, 3, 2, 1, 3 }, new List<int> { 1, 0, 0, 1, 1 });

            //for (int i = 0; i < answer.Length; i++)
            //{
            //    Console.WriteLine($"{answer[i]}");
            //}
            #endregion

            #region DFSSimulation
            //Console.WriteLine(SimulationExample.Ex01.Ex01Solver(4, 6,
            //    new int[,]
            //    {
            //        {0, 0, 0, 0, 0, 0},
            //        {0, 0, 0 ,0, 0, 0},
            //        {0, 0, 1 ,0, 6, 0},
            //        {0, 0, 0 ,0, 0, 0}
            //    }));

            //Console.WriteLine(SimulationExample.Ex01.Ex01Solver(6, 6,
            //    new int[,]
            //    {
            //        {0, 0, 0, 0, 0, 0},
            //        {0, 2, 0, 0, 0, 0},
            //        {0, 0, 0, 0, 6, 0},
            //        {0, 6, 0 ,0, 2, 0},
            //        {0, 0, 0 ,0, 0, 0},
            //        {0, 0, 0 ,0, 0, 5}
            //    }));


            //Console.WriteLine(SimulationExample.Ex01.Ex01Solver(6, 6,
            //    new int[,]
            //    {
            //        {1, 0, 0, 0, 0, 0},
            //        {0, 1, 0, 0, 0, 0},
            //        {0, 0, 1, 0, 0, 0},
            //        {0, 0, 0 ,1, 0, 0},
            //        {0, 0, 0 ,0, 1, 0},
            //        {0, 0, 0 ,0, 0, 1}
            //    }));

            //Console.WriteLine(SimulationExample.Ex01.Ex01Solver(6, 6,
            //    new int[,]
            //    {
            //        {1, 0, 0, 0, 0, 0},
            //        {0, 1, 0, 0, 0, 0},
            //        {0, 0, 1, 5, 0, 0},
            //        {0, 0, 5 ,1, 0, 0},
            //        {0, 0, 0 ,0, 1, 0},
            //        {0, 0, 0 ,0, 0, 1}
            //    }));

            //Console.WriteLine(SimulationExample.Ex01.Ex01Solver(1, 7,
            //    new int[,]
            //    {
            //        {0, 1, 2, 3, 4, 5, 6}
            //    }));

            //Console.WriteLine(SimulationExample.Ex01.Ex01Solver(3, 7,
            //    new int[,]
            //    {
            //        {4, 0, 0, 0, 0, 0, 0},
            //        {0, 0, 0, 2, 0, 0, 0},
            //        {0, 0, 0, 0, 0, 0, 4}
            //    }));
            #endregion;

            #region ArrayRotation
            //Console.WriteLine(SimulationExample.Ex02.Solver(5, 4, 4, new List<(int, int, int[,])>()
            //{
            //    (3, 3, new int[,]
            //    {
            //        {1, 0, 1 },
            //        {1, 1, 1 },
            //        {1, 0, 1 }
            //    }),
            //    (2, 5, new int[,]
            //    {
            //        {1, 1, 1, 1, 1 },
            //        {0, 0, 0, 1, 0 }
            //    }),
            //    (2, 3, new int[,]
            //    {
            //        {1, 1, 1 },
            //        {1, 0, 1 }
            //    }),
            //    (3, 3, new int[,]
            //    {
            //        {1, 0, 0 },
            //        {1, 1, 1 },
            //        {1, 0, 0 }
            //    })
            //}));
            #endregion

            #region CombinationExample
            //Console.WriteLine(SimulationExample.Ex03.Solver(5, 3, new int[,]
            //{
            //    {0, 0, 1, 0, 0 },
            //    {0, 0, 2, 0 ,1 },
            //    {0, 1, 2, 0, 0 },
            //    {0, 0, 1 ,0 ,0 },
            //    {0, 0, 0, 0, 2 }
            //}));

            //Console.WriteLine(SimulationExample.Ex03.Solver(5, 2, new int[,]
            //{
            //    {0, 2, 0, 1, 0 },
            //    {1, 0, 1, 0 ,0 },
            //    {0, 0, 0, 0, 0 },
            //    {2, 0, 0 ,1 ,1 },
            //    {2, 2, 0, 1, 2 }
            //}));

            //Console.WriteLine(SimulationExample.Ex03.Solver(5, 1, new int[,]
            //{
            //    {1, 2, 0, 0, 0 },
            //    {1, 2, 0, 0 ,0 },
            //    {1, 2, 0, 0, 0 },
            //    {1, 2, 0, 0, 0 },
            //    {1, 2, 0, 0, 0 }
            //}));
            #endregion

            #region Sorting
            #region Sorting Practice
            //int[] a = Sorting.MergeSorting.Merge(2, 2, new int[] { 3, 5 }, new int[] { 2, 9 });
            //foreach (int i in a)
            //{
            //    Console.Write($"{i} ");
            //}
            //Console.WriteLine();
            //a = Sorting.MergeSorting.Merge(2, 1, new int[] { 4, 7 }, new int[] { 1 });
            //foreach (int i in a)
            //{
            //    Console.Write($"{i} ");
            //}
            //Console.WriteLine();
            //a = Sorting.MergeSorting.Merge(4, 3, new int[] { 2, 3, 5, 9 }, new int[] { 1, 4, 7 });
            //foreach (int i in a)
            //{
            //    Console.Write($"{i} ");
            //}

            ////int[] a = Sorting.MergeSorting.MergeSort(new int[] { 5, 5, 4, 3, 2, 1, 9, 540, 50, 354, 6891, 65, 8641, 861, 681, 684, 684, 65, 1, 68, 31, 54 });
            //int[] a = Sorting.CountSorting.CountSort(new int[] { 5, 5, 4, 3, 2, 1, 9, 540, 50, 354, 6891, 65, 8641, 861, 681, 684, 684, 65, 1, 68, 31, 54 });
            //string s = string.Empty;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (i == a.Length - 1)
            //    {
            //        s += a[i].ToString();
            //    }
            //    else
            //    {
            //        s += $"{a[i]}, ";
            //    }
            //}
            //Console.WriteLine(s);
            #endregion
            #region Ex01
            //string[] a = SortingExample.Ex01.Solver(5, new string[]
            //{
            //    "ABCD", "145C", "A", "A910", "Z321"
            //});
            //string s = string.Empty;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (i == a.Length - 1) s += a[i];
            //    else s += $"{a[i]}, ";
            //}
            //Console.WriteLine(s);

            //a = SortingExample.Ex01.Solver(2, new string[]
            //{
            //    "Z19", "Z20"
            //});
            //s = string.Empty;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (i == a.Length - 1) s += a[i];
            //    else s += $"{a[i]}, ";
            //}
            //Console.WriteLine(s);

            //a = SortingExample.Ex01.Solver(4, new string[]
            //{
            //    "34H2BJS6N", "PIM12MD7RCOLWW09", "PYF1J14TF", "FIPJOTEA5"
            //});
            //s = string.Empty;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (i == a.Length - 1) s += a[i];
            //    else s += $"{a[i]}, ";
            //}
            //Console.WriteLine(s);

            //a = SortingExample.Ex01.Solver(4, new string[]
            //{
            //    "ABCDE", "BCDEF", "ABCDA", "BAAAA", "ACAAA"
            //});
            //s = string.Empty;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (i == a.Length - 1) s += a[i];
            //    else s += $"{a[i]}, ";
            //}
            //Console.WriteLine(s);
            #endregion
            #region Ex02

            //Console.WriteLine(SortingExample.Ex02.Solver(new long[] { 5, 1, 2, 1, 2, 1 }));

            //Console.WriteLine(SortingExample.Ex02.Solver(new long[] { 6, 1, 2, 1, 2, 1, 1, 2 }));

            //Random rand = new Random();

            //long[] arr = new long[100];

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = rand.Next(-10, 11);
            //    if (rand.Next(2) == 0) arr[i] *= -1;
            //}

            //Console.WriteLine(SortingExample.Ex02.Solver(arr));

            //Console.WriteLine(SortingExample.Ex02.Solver(new long[] { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 154, 3, 54, 8, 2, 615, 8, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19 }));

            #endregion
            #endregion

            #region Dynamic
            //Console.WriteLine(Dynamic.EX01.Solver(10));

            //Console.WriteLine(Dynamic.EX02.Solver(11, new int[]
            //{
            //    10, 20, 15, 25, 10, 10, 30, 20, 10, 20, 20, 30
            //}));


            //Console.WriteLine(Dynamic.EX02.Solver2(11, new int[]
            //{
            //    10, 20, 15, 25, 10, 10, 30, 20, 10, 20, 20, 30
            //}));

            //Console.WriteLine(Dynamic.EX03.Solver(3, new int[3, 3]
            //{ {26, 40, 83 }, {49, 60 ,57 }, {13, 89, 99 } }));

            //Console.WriteLine(Dynamic.EX03.Solver(3, new int[3, 3]
            //{ {1, 100, 100 }, {100, 1 ,100 }, {100, 100, 1 } }));

            //Console.WriteLine(Dynamic.EX03.Solver(3, new int[3, 3]
            //{ {1, 100, 100 }, {100, 100, 100 }, {1, 100, 100 } }));

            //Console.WriteLine(Dynamic.EX03.Solver(6, new int[6, 3]
            //{ {30, 19, 5 }, {64, 77, 64 }, {15, 19, 97 } , {4, 71, 57 }, {90, 86, 84 }, {93, 32, 91 } }));

            //Console.WriteLine(Dynamic.EX03.Solver(8, new int[8, 3]
            //{ {71, 39, 44 }, {32, 83, 55 }, {51, 37, 63 } , {89, 29, 100 }, {83, 58, 11 }, {65, 13, 15 }, {47, 25, 29 }, {60, 66, 19 } }));

            //Console.WriteLine(Dynamic.EX04.Solver(2));
            //Console.WriteLine(Dynamic.EX04.Solver(9));

            //int[] ex05 = Dynamic.EX05.Solver(5, 3, new int[] { 5, 4, 3, 2, 1 }, new int[,]
            //{
            //    {1, 3 },
            //    {2, 4 },
            //    {5, 5 }
            //});
            //string ex05Answer = "";
            //for (int i = 0; i < ex05.Length; i++)
            //{
            //    ex05Answer += $"{ex05[i]}, ";
            //}
            //Console.WriteLine(ex05Answer);

            //(int n, int[] traces) = Dynamic.EX06.Solver(10);
            //Console.WriteLine(n);
            //string ex06Answer = "";
            //for (int i = 0; i < traces.Length; i++)
            //{
            //    ex06Answer += $"{traces[i]}, ";
            //}
            //Console.WriteLine(ex06Answer);

            //Console.WriteLine(Dynamic.EX07.Solver(10, 4200, new int[] { 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 50000 }));

            //Console.WriteLine(Dynamic.EX08.Solver(4, 7, new int[,] { { 6, 13 }, { 4, 8 }, { 3, 6 }, { 5, 12 } }));
            //Console.WriteLine(Dynamic.EX08.Solver2(4, 7, new int[,] { { 6, 13 }, { 4, 8 }, { 3, 6 }, { 5, 12 } }));
            //Console.WriteLine(Dynamic.EX09.Solver(5, 2));
            #endregion

            #region Greedy
            //Console.WriteLine(Greedy.EX01.Solver(10, 4200, new int[] { 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 50000 }));
            //Console.WriteLine(Greedy.EX02.Solver(11, new int[][] {
            //    new int[] { 1, 4 },
            //    new int[] { 3, 5 },
            //    new int[] { 0, 6 },
            //    new int[] { 5, 7 },
            //    new int[] { 3, 8 },
            //    new int[] { 5, 9 },
            //    new int[] { 6, 10 },
            //    new int[] { 8, 11 },
            //    new int[] { 8, 12 },
            //    new int[] { 2, 13 },
            //    new int[] { 12, 14}
            //}));
            //Console.WriteLine(Greedy.EX03.Solver(2, new int[] { 10, 20 }));

            //Console.WriteLine(Greedy.EX04.Solver(5, new int[] { 1, 1, 1, 6, 0 }, new int[] { 2, 7, 8, 3, 1 }));
            //Console.WriteLine(Greedy.EX04.Solver(3, new int[] { 1, 1, 3 }, new int[] { 10, 30, 20 }));
            //Console.WriteLine(Greedy.EX04.Solver(9, new int[] { 5, 15, 100, 31, 39, 0, 0, 3, 26 }, new int[] { 11, 12, 13, 2, 3, 4, 5, 9, 1 }));
            #endregion

            #region MathPractice
            //string s = string.Empty;

            //List<int> primes = MathPractice.Prime.GetPrime(100);
            //for (int i = 0; i < primes.Count; i++)
            //{
            //    s = $"{s}, {primes[i]}";
            //}
            //Console.WriteLine(s);

            //s = string.Empty;

            //List<int> primes2 = MathPractice.Prime.Sieve(100);
            //for (int i = 0; i < primes2.Count; i++)
            //{
            //    s = $"{s}, {primes2[i]}";
            //}
            //Console.WriteLine(s);

            //List<int> primes = MathPractice.Prime.SieveFromTo(3, 13);
            //for (int i = 0; i < primes.Count; i++)
            //{
            //    s = $"{s}, {primes[i]}";
            //}
            //Console.WriteLine(s);

            //List<int> primes3 = new List<int>();
            //primes3 = MathPractice.Prime.DivideByPrime(9991);

            //s = string.Empty;
            //for (int i = 0; i < primes3.Count; i++)
            //{
            //    s = $"{s}, {primes3[i]} ";
            //}
            //Console.WriteLine(s);

            //int[] dates = MathPractice.Prime.EX01.Solver(3, new int[3][] {
            //    new int[] {10, 12, 3, 9 },
            //    new int[] {10, 12, 7, 2 },
            //    new int[] {13, 11, 5, 6 }
            //});
            //s = string.Empty;
            //for (int i = 0; i < dates.Length; i++)
            //{
            //    s = $"{s}, {dates[i]}";
            //}
            //Console.WriteLine(s);
            #endregion

            #region BinarySearch
            //string sB = string.Empty;

            //int[] arrEX01 = BinarySearch.EX01.Solver(5, new int[] { 4, 1, 5, 2, 3 }, 5, new int[] { 1, 3, 7, 9, 5 });
            //for (int i = 0; i < arrEX01.Length; i++)
            //{
            //    sB = $"{sB}, {arrEX01[i]}";
            //}
            //Console.WriteLine(sB);

            //arrEX01 = BinarySearch.EX01.Solver(5, new int[] { 1, 3, 5, 6, 7, 7, 1, 3, 5 }, 5, new int[] { 1, 3, 7, 9, 5 });
            //sB = string.Empty;
            //for (int i = 0; i < arrEX01.Length; i++)
            //{
            //    sB = $"{sB}, {arrEX01[i]}";
            //}
            //Console.WriteLine(sB);

            //int[] arrEX02 = BinarySearch.EX02.Solver(10, new int[] { 6, 3, 2, 10, 10, 10, -10, -10, 7, 3 }, 8, new int[] { 10, 9, -5, 2, 3, 4, 5, -10 });
            //for (int i = 0; i < arrEX02.Length; i++)
            //{
            //    sB = $"{sB}, {arrEX02[i]}";
            //}
            //Console.WriteLine(sB);

            //int[] arrEX03 = BinarySearch.EX03.Solver(5, new int[] { 2, 4, -10, 4, -9 });
            //sB = string.Empty;
            //for(int i = 0; i < arrEX03.Length; i++)
            //{
            //    sB = $"{sB}, {arrEX03[i]}";
            //}
            //Console.WriteLine(sB);

            //arrEX03 = BinarySearch.EX03.Solver(6, new int[] { 1000, 999, 1000, 999, 1000, 999 });
            //sB = string.Empty;
            //for (int i = 0; i < arrEX03.Length; i++)
            //{
            //    sB = $"{sB}, {arrEX03[i]}";
            //}
            //Console.WriteLine(sB);

            //Console.WriteLine(BinarySearch.EX04.Solver(5, new int[] { 2, 3, 5, 10, 18 }));

            //Console.WriteLine(BinarySearch.EX05.Solver(4, 11, new int[] { 802, 743, 457, 539 }));

            //Console.WriteLine(BinarySearch.EX06.Solver(3, 3, new int[] { 1, 3, 5 }));
            //Console.WriteLine(BinarySearch.EX06.Solver2(3, 3, new int[] { 1, 3, 5 }));
            #endregion

            #region TwoPointer
            //Console.WriteLine(TwoPointer.EX01.Solver(3, 3, new int[] { 1, 5, 3 }));
            //Console.WriteLine(TwoPointer.EX02.Solver(10, 15, new int[] { 5, 1, 3, 5, 10, 7, 4, 9, 2, 8 }));
            #endregion

            #region BinaryTree

            //List<(int, List<(char, int)>)> inputs = new List<(int, List<(char, int)>)>();

            //List<(char, int)> input1 = new List<(char, int)>();
            //input1.Add(('I', 16));
            //input1.Add(('I', -5643));
            //input1.Add(('D', -1));
            //input1.Add(('D', 1));
            //input1.Add(('D', 1));
            //input1.Add(('I', 123));
            //input1.Add(('D', -1));
            //inputs.Add((7, input1));

            //List<(char, int)> input2 = new List<(char, int)>();
            //input2.Add(('I', -45));
            //input2.Add(('I', 653));
            //input2.Add(('D', 1)); ;
            //input2.Add(('I', -642));
            //input2.Add(('I', 45));
            //input2.Add(('I', 97));
            //input2.Add(('D', 1));
            //input2.Add(('D', -1));
            //input2.Add(('I', 333));
            //inputs.Add((9, input2));

            //string[] sBT = BinaryTree.EX01.Solver(2, inputs);
            //foreach (string s in sBT)
            //{
            //    Console.WriteLine(s);
            //}

            //Console.WriteLine(BinaryTree.EX02.Solver(2, 1,
            //    new int[][] { new int[2] { 5, 10 }, new int[2] { 100, 100 } },
            //    new int[] { 11 }));
            //Console.WriteLine(BinaryTree.EX02.Solver(3, 2,
            //    new int[][] { new int[2] { 1, 65 }, new int[2] { 5, 23 }, new int[2] { 2, 99 } },
            //    new int[] { 10, 2 }));
            #endregion

            #region Graph
            //Console.WriteLine(Graph.EX02.Solver(6, 5, new List<int[]>()
            //{
            //    new int[2] { 1, 2},
            //    new int[2] { 2, 5},
            //    new int[2] { 5, 1},
            //    new int[2] { 3, 4},
            //    new int[2] { 4, 6},
            //}));

            //Console.WriteLine(Graph.EX02.Solver(6, 8, new List<int[]>()
            //{
            //    new int[2] { 1, 2},
            //    new int[2] { 2, 5},
            //    new int[2] { 5, 1},
            //    new int[2] { 3, 4},
            //    new int[2] { 4, 6},
            //    new int[2] { 5, 4},
            //    new int[2] { 2, 3},
            //}));

            //string s = string.Empty;
            //int[][] arrEX03 = Graph.EX03.Solver(4, 5, 1, new List<int[]>()
            //{
            //    new int[2] { 1, 2},
            //    new int[2] { 1, 3},
            //    new int[2] { 1, 4},
            //    new int[2] { 2, 4},
            //    new int[2] { 3, 4},
            //});
            //for (int i = 0; i < 2; i++)
            //{
            //    s = string.Empty;
            //    for (int j = 0; j < 4; j++)
            //    {
            //        s = $"{s}, {arrEX03[i][j]}";
            //    }
            //    Console.WriteLine(s);
            //}
            //;

            //arrEX03 = Graph.EX03.Solver(5, 5, 3, new List<int[]>()
            //{
            //    new int[2] { 5, 4},
            //    new int[2] { 5, 2},
            //    new int[2] { 1, 2},
            //    new int[2] { 3, 4},
            //    new int[2] { 3, 1},
            //});
            //for (int i = 0; i < 2; i++)
            //{
            //    s = string.Empty;
            //    for (int j = 0; j < 5; j++)
            //    {
            //        s = $"{s}, {arrEX03[i][j]}";
            //    }
            //    Console.WriteLine(s);
            //}

            #endregion

            #region Topological Sort
            //string s01 = string.Empty;

            //int[] arrEX01 = TopologicalSort.EX01.Solver(3, 2, new int[2][] { new int[] { 1, 3 }, new int[] { 2, 3 } });

            //foreach (int i in arrEX01)
            //{
            //    s01 = $"{s01}, {i}";
            //}
            //Console.WriteLine(s01);

            //s01 = string.Empty;

            //arrEX01 = TopologicalSort.EX01.Solver(4, 2, new int[2][] { new int[] { 4, 2 }, new int[] { 3, 1 } });

            //foreach (int i in arrEX01)
            //{
            //    s01 = $"{s01}, {i}";
            //}
            //Console.WriteLine(s01);

            //s01 = string.Empty;

            //arrEX01 = TopologicalSort.EX01.Solver2(4, 2, new int[2][] { new int[] { 4, 2 }, new int[] { 3, 1 } });

            //foreach (int i in arrEX01)
            //{
            //    s01 = $"{s01}, {i}";
            //}
            //Console.WriteLine(s01);

            #endregion

            #region MinSpanningTree

            //Console.WriteLine(MinimumSpanningTree.EX01.Solver(3, 3, new (int, int, int)[3] { (1, 2, 1), (2, 3, 2), (1, 3, 3) }));
            //Console.WriteLine(MinimumSpanningTree.EX01.Solver2(3, 3, new (int, int, int)[3] { (1, 2, 1), (2, 3, 2), (1, 3, 3) }));

            //Console.WriteLine(MinimumSpanningTree.EX02.Solver(4, new int[] { 5, 4, 4, 3 }
            //, new int[4][] {new int[] {0, 2, 2, 2 },
            //                new int[] {2, 0, 3, 3 },
            //                new int[] {2, 3, 0, 4 },
            //                new int[] { 2, 3, 4, 0} }));

            #endregion

            #region Floyd
            //Floyd.EX01.Solver(5, 14, new (int, int, int)[14]
            //{
            //    (1, 2, 2),
            //    (1, 3, 3),
            //    (1, 4, 1),
            //    (1, 5, 10),
            //    (2, 4, 2),
            //    (3, 4, 1),
            //    (3, 5, 1),
            //    (4, 5, 3),
            //    (3, 5, 10),
            //    (3, 1, 8),
            //    (1, 4, 2),
            //    (5, 1, 7),
            //    (3, 4, 2),
            //    (5, 2, 4),
            //});
            //#endregion




            //Console.WriteLine(Problems.EX01.Solver(1, 4, 4, new int[,]
            //{ { 0, 0, 0, 0 },
            //  { 1, 0, 0, 0 },
            //  { 0, 0, 1, 0 },
            //  { 0, 1, 0, 0 } }));

            //Console.WriteLine(Problems.EX01.Solver(2, 5, 2, new int[,]
            //{ { 0, 0, 1, 1, 0 },
            //  { 0, 0, 1, 1, 0 }}));
            #endregion

            #region KMP
            //KMP.EX01.Main("baekjoonbaekjoonbaekjoon", "aek");
            #endregion

            Problem1.Main(2, 1, new int[2, 2] { { 5, 10 }, { 100, 100 } }, new int[] { 11 });
            Problem1.Main(3, 2, new int[3, 2] { { 1, 65 }, { 5, 23 }, { 2, 99} }, new int[] { 10, 2 });
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
            public float Mains(List<float[]> points)
            {
                points.Sort((a, b) => a[0].CompareTo(b[0]));
                return (float)Math.Sqrt((double)DQ(points, 0, points.Count));
            }

            public float DQ(List<float[]> points, int leftIn, int rightEx)
            {
                int n = rightEx - leftIn;

                if (n <= 3)
                {
                    float best = float.MaxValue;
                    for (int i = leftIn; i < rightEx; i++)
                    {
                        for (int j = i + 1; j < rightEx; j++)
                        {
                            float dx = points[i][0] - points[j][0];
                            float dy = points[i][1] - points[j][1];

                            float d2 = dx * dx + dy * dy;
                            if (best > d2) d2 = best;
                        }
                    }

                    points.Sort(leftIn, n, Comparer<float[]>.Create((a, b) => a[1].CompareTo(b[1])));
                    return best;
                }

                int middle = (rightEx + leftIn) / 2;
                float middleX = points[middle][0];

                float D2L = DQ(points, leftIn, middle);
                float D2R = DQ(points, middle, rightEx);
                float D2 = Math.Min(D2L, D2R);

                int lIndex = leftIn, rIndex = middle;
                List<float[]> temp = new List<float[]>();

                while (lIndex < middle && rIndex < rightEx)
                {
                    if (points[lIndex][1] < points[rIndex][1]) temp.Add(points[lIndex++]);
                    else temp.Add(points[rIndex]);
                }
                while (lIndex < middle) temp.Add(points[lIndex++]);
                while (rIndex < rightEx) temp.Add(points[rIndex++]);

                for (int i = 0; i < temp.Count; i++)
                {
                    points[leftIn + i] = temp[i];
                }


                List<float[]> strip = new List<float[]>();
                for (int i = leftIn; i < rightEx; i++)
                {
                    float dx = points[i][0] - middle;
                    if (dx * dx < D2) strip.Add(points[i]);
                }

                for (int i = 0; i < strip.Count; i++)
                {
                    for (int j = i + 1; j < strip.Count && j <= i + 7; j++)
                    {
                        float dy = points[i][1] - points[j][1];

                        if (dy * dy > D2) break;

                        float dx = points[i][0] - points[i][1];

                        float Dsqr = dx * dx + dy * dy;
                        if (Dsqr < D2) D2 = Dsqr;

                    }
                }

                return D2;
            }

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

    public class SimulationExample
    {
        public static class Ex01
        {
            static readonly int[][][] direction = new int[][][]
            {
                null,
                new int[][] { new int[] { 0}, new int[] {1 }, new int[] {2}, new int[] { 3 } },
                new int[][] { new int[] { 0, 2}, new int[] {1, 3 } },
                new int[][] { new int[] { 0, 1}, new int[] {1, 2 }, new int[] {2, 3}, new int[] { 3, 0 } },
                new int[][] { new int[] { 0, 1, 2}, new int[] { 1, 2, 3}, new int[] { 2, 3, 1}, new int[] {3, 1, 2} },
                new int[][] { new int[] { 0, 1, 2, 3} }
            };
            static List<(int x, int y, int type)> cctvs;
            static int answer = 0;
            static int n, m;
            static readonly int[] dx = new int[] { 1, 0, -1, 0 };
            static readonly int[] dy = new int[] { 0, 1, 0, -1 };

            public static int Ex01Solver(int N, int M, int[,] map)
            {
                answer = N * M;
                n = N;
                m = M;
                cctvs = new List<(int, int, int)>();

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        if (map[i, j] != 0 && map[i, j] != 6)
                        {
                            cctvs.Add((i, j, map[i, j]));
                        }
                    }
                }

                DFS(0, map);
                return answer;
            }

            private static void DFS(int depth, int[,] nowMap)
            {
                if (depth == cctvs.Count)
                {
                    int empty = 0;

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            if (nowMap[i, j] == 0) empty++;
                        }
                    }

                    answer = empty < answer ? empty : answer;
                    return;
                }

                (int x, int y, int type) = cctvs[depth];

                foreach (int[] dirs in direction[type])
                {
                    int[,] copiedMap = (int[,])nowMap.Clone();

                    foreach (int dir in dirs)
                    {
                        Watch(x, y, dir, copiedMap);
                    }

                    DFS(depth + 1, copiedMap);

                }

            }

            private static void Watch(int x, int y, int dir, int[,] nowMap)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];

                while (nx >= 0 && ny >= 0 && nx < n && ny < m && nowMap[nx, ny] != 6)
                {
                    nowMap[nx, ny] = -1;

                    nx += dx[dir];
                    ny += dy[dir];
                }
            }


        }

        public static class Ex02
        {
            static int answer = 0;

            public static int Solver(int N, int M, int K, List<(int, int, int[,])> stickers)
            {
                answer = 0;

                int[,] noteBook = new int[N, M];

                for (int i = 0; i < K; i++)
                {
                    (int r, int c, int[,] sticker) = stickers[i];

                    FindPosition(r, c, sticker, N, M, noteBook);
                }

                return answer;
            }

            private static void FindPosition(int r, int c, int[,] sticker, int N, int M, int[,] noteBook)
            {
                for (int i = 0; i <= N - r; i++)
                {
                    for (int j = 0; j <= M - c; j++)
                    {
                        if (TryAttach(r, c, sticker, i, j, noteBook)) return;
                    }
                }

                int[,] sticker90 = Rotate(r, c, sticker);
                //int[,] sticker90 = new int[c, r];
                //for (int i = 0; i < r; i++)
                //{
                //    for (int j = 0; j < c; j++)
                //    {
                //        sticker90[j, i] = sticker[r - 1 - i, j];
                //    }
                //}

                for (int i = 0; i <= N - c; i++)
                {
                    for (int j = 0; j <= M - r; j++)
                    {
                        if (TryAttach(c, r, sticker90, i, j, noteBook)) return;
                    }
                }

                int[,] sticker180 = Rotate(c, r, sticker90);
                //int[,] sticker180 = new int[r, c];
                //for (int i = 0; i < c; i++)
                //{
                //    for (int j = 0; j < r; j++)
                //    {
                //        sticker180[j, i] = sticker90[c - i - 1, j];
                //    }
                //}

                for (int i = 0; i <= N - r; i++)
                {
                    for (int j = 0; j <= M - c; j++)
                    {
                        if (TryAttach(r, c, sticker180, i, j, noteBook)) return;
                    }
                }


                int[,] sticker270 = Rotate(r, c, sticker180);
                //int[,] sticker270 = new int[c, r];
                //for (int i = 0; i < r; i++)
                //{
                //    for (int j = 0; j < c; j++)
                //    {
                //        sticker270[j, i] = sticker180[r - i - 1, j];
                //    }
                //}

                for (int i = 0; i <= N - c; i++)
                {
                    for (int j = 0; j <= M - r; j++)
                    {
                        if (TryAttach(c, r, sticker270, i, j, noteBook)) return;
                    }
                }
            }

            private static bool TryAttach(int r, int c, int[,] sticker, int y, int x, int[,] noteBook)
            {
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        if (noteBook[i + y, j + x] == 1 && sticker[i, j] == 1) return false;
                    }
                }

                int sum = 0;
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        if (sticker[i, j] == 1)
                        {
                            sum++;
                            noteBook[y + i, x + j] = 1;
                        }
                    }
                }

                answer += sum;
                return true;
            }

            private static int[,] Rotate(int r, int c, int[,] original)
            {
                int[,] rotated = new int[c, r];

                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        rotated[j, i] = original[r - i - 1, j];
                    }
                }

                return rotated;
            }

        }

        public static class Ex03
        {
            static int answer;
            static int n;
            static int m;
            static List<(int x, int y)> chickenPos;
            static List<(int x, int y)> housePos;

            public static int Solver(int N, int M, int[,] map)
            {
                answer = int.MaxValue;
                n = N;
                m = M;

                FindPoses(map);
                Function(0, m, new List<int>());

                return answer;
            }

            public static void FindPoses(int[,] map)
            {
                housePos = new List<(int x, int y)>();
                chickenPos = new List<(int x, int y)>();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        switch (map[i, j])
                        {
                            case 1:
                                {
                                    housePos.Add((i, j));
                                    break;
                                }
                            case 2:
                                {
                                    chickenPos.Add((i, j));
                                    break;
                                }
                            default: break;
                        }
                    }
                }
            }

            public static void Function(int startIndex, int endCount, List<int> chosenIndex)
            {
                if (chosenIndex.Count == endCount)
                {
                    int result = CalculateLeastDistance(chosenIndex);
                    answer = result < answer ? result : answer;
                    return;
                }

                for (int i = startIndex; i < chickenPos.Count; i++)
                {
                    chosenIndex.Add(i);
                    Function(startIndex + 1, endCount, chosenIndex);
                    chosenIndex.RemoveAt(chosenIndex.Count - 1);
                }
            }

            public static int CalculateLeastDistance(List<int> chosenIndex)
            {
                int sum = 0;
                for (int i = 0; i < housePos.Count; i++)
                {
                    int leastDist = 2 * n;
                    (int xh, int yh) = housePos[i];

                    for (int j = 0; j < chosenIndex.Count; j++)
                    {
                        (int xc, int yc) = chickenPos[chosenIndex[j]];
                        int dist = Math.Abs(xh - xc) + Math.Abs(yh - yc);
                        leastDist = dist < leastDist ? dist : leastDist;
                    }
                    sum += leastDist;
                }

                return sum;
            }
        }
    }

    public class Sorting
    {
        public static class MergeSorting
        {
            static int N, M;
            static int[] original;
            static int[] tempSorted;


            public static int[] MergeSort(int[] array)
            {
                original = array;
                int length = array.Length;
                tempSorted = new int[length];
                Merge(0, length);

                return original;
            }

            private static void Merge(int start, int end)
            {
                if (end - start == 1) return;

                int mid = (start + end) / 2;
                Merge(start, mid);
                Merge(mid, end);
                MergeTwo(start, end);
            }

            private static void MergeTwo(int start, int end)
            {
                int mid = (start + end) / 2;
                int left = start;
                int right = mid;

                int di = 0;

                while (left < mid && right < end)
                {
                    tempSorted[start + di] = original[left] < original[right] ? original[left++] : original[right++];
                    di++;
                }
                while (left < mid)
                {
                    tempSorted[start + di] = original[left++];
                    di++;
                }
                while (right < end)
                {
                    tempSorted[start + di] = original[right++];
                    di++;
                }

                Array.Copy(tempSorted, start, original, start, end - start);
            }

            public static int[] Merge(int n, int m, int[] a, int[] b)
            {
                int[] result = new int[n + m];

                N = n;
                M = m;
                int aIndex = 0;
                int bIndex = 0;
                int resultIndex = 0;

                while (aIndex < n && bIndex < m)
                {
                    result[resultIndex++] = a[aIndex] < b[bIndex] ? a[aIndex++] : b[bIndex++];
                }
                while (aIndex < n)
                {
                    result[resultIndex++] = a[aIndex++];
                }
                while (bIndex < m)
                {
                    result[resultIndex++] = b[bIndex++];
                }

                return result;
            }

        }

        public static class CountSorting
        {
            static int[] original;
            static int[] counts;

            public static int[] CountSort(int[] a)
            {
                int min = a.Min();
                int max = a.Max();
                int count = max - min + 1;

                original = a;
                counts = new int[count];
                int limit = original.Length;

                for (int i = 0; i < limit; i++)
                {
                    counts[original[i] - min]++; ;
                }

                int added = 0;
                for (int i = 0; i < count && added < limit; i++)
                {
                    while (counts[i] > 0)
                    {
                        counts[i]--;
                        original[added++] = i + min;
                    }
                }


                return original;
            }
        }
    }

    public class SortingExample
    {
        public static class Ex01_re
        {
            public static string[] Solver(int N, string[] array)
            {
                Array.Sort(array, (a, b) =>
                {
                    if (a.Length != b.Length) return a.Length.CompareTo(b.Length);

                    int sum1 = GetSum(a);
                    int sum2 = GetSum(b);
                    if (sum1 != sum2) return sum1.CompareTo(sum2);

                    return CompareWithLetterPriority(a, b);


                });


                return array;
            }

            private static int GetSum(string s)
            {
                int sum = 0;
                foreach (char c in s)
                {
                    if (char.IsDigit(c)) sum += c - '0';
                }
                return sum;

            }

            private static int CompareWithLetterPriority(string s1, string s2)
            {
                int limit = s1.Length;
                for (int i = 0; i < limit; i++)
                {
                    char c1 = s1[i];
                    char c2 = s2[i];

                    if (c1 == c2) continue;

                    bool c1IsDigit = char.IsDigit(c1);
                    bool c2IsDigit = char.IsDigit(c2);

                    if (c1IsDigit && !c2IsDigit) return -1;
                    if (!c1IsDigit && c2IsDigit) return 1;

                    return c1.CompareTo(c2);
                }

                return -1;
            }
        }

        public static class Ex01
        {
            static string[] result;

            public static string[] Solver(int N, string[] array)
            {
                Array.Sort(array, (a, b) =>
                {
                    if (a.Length != b.Length)
                        return a.Length.CompareTo(b.Length);

                    int sum1 = GetSum(a);
                    int sum2 = GetSum(b);
                    if (sum1 != sum2)
                        return sum1.CompareTo(sum2);

                    return CompareWithLetterPriority(a, b);
                });

                return array;
            }

            private static int GetSum(string s1)
            {
                int sum = 0;
                foreach (char c in s1)
                {
                    if (char.IsDigit(c)) sum += c - '0';
                }

                //foreach(string s in s1.Select(ch => ch.ToString()).ToArray())
                //{
                //    if (int.TryParse(s, out int x))
                //    {
                //        sum += x;
                //    }
                //}

                return sum;
            }

            private static int CompareWithLetterPriority(string s1, string s2)
            {
                int len = s1.Length;

                for (int i = 0; i < len; i++)
                {
                    char ca = s1[i];
                    char cb = s2[i];

                    if (ca == cb) continue;

                    bool aIsDigit = char.IsDigit(ca);
                    bool bIsDigit = char.IsDigit(cb);

                    if (aIsDigit && !bIsDigit) return -1;
                    if (!aIsDigit && bIsDigit) return 1;

                    return ca.CompareTo(cb);
                }

                return 1;
            }

        }

        public static class Ex02
        {
            public static long Solver(long[] array)
            {
                int maxCount = 0;
                long maxNumber = 0;
                int count = 0;

                Array.Sort(array, (a, b) => a.CompareTo(b));

                maxNumber = array[0];
                maxCount = 1;
                count = 1;
                long before = array[0];

                for (int i = 1; i < array.Length; i++)
                {
                    long now = array[i];
                    if (now == before)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > maxCount)
                        {
                            maxNumber = before;
                            maxCount = count;
                        }
                        count = 1;
                    }
                    before = now;
                }

                if (count > maxCount)
                {
                    maxNumber = before;
                    maxCount = count;
                }
                return maxNumber;

            }
        }

    }

    public class Dynamic
    {
        public static class EX01
        {
            static int[] arr;

            public static int Solver(int target)
            {
                arr = new int[target + 1];
                int i = 1;
                arr[1] = 0;

                while (i != target)
                {
                    i++;
                    int d1 = int.MaxValue;
                    int d2 = int.MaxValue;
                    int d3 = int.MaxValue;

                    if (i % 3 == 0) d1 = arr[i / 3] + 1;
                    if (i % 2 == 0) d2 = arr[i / 2] + 1;
                    d3 = arr[i - 1] + 1;
                    arr[i] = Math.Min(Math.Min(d1, d2), d3);
                }

                return arr[i];
            }
        }

        public static class EX02
        {
            public static int[,] arr;
            public static int[] maxes;

            public static int Solver(int target, int[] steps)
            {
                if (target == 1) return steps[0];

                if (target == 2) return steps[0] + steps[1];

                int i = 2;
                arr = new int[target, 2];

                arr[0, 0] = steps[0];
                arr[0, 1] = 0;
                arr[1, 0] = steps[0] + steps[1];
                arr[1, 1] = steps[1];

                while (i < target)
                {
                    arr[i, 0] = arr[i - 1, 1] + steps[i];
                    arr[i, 1] = Math.Max(arr[i - 2, 0], arr[i - 2, 1]) + steps[i];
                    Console.WriteLine($"{i}, {Math.Max(arr[i, 0], arr[i, 1])}");
                    i++;
                }

                return Math.Max(arr[i - 1, 0], arr[i - 1, 1]);

            }

            public static int[] subs;
            public static int Solver2(int target, int[] steps)
            {
                if (target == 1) return steps[0];
                if (target == 2) return steps[0] + steps[1];

                target = target += 1;
                subs = new int[target];
                int i = 3;
                int sum = steps[0] + steps[1] + steps[2];
                subs[0] = steps[0];
                subs[1] = steps[1];
                subs[2] = steps[2];

                while (i < target)
                {
                    sum += steps[i];
                    subs[i] = Math.Min(subs[i - 2], subs[i - 3]) + steps[i];
                    Console.WriteLine($"{i}: {sum - subs[i]}");
                    i++;
                }

                return sum - subs[i - 1];
            }
        }

        public static class EX03
        {
            static int[,] costSum;

            public static int Solver(int N, int[,] costs)
            {
                costSum = new int[N, 3];

                costSum[0, 0] = costs[0, 0];
                costSum[0, 1] = costs[0, 1];
                costSum[0, 2] = costs[0, 2];

                for (int i = 1; i < N; i++)
                {
                    costSum[i, 0] = Math.Min(costSum[i - 1, 1], costSum[i - 1, 2]) + costs[i, 0];
                    costSum[i, 1] = Math.Min(costSum[i - 1, 0], costSum[i - 1, 2]) + costs[i, 1];
                    costSum[i, 2] = Math.Min(costSum[i - 1, 0], costSum[i - 1, 1]) + costs[i, 2];
                }

                return Math.Min(Math.Min(costSum[N - 1, 0], costSum[N - 1, 1]), costSum[N - 1, 2]);
            }
        }

        public static class EX04
        {

            public static int Solver(int N)
            {
                int[] arr = new int[N + 1];

                arr[0] = 0;
                arr[1] = 1;
                arr[2] = 2;
                int i = 3;
                if (N <= 2) return arr[N];

                while (i <= N)
                {
                    arr[i] = arr[i - 1] + arr[i - 2];
                    i += 1;
                }
                return arr[N];

            }
        }

        public static class EX05
        {
            public static int[] Solver(int N, int M, int[] arr, int[,] intervals)
            {
                int[] sums = new int[N + 1];
                sums[0] = 0;
                int[] results = new int[M];

                for (int i = 1; i < N + 1; i++)
                {
                    sums[i] = sums[i - 1] + arr[i - 1];
                }

                for (int i = 0; i < M; i++)
                {
                    results[i] = sums[intervals[i, 1]] - sums[intervals[i, 0] - 1];
                }

                return results;
            }
        }

        public static class EX06
        {
            public static (int, int[]) Solver(int N)
            {
                int[] mins = new int[N + 1];
                int[] nexts = new int[N + 1];
                List<int> traces = new List<int>();


                mins[0] = 0;
                nexts[0] = 0;
                mins[1] = 0;
                nexts[1] = 0;

                if (N == 1)
                {
                    traces.Add(0);
                    return (mins[1], new int[] { 1 });
                }


                mins[2] = 1;
                nexts[2] = 1;
                int i = 3;
                int d1 = 0;

                while (i <= N)
                {
                    mins[i] = mins[i - 1] + 1;
                    nexts[i] = i - 1;

                    if (i % 3 == 0)
                    {
                        d1 = mins[i / 3] + 1;
                        if (d1 < mins[i])
                        {
                            mins[i] = d1;
                            nexts[i] = i / 3;
                        }
                    }
                    if (i % 2 == 0)
                    {
                        d1 = mins[i / 2] + 1;
                        if (d1 < mins[i])
                        {
                            mins[i] = d1;
                            nexts[i] = i / 2;
                        }
                    }
                    i++;
                }

                i = N;
                traces.Add(N);
                while (i != 1)
                {
                    i = nexts[i];
                    traces.Add(i);
                }

                return (mins[N], traces.ToArray());

            }
        }

        public static class EX07
        {
            public static int Solver(int coinCounts, int targetCost, int[] coinValues)
            {
                int[] results = new int[targetCost + 1];

                results[0] = 0;
                results[1] = 1;
                if (targetCost == 1) return 1;

                int i = 2;
                while (i <= targetCost)
                {
                    int d = int.MaxValue; ;
                    for (int j = 0; j < coinCounts; j++)
                    {
                        if (i >= coinValues[j] && i % coinValues[j] == 0)
                        {
                            if (results[i - coinValues[j]] + 1 < d)
                            { d = results[i - coinValues[j]] + 1; }
                        }
                    }

                    results[i] = d;
                    i++;

                }

                return results[targetCost];
            }
        }

        public static class EX08
        {
            public static int Solver(int N, int K, int[,] Vs)
            {
                int[,] Ds = new int[N + 1, K + 1];


                for (int i = 1; i <= N; i++)
                {
                    int W = Vs[i - 1, 0];
                    int V = Vs[i - 1, 1];

                    for (int j = 0; j <= K; j++)
                    {
                        if (j >= W)
                        {
                            Ds[i, j] = Math.Max(Ds[i - 1, j], Ds[i - 1, j - W] + V);
                        }
                        else
                        {
                            Ds[i, j] = Ds[i - 1, j];
                        }
                    }
                }

                return Ds[N, K];
            }

            public static int Solver2(int N, int K, int[,] Vs)
            {
                int[] Ds = new int[K + 1];

                for (int i = 0; i < N; i++)
                {
                    int W = Vs[i, 0];
                    int V = Vs[i, 1];

                    for (int j = K; j >= W; j--)
                    {
                        Ds[j] = Math.Max(Ds[j], Ds[j - W] + V);
                    }
                }

                return Ds[K];
            }

        }

        public static class EX09
        {
            public static int Solver(int N, int K)
            {
                int[,] Ds = new int[N + 1, N + 1];

                for (int i = 1; i <= N; i++)
                {
                    Ds[i, 0] = 1;
                    Ds[i, i] = 1;
                    for (int j = 1; j < i; j++)
                    {
                        Ds[i, j] = (Ds[i - 1, j] + Ds[i - 1, j - 1]) % 10007;
                    }
                }

                return Ds[N, K];
            }
        }
    }

    public class Greedy
    {
        public static class EX01
        {
            public static int Solver(int coinCounts, int targetCost, int[] coinValues)
            {
                int result = 0;
                int rest = targetCost;

                for (int i = coinCounts - 1; i >= 0; i--)
                {
                    while (rest >= coinValues[i])
                    {
                        result += rest / coinValues[i];
                        rest = rest % coinValues[i];
                    }
                }

                return result;
            }
        }

        public static class EX02
        {
            public static int Solver(int N, int[][] times)
            {

                Array.Sort(times, (a, b) =>
                {
                    int x = a[1].CompareTo(b[1]);

                    if (x == 0) return a[0].CompareTo(b[0]);

                    return x;
                });

                int result = 0;
                int t = 0;
                for (int i = 0; i < N; i++)
                {
                    if (t > times[i][0]) continue;

                    result++;

                    t = times[i][1];
                }

                return result;
            }
        }

        public static class EX03
        {
            public static float Solver(int N, int[] ropes)
            {
                float answer = 0;
                Array.Sort(ropes, (a, b) => b.CompareTo(a));
                for (int i = 1; i <= N; i++)
                {
                    float result = 0;
                    for (int j = 0; j < i; j++)
                    {
                        result += ropes[j];
                    }
                    result /= i;
                    if (result > answer) answer = result;
                }

                return answer;
            }
        }

        public static class EX04
        {
            public static int Solver(int N, int[] arrayA, int[] arrayB)
            {
                Array.Sort(arrayA, (a, b) => a.CompareTo(b));
                Array.Sort(arrayB, (a, b) => -a.CompareTo(b));

                int answer = 0;
                for (int i = 0; i < N; i++)
                {
                    answer += arrayA[i] * arrayB[i];
                }

                return answer;
            }
        }
    }

    public class MathPractice
    {
        public static class Prime
        {
            public static bool CheckIsPrime(int n)
            {
                if (n == 1) return false;
                for (int i = 2; i * i < n; i++)
                {
                    if (n % i == 0) return false;
                }

                return true;
            }

            public static List<int> GetPrime(int n)
            {
                List<int> primes = new List<int>();

                for (int i = 2; i <= n; i++)
                {
                    bool isPrime = true;
                    foreach (int p in primes)
                    {
                        if (p * p > i) break;
                        if (i % p == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime) primes.Add(i);
                }

                return primes;
            }

            public static List<int> Sieve(int n)
            {
                bool[] isPrime = new bool[n + 1];
                List<int> primes = new List<int>();
                for (int i = 1; i <= n; i++) isPrime[i] = true;

                for (int i = 2; i * i <= n; i++)
                {
                    if (!isPrime[i]) continue;

                    for (int j = i * i; j <= n; j += i)
                    {
                        isPrime[j] = false;
                    }
                }

                for (int i = 2; i <= n; i++)
                {
                    if (isPrime[i]) primes.Add(i);
                }

                return primes;
            }


            public static List<int> SieveFromTo(int from, int to)
            {
                bool[] isPrime = new bool[to + 1];
                List<int> primes = new List<int>();
                for (int i = 2; i <= to; i++) isPrime[i] = true;

                for (int i = 2; i * i <= to; i++)
                {
                    if (!isPrime[i]) continue;

                    for (int j = i * i; j <= to; j += i)
                    {
                        isPrime[j] = false;
                    }
                }

                for (int i = from; i <= to; i++)
                {
                    if (isPrime[i]) primes.Add(i);
                }

                return primes;
            }

            public static List<int> DivideByPrime(int n)
            {
                List<int> primes = new List<int>();

                for (int i = 2; i * i <= n; i++)
                {
                    while (n % i == 0)
                    {
                        primes.Add(i);
                        n /= i;
                    }

                }
                if (n != 1) primes.Add(n);

                return primes;
            }

            public static List<int> GetDivisor(int n)
            {
                List<int> divisors = new List<int>();

                for (int i = 1; i * i <= n; i++)
                {
                    if (n % i == 0)
                    {
                        divisors.Add(i);
                    }
                }

                for (int i = divisors.Count - 1; i >= 0; i--)
                {
                    int div = divisors[i];
                    if (div * div == n) continue;
                    divisors.Add(n / div);
                }

                return divisors;
            }

            public static int GetGCD(int n, int m)
            {
                if (n == 0) return m;
                return GetGCD(m % n, n);
            }

            public static int GetLCM(int n, int m)
            {
                return n / GetGCD(n, m) * m;
            }

            public static class EX01
            {

                public static int[] Solver(int N, int[][] dates)
                {
                    int[] answer = new int[N];

                    for (int i = 0; i < N; i++)
                    {
                        int a = dates[i][0];
                        int b = dates[i][1];
                        int m = dates[i][2];
                        int n = dates[i][3];
                        if (b == n) n = 0;

                        int l = GetLCM(a, b);
                        bool found = false;
                        for (int j = m; j <= l; j += a)
                        {
                            if (j % b == n)
                            {
                                answer[i] = j;
                                found = true;
                                break;
                            }
                        }
                        if (!found) answer[i] = -1;

                    }

                    return answer;

                }

            }
        }
    }

    public class BinarySearch
    {
        public class EX01
        {
            public static bool BinarySearchByRecurrence(int[] array, int target, int start, int end, bool needSort = false)
            {
                if (needSort) Array.Sort(array, (a, b) => a.CompareTo(b));

                if (start > end)
                {
                    return false;
                }

                int mid = (end + start) / 2;
                bool found = false;

                if (array[mid] == target) return true;
                else if (array[mid] > target)
                    return BinarySearchByRecurrence(array, target, start, mid - 1);
                else
                    return BinarySearchByRecurrence(array, target, mid + 1, end);
            }

            public static bool BinarySearch(int[] arr, int target, int start, int end, bool needSort = false)
            {
                if (needSort) Array.Sort(arr, (a, b) => a.CompareTo(b));

                while (start <= end)
                {
                    int mid = (start + end) / 2;
                    if (arr[mid] == target) return true;
                    else if (target < arr[mid]) end = mid - 1;
                    else start = mid + 1;
                }

                return false;
            }

            public static int[] Solver(int N, int[] array, int M, int[] targets)
            {
                int[] answer = new int[M];

                Array.Sort(array, (a, b) => a.CompareTo(b));

                for (int i = 0; i < M; i++)
                {
                    //answer[i] = BinarySearch(array, targets[i], 0, N - 1) ? 1 : 0;
                    //answer[i] = Array.BinarySearch(array, targets[i]) >= 0 ? 1 : 0;
                    answer[i] = Array.BinarySearch(array, targets[i]);
                }

                return answer;
            }
        }

        public static int BinarySearchIndex(int[] arr, int target)
        {
            Array.Sort(arr, (a, b) => a.CompareTo(b));

            int start = 0;
            int end = arr.Length - 1;

            while (start <= end)
            {
                int mid = (start + end) / 2;
                if (arr[mid] == target) return mid;
                else if (target < arr[mid]) end = mid - 1;
                else if (arr[mid] < target) start = mid + 1;
            }

            return -1;
        }

        public static int GetLowerBoundIndex(int[] arr, int target)
        {
            int start = 0;
            int end = arr.Length;

            while (start < end)
            {
                int mid = (start + end) / 2;

                if (arr[mid] < target) start = mid + 1;
                else end = mid;
            }

            return start;
        }

        public static int GetUpperBoundIndex(int[] arr, int target)
        {
            int start = 0;
            int end = arr.Length;

            while (start < end)
            {
                int mid = (start + end) / 2;

                if (arr[mid] <= target) start = mid + 1;
                else end = mid;
            }

            return start;
        }

        public class EX02
        {
            public static int[] Solver(int N, int[] cards, int M, int[] targets)
            {
                int[] answer = new int[M];

                Array.Sort(cards, (a, b) => a.CompareTo(b));
                for (int i = 0; i < M; i++)
                {
                    answer[i] = GetUpperBoundIndex(cards, targets[i]) - GetLowerBoundIndex(cards, targets[i]);
                }
                return answer;
            }
        }

        public static int GetTargetLeftIndex(int[] arr, int target)
        {
            int start = -1;
            int end = arr.Length - 1;

            while (start < end)
            {
                int mid = (start + end + 1) / 2; //// Check When end = start + 1;
                if (target <= arr[mid]) end = mid - 1;
                else if (arr[mid] < target) start = mid;
            }
            return start;
        }

        public class EX03
        {
            public static int[] Solver(int N, int[] arr)
            {
                int[] answer = new int[N];
                int[] unique = arr.Distinct().OrderBy(x => x).ToArray();
                for (int i = 0; i < N; i++)
                {
                    answer[i] = BinarySearchIndex(unique, arr[i]);
                }
                return answer;
            }
        }

        public class EX04
        {
            public static int Solver(int N, int[] arr)
            {
                HashSet<int> sumSet = new HashSet<int>();

                Array.Sort(arr, (a, b) => a.CompareTo(b));

                for (int i = 0; i < N; i++)
                {
                    for (int j = i; j < N; j++)
                    {
                        sumSet.Add(arr[i] + arr[j]);
                    }
                }

                for (int i = N - 1; i >= 0; i--)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (sumSet.Contains(arr[i] - arr[j])) return arr[i];
                    }
                }

                return -1;
            }
        }

        public class EX05
        {
            public static int Solver(int K, int N, int[] arr)
            {
                int made = 0;

                Array.Sort(arr, (a, b) => a.CompareTo(b));

                int start = 1;
                int end = arr[0];

                while (start < end)
                {
                    made = 0;
                    int mid = (start + end + 1) / 2;

                    for (int i = 0; i < K; i++)
                    {
                        made += arr[i] / mid;
                    }

                    if (made < N) end = mid - 1;
                    else start = mid;
                }

                return start;
            }
        }

        public class EX06
        {
            public static int Solver(int N, int M, int[] arr)
            {
                int answer = int.MaxValue;

                Array.Sort(arr, (a, b) => a.CompareTo(b));
                for (int i = 0; i < N; i++)
                {
                    int target = arr[i] + M;
                    int ind = Array.BinarySearch(arr, target);

                    if (0 <= ind) return M;

                    if (ind < 0) ind = ~ind;

                    if (ind >= N) continue;
                    else if (ind == 0) return M;
                    answer = Math.Min(answer, arr[ind] - arr[i]);
                }

                return answer;
            }

            public static int Solver2(int N, int M, int[] arr)
            {
                int answer = int.MaxValue;

                Array.Sort(arr, (a, b) => a.CompareTo(b));

                for (int i = 0; i < N; i++)
                {
                    int target = arr[i] + M;
                    int ind = BinarySearch(arr, i, target);

                    if (ind == N) continue;

                    int mini = arr[ind] - arr[i];
                    answer = Math.Min(answer, mini);
                }
                return answer;
            }

            public static int BinarySearch(int[] arr, int start, int target)
            {
                int st = start;
                int en = arr.Length;

                while (st < en)
                {
                    int mid = (st + en) / 2;

                    if (arr[mid] == target) return mid;
                    else if (arr[mid] < target) st = mid + 1;
                    else en = mid;
                }

                return st;
            }


        }
    }

    public class TwoPointer
    {
        public static class EX01
        {
            public static int Solver(int N, int M, int[] arr)
            {
                int answer = int.MaxValue;

                int start = 0;
                int end = 0;

                Array.Sort(arr, (a, b) => a.CompareTo(b));

                for (int i = 0; i < N; i++)
                {
                    while (end < N && arr[end] - arr[start] < M) end++;
                    if (end == N) continue;

                    answer = Math.Min(answer, arr[end] - arr[start]);
                }

                return answer;
            }
        }

        public static class EX02
        {
            public static int Solver(int N, int S, int[] arr)
            {
                int answer = int.MaxValue;
                int end = 0;
                int sum = arr[0];

                for (int start = 0; start < N; start++)
                {
                    while (end < N && sum < S)
                    {
                        end++;

                        if (end != N) sum += arr[end];
                    }
                    if (end == N) break;

                    answer = Math.Min(answer, end - start + 1);

                    sum -= arr[start];
                }

                if (answer == int.MaxValue) return 0;

                return answer;
            }
        }
    }

    public class BinaryTree
    {
        public static class EX01
        {
            public static string[] Solver(int N, List<(int, List<(char, int)>)> Operators)
            {
                SortedDictionary<int, int> result = new SortedDictionary<int, int>();

                string[] answer = new string[N];
                for (int i = 0; i < N; i++)
                {
                    (int M, List<(char, int)> ops) = Operators[i];

                    for (int j = 0; j < M; j++)
                    {
                        (char op, int value) = ops[j];

                        switch (op)
                        {
                            case 'I':
                                {
                                    Add(result, value);
                                    break;
                                }
                            case 'D':
                                {
                                    if (result.Count == 0) break;

                                    if (value == 1) Remove(result, result.Keys.Max());
                                    else if (value == -1) Remove(result, result.Keys.Min());
                                    break;
                                }
                        }
                    }

                    if (result.Count == 0) answer[i] = "EMPTY";

                    else answer[i] = $"{result.Keys.Max()} {result.Keys.Min()}";
                }

                return answer;
            }

            static void Add(SortedDictionary<int, int> dic, int x)
            {
                if (dic.ContainsKey(x))
                {
                    dic[x]++;
                }
                else
                    dic[x] = 1;
            }

            static bool Remove(SortedDictionary<int, int> dic, int x)
            {
                if (dic.ContainsKey(x))
                {
                    dic[x]--;
                    if (dic[x] == 0)
                    {
                        dic.Remove(x);
                    }
                    return true;
                }

                return false;
            }
        }

        public static class EX02
        {
            public static int Solver(int N, int K, int[][] jewels, int[] bags)
            {
                SortedDictionary<int, int> bagsSD = new SortedDictionary<int, int>();

                Array.Sort(jewels, (a, b) => a[1].CompareTo(b[1]));

                for (int i = 0; i < K; i++)
                {
                    Add(bagsSD, bags[i]);
                }

                int sum = 0;
                int bag = bagsSD.Keys.Max();

                while (N > 0)
                {
                    N--;
                    int Mi = jewels[N][0];
                    int Vi = jewels[N][1];

                    if (bag < Mi) continue;

                    bagsSD.Remove(bagsSD.Keys.Max());
                    sum += Vi;

                    if (bagsSD.Count == 0) return sum;
                    bag = bagsSD.Keys.Max();

                }

                return sum;
            }

            public static void Add(SortedDictionary<int, int> sd, int bagsM)
            {
                if (sd.ContainsKey(bagsM))
                {
                    sd[bagsM]++; return;
                }

                sd[bagsM] = 1;
            }

            public static bool Remove(SortedDictionary<int, int> sd, int bagsM)
            {
                if (sd.ContainsKey(bagsM))
                {
                    sd[bagsM]--;
                    if (sd[bagsM] == 0) sd.Remove(bagsM);

                    return true;
                }

                return false;
            }
        }
    }

    public class Graph
    {
        public static class EX01
        {
            public static void BFS(List<int>[] adj)
            {
                int N = adj.Length;
                Queue<int> q = new Queue<int>();
                bool[] vis = new bool[N + 1];

                q.Enqueue(0);
                vis[0] = true;

                while (q.Any())
                {
                    int cur = q.Dequeue();

                    foreach (int v in adj[cur])
                    {
                        if (vis[v]) continue;
                        vis[v] = true;
                        q.Enqueue(v);
                    }
                }
            }

            public static void BFSwDistance(List<int>[] adj, int start)
            {
                int N = adj.Length;
                Queue<int> q = new Queue<int>();
                int[] dist = Enumerable.Repeat(-1, N).ToArray();

                q.Enqueue(start);
                dist[start] = 0;
                while (q.Any())
                {
                    int cur = q.Dequeue();

                    foreach (int v in adj[cur])
                    {
                        if (dist[v] != -1) continue;
                        q.Enqueue(v);
                        dist[v] = dist[cur] + 1;
                    }
                }
            }

            public static void BFSwoLinkedGraph(List<int>[] adj)
            {
                int N = adj.Length;
                bool[] vis = new bool[N];
                Queue<int> q = new Queue<int>();

                for (int u = 0; u < N; u++)
                {
                    if (vis[u]) continue;

                    q.Enqueue(u);
                    vis[u] = true;

                    while (q.Any())
                    {
                        int cur = q.Dequeue();

                        foreach (int v in adj[cur])
                        {
                            if (vis[v]) continue;
                            q.Enqueue(v);
                            vis[v] = true;
                        }
                    }
                }
            }

            public static void DFSwoLinkedGraph2(List<int>[] adj)
            {
                int N = adj.Length;
                bool[] vis = new bool[N];
                Stack<int> s = new Stack<int>();

                for (int u = 0; u < N; u++)
                {
                    if (vis[u]) continue;

                    s.Push(u);
                    vis[u] = true;

                    while (s.Any())
                    {
                        int cur = s.Pop();

                        foreach (int v in adj[cur])
                        {
                            if (vis[v]) continue;
                            s.Push(v);
                            vis[v] = true;
                        }
                    }

                }

            }

            public static void DFSwoLinkedGraph(List<int>[] adj, int start)
            {
                int N = adj.Length;
                bool[] vis = new bool[N];
                Stack<int> s = new Stack<int>();

                s.Push(start);
                vis[start] = true;

                while (s.Any())
                {
                    int u = s.Pop();
                    if (vis[u]) continue;
                    vis[u] = true;

                    foreach (int v in adj[u])
                    {
                        if (vis[v]) continue;
                        s.Push(v);
                    }
                }
            }
        }

        public static class EX02
        {
            public static int Solver(int N, int M, List<int[]> connects)
            {
                int answer = 0;
                List<int>[] adj = new List<int>[N];
                bool[] vis = new bool[N];

                for (int i = 0; i < N; i++)
                {
                    adj[i] = new List<int>();
                }

                foreach (int[] uv in connects)
                {
                    adj[uv[0] - 1].Add(uv[1] - 1);
                    adj[uv[1] - 1].Add(uv[0] - 1);
                }

                for (int i = 0; i < N; i++)
                {
                    if (vis[i]) continue;

                    Queue<int> q = new Queue<int>();
                    answer++;
                    q.Enqueue(i);
                    vis[i] = true;

                    while (q.Any())
                    {
                        int u = q.Dequeue();

                        foreach (int v in adj[u])
                        {
                            if (vis[v]) continue;
                            q.Enqueue(v);
                            vis[v] = true;
                        }
                    }

                }


                return answer;
            }
        }

        public static class EX03
        {
            static int N;
            static int M;
            static List<int>[] adj;

            public static int[][] Solver(int n, int m, int start, List<int[]> connects)
            {
                N = n;
                M = m;
                adj = new List<int>[N + 1];

                for (int i = 1; i <= N; i++)
                {
                    adj[i] = new List<int>();
                }

                connects.Sort((a, b) =>
                {
                    if (a[0] == b[0]) return a[1].CompareTo(b[1]);

                    return a[0].CompareTo(b[0]);
                });

                foreach (int[] uv in connects)
                {
                    adj[uv[0]].Add(uv[1]);
                    adj[uv[1]].Add(uv[0]);
                }

                int[][] answer = new int[2][] { getDFS(start), getBFS(start) };

                return answer;
            }

            public static int[] getDFS(int start)
            {
                List<int> result = new List<int>();

                Stack<int> s = new Stack<int>();
                bool[] vis = new bool[N + 1];

                s.Push(start);

                while (s.Any())
                {
                    int u = s.Pop();
                    if (vis[u]) continue;
                    vis[u] = true;
                    result.Add(u);

                    for (int i = adj[u].Count - 1; i >= 0; i--)
                    {
                        int v = adj[u][i];
                        if (vis[v]) continue;
                        s.Push(v);
                    }
                }

                return result.ToArray();
            }

            public static int[] getBFS(int start)
            {
                List<int> result = new List<int>();

                Queue<int> q = new Queue<int>();
                bool[] vis = new bool[N + 1];

                q.Enqueue(start);
                vis[start] = true;

                while (q.Any())
                {
                    int u = q.Dequeue();
                    result.Add(u);

                    foreach (int v in adj[u])
                    {
                        if (vis[v]) continue;
                        q.Enqueue(v);
                        vis[v] = true;
                    }
                }

                return result.ToArray();
            }
        }
    }

    public class TopologicalSort
    {
        public static class EX01
        {
            static List<int>[] adj;
            static int[] inDeg;
            static int N;
            static int M;

            public static int[] Solver(int n, int m, int[][] comp)
            {
                N = n;
                M = m;
                adj = new List<int>[N + 1];
                inDeg = new int[N + 1];

                int[] answer = new int[N];

                for (int i = 1; i <= N; i++)
                {
                    adj[i] = new List<int>();
                }

                for (int i = 0; i < M; i++)
                {
                    int u = comp[i][0];
                    int v = comp[i][1];

                    adj[u].Add(v);
                    inDeg[v]++;
                }

                Queue<int> q = new Queue<int>();

                for (int i = 1; i <= N; i++)
                {
                    if (inDeg[i] == 0) q.Enqueue(i);
                }

                int ind = 0;

                while (q.Any())
                {
                    int cur = q.Dequeue();

                    answer[ind++] = cur;

                    foreach (int nxt in adj[cur])
                    {
                        inDeg[nxt]--;

                        if (inDeg[nxt] == 0) q.Enqueue(nxt);
                    }

                }

                return answer;

            }

            public static int[] Solver2(int n, int m, int[][] comp)
            {
                M = m;
                N = n;
                adj = new List<int>[N + 1];
                inDeg = new int[N + 1];

                int[] answer = new int[N];

                for (int i = 1; i <= N; i++)
                {
                    adj[i] = new List<int>();
                }

                for (int i = 0; i < M; i++)
                {
                    int u = comp[i][0];
                    int v = comp[i][1];

                    adj[u].Add(v);
                    inDeg[v]++;
                }

                Stack<int> s = new Stack<int>();

                for (int i = 1; i <= N; i++)
                {
                    if (inDeg[i] == 0) s.Push(i);
                }

                int ind = 0;

                while (s.Any())
                {
                    int cur = s.Pop();
                    answer[ind++] = cur;

                    foreach (int nxt in adj[cur])
                    {
                        inDeg[nxt]--;

                        if (inDeg[nxt] == 0) s.Push(nxt);
                    }

                }

                return answer;
            }

        }

    }

    public class MinimumSpanningTree
    {
        public static class EX01
        {
            public class DSU
            {
                int[] parents;
                int[] rank;

                public DSU(int n)
                {
                    parents = new int[n + 1];
                    rank = new int[n + 1];

                    for (int i = 1; i <= n; i++)
                    {
                        parents[i] = i;
                        rank[i] = 1;
                    }
                }

                public int Find(int a)
                {
                    if (parents[a] == a) return a;
                    return parents[a] = Find(parents[a]);
                }

                public bool Union(int a, int b)
                {
                    a = Find(a);
                    b = Find(b);

                    if (a == b) return false;

                    if (rank[a] < rank[b])
                        (a, b) = (b, a);

                    parents[b] = a;
                    if (rank[a] == rank[b]) rank[a]++;
                    return true;
                }
            }

            public static int Solver(int V, int E, (int A, int B, int C)[] edges)
            {
                int answer = 0;

                DSU dsu = new DSU(V);

                Array.Sort(edges, (a, b) => a.C.CompareTo(b.C));

                int cnt = 0;

                for (int i = 0; i < E; i++)
                {
                    (int u, int v, int weight) = edges[i];

                    if (dsu.Union(u, v))
                    {
                        answer += weight;
                        cnt++;

                        if (cnt == V - 1) break;
                    }
                }

                return answer;
            }

            public static int Solver2(int V, int E, (int A, int B, int C)[] edges)
            {
                int answer = 0;
                int count = 0;

                List<(int cost, int v)>[] adj = new List<(int cost, int v)>[V + 1];
                bool[] check = new bool[V + 1];

                for (int i = 1; i <= V; i++)
                {
                    adj[i] = new List<(int cost, int v)>();
                }

                for (int i = 0; i < E; i++)
                {
                    (int u, int v, int cost) = edges[i];
                    adj[u].Add((cost, v));
                    adj[v].Add((cost, u));
                }

                MinHeap<(int cost, int u, int v)> pq = new MinHeap<(int cost, int u, int v)>();

                check[1] = true;
                foreach (var x in adj[1])
                {
                    pq.Push((x.cost, 1, x.v));
                }

                while (count < V - 1)
                {
                    (int cost, int u, int v) = pq.Pop();

                    if (check[v]) continue;

                    answer += cost;
                    count++;

                    foreach (var x in adj[v])
                    {
                        if (check[x.v]) continue;
                        pq.Push((x.cost, v, x.v));
                    }

                }


                return answer;
            }


        }

        public static class EX02
        {
            public class MinHeap<T> where T : IComparable<T>
            {
                List<T> _data = new List<T>();
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

            public class DSU
            {
                int[] parents;
                int[] rank;

                public DSU(int N)
                {
                    parents = new int[N + 1];
                    rank = new int[N + 1];
                    for (int i = 1; i <= N; i++)
                    {
                        parents[i] = i;
                        rank[i] = 1;
                    }
                }

                public int Find(int x)
                {
                    if (parents[x] == x) return x;
                    return parents[x] = Find(parents[x]);
                }

                public bool Union(int a, int b)
                {
                    a = Find(a);
                    b = Find(b);
                    if (a == b) return true;

                    if (rank[a] < rank[b]) (a, b) = (b, a);

                    parents[b] = a;
                    if (rank[a] == rank[b]) rank[a]++;
                    return false;
                }
            }

            public static int Solver(int N, int[] W, int[][] adj)
            {
                int answer = 0;


                List<(int cost, int st, int end)> edges = new List<(int cost, int st, int end)>();

                for (int i = 0; i < W.Length; i++)
                {
                    edges.Add((W[i], i + 1, N + 1));
                }

                for (int i = 0; i < N; i++)
                {
                    for (int j = i + 1; j < N; j++)
                    {
                        edges.Add((adj[i][j], i + 1, j + 1));
                    }
                }

                edges.Sort((a, b) => a.cost.CompareTo(b.cost));

                int index = 0;
                int count = 0;

                N++;

                DSU dsu = new DSU(N);
                while (count < N - 1)
                {
                    (int cost, int st, int end) = edges[index++];

                    if (dsu.Union(st, end)) continue;

                    answer += cost;
                    count++;
                }


                return answer;
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
                T root = _data[0];
                _data[0] = _data[_data.Count - 1];
                _data.RemoveAt(_data.Count - 1);

                int index = 0;

                while (true)
                {
                    int left = index * 2 + 1, right = index * 2 + 2, smallest = index;
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

    public class Problems
    {
        public static class EX01
        {
            static int K, W, H;
            static int[,] board;
            static bool[,,] visited;

            static int[] dx4 = new int[] { 1, 0, -1, 0 };
            static int[] dy4 = new int[] { 0, 1, 0, -1 };

            static int[] dx8 = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
            static int[] dy8 = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

            struct Node
            {
                public int y, x, kUsed, dist;
                public Node(int y, int x, int kUsed, int dist)
                {
                    this.y = y;
                    this.x = x;
                    this.kUsed = kUsed;
                    this.dist = dist;
                }
            }

            public static int Solver(int _K, int _W, int _H, int[,] _board)
            {
                if (_H == 1 && _W == 1) return 0;

                K = _K;
                W = _W;
                H = _H;
                board = _board;
                visited = new bool[_H, _W, _K + 1];


                return BFS();

            }

            static int BFS()
            {
                Queue<Node> q = new Queue<Node>();
                visited[0, 0, 0] = true;
                q.Enqueue(new Node(0, 0, 0, 0));

                while (q.Any())
                {
                    var cur = q.Dequeue();

                    if (cur.y == H - 1 && cur.x == W - 1)
                        return cur.dist;

                    for (int dir4 = 0; dir4 < 4; dir4++)
                    {
                        int nx = cur.x + dx4[dir4];
                        int ny = cur.y + dy4[dir4];

                        if (ny < 0 || ny >= H || nx < 0 || nx >= W) continue;
                        if (board[ny, nx] == 1) continue;
                        if (visited[ny, nx, cur.kUsed]) continue;

                        visited[ny, nx, cur.kUsed] = true;
                        q.Enqueue(new Node(ny, nx, cur.kUsed, cur.dist + 1));
                    }

                    if (cur.kUsed < K)
                    {
                        for (int dir8 = 0; dir8 < 8; dir8++)
                        {
                            int nx = cur.x + dx8[dir8];
                            int ny = cur.y + dy8[dir8];

                            if (nx < 0 || nx >= W || ny < 0 || ny >= H) continue;
                            if (board[ny, nx] == 1) continue;
                            if (visited[ny, nx, cur.kUsed + 1]) continue;

                            visited[ny, nx, cur.kUsed + 1] = true;
                            q.Enqueue(new Node(ny, nx, cur.kUsed + 1, cur.dist + 1));
                        }
                    }
                }

                return -1;

            }

        }
    }

    public class Floyd
    {
        public static class EX01
        {
            public static int[,] D;
            public static int[,] nxt;

            public static void Solver(int N, int M, (int st, int ed, int cost)[] edges)
            {
                D = new int[N + 1, N + 1];
                nxt = new int[N + 1, N + 1];

                int INF = 20000000;

                for (int i = 1; i <= N; i++)
                {
                    for (int j = 1; j <= N; j++)
                    {
                        if (i == j) D[i, j] = 0;
                        else D[i, j] = INF;
                    }
                }

                for (int i = 0; i < M; i++)
                {
                    (int st, int ed, int cost) = edges[i];
                    D[st, ed] = Math.Min(D[st, ed], cost);
                    nxt[st, ed] = ed;
                }

                for (int k = 1; k <= N; k++)
                {
                    for (int i = 1; i <= N; i++)
                    {
                        for (int j = 1; j <= N; j++)
                        {
                            if (D[i, j] > D[i, k] + D[k, j])
                            {
                                D[i, j] = D[i, k] + D[k, j];
                                nxt[i, j] = nxt[i, k];
                            }
                        }
                    }
                }

                for (int i = 1; i <= N; i++)
                {
                    for (int j = 1; j <= N; j++)
                    {
                        if (D[i, j] == INF) D[i, j] = 0;

                        Console.Write($"{D[i, j]} ");
                    }

                    Console.WriteLine();
                }

                for (int st = 1; st <= N; st++)
                {
                    for (int ed = 1; ed <= N; ed++)
                    {
                        if (st == ed) Console.WriteLine("0");
                        else if (D[st, ed] == 0) Console.WriteLine("0");
                        else
                        {
                            List<int> route = new List<int>();
                            int u = st;

                            route.Add(u);
                            while (u != ed)
                            {
                                u = nxt[u, ed];
                                route.Add(u);
                            }

                            Console.Write($"{route.Count} ");
                            for (int i = 0; i < route.Count; i++)
                            {
                                Console.Write($"{route[i]} ");
                            }
                            Console.WriteLine();
                        }
                    }
                }

                return;

            }
        }
    }

    public class KMP
    {
        public static class EX01
        {
            public static void Main(string S, string P)
            {
                int N = S.Length;
                int M = P.Length;

                int[] F = GetF(P);
                int j = 0;
                int count = 0;

                for (int i = 0; i < N; i++)
                {
                    while (j > 0 && S[i] != P[j]) j = F[j - 1];
                    if (S[i] == P[j]) j++;

                    if (j == M)
                    {
                        count++;
                        j = F[M - 1];
                    }
                }

                Console.WriteLine($"{count}");
            }


            public static int[] GetF(string S)
            {
                int N = S.Length;

                int[] F = new int[N];

                int j = 0;

                for (int i = 1; i < N; i++)
                {
                    while (j > 0 && S[i] != S[j]) j = F[j - 1];
                    if (S[i] == S[j]) F[i] = ++j;
                }

                return F;
            }
        }






    }

    public class Trie
    {
        private class Node
        {
            public int[] Next = new int[26];
            public bool IsEnd = false;

            public Node()
            {
                for (int i = 0; i < 26; i++) Next[i] = -1;
            }
        }

        private List<Node> nodes = new List<Node>();

        public Trie()
        {
            nodes.Add(new Node());
        }

        public void Insert(string s)
        {
            int cur = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int c = s[i] - 'a';

                if (c < 0 || c >= 26) throw new ArgumentException("소문자 a-z가 아닙니다.");

                if (nodes[cur].Next[c] == -1)
                {
                    nodes[cur].Next[c] = nodes.Count;
                    nodes.Add(new Node());
                }

                cur = nodes[cur].Next[c];
            }

            nodes[cur].IsEnd = true;
        }

        public bool Contains(string s)
        {
            int cur = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int c = s[i] - 'a';

                if (c < 0 || c >= 0) return false;

                int nxt = nodes[cur].Next[c];
                if (nxt == -1) return false;
                cur = nxt;
            }

            return nodes[cur].IsEnd;
        }

        public bool StartWith(string prefix)
        {
            int cur = 0;

            for (int i = 0; i < prefix.Length; i++)
            {
                int c = prefix[i] - 'a';

                if (c < 0 || c >= 26) return false;

                int nxt = nodes[cur].Next[c];
                if (nxt == -1) return false;
                cur = nxt;
            }

            return true;
        }

        public bool Delete(string s)
        {
            int cur = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int c = s[i] - 'a';
                if (c < 0 || c >= 26) return false;

                int nxt = nodes[cur].Next[c];
                if (nxt == -1) return false;

                cur = nxt;
            }

            if (!nodes[cur].IsEnd) return false;

            nodes[cur].IsEnd = false;
            return true;
        }

    }

    public class Problem1
    {
        public static void Main(int N, int K, int[,] jewels, int[] bags)
        {
            int[][] jewelsArr = new int[N][];

            for (int i = 0; i < N; i++)
            {
                jewelsArr[i] = new int[2] { jewels[i, 0], jewels[i, 1] };
            }
            Array.Sort(jewelsArr, (a, b) => a[0].CompareTo(b[0]));
            Array.Sort(bags, (a, b) => a.CompareTo(b));

            MinHeap<int> mh = new MinHeap<int>();
            int ind = 0;
            int answer = 0;

            for (int i = 0; i < K; i++)
            {
                int C = bags[i];

                while (ind < N && jewelsArr[ind][0] < C)
                {
                    mh.Push(-jewelsArr[ind++][1]);
                }

                if (mh.Count > 0)
                {
                    answer -= mh.Pop();
                }
            }

            Console.WriteLine(answer);
            return ;
        }

        class MinHeap<T> where T : IComparable<T>
        {
            List<T> data = new List<T>();
            public int Count { get { return data.Count; } }

            public void Push(T newItem)
            {
                int index = Count;
                data.Add(newItem);

                while (index > 0)
                {
                    int parent = (index - 1) / 2;

                    if (data[parent].CompareTo(data[index]) <= 0) break;

                    (data[parent], data[index]) = (data[index], data[parent]);
                    index = parent;
                }
            }

            public T Pop()
            {
                T root = data[0];
                data[0] = data[Count - 1];
                data.RemoveAt(Count - 1);

                int index = 0;

                while (true)
                {
                    int left = 2 * index + 1, right = 2 * index + 2, smallest = index;
                    if (left < Count && data[left].CompareTo(data[index]) < 0) smallest = left;
                    if (right < Count && data[right].CompareTo(data[index]) < 0) smallest = right;
                    if (smallest == index) break;

                    (data[smallest], data[index]) = (data[index], data[smallest]);
                    index = smallest;
                }

                return root;
            }

            public T Peek() { return data[0]; }
        }
    }
}