using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;




namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*essential variable/lists */
            string user_age = ""; // stores user's age
            string movie_choice = ""; // stores user's movie choice
            int row, column = 0; // stores user seat

            DateTime user_date;
            Dictionary<string, string> movies = new Dictionary<string, string>() // a dictionary for the movies, and their age ratings (can be easily changed and program will run fine)
            {
                {"PlaceHolder", "69"},
                {"Rush", "15"},
                {"How I Live Now", "15"},
                {"Thor: The Dark World", "12"},
                {"Filth", "18"},
                {"Planes", "U"},
            };

            Dictionary<string, int> tickets = new Dictionary<string, int>() // a dictionary for the movies (written in a way to take input from the first dictionary) and how many tickets have been sold for that
            {
                {"PlaceHolder", 0},
                {movies.ElementAt(1).Key, 0},
                {movies.ElementAt(2).Key, 0},
                {movies.ElementAt(3).Key, 0},
                {movies.ElementAt(4).Key, 0},
                {movies.ElementAt(5).Key, 0},
            };

            List<List<string>> seats = new List<List<string>>();

            seats.Add(new List<string> {"O", "O", "O", "O", "O"});
            seats.Add(new List<string> {"O", "O", "O", "O", "O"});
            seats.Add(new List<string> {"O", "O", "O", "O", "O"});
            seats.Add(new List<string> {"O", "O", "O", "O", "O"});
            seats.Add(new List<string> {"O", "O", "O", "O", "O"});

            while (movie_choice != "specialexitcode") // allows the program to run forever except when the specialexitcode is entered.
            {
                movie_choice = Validate(movie_choice, movies, tickets); // receives movie_choice with validation
                Console.Clear();

                if(Convert.ToInt32(movie_choice) > 5 || Convert.ToInt32(movie_choice) < 1) // if the movie is out of range
                {
                    Console.WriteLine("Access denied - no such film");
                    Thread.Sleep(5000);
                }
                
                else // will only run if movie is in range
                {

                    user_age = ValidateAge(user_age, movies, tickets); // receives user_age with validation
                    Console.Clear();

                    if(movies.ElementAt(Convert.ToInt32(movie_choice)).Value != "U" && Convert.ToInt32(user_age) < Convert.ToInt32(movies.ElementAt(Convert.ToInt32(movie_choice)).Value)) // if the user is too young and movie is not a U
                    {
                        Console.WriteLine("Access denied - you are too young");
                        Thread.Sleep(5000);
                    }

                    else // will only run if movie is in range and user is old enough
                    {
                        user_date = ValidateDate(movies, tickets);
                        var diffOfDates = user_date - DateTime.Now;
                        if (diffOfDates.Days > 7 || diffOfDates.Days < 0)
                        {
                            Console.WriteLine("Access denied - date is invalid");
                            Thread.Sleep(5000);
                        }
                        
                        else // will only run if movie is in range, user is old enough and date is valid
                        {
                            
                            (row, column) = ValidateSeat(seats); // takes seat input

                            seats[row][column] = "X"; // assigns seat as "taken"

                            Console.Clear();
                            Print(DateOnly.FromDateTime(user_date), user_age, movie_choice, movies, row, column); // prints ticket

                            // Ticket counting system

                            switch (Convert.ToInt32(movie_choice))
                            {
                                case 1:
                                    tickets["Rush"] += 1;
                                    break;
                                case 2:
                                    tickets["How I Live Now"] += 1;
                                    break;
                                case 3:
                                    tickets["Thor: The Dark World"] += 1;
                                    break;
                                case 4:
                                    tickets["Filth"] += 1;
                                    break;
                                case 5:
                                    tickets["Planes"] += 1;
                                    break;
                            }

                            /* RESETING ALL VARIABLES*/
                            user_age = "";
                            movie_choice = "";
                            row = 0;
                            column = 0;


                        }
                    }
                }
            }
        }

        /* THIS PRINTS THE MENU SCREEN AND TAKES USER INPUT*/
        static string menu(Dictionary<string, string> movies, Dictionary<string, int> tickets)
        {
            // I wrote the code in a way which allows everything to still work if the dictionary is changed to have different movies and age ratings.
            Console.Clear();
            Console.Write($"Welcome to Aquinas Multiplex\nWe are presently showing\n1. {movies.ElementAt(1).Key} ({movies.ElementAt(1).Value})  Tickets sold: {tickets.ElementAt(1).Value}\n2. {movies.ElementAt(2).Key} ({movies.ElementAt(2).Value})  Tickets sold: {tickets.ElementAt(2).Value}\n3. {movies.ElementAt(3).Key} ({movies.ElementAt(3).Value})  Tickets sold: {tickets.ElementAt(3).Value}\n4. {movies.ElementAt(4).Key} ({movies.ElementAt(4).Value})  Tickets sold: {tickets.ElementAt(4).Value}\n5. {movies.ElementAt(5).Key} ({movies.ElementAt(5).Value})  Tickets sold: {tickets.ElementAt(5).Value}\n\n");
            return"";
        }

        static string Validate(string movie_choice, Dictionary<string, string> movies, Dictionary<string, int> tickets) // Allows the user to input movie choice
        {
            menu(movies, tickets);
            do{
                Console.WriteLine("Enter the number of the film you wish to see: ");
                movie_choice = Console.ReadLine();
                try
                {
                    int movie_choice_int = Convert.ToInt32(movie_choice);
                    break;
                }
                catch
                {
                    menu(movies, tickets);
                    Console.WriteLine("Enter a movie from 1 - 5!");
                }
            }while(true);

            return movie_choice;

        }

        static string ValidateAge(string user_age, Dictionary<string, string> movies, Dictionary<string, int> tickets) // Allows the user to input their age
        {
            menu(movies, tickets);
            do{
                Console.WriteLine("Enter your age: ");
                user_age = Console.ReadLine();
                try
                {
                    int user_age_int = Convert.ToInt32(user_age);
                    break;
                }
                catch
                {
                    menu(movies, tickets);
                    Console.WriteLine("Enter a number!");
                }
            }while(true);

            return user_age;

        }
        static DateTime ValidateDate(Dictionary<string, string> movies, Dictionary<string, int> tickets) // Allows the user to input when they want to watch
        {
            DateTime user_date = DateTime.Now;
            string input;
            menu(movies, tickets);
            do{
                Console.WriteLine("When would you like to watch the movie? ");
                input = Console.ReadLine();
                try
                {
                    user_date = Convert.ToDateTime(input);
                    break;
                }
                catch
                {
                    menu(movies, tickets);
                    Console.WriteLine("Enter a date!");
                }
            }while(true);

            return user_date;

        }
        // prints ticket
        static void Print(DateOnly user_date, string user_age, string movie_choice, Dictionary<string, string> movies, int row, int column)
        {
            Console.WriteLine("-----------------");
            Thread.Sleep(100);
            Console.WriteLine($"Film : {movies.ElementAt(Convert.ToInt32(movie_choice)).Key}");
            Thread.Sleep(100);
            Console.WriteLine($"Date : {user_date}");
            Thread.Sleep(100);
            Console.WriteLine($"Seat : {row}{column}");
            Thread.Sleep(100);
            Console.WriteLine("");
            Thread.Sleep(100);
            Console.WriteLine("Enjoy the film");
            Thread.Sleep(100);
            Console.WriteLine("-----------------");
            Thread.Sleep(5000);
            
        }

        static void displaySeats(List<List<string>> seats)
        {
            /* Displays Seats*/

            Console.Clear();
            int i = 0;

            Console.Write("  12345");

            foreach(var list in seats)
            {


                Console.WriteLine("");
                i++;
                Console.Write($"{i} ");

                foreach (var item in list)
                {
                    Console.Write(item);
                }
            }
            

        }

        static (int, int) ValidateSeat(List<List<string>> seats)
        {
            int row = 0;
            int column = 0;

            while (true)
            {

                displaySeats(seats);
                Console.WriteLine("\nPlease enter where you would like to sit!");
                Console.WriteLine("\nEnter row: ");
                string roe = Console.ReadLine();
                Console.WriteLine("Enter column: ");
                string colum = Console.ReadLine();

                try
                {
                    row = int.Parse(roe);
                    column = int.Parse(colum);

                    if (  (row < 6 || row > 0) && (column < 6 || column > 0)  && (seats[row][column] == "O") ) // if the seat is valid and not taken
                    {
                        break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("That spot has been taken!");
                    Thread.Sleep(1000);
                }

            }

            return (row, column);
        }
    }
}