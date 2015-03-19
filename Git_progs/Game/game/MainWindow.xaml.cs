//GROUP : 6
//GROUP MEMBERS: William Wiltshire, Kirsti Tench, Zahidali Maknojia, Krishna Bhandari, Aleksander Mukhin
//ASSIGNMENT# 6
//Description: A paper rock scissors game

using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // initialize components for the 2 timers used for animating the paper rock scissors man
        public int countDown = COUNT;
        const int COUNT = 3;
        const int PAUSE = 1;
        public int pause = PAUSE;
        private DispatcherTimer Timer;
        private DispatcherTimer PauseTimer;

        // call a new reference to the game engine class where all calculations are handled
        GameEngine gameCalc = new GameEngine();
        public MainWindow()
        {
            this.DataContext = gameCalc;
            InitializeComponent();
            //further elements initialized for the animated man
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0,0,0,0,200);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            PauseTimer = new DispatcherTimer();
            PauseTimer.Interval = new TimeSpan(0, 0, 1);
            PauseTimer.Tick += PauseTimer_Tick;
           
        }

        // event handler for the pause timer which just makes the man pause on his choice
        void PauseTimer_Tick(object sender, EventArgs e)
        {
            pause++;
            if (pause == 2)
            {
                Timer.Start();
                PauseTimer.Stop();
                pause = PAUSE;
            }
        }

        // timer tick event handler for main timer that will control the animation calls the AnimateMan method below
        void Timer_Tick(object sender, EventArgs e)
        {
            AnimateMan();
        }

        // button event handlers
        private void Rock_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            gameCalc.ComputerChoice();
            AnimateChoice();
            gameCalc.Choice = 1;
            gameCalc.DetermineResult();
            gameCalc.TallyScore();
            PauseTimer.Start();

            CompChoice.Content = "Old Man Chooses:\n" + gameCalc.CompUsed + "\n" + gameCalc.DetermineResult();
            ScoreBoard.Content = "Wins: " + gameCalc.Wins.ToString() + " Losses: " + gameCalc.Losses.ToString() + " Ties: " + gameCalc.Ties.ToString();
        }

        private void Paper_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            gameCalc.ComputerChoice();
            AnimateChoice();
            gameCalc.Choice = 2;
            gameCalc.DetermineResult();
            gameCalc.TallyScore();
            PauseTimer.Start();

            CompChoice.Content = "Old Man Chooses:\n" + gameCalc.CompUsed + "\n" + gameCalc.DetermineResult();
            ScoreBoard.Content = "Wins: " + gameCalc.Wins.ToString() + " Losses: " + gameCalc.Losses.ToString() + " Ties: " + gameCalc.Ties.ToString();

        }

        private void Scissors_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            gameCalc.ComputerChoice();
            AnimateChoice();
            gameCalc.Choice = 3;
            gameCalc.DetermineResult();
            gameCalc.TallyScore();
            PauseTimer.Start();

            CompChoice.Content = "Old Man Chooses:\n" + gameCalc.CompUsed + "\n" + gameCalc.DetermineResult();
            ScoreBoard.Content = "Wins: " + gameCalc.Wins.ToString() + " Losses: " + gameCalc.Losses.ToString() + " Ties: " + gameCalc.Ties.ToString();
        }

        //AnimateMan checks countDown on each tick of the event handler and displays the image associated with it
        public void AnimateMan()
        {
            
            if (countDown == 3)
            {
                Animate.Source = new BitmapImage(new Uri(@"/images/count01.png", UriKind.Relative));
                countDown--;
            }
            else if (countDown == 2)
            {
                Animate.Source = new BitmapImage(new Uri(@"/images/count02.png", UriKind.Relative));
                countDown--;
            }
            else if (countDown == 1)
            {
                Animate.Source = new BitmapImage(new Uri(@"/images/count03.png", UriKind.Relative));
                countDown--;
            }
            else if (countDown == 0)
            {
                countDown = COUNT;
                Animate.Source = new BitmapImage(new Uri(@"/images/count01.png", UriKind.Relative));
                countDown--;
            }

        }

        // AnimateChoice checks the results of the calculation and determines which image to associate with the choice
        public void AnimateChoice()
        {
            if (gameCalc.ComputerChoice() == 1)
            {
                Animate.Source = new BitmapImage(new Uri(@"/images/ROCK.png", UriKind.Relative));
            }
            else if (gameCalc.ComputerChoice() == 2)
            {
                Animate.Source = new BitmapImage(new Uri(@"/images/PAPER.png", UriKind.Relative));
            }
            else if (gameCalc.ComputerChoice() == 3)
            {
                Animate.Source = new BitmapImage(new Uri(@"/images/SCISSORS.png", UriKind.Relative));
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            gameCalc.Reset();
            ScoreBoard.Content = "Wins: 0 Losses: 0 Ties: 0";
        }        
    }
}
