using System;
using System.Collections.Generic;

namespace Lab5CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What task do you want?");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            Console.WriteLine("3. Task 3");
            Console.WriteLine("4. Exit");

            int choice;
            bool isValidChoice = false;

            do
            {
                Console.Write("Enter number of task: ");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);

                if (!isValidChoice || choice < 1 || choice > 4)
                {
                    Console.WriteLine("This task does not exist");
                    isValidChoice = false;
                }
            } while (!isValidChoice);

            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    break;
            }
        }

        interface IEmployee
        {
            string Name { get; }
            string Surname { get; }
            int Age { get; }
            void Show();
        }

        interface ISpecialization
        {
            string Specialization { get; }
            double Salary { get; }
        }

        interface IRole
        {
            string Role { get; }
        }

        interface IDepartment
        {
            string Department { get; }
        }

        abstract class Worker : IEmployee
        {
            public string Name { get; }
            public string Surname { get; }
            public int Age { get; }

            public Worker(string name, string surname, int age)
            {
                Name = name;
                Surname = surname;
                Age = age;
            }

            public virtual void Show()
            {
                Console.WriteLine($"Name: {Name}, Surname: {Surname}, Age: {Age}");
            }
        }

        class Engineer : Worker, ISpecialization
        {
            public string Specialization { get; }
            public double Salary { get; }

            public Engineer(string name, string surname, int age, string specialization, double salary) : base(name, surname, age)
            {
                Specialization = specialization;
                Salary = salary;
            }

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Specialization: {Specialization}, Salary: {Salary}");
            }
        }

        class HumanResources : Worker, IRole
        {
            public string Role { get; }

            public HumanResources(string name, string surname, int age, string role) : base(name, surname, age)
            {
                Role = role;
            }

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Role: {Role}");
            }
        }

        class Administration : HumanResources, IDepartment
        {
            public string Department { get; }

            public Administration(string name, string surname, int age, string role, string department) : base(name, surname, age, role)
            {
                Department = department;
            }

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Department: {Department}");
            }
        }

        static void Task1()
        {
            Console.WriteLine("Task 1");
            List<IEmployee> employees = new List<IEmployee>();
            employees.Add(new Engineer("Jane", "Smith", 28, "Software Engineering", 50000));
            employees.Add(new HumanResources("Mike", "Johnson", 40, "Recruiter"));
            employees.Add(new Administration("Emily", "Williams", 32, "Manager", "HR"));

            foreach (var employee in employees)
            {
                employee.Show();
                Console.WriteLine();
            }
        }

        abstract class PhoneDirectory
        {
            public abstract void DisplayInformation();
            public abstract bool MatchesSearchCriteria(string criterion);
        }

        class Person : PhoneDirectory
        {
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }

            public override void DisplayInformation()
            {
                Console.WriteLine($"Person: {LastName}, {Address}, {PhoneNumber}");
            }

            public override bool MatchesSearchCriteria(string criterion)
            {
                return LastName.Equals(criterion, StringComparison.OrdinalIgnoreCase);
            }
        }

        class Organization : PhoneDirectory
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string ContactPerson { get; set; }

            public override void DisplayInformation()
            {
                Console.WriteLine($"Organization: {Name}, {Address}, {Phone}, {Fax}, {ContactPerson}");
            }

            public override bool MatchesSearchCriteria(string criterion)
            {
                return Name.Contains(criterion);
            }
        }

        class Friend : PhoneDirectory
        {
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime DateOfBirth { get; set; }

            public override void DisplayInformation()
            {
                Console.WriteLine($"Friend: {LastName}, {Address}, {PhoneNumber}, {DateOfBirth}");
            }

            public override bool MatchesSearchCriteria(string criterion)
            {
                return LastName.Contains(criterion);
            }
        }

        static void Task2()
        {
            Console.WriteLine("Task 2");
            PhoneDirectory[] n = new PhoneDirectory[]
            {
                new Person { LastName = "Chervinska", Address = "Kyiv", PhoneNumber = "111-11-11" },
                new Organization { Name = "Andrey", Address = "Lviv", Phone = "222-22-22", Fax = "222-22-23", ContactPerson = "Petro" },
                new Friend { LastName = "Sidorov", Address = "Odessa", PhoneNumber = "333-33-33", DateOfBirth = new DateTime(1990, 5, 15) }
            };

            foreach (var record in n)
            {
                record.DisplayInformation();
            }

            string searchCriterion = "Chervinska";
            Console.WriteLine($"\nSearch results for last name '{searchCriterion}':");
            foreach (var record in n)
            {
                if (record.MatchesSearchCriteria(searchCriterion))
                {
                    record.DisplayInformation();
                }
            }
        }

        class WorkerTask3 : Worker, IEnumerable<WorkerTask3>
        {
            public WorkerTask3(string name, string surname, int age) : base(name, surname, age)
            {
                Console.WriteLine("Worker constructor called.");
            }

            ~WorkerTask3()
            {
                Console.WriteLine("Worker destructor called.");
            }

            public override void Show()
            {
                base.Show();
            }

            public IEnumerator<WorkerTask3> GetEnumerator()
            {
                yield return this;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class EngineerTask3 : WorkerTask3
        {
            public EngineerTask3(string name, string surname, int age, string specialization, double salary) : base(name, surname, age)
            {
                Specialization = specialization;
                Salary = salary;
                Console.WriteLine("Engineer constructor called.");
            }

            ~EngineerTask3()
            {
                Console.WriteLine("Engineer destructor called.");
            }

            public string Specialization;
            public double Salary;

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Specialization: {Specialization}, Salary: {Salary}");
            }
        }

        class HumanResourcesTask3 : WorkerTask3
        {
            public HumanResourcesTask3(string name, string surname, int age, string role) : base(name, surname, age)
            {
                Role = role;
                Console.WriteLine("HumanResources constructor called.");
            }

            ~HumanResourcesTask3()
            {
                Console.WriteLine("HumanResources destructor called.");
            }

            public string Role;
            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Role: {Role}");
            }
        }

        class AdministrationTask3 : HumanResourcesTask3
        {
            public AdministrationTask3(string name, string surname, int age, string role, string department) : base(name, surname, age, role)
            {
                Department = department;
                Console.WriteLine("Administration constructor called.");
            }

            ~AdministrationTask3()
            {
                Console.WriteLine("Administration destructor called.");
            }

            public string Department;
            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Department: {Department}");
            }
        }

        static void Task3()
        {
            Console.WriteLine("Task 3");
            List<WorkerTask3> workers = new List<WorkerTask3>();
            workers.Add(new WorkerTask3("John", "Doe", 35));
            workers.Add(new EngineerTask3("Jane", "Smith", 28, "Software Engineering", 50000));
            workers.Add(new HumanResourcesTask3("Mike", "Johnson", 40, "Recruiter"));
            workers.Add(new AdministrationTask3("Emily", "Williams", 32, "Manager", "HR"));

            foreach (var worker in workers)
            {
                foreach (var item in worker)
                {
                    item.Show();
                    Console.WriteLine();
                }
            }
        }
    }
}
