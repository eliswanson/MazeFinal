using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Controls;
using CSharpMaze.QuestionsPackage;

namespace CSharpMaze
{
    class QuestionDriver
    {
        #region Properties and fields              
        public List<Ques_Ans> QuestionsList { get; set; } = new List<Ques_Ans>(); // add if statement? if list not null, don't set?
        private GroupBox gbMC;
        private GroupBox gbTF;
        private GroupBox gbSA;
        private Ques_Ans currentQuestion;
        public const string EasyString = "Easy";
        public const string MediumString = "Medium";
        public const string HardString = "Difficult";
        public readonly int QuestionCount = 50; //change later. add roomcount to mazedriver, calculate based off that
        //maybe inside roomcount property check questioncount, if it's empty then initialize it?
        public string DifficultyString { get; }
        #endregion

        #region Constructors
        public QuestionDriver(GroupBox gbMC, GroupBox gbTF, GroupBox gbSA)
        {
            //Query database
            //Make MC object for each row in MC table and add to questions
            //Make TF object for each row in TF table and add to questions
            //Make Short object for each row in Short table and add to questions
            this.gbMC = gbMC;
            this.gbTF = gbTF;
            this.gbSA = gbSA;
            DifficultyString = MediumString;
            QueryFromMCTable();
            QueryFromTFTable();
            QueryFromSATable();
        }
        public QuestionDriver(GroupBox gbMC, GroupBox gbTF, GroupBox gbSA, string difficulty)
        {
            //Query database
            //Make MC object for each row in MC table and add to questions
            //Make TF object for each row in TF table and add to questions
            //Make Short object for each row in Short table and add to questions
            this.gbMC = gbMC;
            this.gbTF = gbTF;
            this.gbSA = gbSA;
            DifficultyString = difficulty;
            //InitializeQuestions();
            QueryFromMCTable(DifficultyString, 20);
            QueryFromTFTable(DifficultyString, 20);
            QueryFromSATable(DifficultyString, 20);
        }
        //!!!! Loading with random list
        public QuestionDriver(GroupBox gbMC, GroupBox gbTF, GroupBox gbSA, List<Ques_Ans> ques, string difficulty)
        {
            //Query database
            //Make MC object for each row in MC table and add to questions
            //Make TF object for each row in TF table and add to questions
            //Make Short object for each row in Short table and add to questions
            this.gbMC = gbMC;
            this.gbTF = gbTF;
            this.gbSA = gbSA;
            QuestionsList = ques;
            DifficultyString = difficulty;
            foreach (Ques_Ans q in QuestionsList)
            {
                if (q is MultipleChoice)
                    q.MyBox = gbMC;
                if (q is TrueFalse)
                    q.MyBox = gbTF;
                if (q is ShortAns)
                    q.MyBox = gbSA;
            }
        }
        #endregion

       /* private void InitializeQuestions()
        {
            switch (DifficultyString)
            {
                case (EasyString):                
                    
                    break;

                case (MediumString):

                    break;

                case (HardString):

                    break;

                default:
                    throw new ArgumentOutOfRangeException("String " + DifficultyString + " is not a valid argument");
            }
        }

        private void Easy()
        {
            QueryFromMCTable();
        }*/

        #region DB queries to create a random QuestionsList of size QuestionCount
        private void QueryFromMCTable(string difficulty, int mcCount)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader readers;
            Random random;
            List<MultipleChoice> mcList;

            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();
            
            sqlite_cmd.CommandText = "Select * from MutipleChoiceTable WHERE Difficulty = '" + DifficultyString + "'";

            readers = sqlite_cmd.ExecuteReader();

            mcList = new List<MultipleChoice>();
            while (readers.Read())
            {
                MultipleChoice mc = new MultipleChoice(gbMC)
                {
                    Ques = readers["Question"].ToString(),
                    Ans1 = readers["Ans1"].ToString(),
                    Ans2 = readers["Ans2"].ToString(),
                    Ans3 = readers["Ans3"].ToString(),
                    Ans4 = readers["Ans4"].ToString(),
                    Final = readers["AnsFinal"].ToString()
                };
                mcList.Add(mc);
            }
            readers.Close();
            sqlite_conn.Close();

            random = new Random();
            mcList = mcList.OrderBy(user => random.Next()).Take(mcCount).ToList();
            QuestionsList.AddRange(mcList);
        }
        private void QueryFromTFTable(string difficulty, int tfCount)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader readers;
            Random random;
            List<TrueFalse> tfList;

            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from TrueFalseTable WHERE Difficulty = '" + DifficultyString + "'";

            readers = sqlite_cmd.ExecuteReader();

            tfList = new List<TrueFalse>();
            while (readers.Read())
            {
                TrueFalse tf = new TrueFalse(gbTF)
                {
                    Ques = readers["Question"].ToString(),
                    Ans1 = readers["Ans1"].ToString(),
                    Ans2 = readers["Ans2"].ToString(),
                    Final = readers["AnsFinal"].ToString()
                };
                tfList.Add(tf);
            }
            readers.Close();
            sqlite_conn.Close();

