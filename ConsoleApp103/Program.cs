using System;
namespace ConsoleApp138
{
    public interface IPayment : IComparable<IPayment>
    {
        double GetAmount();
    }

    public class Employee : IPayment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        private static Random r;
        public Employee()
        {
            r = new Random();
        }
        public double GetAmount()
        {
            return Salary;
        }
        public static string GetARandomName()
        {
            int len = r.Next(2, 8);
            string name = "";
            for (int i = 0; i < len; i++)
            {
                name += (char)(r.Next(0, 26) + 'A');
            }
            return name;
        }
        public static Employee GetARandomEmployee()
        {
            Employee employee = new Employee
            {
                Id = r.Next(1, 1000),
                Name = GetARandomName(),
                Salary = r.NextDouble() * 3000 + 2000,
            };
            return employee;
        }

        public override string ToString()
        {
            return $"Employee ID: {Id,5},  Name: {Name,-22}, Salary:{Salary:0.00}";
        }
        public int CompareTo(IPayment other)
        {
            return other.GetAmount().CompareTo(GetAmount());
            /*
            if (other.Salary > Salary) return 1;
            else if(other.Salary < Salary) return -1;
            else return Name.CompareTo(other.Name);
            */
        }
    }

    public class UtilityBill : IPayment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Bill { get; set; }
        private static Random r;
        public UtilityBill()
        {
            r = new Random();
        }
        public double GetAmount()
        {
            return Bill;
        }
        public static string GetARandomTitle()
        {
            int len = r.Next(5, 20);
            string title = "";
            for (int i = 0; i < len; i++)
            {
                title += (char)(r.Next(0, 26) + 'A');
            }
            return title;
        }
        public static UtilityBill GetARandomBill()
        {
            UtilityBill bill = new UtilityBill
            {
                Id = r.Next(1, 1000),
                Title = GetARandomTitle(),
                Bill = r.NextDouble() * 3000 + 2000,
            };
            return bill;
        }

        public override string ToString()
        {
            return $"Utility ID:  {Id,5}, Title: {Title,-22}, Bill:  {Bill:0.00}";
        }
        public int CompareTo(IPayment other)
        {
            return other.GetAmount().CompareTo(GetAmount());

        }
    }

    internal class Program
    {
        static void Display(Employee[] empArray)
        {
            Console.WriteLine("Elements of the employee array: ");
            foreach (var e in empArray)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
        }
        static void Display(UtilityBill[] ulilityArray)
        {
            Console.WriteLine("Elements of the utility array: ");
            foreach (var u in ulilityArray)
            {
                Console.WriteLine(u);
            }
            Console.WriteLine();
        }
        static void Display(IPayment[] combinedArray)
        {
            Console.WriteLine("Elements of the Combined Array: ");
            foreach (var e in combinedArray)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Employee[] empArray = new Employee[20];
            for (int i = 0; i < empArray.Length; i++)
            {
                empArray[i] = Employee.GetARandomEmployee();
            }
            empArray[0].Salary = 1000;
            empArray[empArray.Length - 1].Salary = 1000;
            empArray[empArray.Length / 2].Salary = 1000;
            //Display(empArray);
            Array.Sort(empArray);
            Display(empArray);

            UtilityBill[] utilityArray = new UtilityBill[50];
            for (int i = 0; i < utilityArray.Length; i++)
            {
                utilityArray[i] = UtilityBill.GetARandomBill();
            }

            //Display(utilityArray);
            Array.Sort(utilityArray);
            Display(utilityArray);

            IPayment[] combinedArray = new IPayment[empArray.Length + utilityArray.Length];
            for (int i = 0; i < empArray.Length; i++)
            {
                combinedArray[i] = empArray[i];
            }
            for (int i = 0; i < utilityArray.Length; i++)
            {
                combinedArray[empArray.Length + i] = utilityArray[i];
            }
            //Display(combinedArray);
            Array.Sort(combinedArray);
            Display(combinedArray);
        }
    }
}