using System;
using System.Collections.Generic;
using Course_Catalogue.Models;

namespace Course_Catalogue.Services
{
    public class LearnerService
    {
        private Dictionary<string, Learner> _learners;

        public LearnerService(Dictionary<string, Learner> learners)
        {
            _learners = learners;
        }

        public void RegisterLearner(string id, string name, string contact, string email)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("-> Error: Learner ID and Name cannot be empty.");
                return;
            }

            if (_learners.ContainsKey(id))
            {
                Console.WriteLine($"-> Error: Learner ID '{id}' already exists.");
                return;
            }

            if (string.IsNullOrWhiteSpace(contact) || contact.Length != 10 || !long.TryParse(contact, out _))
            {
                Console.WriteLine("-> Error: Invalid contact number. Must be 10 digits.");
                return;
            }

            Learner newLearner = new Learner(id.Trim().ToUpper(), name.Trim(), contact.Trim(), email.Trim());
            _learners.Add(newLearner.LearnerId, newLearner);
            Console.WriteLine("-> Success: Learner registered successfully.");
        }

        public void DisplayAllLearners()
        {
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"| {"Learner ID",-12} | {"Name",-22} | {"Contact Number",-16} | {"Email",-25} |");
            Console.WriteLine(new string('-', 85));

            foreach (var learner in _learners.Values)
            {
                Console.WriteLine($"| {learner.LearnerId,-12} | {learner.Name,-22} | {learner.ContactNumber,-16} | {learner.Email,-25} |");
            }
            Console.WriteLine(new string('-', 85));
        }

        public void SearchLearnerById(string id)
        {
            if (_learners.TryGetValue(id, out Learner learner))
            {
                Console.WriteLine($"-> Found: ID: {learner.LearnerId} | Name: {learner.Name} | Contact: {learner.ContactNumber} | Email: {learner.Email}");
            }
            else
            {
                Console.WriteLine($"-> Error: Learner with ID '{id}' was not found.");
            }
        }
    }
}