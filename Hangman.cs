
using System.ComponentModel.Design;
using System.Text;

namespace HangmanC
{
    internal class Hangman
    {
        static List<string> wordList = new List<string> { "hangman", "game", "forest", "children", "play" };
        
        static List<string> randomWord = new List<string>();//string to hold the random word

        static List<char> guessedLetter = new List<char>();//string to hold the letters that have already been guessed          

        static List<string> guessedWord = new List<string>();//string to hold the random word

        static int guessedLetterRight = 0;
        //static string guessedWord = " ";
        static StringBuilder buildNewWord = new StringBuilder();//get a new Stringbuilder
        static StringBuilder checkWord = new StringBuilder();//checked the word
        static int indexSecretWord = 0;//index in the secret word
        static int guessLeft = 0;//guesses left
        static bool isPlaying = true;//if i want to keep playing
        static char playAgain = 'y';//if play again


        public void RunHangman()
        {
            do
            {
                WelcomesMenu();
                char c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'p':
                        GamePresentation();
                        break;
                    default:
                        break;
                }
                char selection = Console.ReadKey(true).KeyChar;
                switch (selection)
                {
                    case 'l':
                        //SecretWord();
                        ChooseLetterAndCheck();
                         CheckLetterInSecretWord();
                        break;
                    case 'g':
                        //PlayAgain();
                       GuessTheWord();
                       // GuessLetter();
                        break;
                    case 'w':
                       GuessWordAndCheck();
                        break;
                    case 'o':
                        Console.WriteLine("Now you exit the game!");
                        break;
                    default:
                        Console.WriteLine(Console.ForegroundColor = ConsoleColor.Red);
                        Console.WriteLine("Invalid Value!");
                        break;
                }
                Console.ResetColor();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                //GuessWordWithLines();
                //ChooseLetterAndCheck();
                //ResetVariables();
                PlayAgain();
            } while (playAgain == 'n');

        }


        // gissat ord.Clear();
        // kontrollerat ord.Clear();
        // gissnigar kvar = 10;
        // om fortsätta speletifConti => true;
        public void reset()
        {
            string word = SecretWord();
            if (guessedWord.Contains(word))
            {
                Console.WriteLine("Gueesed word.");
                guessedWord.Clear();
            }
         

        }

        private static string SecretWord()  //just get and create the random word
        {
            Random random = new Random();
            int indexOfWord = random.Next(wordList.Count);
            string randomWord = wordList[indexOfWord];
            foreach (char c in randomWord)
            {
                Console.Write("_ ");
            }
            return randomWord;    
        }

        public void PlayAgain()
        {
            while( playAgain != 'y' && playAgain != 'n' )
            {
                Console.WriteLine($"Great game!");
                Console.WriteLine("Did you want to play again? y/n");
                playAgain = Console.ReadKey(true).KeyChar;
            }
            Console.Clear();
        }

        public static void GuessTheWord()
        {
            string randomWord = SecretWord();
            buildNewWord = new StringBuilder(randomWord.Length);
            for (int i = 0; i < randomWord.Length; i++)
            {
                buildNewWord.Append('_', randomWord[i]);

            }
        }
        //private static void WinnerCheck()
        //{
        //    string word = SecretWord();
        //    bool won = true;

        //    while (won)
        //    {
        //        if (word.Equals(notCorrectGuess))
        //        {
        //            Console.WriteLine("Loooser!");
        //            LoserImage();
        //            break;
        //        }
        //        if (word.Equals(correctGuess))
        //        {
        //            Console.WriteLine("Winner");
        //            WinnerImage();
        //            break;
        //        }
        //    }
        //}

