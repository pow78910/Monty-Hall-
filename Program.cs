using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Xml;

namespace MontyHall
{
    class Program
    {
        int swapWinScore;
        int keepWinScore;
       public static int score = 0;

        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            
            Console.Clear();
            Console.WriteLine("Weclome to the Monty Hall problem");
            Console.WriteLine("Behind one of these doors is a ferrari, in the other two, goats.");
            Console.WriteLine("You must choose one, then one of the remaining doors will then be revealed.");
            Console.WriteLine("You will then have to option to switch door, or stick with the one you have\n\n");
            Console.WriteLine("\nAre you ready (press any key to continue)");
            Console.ReadKey();
            

            for (int x = 0; x < 100;x++)
            {
                Console.Clear();       
                string[] doors = DoorsInit(3);

                int winningDoor = WinningDoorInit(3);

                Console.WriteLine("\nChoose a door(1,2,3)\n");
                
                int firstDecision = FirstDecision();
                doors[firstDecision] = $"Door {firstDecision + 1} - Chosen Door";
                int firstReveal = FirstRevealInit(winningDoor, firstDecision, 3);
                
                FirstRevealOutput(doors, winningDoor, firstDecision, firstReveal, 3);
                
                //TEST CODE    
            //Console.WriteLine($"\nWinning Door {winningDoor + 1}\nYour Decision {firstDecision + 1}\ndoor to be revealed {firstReveal + 1}");
            
            bool winning = Winning(winningDoor, firstDecision);
            Console.WriteLine($"Bool winning = {winning}");
            
            //TEST CODE
            //Console.WriteLine($"\nTEST WINNING BOOL = {winning}");

            Console.WriteLine("You can now choose to switch or keep you door(s/k)");
            
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 
            bool swapDecision = SwapOrKeep();
            if (swapDecision == true)
            {
                (doors, winning) = Swap(doors, winningDoor,  firstDecision,  firstReveal,  winning);
            }
            else if (swapDecision == false)
                {
                    NoSwap(doors, winningDoor,  firstDecision,  firstReveal,  winning);
                }

            Classes.output(doors, 4);
            
            Console.WriteLine(winning);
            score = WinOrLose(winning, score);
            

            Console.WriteLine("Press any key to play again");
            Console.ReadKey();
        }
          //SwapKeepWinScore();



        }
           
        public static string[] DoorsInit(int numOfDoors)
            {
                string[] doors = ["Door 1", "Door 2", "Door 3"];


                Classes.output(doors, 1);
                
            /*
                for (int x = 0; x < numOfDoors; x++)
                {
                    doors[x] = "Goat";
                }*/
                return doors;
                
                
            }

        public static int WinningDoorInit(int numOfDoors)
            {
                Random random = new Random();
                int winningDoor = random.Next(numOfDoors);
                return winningDoor;

            }

        public static int FirstDecision()
        {
            while(true)
            {
                ConsoleKeyInfo firstDecisionInput = Console.ReadKey(true);

                if (firstDecisionInput.KeyChar == '1' || firstDecisionInput.KeyChar == '2' || firstDecisionInput.KeyChar == '3')
                {
                return int.Parse(firstDecisionInput.KeyChar.ToString()) - 1;
                }
                Console.WriteLine("Invalid Input - try again");

            }


            
        }

        public static bool Winning(int winningDoor, int firstDecision)
        {
            Console.WriteLine($"winning door = {winningDoor}, first decision = {firstDecision}");
            bool winning = false;

            if (winningDoor == firstDecision)
            {
                winning = true; 
            }
            else
            {
                winning = false;
            }
            return winning;
        }
        
        public static int FirstRevealInit(int winningDoor, int firstDecision, int numOfDoors)
        {
            int firstReveal = 0;
        
                while(firstReveal == firstDecision || firstReveal == winningDoor)
                {
                Random random = new Random();
                firstReveal = random.Next(numOfDoors);
                }

            return firstReveal;
        
        }
        
        public static void FirstRevealOutput(string[] doors, int winningDoor, int firstDecision, int firstReveal, int numOfDoors )
        {
            doors[firstReveal] = $"Door {firstReveal + 1} - Goat - Revealed Door";

            Classes.output(doors, 3);
        }
        
        public static bool SwapOrKeep()
        {
            bool swapDecision = false;
            

            while (true)
            {
                char input = Classes.menuInput();
                if (input == 's'|| input == 'S')
                {
                    swapDecision = true;
                    Console.WriteLine("\nYou chose to swap doors");
                    return swapDecision;
                }
                else if (input == 'k' || input == 'K')
                {
                    swapDecision = false;
                    Console.WriteLine("\nYou chose to keep your current door");
                    return swapDecision;
                }
                Console.WriteLine("Invalid Input, try again");
            }
            
            

        }

        public static (string[] doors, bool winning) Swap (string[] doors, int winningDoor, int firstDecision, int firstReveal, bool winning)
        {

            Console.WriteLine($"\nBool winning before swap = {winning}");
            Console.ReadKey();
            if (winning == false)
            {
                winning = true;
                  Console.WriteLine($"\nBool winning after swap = {winning}");
                int tmpDoor = winningDoor;
                int secondDecision = tmpDoor;
                winningDoor = firstDecision;
                
                doors[firstDecision] = $"Door {firstDecision + 1}";
                doors[secondDecision] = $"Door {secondDecision + 1} - Ferrari";
             
                
            }
            else if (winning == true)
            {
                
                winning = false;
                 Console.WriteLine($"\nBool winning after swap = {winning}");
                while(firstDecision == winningDoor && firstDecision == firstReveal)
                {
                    Random random = new Random();
                    int secondDecision = random.Next(3);
                    doors[secondDecision] = $"Door {secondDecision + 1} Goat";
                    
                }
                
               
                
            }

            return (doors, winning);
      
        }

        public static string[] NoSwap(string[] doors, int winningDoor, int firstDecision, int firstReveal, bool winning)
        {
            if (winning == true)
            {
            doors[winningDoor] = $"Door {winningDoor + 1} - Ferrari";
            }
            else if (winning == false)
            {
                doors[firstDecision] = $"Door {firstDecision + 1} - Goat";
            }
            

            return doors;
        }


        
        
        public static int WinOrLose(bool winning, int score)
        {

            Console.WriteLine($"\nBool winning = {winning}\n");
            if (winning == true)
            {
                score++;
            Console.WriteLine($"You chose the correct door , you win the ferrari");
                Console.WriteLine($"Score: {score}");
            }
            else if (winning == false)
            {
                Console.WriteLine("You lost, unlucky");
                Console.WriteLine($"Score: {score}");
            }
            return score;
        }
    }
}
    