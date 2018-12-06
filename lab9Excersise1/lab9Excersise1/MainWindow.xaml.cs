using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Threading;

namespace lab9Excersise1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
        ObservableCollection<Movie> filteredMovies = new ObservableCollection<Movie>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create Movie
            Movie movie1 = new Movie("Man on Fire", "Action", 5);

            //Add to List
            movies.Add(movie1);

            //Display on Screen
            lbxMovies.ItemsSource = movies;

            //Set Timer

            //Create Dispatcher Timer
            DispatcherTimer dt = new DispatcherTimer();

            //Link Dispatcher Timer to method
            dt.Tick += Dt_Tick;

            //Set interval
            dt.Interval = new TimeSpan(0, 0, 1);

            //Start timer
            dt.Start();

        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Take in Values
            string title = tbxMovieName.Text;
            string genre = tbxMovieGenre.Text;
            int rating = ++cbxMovieRating.SelectedIndex;
            

            //Create Movie object
            Movie addMovie = new Movie(title, genre, rating);

            //Add movie to list
            movies.Add(addMovie);

            //Deletes Everything in Text Boxes
            tbxMovieName.Text = "";
            tbxMovieGenre.Text = "";
            cbxMovieRating.Text = "";

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Check What was selected
            Movie currentlySelected = lbxMovies.SelectedItem as Movie;

            //Check if something is selected
            if(currentlySelected != null)
            {
                //Remove from List
                if (lbxMovies.ItemsSource == movies)
                    movies.Remove(currentlySelected);
                else
                    filteredMovies.Remove(currentlySelected);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Save movies list to a formatted serialized string

            string moviesString = JsonConvert.SerializeObject(movies, Formatting.Indented);

            //write that serialized string to a file
            using (StreamWriter sr = new StreamWriter(@"C:\Users\s00173652\movies.json"))
            {

                sr.Write(moviesString);

            }

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Take in Serialized string and save it to a var string
            

            using (StreamReader sr = new StreamReader(@"C:\Users\s00173652\movies.json"))
            {
                string moviesString = sr.ReadToEnd();

                //Desearialize it and save the outcome to the movies list
                movies = JsonConvert.DeserializeObject<ObservableCollection<Movie>>(moviesString);
            }

            lbxMovies.ItemsSource = movies;

        }

        private void tbxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //Clear the current filtered movies list
            filteredMovies.Clear();

            //every time a key is relesed set what is currently in the text box to search
            string search = tbxSearch.Text.ToLower();

            //run through all the movies to see what contains the search terms
            foreach(Movie m in movies)
            {
                //add those movies to the filtered list
                if (m.Title.ToLower().Contains(search))
                    filteredMovies.Add(m);
            }

            //display the filtered list
            lbxMovies.ItemsSource = filteredMovies;

        }
    }
}
