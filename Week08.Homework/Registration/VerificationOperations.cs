using System;
using System.Linq;
using System.Threading;

namespace Week08.Homework.Registration
{
    public class VerificationOperations
    {
        //Verify the username
        public bool VerifyUsername(string username)
        {
            //Check database
            User user = Database.users.Find(x => x.Username == username);
            if (user == null)
            {
                Console.WriteLine("Username doesn't exist");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                return false;
            }
            else
                return true;
        }
        //Verify the password
        public bool VerifyPassword(User user, string password)
        {
                if (user.Password == password)
                    return true;
                else
                    return false;
        }
        public void ForgetPassword(User user)
        {
            bool IsPasswordSet = true;
            do
            {
                //Ask for birthdate
                Console.Write("Enter your birthdate (format dd//MM/yyyy) in order to confirm your identity: ");
                string inputBirthdate = Console.ReadLine().Trim(' ');
                //Check format
                if (DateTime.TryParse(inputBirthdate, out DateTime birthday))
                {
                    //Check identity
                    if (user.BirthDate.Equals(birthday))
                    {
                        RegistrationOperations registration = new RegistrationOperations();
                        Console.WriteLine("Let's set a new password!");
                        registration.GetPassword(user);
                        IsPasswordSet = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format!");
                    IsPasswordSet = false;
                }
            } while (!IsPasswordSet);
            
            
        }
        public bool VerifyLogin()
        {
            //Ask for username and password
            Console.Write("Enter your username: ");
            string loginUsername = Console.ReadLine().Trim(' ').ToLower();
            Console.Write("Enter your password: ");
            string loginPassword = Console.ReadLine();
            //Verify username and password
            if (VerifyUsername(loginUsername))
            {
                //Find the user
                User user = Database.users.Where(x => x.Username.Equals(loginUsername)).First();
                //Check the password
                if (VerifyPassword(user, loginPassword))
                {
                    //Welcome the user
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Successfully logged in!");
                    return true;
                }
                else
                {
                    //Ask if forgot the password
                    Console.WriteLine("Incorrect Password. Did you forget your password? yes/no");
                    string choice = Console.ReadLine().Trim(' ').ToLower();
                    if (choice.Equals("yes"))
                    {
                        //Redirect to set a new password
                        ForgetPassword(user);
                        Console.WriteLine("You're being redirected to main page");
                        Thread.Sleep(1000);
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("You're being redirected to main page");
                        Thread.Sleep(1000);
                        return false;
                    }
                }
            }
            else
            {
                //Error
                Console.WriteLine("Incorrect username. You are being redirected to the main page");
                Console.WriteLine("");
                return false;
            }
        }
    }
}