        public void GuessWordAndCheck()
        {
            string word = SecretWord();
            Console.WriteLine("Guess the word: ");
            while (isPlaying)
            { 
                do
                {
                    string? userChoice = Console.ReadLine().ToLower();
                    if (userChoice.Equals(word))
                    {
                        Console.WriteLine("Yes it is this word");
                        WinnerImage();
                        guessLeft++;
                    }
                    else if (!userChoice.Equals(word))
                    {
                        Console.WriteLine("Not this word");
                        guessLeft++;
                        HangmanMaker(guessLeft);
                    }
                    if(guessLeft == 10)
                    {
                        LoserImage();
                    }
                } while (guessLeft == 10);
            }
        }
        public void ChooseLetterAndCheck()
        {
            string word = SecretWord();
            guessedLetter = new List<char>();
            while (guessLeft != 10)
            {
                do
                {
                    foreach (char letter in guessedLetter)
                    {
                        Console.Write(letter + " ");
                    }
                    
                    Console.WriteLine("Guess the letters: ");
                    char userChoice = Console.ReadLine()[0];
                    if (guessedLetter.Contains(userChoice))
                    {
                        Console.WriteLine("Yes it is this letter");
                        guessLeft++;
                    }
                    else if (!userChoice.Equals(word))
                    {
                        Console.WriteLine("Not this letter. ");
                        guessLeft++;
                        HangmanMaker(guessLeft);
                    }
                } while (guessLeft == 10);
                if (guessedLetter.Contains(word[0]))
                {
                    CheckLetterInSecretWord();
                    WinnerImage();
                }
                else
                {
                    LoserImage();
                }
            }
        }

        public static void CheckLetterInSecretWord()
        {
            string word = SecretWord();
            
            char userGuess = Console.ReadLine()[0];
            // Checka av hela ordet innehåller de gissade bokstäverna
            if (word.ToString().Contains(userGuess))
            {
                Console.WriteLine("Bra jobbat");
                while (checkWord.ToString().Contains(userGuess))
                {
                    Console.WriteLine("Bra jobbat" + checkWord.ToString());

                    // Ett exempel 
                    indexSecretWord = checkWord.ToString().IndexOf(userGuess);
                    checkWord[indexSecretWord] = '_';
                    guessedWord[indexSecretWord] = userGuess.ToString();

                }
            }
            else
            {
                if (!word.Contains(userGuess))
                {

                    Console.WriteLine("Wrong letter, try again");

                }
                else { Console.WriteLine("you have already tried with this letter, try again"); }

            }

            Console.WriteLine(/*Skriva ut det gissade ordet*/);

        }

