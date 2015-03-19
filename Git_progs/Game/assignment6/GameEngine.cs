//GROUP : 6
//GROUP MEMBERS: Keval Shah and Julian Ken
//Description: A paper rock scissors game

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace assignment6
{
    class GameEngine : INotifyPropertyChanged
    {
        // attributes defined here       
        private int choice;
        private int wins;
        private int losses;
        private int ties;
        private string result;
        private string compUsed;


        // getters and setters for each attribute
        #region GETTERSnSETTERS
        public int Wins
        {
            get 
            { return wins; }
            set 
            { this.wins = value; NotifyPropertyChanged(); }
        }

        public int Losses
        {
            get 
            { return losses; }
            set
            { this.losses = value; NotifyPropertyChanged(); }
        }

        public int Ties
        {
            get
            { return ties; }
            set
            { this.ties = value; NotifyPropertyChanged(); }
        }

        public string CompUsed
        {
            get
            { return compUsed; }
            set
            { this.compUsed = value; NotifyPropertyChanged(); }
        }

        public int Choice
        {
            get
            { return choice; }
            set
            { this.choice = value; NotifyPropertyChanged(); }
        }
        public string Result
        {
            get
            { return result; }
            set
            { this.result = value; NotifyPropertyChanged(); }
        }
        #endregion

        // this method uses a random num generator to determine the computer choice of paper rock or scissors
        #region COMPUTERCHOICE
        public int ComputerChoice()
        {
            Random num = new Random();
            int compChoice = num.Next(1,4);

            if (compChoice == 1)
                CompUsed = "Rock";
            else if (compChoice == 2)
                CompUsed = "Paper";
            else if (compChoice == 3)
                CompUsed = "Scissors";

            return compChoice;
        }
        #endregion

        //this method compares the user choice and comp choice and determines the winner
        #region DETERMINERESULTS
        public string DetermineResult()
        {
            
            if(Choice == 1 && ComputerChoice() == 1)
                Result = "It's a TIE!!";
            
            else if (Choice == 1 && ComputerChoice() == 2)
                Result = "YOU LOSE!!!";
            
            else if (Choice == 1 && ComputerChoice() == 3)
                Result = "YOU WIN!!";
            
            else if (Choice == 2 && ComputerChoice() == 1)
                Result = "YOU WIN!!";
            
            else if (Choice == 2 && ComputerChoice() == 2)
                Result = "It's a TIE!!";
            
            else if (Choice == 2 && ComputerChoice() == 3)
                Result = "YOU LOSE!!!";
            
            else if (Choice == 3 && ComputerChoice() == 1)
                Result = "YOU LOSE!!!";
            
            else if (Choice == 3 && ComputerChoice() == 2)
                Result = "YOU WIN!!";
            
            else if (Choice == 3 && ComputerChoice() == 3)
                Result = "It's a TIE!!";
            

            return Result;
        }
#endregion

        #region TALLYSCORE
        public void TallyScore()
        {

            if (Choice == 1 && ComputerChoice() == 1)
                Ties++;

            else if (Choice == 1 && ComputerChoice() == 2)
                Losses++;

            else if (Choice == 1 && ComputerChoice() == 3)
                Wins++;

            else if (Choice == 2 && ComputerChoice() == 1)
                Wins++;

            else if (Choice == 2 && ComputerChoice() == 2)
                Ties++;

            else if (Choice == 2 && ComputerChoice() == 3)
                Losses++;

            else if (Choice == 3 && ComputerChoice() == 1)
                Losses++;

            else if (Choice == 3 && ComputerChoice() == 2)
                Wins++;

            else if (Choice == 3 && ComputerChoice() == 3)
                Ties++;
        }
        #endregion

        #region RESET
        public void Reset()
        {
            Wins = 0;
            Losses = 0;
            Ties = 0;
        }
        #endregion

        #region Notify

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
