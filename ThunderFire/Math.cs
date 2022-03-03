using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Funções Matemáticas
    /// </summary>
    public class Maths
    {
        /// <summary>
        /// Calcula o Fatorial de um número
        /// </summary>
        /// <param name="n">Número inteiro</param>
        /// <returns>int</returns>
        public static int Fatorial(int n)
        {
            int r = 1;
          
            for (int i = 1; i <= n; i++)
            {
                r *= n;
            }
            return r;
        }
    }



    //public class Permute
    //{
    //    private static bool LEFT_TO_RIGHT = true;
    //    private static bool RIGHT_TO_LEFT = false;

    //    // Utility functions for 
    //    // finding the position  
    //    // of largest mobile  
    //    // integer in a[]. 
    //    public static int searchArr(int[] a, int n,
    //                                    int mobile)
    //    {
    //        for (int i = 0; i < n; i++)

    //            if (a[i] == mobile)
    //                return i + 1;

    //        return 0;
    //    }

    //    // To carry out step 1 
    //    // of the algorithm i.e. 
    //    // to find the largest 
    //    // mobile integer. 
    //    public static int getMobile(int[] a,
    //                   bool[] dir, int n)
    //    {
    //        int mobile_prev = 0, mobile = 0;

    //        for (int i = 0; i < n; i++)
    //        {
    //            // direction 0 represents 
    //            // RIGHT TO LEFT. 
    //            if (dir[a[i] - 1] == RIGHT_TO_LEFT &&
    //                                          i != 0)
    //            {
    //                if (a[i] > a[i - 1] &&
    //                            a[i] > mobile_prev)
    //                {
    //                    mobile = a[i];
    //                    mobile_prev = mobile;
    //                }
    //            }

    //            // direction 1 represents 
    //            // LEFT TO RIGHT. 
    //            if (dir[a[i] - 1] == LEFT_TO_RIGHT && i != n - 1)
    //            {
    //                if (a[i] > a[i + 1] &&
    //                            a[i] > mobile_prev)
    //                {
    //                    mobile = a[i];
    //                    mobile_prev = mobile;
    //                }
    //            }
    //        }

    //        if (mobile == 0 && mobile_prev == 0)
    //            return 0;
    //        else
    //            return mobile;
    //    }

    //    // Prints a single  
    //    // permutation 
    //    public static int printOnePerm(int[] a, bool[] dir,
    //                                                    int n)
    //    {
    //        int mobile = getMobile(a, dir, n);
    //        int pos = searchArr(a, n, mobile);

    //        // swapping the elements 
    //        // according to the 
    //        // direction i.e. dir[]. 
    //        if (dir[a[pos - 1] - 1] == RIGHT_TO_LEFT)
    //        {
    //            int temp = a[pos - 1];
    //            a[pos - 1] = a[pos - 2];
    //            a[pos - 2] = temp;
    //        }
    //        else if (dir[a[pos - 1] - 1] == LEFT_TO_RIGHT)
    //        {
    //            int temp = a[pos];
    //            a[pos] = a[pos - 1];
    //            a[pos - 1] = temp;
    //        }

    //        // changing the directions 
    //        // for elements greater  
    //        // than largest mobile integer. 
    //        for (int i = 0; i < n; i++)
    //        {
    //            if (a[i] > mobile)
    //            {
    //                if (dir[a[i] - 1] == LEFT_TO_RIGHT)
    //                    dir[a[i] - 1] = RIGHT_TO_LEFT;

    //                else if (dir[a[i] - 1] == RIGHT_TO_LEFT)
    //                    dir[a[i] - 1] = LEFT_TO_RIGHT;
    //            }
    //        }

    //        for (int i = 0; i < n; i++)
    //            Console.Write(a[i]);

    //        Console.Write(" ");

    //        return 0;
    //    }


    //    // This function mainly 
    //    // calls printOnePerm() 
    //    // one by one to print 
    //    // all permutations. 
    //    public static void printPermutation(int [] n)
    //    {
    //        int size = n.Length;

    //        // To store current 
    //        // directions 
    //        bool[] dir = new bool[n.Length];


    //        // initially all directions 
    //        // are set to RIGHT TO  
    //        // LEFT i.e. 0. 
    //        for (int i = 0; i < size; i++)
    //            dir[i] = RIGHT_TO_LEFT;

    //        // for generating permutations 
    //        // in the order. 
    //        for (int i = 1; i < Maths.Fatorial(size); i++)
    //            printOnePerm(n, dir, size);
    //    }

    //    // Driver function  
    //    public static void Main()
    //    {
    //        int n = 4;
    //        //printPermutation(n);
    //    }

    //}
}