using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using System.Xml;

namespace MontyHall
{
    class Program
    {
        public static int swapWinScore;
        public static int keepWinScore;
       public static double score = 0;
       public static int revealInstance = 0;

       public static bool auto = false; 

       public static int roundNum = 100;
       
       

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
            
            Console.WriteLine("Choose which version you want to play\n\n");
            Console.WriteLine("1) Standard game(3 Doors)");
            Console.WriteLine("2) Increased doors (100 Doors)");
            Console.WriteLine("3) Custom number of doors");
            Console.WriteLine("\n\na) Turn auto on/off");
            Console.WriteLine($"r) Choose amount of rounds to play - {roundNum}");
            Console.WriteLine($"\n\n Auto = {auto}");

            char input = Classes.menuInput();

            while (true)
            {
                if (input == '1')
                {
                    Start(3, auto, roundNum);
                }
                else if(input == '2')
                {
                    Start(100, auto, roundNum);
                }
                else if (input == '3')
                {
                   int customDoors = Classes.CustomDoorNum();
        
                   Start(customDoors, auto, roundNum);
                }
                else if (input == 'a' || input =='A')
                {
                    if (auto == false)
                    {
                    auto = true;
                    }
                    else if (auto == true)
                    {
                        auto = false;
                    }
                    Menu();

                }
                else if (input == 'r' || input == 'R')
                {
                    roundNum = Classes.RoundNum(roundNum);
                    Menu();
                }
            
            }
        }

        static void Start(int numOfDoors, bool auto, int roundNum)
        {

            
            int roundCount = 0;
            swapWinScore = 0;
            keepWinScore = 0;
            score = 0;

            for (int x = 0; x < roundNum; x++)
            {
                roundCount++;
                Console.Clear();     

               
                string[] doors = DoorsInit(numOfDoors);

                int winningDoor = WinningDoorInit(numOfDoors);

                int firstDecision;
                switch(auto)
                {
                    case false:
                    
                     firstDecision = FirstDecision(numOfDoors);
                        break;
                    case true:
                        firstDecision = FirstDecisionAuto(numOfDoors);
                        break;
                    

                }
                


                doors[firstDecision] = $"Door {firstDecision + 1} - Chosen Door";
                int firstReveal = FirstRevealInit(winningDoor, firstDecision, numOfDoors);
                //test - maybe delete later 
                int[] firstRevealUpd = FirstRevealUpd(winningDoor,  firstDecision,  numOfDoors);

                FirstRevealOutput(doors, winningDoor, firstDecision, firstReveal, firstRevealUpd, numOfDoors);
                
                //TEST CODE    
                //Console.WriteLine($"\nWinning Door {winningDoor + 1}\nYour Decision {firstDecision + 1}\ndoor to be revealed {firstReveal + 1}");
                
                bool winning = Winning(winningDoor, firstDecision);
                //TEST CODE 
                
                
                //TEST CODE
                //Console.WriteLine($"\nTEST WINNING BOOL = {winning}");

                Console.WriteLine("\n\n\nYou can now choose to keep(1) your door or swap(2) with the remaining door");
                
    
    
                bool swapDecision;

                switch(auto)
                    {
                        case false:
                            swapDecision = SwapOrKeep();
                            break;

                        case true:

                            swapDecision = true;
                            break;
                    

                    }
           
                if (swapDecision == true)
                {
                    (doors, winning) = Swap(doors, winningDoor,  firstDecision,  firstReveal,  winning);
                }
                else if (swapDecision == false)
                    {
                        NoSwap(doors, winningDoor,  firstDecision,  firstReveal,  winning);
                    }

                revealInstance = 1;
                Classes.output(doors, numOfDoors, revealInstance, firstDecision);
                
                switch(auto)
                {
                    case false:
                        score = WinOrLose(winning, score);
                    break;
                    case true:
                        break;
                }

                (swapWinScore, keepWinScore) = SwapKeepScores(swapWinScore, keepWinScore, swapDecision, winning);
                double swapWinPercentage = (swapWinScore * 100) / roundCount;
                double keepWinPercentage = (keepWinScore * 100) / roundCount;
            
                Console.WriteLine($"\n\nSwapping Wins: {swapWinScore} ({swapWinPercentage}%)\tKeeping Wins: {keepWinScore} ({keepWinPercentage}%)\n\n");
                
               
                
            
                    
                switch (auto)
                {
                    case false:
                        Console.WriteLine("(1) Play again");
                        Console.WriteLine("(x) Back to menu");
                        char input = Classes.menuInput();
                        switch (input)
                        {
                            case 'x':
                                Menu();
                                break;
                            case 'X':
                                Menu();
                                break;
                            default:
                                break;
                        }
                    break;

                    case true:

                        break;
                }

            
            }
          //SwapKeepWinScore();

            Console.WriteLine("Game Over!!!");
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadKey();
            Menu();

        }
       
