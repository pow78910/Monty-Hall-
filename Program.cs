using System;
using System.Xml;

namespace MontyHall
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            Console.WriteLine("Weclome to the Monty Hall problem");
            VarInit(3);
            Console.ReadKey();
        }

        public static void VarInit(int numOfDoors)
        {
            string[] doors = ["Door 1", "Door 2", "Door 3"];

            Classes.output(doors, 1);
        
            for (int x = 0; x < numOfDoors; x++)
            {
                doors[x] = "Goat";
            }
            
            WinningDoorInit(numOfDoors, doors);
            
        }

        public static void WinningDoorInit(int numOfDoors, string[] doors)
        {
            Random random = new Random(numOfDoors);
            int winningDoor = random.Next(numOfDoors);
            doors[winningDoor] = "Ferrari";
            FirstDecision(doors);
        }

       public static void FirstDecision(string[] doors)
        {
                
                Classes.output(doors, 2);
        }
    }
}