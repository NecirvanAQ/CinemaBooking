using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;




namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*essential variable/lists */
            
            string user_age = ""; // stores user's age
            string movie_choice = ""; // stores user's movie choice
            DateOnly user_date;
            Dictionary<string, string> movies = new Dictionary<string, string>() // a dictionary for the movies, and their age ratings (can be easily changed and program will run fine)
            {
                {"PlaceHolder", "69"},
                {"Rush", "15"},
                {"How I Live Now", "15"},
                {"Thor: The Dark World", "12"},
                {"Filth", "18"},
                {"Planes", "U"},
            };



            while (movie_choice != "specialexitcode") // allows the program to run forever except when the specialexitcode is entered.
            {
                movie_choice = Validate(movie_choice, movies); // receives movie_choice with validation
                Console.Clear();

                if(Convert.ToInt32(movie_choice) > 5 || Convert.ToInt32(movie_choice) < 1) // if the movie is out of range
                {
                    Console.WriteLine("Access denied - no such film");
                    Thread.Sleep(5000);
                }
                
                else // will only run if movie is in range
                {
                    user_age = ValidateAge(user_age, movies); // receives user_age with validation
                    Console.Clear();

                    if(Convert.ToInt32(user_age) < Convert.ToInt32(movies.ElementAt(Convert.ToInt32(movie_choice)).Value)) // if the user is too young
                    {
                        Console.WriteLine("Access denied - you are too young");
                        Thread.Sleep(5000);
                    }

                    else // will only run if movie is in range and user is old enough
                    {
                        
                    }
                }
            }
        }

        /* THIS PRINTS THE MENU SCREEN AND TAKES USER INPUT*/
        static string menu(Dictionary<string, string> movies)
        {
            // I wrote the code in a way which allows everything to still work if the dictionary is changed to have different movies and age ratings.
            Console.Clear();
            Console.Write($"Welcome to Aquinas Multiplex\nWe are presently showing\n1. {movies.ElementAt(1).Key} ({movies.ElementAt(1).Value})\n2. {movies.ElementAt(2).Key} ({movies.ElementAt(2).Value})\n3. {movies.ElementAt(3).Key} ({movies.ElementAt(3).Value})\n4. {movies.ElementAt(4).Key} ({movies.ElementAt(4).Value})\n5. {movies.ElementAt(5).Key} ({movies.ElementAt(5).Value})\n\n");
            return"";
        }

        static string Validate(string movie_choice, Dictionary<string, string> movies)
        {
            menu(movies);
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
                    menu(movies);
                    Console.WriteLine("Enter a movie from 1 - 5!");
                }
            }while(true);

            return movie_choice;

        }

        static string ValidateAge(string user_age, Dictionary<string, string> movies)
        {
            menu(movies);
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
                    menu(movies);
                    Console.WriteLine("Enter a number!");
                }
            }while(true);

            return user_age;

        }
        /*
        static string ValidateDate(DateOnly user_date, Dictionary<string, string> movies)
        {
            string input;
            menu(movies);
            do{
                Console.WriteLine("When would you like to watch the movie? ");
                input = Console.ReadLine();
                try
                {
                    user_date = DateOnly.FromDateTime(Convert.ToDateTime(input));
                    if (user_date -DateOnly.FromDateTime(DateTime.Now)  )
                    break;
                }
                catch
                {
                    menu(movies);
                    Console.WriteLine("Enter a number!");
                }
            }while(true);

            return user_date;

        }
        */
    }
}