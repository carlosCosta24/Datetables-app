using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.XPath;

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
        static void Update(DataTable Data,int ID, string FirstName, double Salary) {

            DataRow[] Rows = Data.Select($"ID = '{ID}'");

            foreach (var Row in Rows) {
                Row["FirstName"] = FirstName;
                Row["Salary"] = Salary;
            }
            //Data.AcceptChanges();
            foreach (DataRow Row in Data.Rows)
            {
                Console.WriteLine("ID: {0}\t FirstName: {1}\t LastName: {2}\t Gender: {3}\t Salary:{4}\t BirthDate{5}\t",
                 Row[0], Row[1], Row[2], Row[3], Row[4], Row[5]);
            }


        }
        static void CreatCoulmn(DataTable Table, Type Type, String Name, bool AutoIncrement, 
             string Caption, bool ReadOnly, bool Unique, int IncremntSeed = 0)
        {

            DataColumn Dc = new DataColumn();
            Dc.DataType = Type;
            Dc.ColumnName = Name;
            Dc.AutoIncrement = AutoIncrement;
            Dc.AutoIncrementSeed = IncremntSeed;
            Dc.Caption = Caption;
            Dc.ReadOnly = ReadOnly;
            Dc.Unique = Unique;
            Table.Columns.Add(Dc);
        
        }
        static void Main(string[] args)
        {

            DataTable Employees = new DataTable();
            CreatCoulmn(Employees, typeof(int), "ID", true, "Employee ID", false, true);
            CreatCoulmn(Employees, typeof(string), "FirstName", false, "Employee FirstName", false, false);
            CreatCoulmn(Employees, typeof(string), "LastName", false,  "Employee LastName", false, false);
            CreatCoulmn(Employees, typeof(char), "Gender", false, "Gender", false, false);
            CreatCoulmn(Employees, typeof(float), "Salary", false, "Salary Amount", false, false);
            CreatCoulmn(Employees, typeof(DateTime), "BirthDate", false, "Employee Birth Date", false, false);

            DataColumn[] PrimaryKey = new DataColumn[1];
            PrimaryKey[0] = Employees.Columns["ID"];
            Employees.PrimaryKey = PrimaryKey;

            Employees.Rows.Add(null, "Carlos", "Costa", 'M', 5000, new DateTime(1998, 7, 9));
            Employees.Rows.Add(null, "Alhoa", "Costa", 'F', 500, new DateTime(2022, 5, 2));
            Employees.Rows.Add(null, "Ricardo", "Costa", 'M', 8000, new DateTime(1989, 7,23));
            Employees.Rows.Add(null, "Koda", "Costa", 'M', 200, new DateTime(2025, 1, 20));

            //data view
            DataView EmployessView = Employees.DefaultView;
            Console.WriteLine("Employees Data view: ");
            for (int i = 0; i < EmployessView.Count; i++) {

                Console.WriteLine("{0}, {1}, {2},{3}, {4},{5}", EmployessView[i][0], EmployessView[i][1], EmployessView[i][2],
                    EmployessView[i][3], EmployessView[i][4], EmployessView[i][5]);
            }

            Console.WriteLine("\n");
            //Filter Dataview
            EmployessView.RowFilter = "Gender = 'M'";
            for (int i = 0; i < EmployessView.Count; i++)
            {

                Console.WriteLine("{0}, {1}, {2},{3}, {4},{5}", EmployessView[i][0], EmployessView[i][1], EmployessView[i][2],
                    EmployessView[i][3], EmployessView[i][4], EmployessView[i][5]);
            }
            Console.WriteLine("\n");
            //sorting
            EmployessView.Sort = "FirstName asc";

            for (int i = 0; i < EmployessView.Count; i++)
            {

                Console.WriteLine("{0}, {1}, {2},{3}, {4},{5}", EmployessView[i][0], EmployessView[i][1], EmployessView[i][2],
                    EmployessView[i][3], EmployessView[i][4], EmployessView[i][5]);
            }


            //creating the columns: 
            /* Employees.Columns.Add("ID", typeof(int));
             Employees.Columns.Add("FirstName", typeof(string));
             Employees.Columns.Add("LastName", typeof(string));
             Employees.Columns.Add("Gender", typeof(Char));
             Employees.Columns.Add("Salary", typeof(float));
             Employees.Columns.Add("DateOfBirth", typeof(DateTime));
            */
            /*
             DataColumn[]  PrimaryKey = new DataColumn[1];
             PrimaryKey[0] = Employees.Columns["ID"];
             Employees.PrimaryKey = PrimaryKey;
            */
            /*
             // filling in the date 
             Employees.Rows.Add(1, "Carlos", "Costa", 'M', 5000, new DateTime(1998, 7, 8));
             Employees.Rows.Add(1, "Alhoa", "Costa", 'F', 500, new DateTime(2022, 5, 8));
             Employees.Rows.Add(3, "Ricardo", "Costa", 'M', 8000, new DateTime(1989, 7, 25));
             Employees.Rows.Add(4, "Koda", "Costa", 'M', 200, new DateTime(2025, 1, 1));
            */
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

            //Delete(Employees, "ID='4'");

            //Update(Employees, 3, "Karla", 200);

            //Employees.Clear();
            //PrintList(Employees);

            //PrintList(Employees);





            Console.ReadKey();
        }
    }
}
