using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Controls;
using CSharpMaze.QuestionsPackage;

namespace CSharpMaze
{
    class QuestionDriver
    {
	    //Changes Made
		public List<Ques_Ans> questions = new List<Ques_Ans>();
        private GroupBox gbMC;
        private GroupBox gbTF;
        private GroupBox gbSA;
        private Ques_Ans currentQuestion;

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
            questions = ques;
            foreach (Ques_Ans q in questions)
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

        public void Display()
        {
            System.Random myRandom = new System.Random();
            int size = questions.Count;
            if (size > 0)
            {
                int indexRandom = myRandom.Next(size);
                //Check if the type of Question is MC, TrueFalse, or ShortAnswer
                if (questions[indexRandom].GetType() == typeof(MultipleChoice))
                {
                    gbMC.Visibility = System.Windows.Visibility.Visible;
                    gbSA.Visibility = System.Windows.Visibility.Hidden;
                    gbTF.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (questions[indexRandom].GetType() == typeof(TrueFalse))
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

                currentQuestion = questions[indexRandom];
                questions[indexRandom].Display();
                //After displaying Question, removing it from the list questions
                questions.RemoveAt(indexRandom); //store question              
            }
        }

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
				questions.Add(mc);
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
				questions.Add(tf);
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
				questions.Add(sa);
            }
            readers.Close();
            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
        }
        public bool IsCorrect(string pAnswer)
        {
            return pAnswer.ToLower() == currentQuestion.Final.ToLower();
        }
    }   
}
