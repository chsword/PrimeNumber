using System;
using System.Collections.Generic;

namespace PN
{
    class Program
    {
       const int Size = 1000;
       static readonly NaturalNumberItem[] AllNumberArray = new NaturalNumberItem[Size];
        static readonly Dictionary<int,NaturalNumberItem> PrimeDict = new Dictionary<int,NaturalNumberItem>();
        static readonly List<int> PrimeList = new List<int>();
        private static readonly Dictionary<int, int> PairDict = new Dictionary<int, int>
        {
            //{9, 19}
        };
        static void Main(string[] args)
        {

            InitPrime();

            PrintPrimeList();
            PrintResultLog(ShowType.OnlyUnvalidateEven);

            Console.ReadKey();
        }

        private static void PrintResultLog(ShowType type)
        {
            var theLastPrime = 2;
            var theLastLastPrime = 2;
            int change = 0;
            int nonPrimeOddCount = 0;
            int unControlCount = 0;
            int allCount = 0;
            for (int i = 2; i < Size; i++)
            {
                var item = AllNumberArray[i];
                if (item.IsPrime)
                {
                    theLastLastPrime = theLastPrime;
                    theLastPrime = i;
                    change = 1;
                }

                if (change == 0 && item.NonPrimeOdd())
                {
                    nonPrimeOddCount++;
                }

                if (item.IsEven)
                {
                    int primeBig = 0;
                    //偶数 原则 
                    if (i == theLastPrime + 1)
                    {
                        //大1的情况仍然使用last last
                        primeBig = theLastLastPrime;
                    }
                    else
                    {
                        // 其它使用last
                        primeBig = theLastPrime;
                    }

                    var l = primeBig;
                    var r = i - primeBig;
                    if (PairDict.ContainsKey(r))
                    {
                        l = PairDict[r];
                        r = i - r;
                    }

                    if (type == ShowType.All ||
                        type == ShowType.OnlyEven ||
                        (type == ShowType.OnlyUnvalidateEven && !PrimeDict.ContainsKey(r))
                    )
                    {
                        Console.Write(
                            $"{item.Number}\t{(item.IsPrime ? "True" : "")}\t{item.Binary}\t{item.NonPrimeOdd()}\t");
                        Console.Write(
                            $"{l}\t{r}\t{(PrimeDict.ContainsKey(r) ? "V" : "")}");
                        Console.WriteLine();
                        PrintResult(i);
                        unControlCount ++;
                    }

                    allCount++;
                }
                else
                {
                    if (type == ShowType.All
                    )
                    {
                        Console.Write($"{item.Number}\t{(item.IsPrime?"True":"")}\t{item.Binary}\t{item.NonPrimeOdd()}\t");
                    }

                    //Console.WriteLine();
                }

               
                //WriteInfo(item);
            }
            Console.WriteLine($"{unControlCount} / {allCount}");
        }

        private static void PrintResult(int yz)
        {
            for (int i = 0; i < PrimeList.Count; i++)
            {
                var l = PrimeList[i];
                var r = yz - PrimeList[i];
                if (PrimeDict.ContainsKey(r) && l <= r)
                {
                    Console.WriteLine($"{yz} = {r}[{PrimeList.IndexOf(r)}] + {l}[{PrimeList.IndexOf(l)}]");
                }
            }
        }

        private static void PrintPrimeList()
        {
            for (int i = 0; i < PrimeList.Count; i++)
            {
                Console.WriteLine($":{i}\t=\t{PrimeList[i]}");
            }
        }

        private static void InitPrime()
        {
            for (int i = 0; i < Size; i++)
            {
                AllNumberArray[i] = new NaturalNumberItem
                {
                    IsPrime = false,
                    Binary = BinaryUtil.GetBinaryString(i),
                    Number = i
                };
            }

            for (int i = 0; i < Size; i++)
            {
                var item = AllNumberArray[i];

                if (!item.IsMark && i > 1)
                {
                    item.IsPrime = true;
                    PrimeDict.Add(i, item);
                    PrimeList.Add(i);
                    int n = 2;
                    int l;
                    while ((l = n * i) < Size)
                    {
                        if (!AllNumberArray[l].IsMark)
                        {
                            AllNumberArray[l].IsMark = true;
                        }

                        n++;
                    }
                }
            }
        }

        private static void WriteInfo(NaturalNumberItem item)
        {
            //var judge = item.Judge();
//          if(item.IsPrime !=  judge|| item.IsPrime)
            //Console.WriteLine($"{item.Number}\t{item.IsPrime}\t{item.Binary}\t{judge}");
            Console.WriteLine($"{item.Number}\t{item.IsPrime}\t{item.Binary}\t{item.NonPrimeOdd()}");
        }
    }

    internal enum ShowType
    {
        All=0,
        OnlyEven=1,
        OnlyUnvalidateEven=2
    }
}
