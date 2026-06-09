using System;
using MontyHall;


public class Classes()
{
    public static void output(string[] doors, int numOfDoors)
    {   
        Console.Clear();
        //Console.WriteLine($"\nOUTPUT TEST {testNum}");
        foreach(string door in doors)
                {   if (numOfDoors <=10 )
                    {
                        Console.WriteLine(door);
                    }
                    else if (numOfDoors > 10)
                    {
                        Console.Write($"{door}\t\t\t");   
                    }
                }
    }


public static char menuInput ()
    {
         ConsoleKeyInfo input = Console.ReadKey(true);
         return input.KeyChar;
    }
}