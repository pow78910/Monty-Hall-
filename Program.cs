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
        public static int swapWinScore;
        public static int keepWinScore;
       public static double score = 0;
       

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Weclome to the Monty Hall problem");
            Console.WriteLine("Behind one of these doors is a ferrari, in the other two, goats.");
            Console.WriteLine("You must choose one, then one of the remaining doors will then be revealed.");
            Console.WriteLine("You will then have to option to switch door, or stick with the one you have\n\n");
            Console.WriteLine("\nAre you ready (press any key to continue)");
            Console.ReadKey();
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            bool auto = false;
            Console.WriteLine("Choose which version you want to play\n\n");
            Console.WriteLine("1) Standard game(3 Doors)");
            Console.WriteLine("2) Increased doors (100 Doors)");
            Console.WriteLine("3) Custom number of doors");
            char input = Classes.menuInput();

            while (true)
            {
                if (input == '1')
                {
                    Start(3, auto);
                }
                else if(input == '2')
                {
                    Start(100, auto);
                }
                else if (input == '3')
                {
                    
                }
                else if (input == 'a' || input =='A')
                {
                    auto = true;

                }
            
            }
        }

        static void Start(int numOfDoors, bool auto)
        {
             int roundCount = 0;
            for (int x = 0; x < 100;x++)
            {
                roundCount++;
                Console.Clear();     

                Console.WriteLine($"\nPick a door(1-{numOfDoors})");  
                string[] doors = DoorsInit(numOfDoors);

                int winningDoor = WinningDoorInit(numOfDoors);

                
                
                int firstDecision = FirstDecision(numOfDoors);
                doors[firstDecision] = $"Door {firstDecision + 1} - Chosen Door";
                int firstReveal = FirstRevealInit(winningDoor, firstDecision, 3);
                
                FirstRevealOutput(doors, winningDoor, firstDecision, firstReveal, 3);
                
                //TEST CODE    
            //Console.WriteLine($"\nWinning Door {winningDoor + 1}\nYour Decision {firstDecision + 1}\ndoor to be revealed {firstReveal + 1}");
            
            bool winning = Winning(winningDoor, firstDecision);
           //TEST CODE 
           // Console.WriteLine($"Bool winning = {winning}");
            
            //TEST CODE
            //Console.WriteLine($"\nTEST WINNING BOOL = {winning}");

            Console.WriteLine("\nYou can now choose to keep(1) your door or swap(2) with the remaining door");
            
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

            Classes.output(doors, numOfDoors);
            
            score = WinOrLose(winning, score);

            (swapWinScore, keepWinScore) = SwapKeepScores(swapWinScore, keepWinScore, swapDecision, winning);
            double swapWinPercentage = (swapWinScore * 100) / roundCount;
            double keepWinPercentage = (keepWinScore * 100) / roundCount;
           
            Console.WriteLine($"Swapping Wins: {swapWinScore}({swapWinPercentage}%)\tKeeping Wins: {keepWinScore}({keepWinPercentage}%");
            
            Console.WriteLine("(1)Play again");
            Console.WriteLine("(x) Back to menu");
            char input = Classes.menuInput();

            switch (input)
                {
                    case 'x':
                        Menu();
                        break;
                    default:
                        break;
                }

            
        }
          //SwapKeepWinScore();



        }
       
        public static string[] DoorsInit(int numOfDoors)
            {
                string[] doors = new string[numOfDoors];

                for (int x = 0; x < numOfDoors; x++)
            {
                doors[x] = $"Door {x + 1}";
            }

                Classes.output(doors, numOfDoors);
                
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

        public static int FirstDecision(int numOfDoors)
        {
            while(true)
            {
                if (numOfDoors < 10)
                {
                    ConsoleKeyInfo firstDecisionInput = Console.ReadKey(true);
                   
                    return int.Parse(firstDecisionInput.KeyChar.ToString()) - 1;
                }
                    
                else if (numOfDoors >= 10)
                    {
                        string firstDecisionInputStr = "ERROR";
                        firstDecisionInputStr = Console.ReadLine();
                        return int.Parse(firstDecisionInputStr) - 1;
                    }

                
                Console.WriteLine("Invalid Input - try again");

            }


            
        }

        public static bool Winning(int winningDoor, int firstDecision)
        {
            //TESTCODE
           // Console.WriteLine($"winning door = {winningDoor}, first decision = {firstDecision}");
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

            Classes.output(doors, numOfDoors);
        }
        
        public static bool SwapOrKeep()
        {
            bool swapDecision = false;
            

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                if (char.IsDigit(input.KeyChar))
                {
                    int swapInput = int.Parse(input.KeyChar.ToString());
               
                    if (swapInput == 1)
                    {
                        swapDecision = false;
                        return swapDecision;
                    }
                    else if (swapInput == 2)
                    {
                        swapDecision = true;
                        return swapDecision;
                    }
                }
                else{
    
                Console.WriteLine("Invalid Input, try again");
                }
            }
            
            

        }

        public static (string[] doors, bool winning) Swap (string[] doors, int winningDoor, int firstDecision, int firstReveal, bool winning)
        {
            //TEST CODE 
            //Console.WriteLine($"\nBool winning before swap = {winning}");
            
            if (winning == false)
            {
                winning = true;
                  //TEST CODE
                  //Console.WriteLine($"\nBool winning after swap = {winning}");
                int tmpDoor = winningDoor;
                int secondDecision = tmpDoor;
                winningDoor = firstDecision;
                
                doors[firstDecision] = $"Door {firstDecision + 1}";
                doors[secondDecision] = $"Door {secondDecision + 1} - Ferrari";
             
                
            }
            else if (winning == true)
            {
                
                winning = false;
                //TEST CODE 
                // Console.WriteLine($"\nBool winning after swap = {winning}");
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


        
        
        public static double WinOrLose(bool winning, double score)
        {
            //TEST CODE 
            //Console.WriteLine($"\nBool winning = {winning}\n");
            if (winning == true)
            {
                score++;
            Console.WriteLine($"\nYou win!!!");
                Console.WriteLine($"Score: {score}");
            }
            else if (winning == false)
            {
                Console.WriteLine("\nYou Lose!!!");
                Console.WriteLine($"\nScore: {score}");
            }
            return score;
        }

        public static ( int swapWinScore, int keepWinScore)SwapKeepScores(int swapWinScore, int keepWinScore, bool swapDecision, bool winning)
        {

          if (winning == true && swapDecision == true)
            {
                swapWinScore++;
            }
            else if (winning == false && swapDecision == false)
            {
                swapWinScore++;
            }
            else if (winning == false && swapDecision == true)
            {
                keepWinScore++;
            }
            else if (winning == true && swapDecision == false)
            {
                keepWinScore++;
            }
            //TEST CODE
            //Console.WriteLine($"\n\n\nWinning {winning}\n SwapDecision: {swapDecision}");

            return (swapWinScore, keepWinScore);
        }
    }

}
    