            random = new Random();
            tfList = tfList.OrderBy(user => random.Next()).Take(tfCount).ToList();
            QuestionsList.AddRange(tfList);
        }
        private void QueryFromSATable(string difficulty, int saCount)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader readers;
            Random random;
            List<ShortAns> saList;

            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from ShortAnsTable WHERE Difficulty = '" + DifficultyString + "'";

            readers = sqlite_cmd.ExecuteReader();

            saList = new List<ShortAns>();
            while (readers.Read())
            {
                ShortAns sa = new ShortAns(gbSA)
                {
                    Ques = readers["Question"].ToString(),
                    Final = readers["AnsFinal"].ToString()
                };
                saList.Add(sa);
            }
            readers.Close();
            sqlite_conn.Close();

            random = new Random();
            saList = saList.OrderBy(user => random.Next()).Take(saCount).ToList();
            QuestionsList.AddRange(saList);
        }
        #endregion

        #region (Outdated) DB queries to create QuestionsList
        private void QueryFromMCTable()
        {
            //List<MultipleChoice> myMCList = new List<MultipleChoice>();
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            //SQLiteDataReader sqlite_datareader;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");

            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            //sqlite_cmd.CommandText = "Select * from MutipleChoiceTable WHERE Difficulty = '" + DifficultyString + "'";
            sqlite_cmd.CommandText = "Select * from MutipleChoiceTable";
            // Let the SQLiteCommand object know our SQL-Query:
            SQLiteDataReader readers = sqlite_cmd.ExecuteReader();

            while (readers.Read())
            {
				MultipleChoice mc = new MultipleChoice(gbMC)
				{
					Ques = readers["Question"].ToString(),
					Ans1 = readers["Ans1"].ToString(),
					Ans2 = readers["Ans2"].ToString(),
					Ans3 = readers["Ans3"].ToString(),
					Ans4 = readers["Ans4"].ToString(),
					Final = readers["AnsFinal"].ToString()
				};
				QuestionsList.Add(mc);
            }
            readers.Close();
            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
            //return myMCList;
        }
        private void QueryFromTFTable()
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            //SQLiteDataReader sqlite_datareader;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");

            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from TrueFalseTable";

            // Let the SQLiteCommand object know our SQL-Query:
            SQLiteDataReader readers = sqlite_cmd.ExecuteReader();

            while (readers.Read())
            {
				TrueFalse tf = new TrueFalse(gbTF)
				{
					Ques = readers["Question"].ToString(),
					Ans1 = readers["Ans1"].ToString(),
					Ans2 = readers["Ans2"].ToString(),
					Final = readers["AnsFinal"].ToString()
				};
				QuestionsList.Add(tf);
            }
            readers.Close();
            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
        }
        private void QueryFromSATable()
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            //SQLiteDataReader sqlite_datareader;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");

            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from ShortAnsTable";

            // Let the SQLiteCommand object know our SQL-Query:
            SQLiteDataReader readers = sqlite_cmd.ExecuteReader();

            while (readers.Read())
            {
				ShortAns sa = new ShortAns(gbSA)
				{
					Ques = readers["Question"].ToString(),
					Final = readers["AnsFinal"].ToString()
				};
				QuestionsList.Add(sa);
            }
            readers.Close();
            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
        }
        #endregion

        public void Display()
        {
            System.Random myRandom = new System.Random();
            int size = QuestionsList.Count;
            if (size > 0)
            {
                int indexRandom = myRandom.Next(size);
                //Check if the type of Question is MC, TrueFalse, or ShortAnswer
                if (QuestionsList[indexRandom].GetType() == typeof(MultipleChoice))
                {
                    gbMC.Visibility = System.Windows.Visibility.Visible;
                    gbSA.Visibility = System.Windows.Visibility.Hidden;
                    gbTF.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (QuestionsList[indexRandom].GetType() == typeof(TrueFalse))
                {
                    gbMC.Visibility = System.Windows.Visibility.Hidden;
                    gbSA.Visibility = System.Windows.Visibility.Hidden;
                    gbTF.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    gbMC.Visibility = System.Windows.Visibility.Hidden;
                    gbSA.Visibility = System.Windows.Visibility.Visible;
                    gbTF.Visibility = System.Windows.Visibility.Hidden;
                }

                currentQuestion = QuestionsList[indexRandom];
                QuestionsList[indexRandom].Display();
                //After displaying Question, removing it from the list questions
                QuestionsList.RemoveAt(indexRandom); //store question              
            }
        }
        public bool IsCorrect(string pAnswer)
        {
            return pAnswer.ToLower() == currentQuestion.Final.ToLower();
        }
    }   
}
