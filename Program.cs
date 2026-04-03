using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Datetables_app
{
    internal class Program
    {
        static void PrintList(DataTable Data) {

            Console.WriteLine("Employees List: ");
            foreach (DataRow Row in Data.Rows)
            {

                Console.WriteLine("ID: {0}\t FirstName: {1}\t LastName: {2}\t Gender: {3}\t Salary:{4}\t BirthDate{5}\t",
                    Row[0], Row[1], Row[2], Row[3], Row[4], Row[5]);

            }
        }
        static void PrintList(DataTable Data, string Filter)
        {
            DataRow[] Rows = Data.Select(Filter);
            Console.WriteLine("Employees List: ");
            foreach (DataRow Row in Rows)
            {
                Console.WriteLine("ID: {0}\t FirstName: {1}\t LastName: {2}\t Gender: {3}\t Salary:{4}\t BirthDate{5}\t",
                    Row[0], Row[1], Row[2], Row[3], Row[4], Row[5]);
            }
        }

        static void PrintAgregate(DataTable Data) {
            //Aggregate functions:
            int EmployeesCount = 0;
            double TotalSalaries = 0;
            double AvgSalaries = 0;
            double MinSalary = 0;
            double MaxSalary = 0;

            EmployeesCount = Data.Rows.Count;
            TotalSalaries = Convert.ToDouble(Data.Compute("sum(Salary)", string.Empty));
            AvgSalaries = Convert.ToDouble(Data.Compute("avg(Salary)", string.Empty));
            MinSalary = Convert.ToDouble(Data.Compute("min(Salary)", string.Empty));
            MaxSalary = Convert.ToDouble(Data.Compute("max(Salary)", string.Empty));

            Console.WriteLine("\n");
            Console.WriteLine("Employees coutn: " + EmployeesCount);
            Console.WriteLine("Total Salaries: " + TotalSalaries);
            Console.WriteLine("Avarg Salary: " + AvgSalaries);
            Console.WriteLine("Max Salary: " + MaxSalary);
            Console.WriteLine("Minimum Salary: " + MinSalary);

        }

        static void PrintAgregate(DataTable Data, string FirstFilter, string SecondFilter = "") {


            //Aggregate functions:
            DataRow[] Result = Data.Select(FirstFilter);
            int EmployeesCount = 0;
            double TotalSalaries = 0;
            double AvgSalaries = 0;
            double MinSalary = 0;
            double MaxSalary = 0;

            EmployeesCount = Result.Count();
            TotalSalaries = Convert.ToDouble(Data.Compute("sum(Salary)", FirstFilter));
            AvgSalaries = Convert.ToDouble(Data.Compute("avg(Salary)", FirstFilter));
            MinSalary = Convert.ToDouble(Data.Compute("min(Salary)", FirstFilter));
            MaxSalary = Convert.ToDouble(Data.Compute("max(Salary)", FirstFilter));

            Console.WriteLine("\n");
            Console.WriteLine("Employees count: " + EmployeesCount);
            Console.WriteLine("Total Salaries: " + TotalSalaries);
            Console.WriteLine("Average Salary: " + AvgSalaries);
            Console.WriteLine("Max Salary: " + MaxSalary);
            Console.WriteLine("Minimum Salary: " + MinSalary);
            Console.WriteLine("\n");
            PrintList(Data, FirstFilter);
           
        }
        static void Delete(DataTable Data, string Target) {

            DataRow[] Rows = Data.Select(Target);
            foreach (var Row in Rows) { 
                Row.Delete();
            }
            //if connected to Database we use this function to reflect the changes:
            //Data.AcceptChanges();
            foreach (DataRow Row in Data.Rows) {
                Console.WriteLine("ID: {0}\t FirstName: {1}\t LastName: {2}\t Gender: {3}\t Salary:{4}\t BirthDate{5}\t",
                 Row[0], Row[1], Row[2], Row[3], Row[4], Row[5]);
            }
        
        }
        static void Main(string[] args)
        {

            DataTable Employees = new DataTable();

            //creating the columns: 
            Employees.Columns.Add("ID", typeof(int));
            Employees.Columns.Add("FirstName", typeof(string));
            Employees.Columns.Add("LastName", typeof(string));
            Employees.Columns.Add("Gender", typeof(Char));
            Employees.Columns.Add("Salary", typeof(float));
            Employees.Columns.Add("DateOfBirth", typeof(DateTime));

            // filling in the date 
            Employees.Rows.Add(1, "Carlos", "Costa", 'M', 5000, new DateTime(1998, 7, 8));
            Employees.Rows.Add(2, "Alhoa", "Costa", 'F', 500, new DateTime(2022, 5, 8));
            Employees.Rows.Add(3, "Ricardo", "Costa", 'M', 8000, new DateTime(1989, 7, 25));
            Employees.Rows.Add(4, "Koda", "Costa", 'M', 200, new DateTime(2025, 1, 1));
            //filtering date

            //PrintList(Employees);
            //PrintAgregate(Employees);
            //Print all M Employees
            //PrintAgregate(Employees, "Gender='M'");
            //PrintAgregate(Employees, "Gender='M' or Gender='F'");
            //PrintAgregate(Employees, "ID='1'");

            //Sorting 
            //Employees.DefaultView.Sort = "ID desc";
            //Employees = Employees.DefaultView.ToTable();
            //PrintList(Employees);

            //Employees.DefaultView.Sort = "FirstName asc";
            //Employees = Employees.DefaultView.ToTable();
            //PrintList(Employees);

            Delete(Employees, "ID='4'");





            Console.ReadKey();
        }
    }
}
