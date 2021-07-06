using System;
using System.IO;
using Tasks;

namespace CSharp_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Ilya!");

            
            #region Task-1_test
            int[] input= new int[]{1,2,3};

            
            foreach(var it in Advanced_Enum.Combine<int>(input, 2)) { 
                foreach (var iter in it) { 
                    Console.Write(iter);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            foreach (var it in Advanced_Enum.Permutations<int>(input))
            {
                foreach (var iter in it)
                {
                    Console.Write(iter);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            foreach (var it in Advanced_Enum.Subset<int>(input))
            {
                foreach (var iter in it)
                {
                    Console.Write(iter);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            #endregion
            /*
            #region Task-2-test
            Pragma pragma = new Pragma();

            pragma.Launch("test_logger.txt");

            pragma.DoSmth();
            pragma.DoSmth();
            pragma.DoSmth();
            pragma.DoSmth();

            FileStream filoviyPotok = new FileStream("test_logger.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamReader str = new StreamReader(filoviyPotok);
            string data = str.ReadToEnd();
            string[] dataArray = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);



            for (int i = 0; i < dataArray.Length; i++)      Console.WriteLine(dataArray[i]);


            
            str.Close();
            #endregion

            */
        }
    }
}