        public static string[] DoorsInit(int numOfDoors)
            {
                string[] doors = new string[numOfDoors];

                for (int x = 0; x < numOfDoors; x++)
            {
                doors[x] = $"Door {x + 1}";
            }

                Classes.output(doors, numOfDoors, revealInstance, null);
                Console.WriteLine("\n\n\nPick a door");
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
            int firstDecision;
            while(true)
            {
                if (numOfDoors < 10)
                {
                    ConsoleKeyInfo firstDecisionInput = Console.ReadKey(true);

                    if (int.TryParse(firstDecisionInput.KeyChar.ToString(), out  firstDecision ))
                    {
                        return firstDecision - 1;
                    }
                }
                   
                
                    
                else if (numOfDoors >= 10)
                {
                        string? firstDecisionInputStr;
                        firstDecisionInputStr = Console.ReadLine();

                        if (int.TryParse(firstDecisionInputStr, out  firstDecision ) && firstDecision <= numOfDoors)
                        {

                        return firstDecision - 1;
                        }
                }

                
                Console.WriteLine("Invalid Input - try again");

            }
        }

        public static int FirstDecisionAuto(int numOfDoors)
        {
            

            Random rand = new Random();
            int firstDecisionAuto = rand.Next(numOfDoors);
            return firstDecisionAuto;

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
     public static int [] FirstRevealUpd(int winningDoor, int firstDecision, int numOfDoors)
        {
            int[] firstRevealUpd = new int[numOfDoors];

            Console.WriteLine("\n\n\nTEST CODE\n\n\n");


            foreach (int reveal in firstRevealUpd)
            {

            Console.WriteLine($"Reveal Values: {reveal}");
            }


            for (int x = 0; x < numOfDoors; x++)
            {
                firstRevealUpd[x] = numOfDoors + 1;
            }
            foreach(int reveal in firstRevealUpd)
            {
                Console.WriteLine(reveal);
            }
            Console.ReadKey();

            for (int x = 0; x < numOfDoors; x++)
            {
                if (x != winningDoor && x != firstDecision)
                {
                    firstRevealUpd[x] = x;
                    if (winningDoor != firstDecision)
                    {
                    firstRevealUpd[x] = x;
                    }
                    else if (winningDoor == firstDecision)
                    {   
                        Console.WriteLine("This code is reachable");
                        firstRevealUpd[x] = numOfDoors + 1;

                        Random rand = new Random();
                        int random = rand.Next(numOfDoors);
                        while (random == winningDoor)
                        {
                            random = rand.Next(numOfDoors);
                        }
                        firstRevealUpd[random] = x;
                        x = numOfDoors;

                    }
            }
                
          
              
                    

                

            }



             Console.WriteLine("\n\n\nTEST CODE 2\n\n\n");
            foreach (int reveal in firstRevealUpd)
            {
                Console.WriteLine($"New reveal values: {reveal}");
            }
            Console.WriteLine("TEST CODE");
            Console.WriteLine($"The winning door is {winningDoor}\nYou chose door {firstDecision}");

            Console.ReadKey();

            return firstRevealUpd;
        }
        
        public static void FirstRevealOutput(string[] doors, int winningDoor, int firstDecision, int firstReveal, int[] firstRevealUpd, int numOfDoors )
        {
       
            
            for (int x = 0; x < numOfDoors; x++)
            {
                if (firstRevealUpd[x] < numOfDoors)
                {
                    doors[firstRevealUpd[x]] = $"Door {x + 1} - Goat";
                }
                else
                {
                    doors[x] = $"Door {x + 1}";
                }
            }
           // doors[winningDoor] = $"Door "
            revealInstance = 0;
            Classes.output(doors, numOfDoors, revealInstance, firstDecision);
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
                doors [firstReveal] = $"Door {firstReveal + 1} - Goat";
                doors[secondDecision] = $"Door {secondDecision + 1} - Ferrari";
             
                
            }
            else if (winning == true)
            {
                
                winning = false;
                //TEST CODE 
                // Console.WriteLine($"\nBool winning after swap = {winning}");
                Random random = new Random();
                int secondDecision = firstDecision;
                while(secondDecision == winningDoor || secondDecision == firstReveal)
                {
                    
                    secondDecision = random.Next(3);

                    
                }
                    doors[firstDecision] = $"Door {firstDecision + 1}";
                    doors[secondDecision] = $"Door {secondDecision + 1} - Goat";
                    doors [firstReveal] = $"Door {firstReveal + 1} - Goat";
                
               
                
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
                Console.WriteLine("\n\n\nYou Lose!!!");
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
    