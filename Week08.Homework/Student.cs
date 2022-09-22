using System;
using System.Globalization;

namespace Week08.Homework
{
    public class Student
    {
        private static int iD = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Faculty { get; set; }
        public byte Age { get; set; }
        //Culture
        public CultureInfo Culture { get; set; }

        public Student(string name, string surname, string faculty, byte age, CultureInfo culture)
        {
            iD++;
            Id = iD;
            Name = name;
            Surname = surname;
            Faculty = faculty;
            Age = age;
            Culture = culture;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Age: {Age}, Faculty: {Faculty}, Id: {Id}, Culture: {Culture.DisplayName}";
        }
    }
}

