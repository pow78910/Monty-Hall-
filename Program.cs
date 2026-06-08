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

                int firstReveal = FirstRevealInit(winningDoor, firstDecision, 3);
                
                FirstRevealOutput(doors, winningDoor, firstDecision, firstReveal, 3);
                
                //TEST CODE    
            //Console.WriteLine($"\nWinning Door {winningDoor + 1}\nYour Decision {firstDecision + 1}\ndoor to be revealed {firstReveal + 1}");
            
            bool winning = Winning(winningDoor, firstDecision);
            
            //TEST CODE
            //Console.WriteLine($"\nTEST WINNING BOOL = {winning}");

            Console.WriteLine("You can now choose to switch or keep you door(s/k)");
            
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 
            bool swapDecision = SwapOrKeep();
            if (swapDecision == true)
            {
                swap(doors, winningDoor,  firstDecision,  firstReveal,  winning);
            }

            Classes.output(doors, 4);
            int score = 0;
            WinOrLose(winning, score);
            

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
                ConsoleKeyInfo firstDecisionInput = Console.ReadKey(true);
            // int firstDecision = Convert.ToInt32(firstDecisonInput.KeyChar);
                return int.Parse(firstDecisionInput.KeyChar.ToString()) - 1;


            
        }

        public static bool Winning(int winningDoor, int firstDecision)
        {
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
            doors[firstReveal] = "Goat - OPENED DOOR";

            Classes.output(doors, 3);
        }
        
        public static bool SwapOrKeep()
        {
            bool swapDecision = false;
            char input = Classes.menuInput();

            if (input == 's'|| input == 'S')
            {
                swapDecision = true;
                Console.WriteLine("\nYou chose to swap doors");
            }
            else if (input == 'k' || input == 'K')
            {
                swapDecision = false;
                Console.WriteLine("\nYou chose to keep your current door");
            }


            return swapDecision;

        }

        public static bool swap (string[] doors, int winningDoor, int firstDecision, int firstReveal, bool winning)
        {
            if (winning != true)
            {
                winning = true;
                int tmpDoor = winningDoor;
                winningDoor = firstDecision;
                firstDecision = tmpDoor;
                
                doors[winningDoor] = "Ferrari";
                
            }
            else if (winning == true)
            {
                winning = false;
                while(firstDecision == winningDoor && firstDecision == firstReveal)
                {
                    Random random = new Random();
                    firstDecision = random.Next(3);
                }
                doors[firstDecision] = $"Door {firstDecision + 1}";
                
            }
            return winning;
        }


        
        
        public static void WinOrLose(bool winning, int score)
        {

            Console.WriteLine($"\nBool winning = {winning}\n");
            if (winning == true)
            {
                score++;
                Console.WriteLine("You got the correct door, you win the ferrari");
                Console.WriteLine($"Your score is {score}");
            }
            else if (winning == false)
            {
                Console.WriteLine("You lost, unlucky");
            }
        }
    }
}
    