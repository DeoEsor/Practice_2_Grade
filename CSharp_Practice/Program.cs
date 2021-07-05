using System;
using Tasks;

namespace CSharp_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Ilya!");

            int[] input= new int[]{1,2,3};

            Advanced_Enum<int> test = new Advanced_Enum<int>(input);

            Console.WriteLine(test.Combine(2));//lol

            Console.WriteLine(test.Permutations);//lol

            Console.WriteLine(test.Subset);//lol
        }
    }
}
