using System;
using System.Data.SqlClient;

namespace ConsoleApp25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=DESKTOP-6676I5B;Initial Catalog=schoo;Integrated Security=True;Connect Timeout=30;";

            while (true)
            {
                Console.WriteLine("select:");
                Console.WriteLine("1 - con");
                Console.WriteLine("2 - discon");
                Console.WriteLine("3 - show inf");
                Console.WriteLine("4 - show pip");
                Console.WriteLine("5 - show grades");
                Console.WriteLine("6 - show min grade");
                Console.WriteLine("7 - show norm grades");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ConnectToDatabase(connectionString);
                        break;
                    case "2":
                        DisconnectFromDatabase();
                        break;
                    case "3":
                        ShowAllData(connectionString);
                        break;
                    case "4":
                        ShowAllNames(connectionString);
                        break;
                    case "5":
                        ShowAllAvgGrades(connectionString);
                        break;
                    case "6":
                        Console.Write("enter min grade: ");
                        if (double.TryParse(Console.ReadLine(), out double minGrade))
                        {
                            ShowStudentsWithMinGradeAbove(connectionString, minGrade);
                        }
                        else
                        {
                            Console.WriteLine("error");
                        }
                        break;
                    case "7":
                        ShowUniqueMinSubjects(connectionString);
                        break;
                    default:
                        Console.WriteLine("error.");
                        break;
                }
            }
        }

        static void ConnectToDatabase(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                    connection.Open();
                    Console.WriteLine("connecter");
                
            }
        }

        static void DisconnectFromDatabase()
        {
            Console.WriteLine("discon");
        }

        static void ShowAllData(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                    connection.Open();
                    string query = "SELECT * FROM school";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["namee"]}, Group: {reader["groupp"]}, Avrage MArk: {reader["mark"]}, min mark: {reader["minname"]}, max mark: {reader["maxname"]}");
                    }

            }
        }

        static void ShowAllNames(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                    connection.Open();
                    string query = "SELECT namee FROM school";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("PIP:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["namee"]);
                    }

            }
        }

        static void ShowAllAvgGrades(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                    connection.Open();
                    string query = "SELECT mark FROM school";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Середні оцінки:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["mark"]);
                    }

            }
        }

        static void ShowStudentsWithMinGradeAbove(string connectionString, double minGrade)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                    connection.Open();
                    string query = $"SELECT namee FROM school WHERE CAST(mark AS FLOAT) > {minGrade}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine($"Студенти з мінімальною оцінкою більше {minGrade}:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["namee"]);
                    }
             
            }
        }

        static void ShowUniqueMinSubjects(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                    connection.Open();
                    string query = "SELECT DISTINCT minname FROM school";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Унікальні назви предметів із мінімальними оцінками:");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["minname"]);
                    }

            }
        }
    }
}
