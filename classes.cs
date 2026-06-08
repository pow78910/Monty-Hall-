using System;
using MontyHall;


public class Classes()
{
    public static void output(string[] doors, int testNum)
    {   Console.WriteLine("\n");
        //Console.WriteLine($"\nOUTPUT TEST {testNum}");
        foreach(string door in doors)
                {   
                    Console.WriteLine(door);
                }
    }


public static char menuInput ()
    {
         ConsoleKeyInfo input = Console.ReadKey(true);
         return input.KeyChar;
    }
}