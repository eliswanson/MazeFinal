using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Controls;
using CSharpMaze.QuestionsPackage;

namespace CSharpMaze
{
    class QuestionDriver
    {
        #region Properties and fields              
        public List<Ques_Ans> QuestionsList { get; set; } // add if statement? if list not null, don't set?
        private GroupBox gbMC;
        private GroupBox gbTF;
        private GroupBox gbSA;
        private Ques_Ans currentQuestion;
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
            QueryFromMCTable();
            QueryFromTFTable();
            QueryFromSATable();
            //Display();
        }
        public QuestionDriver(GroupBox gbMC, GroupBox gbTF, GroupBox gbSA, List<Ques_Ans> ques)
        {
            //Query database
            //Make MC object for each row in MC table and add to questions
            //Make TF object for each row in TF table and add to questions
            //Make Short object for each row in Short table and add to questions
            this.gbMC = gbMC;
            this.gbTF = gbTF;
            this.gbSA = gbSA;
            QuestionsList = ques;
            foreach (Ques_Ans q in QuestionsList)
            {
                if (q is MultipleChoice)
                    q.MyBox = gbMC;
                if (q is TrueFalse)
                    q.MyBox = gbTF;
                if (q is ShortAns)
                    q.MyBox = gbSA;
            }
            //Display();
        }
        #endregion
        #region Queries and Ques_Ans creation
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
