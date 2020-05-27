
using System;
using System.Collections.Generic;


public class Test1
{
    static void Main()
    {
        // var a = Power(2, 8);
        Console.Write("123");
    }

    public static IEnumerable<int> Power(int number, int exponent)
    {
        int result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result = result * number;
            yield return result;
        }
    }

}