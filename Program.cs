using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace HangmanC
{
    internal class Program
    {
        public static List<char> correctGuess = new List<char>();
        public static List<char> notCorrectGuess = new List<char>();
        
        static int guess = 10;
        static StringBuilder sb = new StringBuilder();

        //static string? input = Console.ReadLine();
        static void Main(string[] args)
        {
            bool loop = true;
            WelcomesMenu();
            char c = Console.ReadKey(true).KeyChar;
            switch (c)
            {
                case 'p':
                    Menu();
                    break;
                default:
                    break;
            }
            while (loop)
            {
                try
                {
                    char selection = Console.ReadKey(true).KeyChar;
                    switch (selection)
                    {
                        case 'l':
                            Guess();
                            break;
                        case 'g':
                            GuessLetter();
                            break;
                        case 'w':
                            GuessWord();
                            break;
                        case 'o':
                            Console.WriteLine("Now you exit the game!");
                            loop = false;
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
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exception has occurred.");
                    Console.ResetColor();
                }
            }


        }

        private static string SecretWord()
        {
            Random random = new Random();//random num generator
            List<string> randomWords = new List<string> { "hangman", "game", "forest", "children", "play" };
            int indexOfWord = random.Next(randomWords.Count);
            string wordFound = randomWords[indexOfWord];
            StringBuilder sb = new StringBuilder(wordFound.Length);
            for (int i = 0; i < wordFound.Length; i++)
            {
                sb.Append(wordFound[i]);
                if ( i < wordFound.Length)
                {
                    sb.Replace(wordFound[i], '*');
                }
            }
            Console.WriteLine(wordFound + "SECRETWORD" + sb);
            return wordFound;
        }

        private static void WinnerCheck()
        {
            string word = SecretWord(); 
            bool won = true;

            while (won)
            {
                if (word.Equals(notCorrectGuess))
                {
                    Console.WriteLine("Loooser!");
                    LoserImage();
                    break;
                }
                if (word.Equals(correctGuess))
                {
                    Console.WriteLine("Winner");
                    WinnerImage();
                    break;
                }
            }
        }



        private static void GuessWord()
        {
            
            string word = SecretWord();
            int lengthOfWord = word.Length;
            List<string> wordGuess = new List<string>();
            int correct = 0;
            sb = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                sb.Append(word[i]);
                for (int j = 0;  j < word.Length; j++)
                {
                    sb.Replace(word[j], '*');
                }
            }
            do
            {
                try
                {
                    Console.Write("WORD You have 10 guesses on you! ");
                    Console.WriteLine("Your word is " + lengthOfWord + " * long." + word);
                    Console.WriteLine("Guess word: " + sb);
                    string? input = Console.ReadLine().ToLower();
                   
                    bool isWord = true;
                    if (isWord)
                    {
                        {
                            if (input.Equals(word))
                            {
                                wordGuess.Add(input);
                                correct++;
                                WinnerImage();
                                isWord = false;
                            }
                            else if (!input.Equals(word))
                            {
                                wordGuess.Add(input);
                                correct++;
                                LoserImage();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("I´m catch you!");
                    throw;
                }
            }
            while (correct == 10);
        }

        private static void Guess()
        {
            bool isPlaying = true;
            string words = SecretWord();

            int countWrongGuess = 0;//amount of guesses until the user loses the game
            List<string> guessLetter = new List<string>();//string to hold the letters that have already been guessed          

            Console.WriteLine("I have picked a random word.");
            Console.WriteLine("Your job is to guess the letter in the word!");
            Console.WriteLine("The length of the word is {0} digits! Good luck :) ", sb.Length);
            Console.WriteLine();
            do
            {

                wordLetter(words, guessLetter);

                string initialLetters = wordLetter(words, guessLetter);
                Console.WriteLine("(Guess) Enter a letter for the word: {0}", initialLetters);

                string userInput = Console.ReadLine();

                if (guessLetter.Contains(userInput))//if the user enters a letter that's already been guessed
                {
                    Console.WriteLine("You already entered {0}", userInput);
                    isPlaying = true;

                }
                guessLetter.Add(userInput);//adds the users input to the string of guessed letters               

                if (words.Contains(userInput))//if the user enters a correct letter in the secret word
                {

                    Console.WriteLine("Great guess! {0} is in the word", userInput);
                    isPlaying = true;

                }
                if (correctWord(words, guessLetter))//if the word is filled completely
                {

                    Console.WriteLine($"Great game! The word is {words}. You missed {countWrongGuess} times!");
                    Console.WriteLine("Did you want to play again? y/n");

                    string userChoice = Console.ReadLine();

                    if (string.Equals(userChoice, "y"))
                    {
                        string[] args = null;
                        Main(args);
                    }
                }
                else if (!words.Contains(userInput))//if the user doesn't enter a correct letter in the secret word
                {
                    isPlaying = true;
                    Console.WriteLine("Sorry! {0} isn't in the word :(", userInput);
                    countWrongGuess++;//increases the count of wrong guesses and displays them at the end of the game

                }


            }
            while (isPlaying == true);
        }
        static string wordLetter(string secretWord, List<string> guessLetter)//finds if letter guessed is in secret word
        {
            string correctGuessedLetters = "";//empty string to hold correct letters that are guessed

            for (int i = 0; i < secretWord.Length; i++)//loop that loops through the secret word index length
            {
                string a = Convert.ToString(secretWord[i]);//new string to check each letter in the word to see if correct

                if (guessLetter.Contains(a))
                {
                    correctGuessedLetters += a;//if correct the letter is added to string
                }
                else
                {
                    correctGuessedLetters += "*";//if incorrect the * stays the same
                }
            }
            return correctGuessedLetters;//return the correct guessed letters to be used in the program            
        }
        static bool correctWord(string secretWord, List<string> guessLetter)
        {
            bool correctWord = false;//boolean to see if the correct word has been guessed or not

            for (int i = 0; i < secretWord.Length; i++)
            {
                string a = Convert.ToString(secretWord[i]);//initialized string for the element value of the secretWord array

                if (guessLetter.Contains(a))//checks to see if the string is in the list of letters guessed
                {
                    correctWord = true;//if it contains the string value then we have the correct word
                }
                else
                {
                    correctWord = false;//if it doesn't contain the string value then we don't have the correct word
                }
            }
            return correctWord;
        
    }

        private static void GuessLetter()
        {
            string word = SecretWord();
            char[] charWord = word.ToCharArray();
            char[] secret = new char[charWord.Length];
            int lengthOfWord = word.Length;
            List<char> charGuess = new List<char>(word);
            int guessed = 0;
            sb = new StringBuilder();
            for (int i = 0; i < secret.Length; i++)
            {
                secret[i] = '*';
            }
            for (int i = 0; i < secret.Length; i++)
            {
                Console.Write(secret[i] + " ");
            }
            try
            {
                Console.Write("LETTER You have 10 guesses on you! ");
                Console.WriteLine("Your word is " + lengthOfWord + " * long." + word);
                do
                {
                    sb = new StringBuilder();
                    Console.WriteLine("Guess a letter: ");
                    char letter = Console.ReadLine().ToCharArray()[0];//take input char
                    guessed++;
                    for (int i = 0; i < charWord.Length; i++)
                    {
                        if (charWord[i] == letter)
                        {
                            secret[i] = letter;
                            for (int j = 0; j < secret.Length; j++)
                            {
                                Console.Write(secret[j] + " ");
                                sb.Append(secret[j]);
                                correctGuess.Add(secret[j]);
                            }
                        }
                        else if (charWord[i] != letter)
                        {
                            for (int j = 0; j < secret.Length; j++)
                            {
                                notCorrectGuess.Add(secret[j]);
                            }
                        }
                    }
                   
                    Console.WriteLine(guessed + " : " + secret.Length);
                } while (guessed != 10);
                WinnerCheck();
            }
            catch (Exception)
            {
                Console.WriteLine("I catch you!!");
                throw;
            }
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
        private static void Menu()
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
        }
        private static void HangmanMaker()
        {
            //string word = SecretWord();
            char n = notCorrectGuess[10];
            int guessed = 0;
            do
            {
                if (n.Equals(guessed == 1))
                {
                    Console.WriteLine("Wrong guess, try again");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("___|___");
                }
                if (n.Equals(guessed == 2))
                {
                    Console.WriteLine("Wrong guess, try again");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                }
                if (notCorrectGuess.Equals(guessed == 3))
                {
                    Console.WriteLine("Wrong guess, try again");
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
                if (notCorrectGuess.Equals(guessed == 4))
                {
                    Console.WriteLine("Wrong guess, try again");
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
                if (notCorrectGuess.Equals(guessed == 5))
                {
                    Console.WriteLine("Wrong guess, try again");
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
                if (notCorrectGuess.Equals(guessed == 6))
                {
                    Console.WriteLine("Wrong guess, try again");
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
                if (notCorrectGuess.Equals(guessed == 7))
                {
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
                if (notCorrectGuess.Equals(guessed == 8))
                {
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
                if (notCorrectGuess.Equals(guessed == 9))
                {
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
                if (notCorrectGuess.Equals(guessed == 10))
                {
                    Console.WriteLine("GAME OVER!");
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
                    Console.WriteLine("GAME OVER! The word was " + sb);
                    LoserImage();
                }
            }while(guessed > 10);
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
    }
}