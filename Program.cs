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


            Console.WriteLine("Employees List: ");
            foreach (DataRow Row in Employees.Rows) {

                Console.WriteLine("ID: {0}\t FirstName: {1}\t LastName: {2}\t Gender: {3}\t Salary:{4}\t BirthDate{5}\t", 
                    Row["ID"] ,Row["FirstName"], Row["LastName"], Row["Gender"],Row["Salary"], Row["DateOfBirth"]);
            
            }
            





        }
    }
}
