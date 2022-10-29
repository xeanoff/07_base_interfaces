using System.Collections;

namespace _07_base_interfaces
{
    public enum Genre { Action, Horror, SuperHero }
    class Director : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Director(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public object Clone() => new Director(FirstName, LastName);
        public override string ToString() => $"{FirstName[0]}. {LastName}";
    }
    class Movie : ICloneable, IComparable<Movie>
    {
        public string Title { get; set; }
        public Director Director { get; set; }
        public string Country { get; set; }
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public Movie(string title, Director director, string country, Genre genre, int year, double rating)
        {
            this.Title = title;
            this.Director = director;
            this.Country = country;
            this.Genre = genre;
            this.Year = year;
            this.Rating = rating;
        }
        public object Clone() => new Movie(Title, new Director(Director.FirstName, Director.LastName), Country, Genre, Year, Rating);
        public int CompareTo(Movie? movie)
        {
            if (movie is Movie)
                return Year.CompareTo(movie.Year);
            throw new ArgumentException();
        }
        public override string ToString() => $"Film:\nTitle: {Title}\nDirector: {Director.ToString()}\nCountry: {Country}\nGenre: {Genre}\nYear: {Year}\nRating: {Rating}";
    }
    class Cinema : IEnumerable
    {
        public Movie[]? Movies { get; set; }
        public string? Address { get; set; }
        public void Sort()
        {
            Array.Sort(Movies!);
        }
        public void Sort(IComparer<Movie> comparer)
        {
            Array.Sort(Movies!, comparer);
        }
        public IEnumerator GetEnumerator()
        {
            return Movies!.GetEnumerator();
        }
    }
    class YearComparer : IComparer<Movie>
    {
        public int Compare(Movie? x, Movie? y)
        {
            if (x is Movie && y is Movie)
                return x.Year.CompareTo(y.Year);
            throw new ArgumentException();
        }
    }
    internal class Program
    {
        static void Main()
        {
            Movie signal = new(
                "Signal",
                new Director("William", "Unbeck"),
                "USA",
                Genre.Horror,
                2014,
                6.2
            );
            Movie tmnt = new(
                "TMNT",
                new Director("Kevin", "Munroe"),
                "USA",
                Genre.SuperHero,
                2007,
                5
            );
            Movie transformers = new(
                "Transformers",
                new Director("Michael", "Bay"),
                "USA",
                Genre.Action,
                2007,
                9
            );
            Movie[] movies = { signal, tmnt, transformers };
            Cinema cinema = new Cinema()
            {
                Movies = movies,
                Address = "Los Angeles/"
            };
            cinema.Sort();
            foreach (Movie movie in cinema.Movies)
            {
                Console.WriteLine(movie.ToString());
                Console.WriteLine("==============================");
            }
            Console.WriteLine();

            cinema.Sort(new YearComparer());
            foreach (Movie movie in cinema.Movies)
            {
                Console.WriteLine(movie.ToString());
                Console.WriteLine("==============================");
            }
            //Director director = new Director("William", "Unbeck");
            //Console.WriteLine(director.ToString());
            //Director director1 = (Director)director.Clone();
            //director1.LastName = "Munroe";
            //Console.WriteLine(director.ToString());
            //Console.WriteLine(director1.ToString());

        }
    }
}