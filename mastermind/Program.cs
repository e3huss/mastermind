using System.Text.RegularExpressions;

bool play = true;
while (play) {
    #region constant init
    const int min = 1;
    const int max = 6;
    const int length = 4;

    // check that it only contains chars in the range [x-y]
    // and contains the correct qty of chars ^(){z}$
    string validationRegex = $"^([{min}-{max}]){{{length}}}$";
    const int tries = 10;

    const char correctChar = '+';
    const char misplacedChar = '-';
    #endregion

    #region gen answer
    Random random = new Random();
    string answer = "";
    for (int i = 0; i < length; i++)
    {
        answer += random.Next(min, max);
    }
    #endregion

    #region player input
    for (int i = 0; i < tries; i++)
    {
        Console.WriteLine($"{tries - i} tries remain");

        string input = "";
        bool validInput = false;
        while (!validInput)
        {
            input = Console.ReadLine() ?? "";
            validInput = new Regex(validationRegex).Matches(input).Any();
            if(!validInput)
            {
                Console.WriteLine($"Invalid input, try again (it must contain {length} terms between {min} and {max})");
            }
        }

        if (input == answer)
        {
            i = tries;
            Console.WriteLine($"Correct, the answer was {answer}");
        }
        else
        {
            string correct = "";
            string misplaced = "";
            for (int ii = 0; ii < length; ii++)
            {
                if (input[ii] == answer[ii])
                {
                    correct += correctChar;
                }
                else if (answer.Contains(input[ii]))
                {
                    misplaced += misplacedChar;
                }
            }

            if (i < tries - 1)
            {
                Console.WriteLine(correct + misplaced);
            }
            else
            {
                Console.WriteLine($"You've run out of tries, the answer was {answer}");
            }
        }
    }
    #endregion

    #region new game
    Console.WriteLine($"Press enter to play again, press anything else to exit");
    if (Console.ReadKey(true).Key != ConsoleKey.Enter)
    {
        play = false;
    }
    #endregion
}