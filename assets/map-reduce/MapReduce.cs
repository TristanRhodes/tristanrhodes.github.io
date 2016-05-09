using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MapReduce.Tests
{
    [TestFixture]
    public class MapReduce
    {
        private Random _random = new Random(100);

        private string[] _firstNames = new string[] { "John", "James", "Gavin", "Richard", "Steven", "Brian" };
        private string[] _lastNames = new string[] { "Smith", "Clarke", "Baker", "Hooper", "Fisher" };

        [Test]
        public void GetMinMaxAverageAgeOfPeopleByLastName()
        {
            var customers = GetCustomers(100);

            // Map Phase - Group our customers by last name
            var mappedCustomers = customers
                        .GroupBy(c => c.LastName)
                        .ToList(); // NOTE: This prevents multiple enumerations.

            // Print our mapped results
            Trace.WriteLine("===Mapped===");
            foreach (var mapped in mappedCustomers)
            {
                Trace.WriteLine(string.Format("{0} ({1})", mapped.Key, mapped.Count()));
            }

            // Reduce Phase - Calculate average age for groups
            var reducedCustomers = mappedCustomers
                        .Select(c => new
                        {
                            LastName = c.Key,
                            Oldest = c.Max(v => v.Age),
                            Youngest = c.Min(v => v.Age),
                            Average = c.Average(v => v.Age)
                        });

            // Print our reduced results
            Trace.WriteLine("===Reduced===");
            foreach (var reduced in reducedCustomers)
            {
                Trace.WriteLine(string.Format("{0} - Youngest: {1}, Oldest: {2}, Average: {3:0.##}", 
                                reduced.LastName, reduced.Youngest, reduced.Oldest, reduced.Average));
            }
        }

        public IEnumerable<Customer> GetCustomers(int setSize)
        {
            for (int i = 0; i < setSize; i++)
            {
                var customer = new Customer()
                {
                    Id = i,
                    Age = _random.Next(1, 100),
                    FirstName = _firstNames[_random.Next(0, _firstNames.Length)],
                    LastName = _lastNames[_random.Next(0, _lastNames.Length)]
                };

                Trace.WriteLine(customer);

                yield return customer;
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1} {2}, Age: {3}", Id, FirstName, LastName, Age);
        }
    }
}
