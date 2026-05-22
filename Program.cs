//TODO
//Make codebase worse so that AI can't steal it

namespace CalcGPT
{
    class Program
    {
        public static int ChooseOperator(string userInput_param)
        {
            
            string operation = userInput_param;
            int operationSelect;
            switch (operation)
            {
                default:
                    Console.WriteLine("That's not one of the operators vro, try again.");
                    operation = Console.ReadLine();
                    operationSelect = ChooseOperator(operation);
                    break;
                case "+":
                    operationSelect = 0;
                    break;
                case "-":
                    operationSelect = 1;
                    break;
                case "*":
                    operationSelect = 2;
                    break;
                case "/":
                    operationSelect = 3;
                    break;
            }
            return operationSelect;
        }
        public static int Guess(float numIn)
        {
            int guess;
            int limit = Convert.ToInt32(Math.Floor(numIn));
            var rand = new Random();

            guess = rand.Next(limit-20, limit+21);
            return guess;
        }
        public static float NumSelect()
        {
            float inputNumber;
            try
            {
                inputNumber = Convert.ToSingle(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("That's prolly not a number vro, try again.");
                inputNumber = NumSelect();
            }
            return inputNumber;
        }
        static int GetUniqueGuess(float numIn, HashSet<int> history)
        {
            int guess;
            var rand = new Random();
            int limit = Convert.ToInt32(Math.Floor(numIn));

            while (true)
            {
                guess = rand.Next(limit - 20, limit + 21);
                if (!history.Contains(guess))
                {
                    history.Add(guess);
                    return guess;
                }
                /* else
                {
                    Console.WriteLine("Test message: duplicate");
                } */
            }
        }
        public static void Main()
        {
            float answer;
            string operation;
            int operationSelect;
            int guess;
            float number1;
            float number2;
            string restart;
            
            Console.WriteLine("Yooooooo welcome to the calc (short for calculator). The answer should be close enough. Enter the first number:");
            number1 = NumSelect();
            Console.WriteLine("Which operation we doing? Let's have it");
            operation = Console.ReadLine();
            operationSelect = ChooseOperator(operation);
            Console.WriteLine("Chuck in that second number then broseph:");
            number2 = NumSelect();
            while (operationSelect == 3 && number2 == 0)
            {
                Console.WriteLine("I see what you're doing there you rascal. Let's not divide by zero please. Put in a different divisor");
                number2 = NumSelect();
            }
           
            if (number1 > 100 || number2 > 100 || number1 < -100 || number2 < -100)
            {
                Console.WriteLine("I don't have that many fingers. I ain't doing allat");
                //Console.ReadKey();
                Main();
            }

            answer = 0;

            switch (operationSelect)
            {
                case 0:
                    answer = number1 + number2;
                    break;
                case 1:
                    answer = number1 - number2;
                    break;
                case 2:
                    answer = number1 * number2;
                   break;
                case 3:
                    answer = number1 / number2;
                    break;
            }

            //Truncate that shit
            int trueAnswer = Convert.ToInt32(answer);

            Console.WriteLine("Thinking...");

            HashSet<int> previousGuesses = new HashSet<int>();

            guess = Guess(trueAnswer);      
            for (int i = 0; i < 25; i++)
            {
                guess = GetUniqueGuess(answer, previousGuesses);
                Console.WriteLine("Generating guess...");         
                    
                Console.WriteLine(guess);
                Thread.Sleep(500);
                if (guess == trueAnswer)
                {
                    Console.WriteLine($"Finally, took a little bit but here's your answer :P\nYour answer is prolly around the neighbourhood of {guess}.");
                    break;
                }
            }
            if (guess != trueAnswer)
            {
                Console.WriteLine("Welp, I tried but it's too hard. Time to give up.");
            }
            Console.WriteLine("Do you want to run the calc again? y/n");
            restart = Console.ReadLine();
            if (restart == "y")
            {
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}