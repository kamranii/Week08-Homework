using System;
namespace Week08.Homework.Registration
{
    public class RegistrationOperations
    {
        public RegistrationOperations()
        {
        }
        
        public void GetUsername(User user)
        {
            //Initialize a username
            string username = "";
            bool IsUsernameValid = true;
            do
            {
                //Kullanıcı adı sor
                Console.Write("Username: ");
                username = Console.ReadLine();
                //Doğrulamalar
                if (username == null || username.Trim(' ') == "")
                {
                    Console.WriteLine("Username cannot be blank!");
                    IsUsernameValid = false;
                }
                else
                {
                    //Kullanıcı adının unique olması
                    var matchUser = Database.users.Find(x => x.Username == username);
                    if (matchUser != null)
                    {
                        Console.WriteLine("Username already exists");
                        IsUsernameValid = false;
                    }
                    else
                    {
                        user.Username = username.Trim(' ').ToLower();
                        IsUsernameValid = true;
                    }
                }
            } while (!IsUsernameValid);
        }

        public void GetPassword(User user)
        {
            //Initialize a password
            string password = "";
            bool IsPasswordInvalid = true;
            bool passwordMatch = true;
            do
            {
                //Ask for the password
                Console.Write("Password: ");
                password = Console.ReadLine();
                //Checkif empty or null
                if (password == null || password.Trim(' ') == "")
                {
                    Console.WriteLine("Password cannot be blank!");
                }
                //Check whitespaces
                else if (password.Contains(' '))
                {
                    Console.WriteLine("Password cannot contain whitespaces!");
                }
                //Check length
                else if (password.Length < 8)
                {
                    Console.WriteLine("Password must be longer than 8 characters!");
                }
                else
                {
                    //Ask for confirmation
                    Console.Write("Confirm Password: ");
                    string confirmPassword = Console.ReadLine();
                    //Compare
                    passwordMatch = confirmPassword.Equals(password);
                    if (passwordMatch)
                    {
                        //Set the password
                        user.Password = password;
                        IsPasswordInvalid = false;
                        Console.WriteLine("Password is set");
                    }
                    else
                    {
                        Console.WriteLine("Passwords don't match!");
                    }
                }
            } while (IsPasswordInvalid || !passwordMatch);
        }
        public void GetEmail(User user)
        {
            do
            {
                //Ask for email address
                Console.Write("Email: ");
                string email = Console.ReadLine().Trim(' ').ToLower();
                //Validate its length
                if (email.Length > 320)
                    Console.WriteLine("Input is too long!!! Please enter a valid one!");
                //Validate the '@' character
                else if (!email.Contains('@'))
                    Console.WriteLine("Invalid email address!!! Please enter a valid one!");
                //Validate '.' 
                else if (!email.Contains('.'))
                    Console.WriteLine("Invalid email address!!! Please enter a valid one!");
                else
                {
                    user.Email = email;
                    break;
                }
            } while (true);
            
        }
        public void GetBirthDate(User user)
        {
            do
            {
                //Ask for the birthdate
                Console.Write("BirthDate: ");
                string birthDateInput = Console.ReadLine().Trim(' ');
                //Initialize a datetime
                try
                {
                    DateTime birthDate = DateTime.Parse(birthDateInput);
                    //Check for a valid birthyear
                    if (birthDate.Year > DateTime.Now.Year)
                        Console.WriteLine("Birthyear is not valid");
                    user.BirthDate = birthDate;
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input! Please try agin");
                }
            } while (true);
        }
        public void CompleteRegistration(User user)
        {
            GetUsername(user);
            GetPassword(user);
            GetEmail(user);
            GetBirthDate(user);
        }
    }
}

