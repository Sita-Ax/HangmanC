using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace HangmanC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hangman hangman = new Hangman();
            hangman.RunHangman();
        }
    }
}