using System;
using System.Collections.Generic;

namespace Week08.Homework
{
    public class FilterContacts
    {
        public static List<Contact> contacts = new List<Contact>();
        public static void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }
        public static void Search(string filter)
        {
            List<Contact> filtered;
            if (filter != null)
            {
                filtered = contacts.FindAll(x => x.Number.Contains(filter.Trim()) || x.Name.Trim().ToLower().Contains(filter.Trim().ToLower()));
                if (filtered != null && filtered.Count != 0)
                    filtered.ForEach(x => Console.WriteLine(x.ToString()));
                else
                    Console.WriteLine("No contact found");
            }
            else
                Console.WriteLine("Invalid input!!!");

        }
        public static void ViewAllContacts()
        {
            if (contacts != null && contacts.Count != 0)
            {
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine(contact.ToString());
                }
            }
            else
                Console.WriteLine("No contacts found");
        }
        public FilterContacts()
        {
        }
    }
}

