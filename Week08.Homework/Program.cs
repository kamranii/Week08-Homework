using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Week08.Homework.Registration;

namespace Week08.Homework
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Task-1 (Overriding .ToString())
            //Culture
            CultureInfo culture = new CultureInfo("es");
            CultureInfo culture1 = new CultureInfo("az");
            Student student = new Student("Kamran", "Imranli", "IT", 27, culture);
            Student student1 = new Student("Xayal", "Imranli", "Graphic Design", 21, culture1);
            Console.WriteLine(student.ToString());
            Console.WriteLine(student1.ToString());
            #endregion
            #region Task-2 (Calculate Deposit)
            Console.Write("Amount to invest in USD: ");
            double amount = double.Parse(Console.ReadLine());
            Console.Write("Period of deposit in years: ");
            byte year = byte.Parse(Console.ReadLine());
            //Output the result
            double endAmount = Calculator(amount, year);
            Console.WriteLine($"Your amount after {year} years will be: {endAmount}$, thus your profit will be {endAmount - amount}$");
            Console.ReadLine();
            //Calculator Method
            double Calculator(double amount, byte year)
            {
                for (int i = 0; i < year; i++)
                {
                    amount *= 1.06;
                }
                return amount;
            }
            #endregion
            #region Task-3 (Filtering Contacts)
            //Add some contacts to the list
            SeedDataBase();
            //Ask user for the operation
            bool IsContinuing = true;
            do
            {
            Start: Console.WriteLine("Choose an operation: ");
                Console.WriteLine("Add\nSearch (by name or number)\nView (All contacts)");
                Console.WriteLine(" ");
                //Get his choice
                Console.WriteLine("Your choice: ");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "add":
                        Contact contact = CreateContact();
                        FilterContacts.AddContact(contact);
                        break;
                    case "search":
                        Console.Write("Name or part of it to search for: ");
                        string search = Console.ReadLine();
                        FilterContacts.Search(search);
                        break;
                    case "view":
                        Console.WriteLine("All contacts are being displayed");
                        FilterContacts.ViewAllContacts();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!!!");
                        break;
                }
                Console.WriteLine("Press Enter to continue or Esc to quit");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key.Equals(ConsoleKey.Escape))
                    IsContinuing = false;
                else if (key.Key.Equals(ConsoleKey.Enter))
                    goto Start;
            } while (IsContinuing);

            static void SeedDataBase()
            {
                //Create and add some contacts
                Contact contact1 = new Contact("Kamran", "+994557270404");
                Contact contact2 = new Contact("Ayxan", "+994557280404");
                Contact contact3 = new Contact("Sarxan", "+9944557290202");
                FilterContacts.AddContact(contact1);
                FilterContacts.AddContact(contact2);
                FilterContacts.AddContact(contact3);
            }
            //Create Contact
            Contact CreateContact()
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Number: ");
                string number = Console.ReadLine();
                Contact contact = new Contact(name, number);
                return contact;
            }
            #endregion
            #region Task-4 (Retrieve from a URL and write to files)
            //Ask for the url
            Console.Write("Input a link: ");
            string url = Console.ReadLine();
            string folderPath = "/Users/kamranimranli/desktop/Json/";
            //Create the directory
            var directory = Directory.CreateDirectory(folderPath);
            //Initialize a to do list
            List<ToDo> toDoList = new List<ToDo>();
            //Complete the list
            await GetToDoList(url);
            //Loop to write to a file
            for (int i = 0; i < toDoList.Count; i++)
            {
                await WriteToFile($"{folderPath}{toDoList[i].id}.odt", toDoList[i]);
            }
            //Get the list elements
            async Task GetToDoList(string url)
            {
                using HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(url);
                ToDo toDo = new ToDo();

                var list = JsonConvert.DeserializeObject<List<ToDo>>(response);
                foreach (var item in list)
                {
                    toDoList.Add(item);
                }
            }
            //Write to the file
            async Task WriteToFile(string path, ToDo toDo)
            {
                await File.WriteAllTextAsync(path, toDo.ToString());
            }
            #endregion
            #region Rock-Paper-Scissors
            //Bool to store choice
            bool IsPlayingAgain = true;
            do
            {
                //Provide the menu
                Console.WriteLine("Let's play Rock-Paper_Scissors.");

                //Store the score
                byte userScore = 0;
                byte cpuScore = 0;
                //Loop to update the score
                while (userScore < 3 && cpuScore < 3)
                {
                    Console.WriteLine("Choose one of the following: ");
                    Console.WriteLine("1. Rock");
                    Console.WriteLine("2. Paper");
                    Console.WriteLine("3. Scissors");
                    Console.Write("Your choice: ");
                    //Store the user's choice
                    byte userChoice = 0;
                    if (byte.TryParse(Console.ReadLine(), out byte choiceInput))
                    {
                        if (choiceInput > 0 || choiceInput < 4)
                        {
                            userChoice = choiceInput;
                        }
                        else
                            Console.WriteLine("Invalid choice!!!");
                    }
                    else
                        Console.WriteLine("Invalid input!!!");
                    //Store the CPU's choice
                    Random random = new Random();
                    int cpuChoice = random.Next(1, 4);
                    string cpuChoiceText = "";
                    if (cpuChoice == 1)
                        cpuChoiceText = "Rock";
                    else if (cpuChoice == 2)
                        cpuChoiceText = "Paper";
                    else
                        cpuChoiceText = "Scissors";
                    //Make the comparison
                    byte result = CompareChoices(userChoice, cpuChoice);
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine($"CPU's choice: {cpuChoiceText}");
                            Console.WriteLine("You won!");
                            userScore++;
                            Console.WriteLine($"User's score = {userScore}, CPU score = {cpuScore}");
                            break;
                        case 2:
                            Console.WriteLine($"CPU's choice: {cpuChoiceText}");
                            Console.WriteLine("CPU won!");
                            cpuScore++;
                            Console.WriteLine($"User's score = {userScore}, CPU score = {cpuScore}");
                            break;
                        case 3:
                            Console.WriteLine($"CPU's choice: {cpuChoiceText}");
                            Console.WriteLine("Tie!");
                            Console.WriteLine($"User's score = {userScore}, CPU score = {cpuScore}");
                            break;
                        default:
                            break;
                    }
                }
                //Output the winner of the game
                if (userScore == 3)
                    Console.WriteLine("You won the game");
                else
                    Console.WriteLine("CPU won the game");
                Console.WriteLine("Do you want to play again? Yes/No");
                string choice = Console.ReadLine().Trim(' ').ToLower();
                if (choice.Equals("no"))
                    IsPlayingAgain = false;
            } while (IsPlayingAgain);
            Console.WriteLine("End of the game!");
            //Compare choices
            byte CompareChoices(byte userChoice, int cpuChoice)
            {
                //If user chose "rock"
                if (userChoice == 1)
                {
                    //If cpu chose "rock"
                    if (cpuChoice == 1)
                    {
                        //Tie
                        return 3;
                    }
                    //If cpu chose "paper"
                    else if (cpuChoice == 2)
                    {
                        //CPU wins
                        return 2;
                    }
                    //If cpu chose "scissors"
                    else
                    {
                        //User wins
                        return 1;
                    }
                }
                //If user chose "paper"
                else if (userChoice == 2)
                {
                    //If cpu chose "rock"
                    if (cpuChoice == 1)
                    {
                        //User wins
                        return 1;
                    }
                    //If cpu chose "paper"
                    else if (cpuChoice == 2)
                    {
                        //Tie
                        return 3;
                    }
                    //If cpu chose "scissors"
                    else
                    {
                        //CPU wins
                        return 2;
                    }
                }
                //If user chose "scissors"
                else
                {
                    //If cpu chose "rock"
                    if (cpuChoice == 1)
                    {
                        //CPU wins
                        return 2;
                    }
                    //If cpu chose "paper"
                    else if (cpuChoice == 2)
                    {
                        //User wins
                        return 1;
                    }
                    else
                    {
                        //Tie
                        return 3;
                    }
                }
            }
            #endregion
            #region Task-5 (User Registration)
            //user registrasiya səhifəsi olsun,
            //username və password təyin etsin,
            //forget password səhifəsi olsun passwordu dəyişdirə bilsin,
            //və əlbəttə duz daxil etdikdə səhifəyə girsin, girdikdə successfully logged in yazılsın.

            //Loop variable to continue or stop
            bool Continues = true;
            do
            {
                //Greeting
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Ask for choice
                Console.WriteLine("Choose one of the follwing in order to continue: ");
                Console.WriteLine("1. Sign in (If you already have an account)");
                Console.WriteLine("2. Sign up (If you want to register)");
                Console.WriteLine("3. Exit");
                //Store choice
                Console.Write("Your choice: ");
                string choiceInput = Console.ReadLine();
                byte choice = default;
                if (byte.TryParse(choiceInput, out byte result))
                    choice = result;
                switch (choice)
                {
                    case 1:
                        //Login
                        Console.WriteLine("Redirecting to the login page");
                        Thread.Sleep(1000);
                        Console.BackgroundColor = ConsoleColor.Black;
                        //Verify to log in
                        VerificationOperations verification = new VerificationOperations();
                        //Check if logged in
                        if (verification.VerifyLogin())
                        {
                            IsContinuing = false;
                            break;
                        }
                        else
                            break;
                    case 2:
                        //Registration
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("Welcome to the Registration page!");
                        //Initialize registration and a user
                        RegistrationOperations operations = new RegistrationOperations();
                        User user = new User();
                        //Get user details
                        operations.CompleteRegistration(user);
                        //Add user to the database
                        Database.users.Add(user);
                        Console.WriteLine("Regstration is successfull. You are being redirected to maing page");
                        break;
                    case 3:
                        IsContinuing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (Continues);
            #endregion
        }
    }
}

