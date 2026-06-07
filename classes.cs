using System;
using MontyHall;


public class Classes()
{
    public static void output(string[] doors, int testNum)
    {
        Console.WriteLine($"OUTPUT TEST {testNum}");
        foreach(string door in doors)
                {   
                    Console.WriteLine(door);
                }
    }
}