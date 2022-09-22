using System;
namespace Week08.Homework
{
    public class Contact
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public Contact(string name, string number)
        {
            Name = name;
            if (long.TryParse(number, out long phoneNumber))
                Number = number;
            else
            {
                Number = null;
                Console.WriteLine("Invalid number format!!!");
            }
        }
        public override string ToString()
        {
            return $"Name: {Name}, Number: {Number}";
        }
    }
}

