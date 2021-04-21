using System;
using System.Collections.Generic;
using Xunit;

namespace TechChallenge.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var list = new List<int> { 3, 4, 5 };
            var queue = new Queue<int>(list);
            Console.WriteLine(queue);
        }

        [Fact]
        public void SizeInt()
        {
            int i = 123456;
            var result = Math.Ceiling(Math.Log10(i));
            Assert.Equal(6, result);
        }


        private int[] Algorithm(int[] seed, int k)
        {
            var seedList = new List<int>(seed);

            var k_reduzido = k % seedList.Count;

            var division_point = seedList.Count - k_reduzido;
            var extracted = seedList.GetRange(division_point, k_reduzido);
            var extracted2 = seedList.GetRange(0, division_point);

            var result = new List<int>();
            result.AddRange(extracted);
            result.AddRange(extracted2);

            return result.ToArray(); ;
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 2, new int[] { 2, 3, 1 })]
        [InlineData(new int[] { 1, 2, 3 }, 5, new int[] { 2, 3, 1 })]
        [InlineData(new int[] { 1, 2, 3 }, 6, new int[] { 1, 2, 3})]
        [InlineData(new int[] { 1, 2, 3 }, 7, new int[] { 3, 1, 2 })]
        public void Rotation(int[] seed, int k, int[] expected)
        {
            var result = Algorithm(seed, k);

            Assert.Equal(expected, result);
        }
    }
}