        private static void WelcomesMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("************************");
            Console.WriteLine("*******Welcome**********");
            Console.WriteLine("*****To My Hangman******");
            Console.WriteLine("*********Game***********");
            Console.WriteLine("************************");
            Console.WriteLine("******Press (p) to******");
            Console.WriteLine("**********PLAY**********");
            Console.ResetColor();
        }

        private static void GamePresentation()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("What is your name? ");
            string? players = Console.ReadLine();
            Console.WriteLine("*********** Hello " + players + " Do you what to guess letters? Press: (l)***********");
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("*********** Hello " + players + " Do you what to guess antoher letters? Press: (g)***********");
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("****************Do you want to guess words? " + players + " Press: (w)***************");
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("******** " + players + " Do you want to exit this game? Press: (o)*******************");
            if (players.Contains('w'))
            {
                Console.WriteLine("**********You have 10 guesses on you!**************");
                Console.WriteLine("******You choose word*********");
            }
            if (players.Contains('l'))
            {
                Console.WriteLine("**********You have 10 guesses on you!**************");
                Console.WriteLine("******You choose letters!*********");
            }
        }

        private static void LoserImage()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("                ooo$$$$$ooo                   ");
            Console.WriteLine("              oo$$$$$$$$$$$oo                 ");
            Console.WriteLine("             oo$$$$$$$$$$$$$$oo               ");
            Console.WriteLine("o$ $$ oo    o$$$$  $$$$$$$  $$$$o     o$ $$ o$");
            Console.WriteLine("oo $$ ^$   o$$$$    $$$$$    $$$$o    $$ $$ o$");
            Console.WriteLine("^$$$$$o$  €$$$$$    $$$$$    $$$$$€   o$ $$ o$");
            Console.WriteLine(" $$$$$$$$$$$$$$$$  $$$$$$$  $$$$$$$$$$$$$$$$  ");
            Console.WriteLine(" ^$$$^^^$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$^^^$$$^ ");
            Console.WriteLine("  ^$$   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$   $$^  ");
            Console.WriteLine("   $$   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$   $$   ");
            Console.WriteLine("^$$$^^^$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$^^^$$$^");
            Console.WriteLine("^$$$^^^$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$^^^$$$^");
            Console.WriteLine("^^^^    $$ $$$$$$$$$$$$$$$$$$$$$$$$ $$    ^^^^");
            Console.WriteLine("        $$  ^^$$$$$$$$$$$$$$$$$$^^  $$        ");
            Console.WriteLine("        $$$   ^^^^$$$$$$$$$$^^^^   $$$        ");
            Console.WriteLine("         $$$$     ^^^^$$^^^^     $$$$         ");
            Console.WriteLine("           $$$$                $$$$           ");
            Console.WriteLine("             $$$$$  $$$$$   $$$$$             ");
            Console.WriteLine("              $$$$$$ $$$$$ $$$$$              ");
            Console.WriteLine("               ^^$$$$$$$$$$$$^^               ");
            Console.WriteLine("                 ^^^^$$$$$$$$                 ");
            Console.WriteLine("                       $$$$$$$                ");
            Console.WriteLine("                       $$$$$$$o               ");
            Console.WriteLine("                        o$$$$$o               ");
            Console.WriteLine("                          o$$o                ");
            Console.WriteLine(" ");
            Console.WriteLine("**********LOOOSER*****************************");
        }

        private static void WinnerImage()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                   $$$$$                       ");
            Console.WriteLine("                   $oo$$                       ");
            Console.WriteLine("                   $$$ooo                      ");
            Console.WriteLine("                   $$$ooo                      ");
            Console.WriteLine("                   $$$ooo                      ");
            Console.WriteLine("                  $$$$$oooo$$$$$$              ");
            Console.WriteLine("                $$$$$$$$$$$$$$$$$$$            ");
            Console.WriteLine("               $$$$$$$$$$$$$$$$$$$$$           ");
            Console.WriteLine("               $$$$$$$$$$$$$$$$$o$$$           ");
            Console.WriteLine("             $$$$$$$$$$$$$$$$$$oo$$            ");
            Console.WriteLine("             $$$$$$$$$$$$$$$$$$$oo$$           ");
            Console.WriteLine("             $$$$$$$$$$$$$$$$$$$ooo$$          ");
            Console.WriteLine("               $$$$$$$$$$$$$$$$$oo$$           ");
            Console.WriteLine("                 $$$$$$$$$$$$oo$$$             ");
            Console.WriteLine("                   $$$$$$$$$$$$$               ");
            Console.WriteLine("                      $$$$$$$                  ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("**********YOU**ARE**THE**WINNER****************");
        }

        private static void HangmanMaker(int wrong)
        {
                if (wrong == 1)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("___|___");
                }
                else if (wrong == 2)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 3)
                {
                    Console.WriteLine("Wrong guess, try again!" + guessLeft);
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 4)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 5)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 6)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft); 
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |          / \\ ");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 7)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |          / \\ ");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 8)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |           |");
                    Console.WriteLine("   |          /|\\");
                    Console.WriteLine("   |         / | \\");
                    Console.WriteLine("   |          / \\ ");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 9)
                {
                    Console.WriteLine("Wrong guess, try again! " + guessLeft);
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |     |     |");
                    Console.WriteLine("   |    _|_   /|\\");
                    Console.WriteLine("   |     |   / | \\");
                    Console.WriteLine("   |     |     |");
                    Console.WriteLine("   |          / \\ ");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                else if (wrong == 10)
                {
                    Console.WriteLine("GAME OVER! You have {0} guesses left!");
                    Console.WriteLine("   ____________");
                    Console.WriteLine("   |          _|_");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |        |     |");
                    Console.WriteLine("   |         \\_ _/");
                    Console.WriteLine("   |     |     |");
                    Console.WriteLine("   |    _|_   /|\\");
                    Console.WriteLine("   |     |   / | \\");
                    Console.WriteLine("   |     |     |");
                    Console.WriteLine("   |          / \\ ");
                    Console.WriteLine("   |         /   \\");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                    LoserImage();
                }
        }
    }
}