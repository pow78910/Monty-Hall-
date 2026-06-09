using System;
using System.Collections;
using MontyHall;


public class Classes()
{
    public static void output(string[] doors, int numOfDoors)
    {   
        Console.Clear();

        Console.WriteLine("\n\n\n");
        //Console.WriteLine($"\nOUTPUT TEST {testNum}");
        foreach(string door in doors)
                {   
                    if (door.Contains("Ferrari"))
                        {
                        Console.ForegroundColor = ConsoleColor.Green;    
                        
                        }
                        else if (door.Contains("Goat"))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;   
                        }
                        else if (!(door.Contains("Ferrari ") && door.Contains("Goat")))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }

                    if (numOfDoors <=10 )
                    {
                        Console.WriteLine(door);
                    }
                    else if (numOfDoors > 10)
                    {
                        Console.Write($"{door}\t\t\t");   
                    }
                     Console.ResetColor();
                }

                
    }


public static char menuInput ()
    {
         ConsoleKeyInfo input = Console.ReadKey(true);
         return input.KeyChar;
    }
}