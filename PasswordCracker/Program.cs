using System;
using System.Text.RegularExpressions;

namespace PasswordCracker {
    class Program {
        static Random random = new Random();
        static void Main(string[] args) {

            while (true) {
                try {
                    Console.WriteLine("1. Generate number to guess \n2. Get it to crack numbers randomly\n3. Close program ");
                    string? input = Console.ReadLine();
                    Regex digit = new Regex("^[1-2]{1}$");
                    if (!digit.IsMatch(input)) {
                        Console.WriteLine("Error: Input most be 1 digit or the number 1 or 2");
                    }

                    switch (input) {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Generating number to guess");
                            guessnumber();
                            break;
                        case "2":
                            Console.WriteLine("Cracking numbers randomly");
                            crackpawd();
                            break;
                        case "3":
                            Console.WriteLine("Exiting program");
                            return;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }   
            }
        }
        
        static void crackpawd() {
            // Get user input for the password
            Console.Write("Enter a password: ");
            string userPassword = Console.ReadLine() ?? String.Empty;

            Regex regex = new Regex("^[a-h0-6]{4}$");
            if (!regex.IsMatch(userPassword)) {
                Console.WriteLine("Error: Password must be exactly 4 characters long and contain only letters or digits.");
                return;
            }
            genNumber(userPassword);
        }
        static string getNumber() {
            var randomNumberString = random.Next(0, 9999).ToString("0000");
            return randomNumberString;
        }
        static void guessnumber() {
            bool firstNumber = false;
            bool secondNumber = false;
            bool thirdNumber = false;

            char[] rand = getNumber().ToString().ToCharArray();

            Regex regex = new Regex("^[0-9]{1}$");
            while (true) {
                Console.WriteLine(rand);
                for (int i = 0; i < 4; i++) {
                    if (thirdNumber) { i = 3; }
                    else if (secondNumber) { i = 2; }
                    else if (firstNumber) { i = 1; }
                   
                    Console.WriteLine($"Enter the {i + 1} number to guess: ");
                    string input = Console.ReadLine() ?? String.Empty;
                    if (!regex.IsMatch(input)) {
                        Console.Clear();
                        Console.WriteLine("Error: Guess most be 1 digit.");
                    }
                    if (input == rand[i].ToString()) {
                        Console.Clear();
                        Console.WriteLine("Correct");
                        if (i == 0) {
                            firstNumber = true;
                        }
                        else if (i == 1) {
                            secondNumber = true;
                        }
                        else if (i == 2) {
                            thirdNumber = true;
                        }
                        if (i == 3) {
                            Console.WriteLine("You guessed the number\n ");
                            return;
                        }
                    }
                    else {
                        Console.WriteLine("Try again");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        static void genNumber(string userPassword) {
            // Store the current cursor position (useful for overwriting later)
            var position = Console.GetCursorPosition();

            // Define the set of possible characters for password generation
            char[] possibleChars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', '0', '1', '2', '3', '4', '5', '6' };
            Random random = new Random();

            // Initialize an empty string to store the guessed password
            string guessedPassword = "";
            int j = 0;
            // Loop until the guessed password matches the user password
            while (guessedPassword != userPassword) {
                guessedPassword = "";
                // Generate a random password with the same length as the user password
                for (int i = 0; i < userPassword.Length; i++) {
                    int randomIndex = random.Next(possibleChars.Length);
                    guessedPassword += possibleChars[randomIndex];
                    j++;
                }
                // Move the cursor back to the beginning of the previous line
                Console.SetCursorPosition(position.Left, position.Top);
                Console.WriteLine("Cracking password... Please be patient...");
                // Display the newly generated guessed password
                Console.WriteLine("Guessed:" + j);
                Console.WriteLine("Your password is: " + guessedPassword);
            }
        }
    }
}
