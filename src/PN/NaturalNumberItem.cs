using System.Collections.Generic;
using System.Linq;

namespace PN
{
    public class NaturalNumberItem
    {
        public bool IsPrime { get; set; }

        public bool IsMark { get; set; }
        public string Binary { get; set; }
        public int Number { get; set; }

        public bool IsEven => Binary.LastOrDefault() == '0';

        /// <summary>
        /// 非素奇数
        /// </summary>
        /// <returns></returns>
        public bool NonPrimeOdd()
        {
            return !IsPrime && Binary.LastOrDefault() == '1';
        }

        public bool Judge()
        {
            //1.末尾为0是为偶数
            if (Binary.LastOrDefault() == '0')
                return false;
            //偶数个 左右对称
            if (Binary.Length % 2 == 0 && IsSymmetry())
            {
                return false;
            }

            return true;
        }
        //对称数

        private bool IsSymmetry()
        {
            var stack = new Stack<char>();
            foreach (var item in Binary)
            {
                if (stack.TryPeek(out var peek))
                {
                    if (peek == item)
                    {
                        stack.Pop();
                        continue;
                    }
                }

                stack.Push(item);

            }

            return stack.Count == 0;
        }
    }
}