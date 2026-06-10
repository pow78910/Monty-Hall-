using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using MontyHall;


public class Classes()
{
    public static void output(string[] doors, int numOfDoors, int? revealInstance, int? firstDecision)
    {   
   
        Console.Clear();
         Console.WriteLine(revealInstance + "\n\n\n");
        Console.WriteLine("\n\n\n");
        //Console.WriteLine($"\nOUTPUT TEST {testNum}");
        for (int x = 0; x < numOfDoors; x++ )
                {   
                    if (doors[x].Contains("Ferrari"))
                        {
                        Console.ForegroundColor = ConsoleColor.Green;    
                        
                        }
                        else if (doors[x].Contains("Goat"))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;   
                        }
                        else if (!(doors[x].Contains("Ferrari ") && doors[x].Contains("Goat")))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        
                        if ( x == firstDecision)
                        {   
                            if (revealInstance == 0)
                            {
                            Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (revealInstance == 1 && (doors[x].Contains("Goat")) )
                            {             
                             Console.ForegroundColor = ConsoleColor.Red;                 
                            }      
                            else if (revealInstance == 1 && (!(doors[x].Contains("Goat"))))
                            {             
                             Console.ForegroundColor = ConsoleColor.Yellow;                 
                            }      
                               
                        }
                        
                        
                        

                    if (numOfDoors <=10 )
                    {
                        Console.WriteLine(doors[x]);
                    }
                    else if (numOfDoors > 10)
                    {
                        Console.Write($"{doors[x]}\t\t\t");   
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