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
        private readonly GroupBox gbMC;
        private readonly GroupBox gbTF;
        private readonly GroupBox gbSA;
        private Ques_Ans currentQuestion;
        public const string EasyString = "Easy";
        public const string MediumString = "Medium";
        public const string HardString = "Difficult";
        public readonly int QuestionCount = 50; //change later. add roomcount to mazedriver, calculate based off that
        //maybe inside roomcount property check questioncount, if it's empty then initialize it?
        public string DifficultyString { get; }
        #endregion

        #region Constructors
       /* public QuestionDriver(GroupBox gbMC, GroupBox gbTF, GroupBox gbSA)
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
        }*/
        public QuestionDriver(GroupBox gbMC, GroupBox gbTF, GroupBox gbSA, string difficulty)
        {
            //Query database
            //Make MC object for each row in MC table and add to questions
            //Make TF object for each row in TF table and add to questions
            //Make Short object for each row in Short table and add to questions
            /*List<MultipleChoice> mcList = new List<MultipleChoice>();
            List<TrueFalse> tfList = new List<TrueFalse>();
            List<ShortAns> saList = new List<ShortAns>();*/

            //List<Ques_Ans>[] questionsListArray = new List<Ques_Ans>[3];

            this.gbMC = gbMC;
            this.gbTF = gbTF;
            this.gbSA = gbSA;
            DifficultyString = difficulty;            

            InitializeQuestions();

            if(QuestionsList.Count < QuestionCount)
                throw new FormatException("Size of QuestionList: " + QuestionsList.Count + ", cannot be less than QuestionCount: " + QuestionCount);
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

        private void InitializeQuestions()
         {
             List<Ques_Ans>[] questionsListArray = new List<Ques_Ans>[3];
             questionsListArray[0] = new List<Ques_Ans>(QueryFromMCTable(-1));
             questionsListArray[1] = new List<Ques_Ans>(QueryFromTFTable(-1));
             questionsListArray[2] = new List<Ques_Ans>(QueryFromSATable(-1));

            switch (DifficultyString)
             {
                 case (EasyString):                
                    Easy(questionsListArray);
                    break;

                 case (MediumString):
                    Medium(questionsListArray);
                    break;

                 case (HardString):
                    Hard(questionsListArray);
                    break;

                 default:
                     throw new ArgumentOutOfRangeException("String " + DifficultyString + " is not a valid argument");
             }
         }

        #region DB queries to create Lists
        private List<MultipleChoice> QueryFromMCTable(int delete)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            List<MultipleChoice> mcList = new List<MultipleChoice>();

            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from MutipleChoiceTable";

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
                    Final = readers["AnsFinal"].ToString(),
                    Diff = readers["Difficulty"].ToString()
                };
                mcList.Add(mc);
            }
            readers.Close();
            sqlite_conn.Close();
            return mcList;
        }
        private List<TrueFalse> QueryFromTFTable(int delete)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            List<TrueFalse> tfList = new List<TrueFalse>();

            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from TrueFalseTable";

            SQLiteDataReader readers = sqlite_cmd.ExecuteReader();

            while (readers.Read())
            {
                TrueFalse tf = new TrueFalse(gbTF)
                {
                    Ques = readers["Question"].ToString(),
                    Ans1 = readers["Ans1"].ToString(),
                    Ans2 = readers["Ans2"].ToString(),
                    Final = readers["AnsFinal"].ToString(),
                    Diff = readers["Difficulty"].ToString()
                };
                tfList.Add(tf);
            }
            readers.Close();
            sqlite_conn.Close();
            return tfList;
        }
        private List<ShortAns> QueryFromSATable(int delete)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            List<ShortAns> saList = new List<ShortAns>();

            sqlite_conn = new SQLiteConnection("Data Source=Ques_AnsDB.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();

            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "Select * from ShortAnsTable";

            SQLiteDataReader readers = sqlite_cmd.ExecuteReader();

            while (readers.Read())
            {
                ShortAns sa = new ShortAns(gbSA)
                {
                    Ques = readers["Question"].ToString(),
                    Final = readers["AnsFinal"].ToString(),
                    Diff = readers["Difficulty"].ToString()
                };
                saList.Add(sa);
            }
            readers.Close();
            sqlite_conn.Close();
            return saList;
        }
        #endregion

        #region Easy, Medium, Hard, and helper method              
        private void Easy(List<Ques_Ans>[] questionsListArray)
        {
            double amount = Math.Ceiling(QuestionCount * .5 * .3) + 1;

            foreach (var questionsList in questionsListArray)
            {
                AddToQuestionsList(questionsList, (int)amount, EasyString);
                AddToQuestionsList(questionsList, (int)amount, MediumString);
            }
        }
        private void Medium(List<Ques_Ans>[] questionsListArray)
        {
            double easyAmount = Math.Ceiling(QuestionCount * .25 * .3) + 1;
            double mediumAmount = Math.Ceiling(QuestionCount * .5 * .3) + 1;
            double hardAmount = Math.Ceiling(QuestionCount * .25 * .3) + 1;

            foreach (var questionsList in questionsListArray)
            {
                AddToQuestionsList(questionsList, (int)easyAmount, EasyString);
                AddToQuestionsList(questionsList, (int)mediumAmount, MediumString);
                AddToQuestionsList(questionsList, (int)hardAmount, HardString);
            }
        }
        private void Hard(List<Ques_Ans>[] questionsListArray)
        {
            double amount = Math.Ceiling(QuestionCount * .5 * .3) + 1;

            foreach (var questionsList in questionsListArray)
            {
                AddToQuestionsList(questionsList, (int)amount, MediumString);
                AddToQuestionsList(questionsList, (int)amount, HardString);
            }
        }
        private void AddToQuestionsList(List<Ques_Ans> questionList, int amount, string difficulty)
        {
            Random random = new Random();
            int indexRandom;

            questionList = new List<Ques_Ans>(questionList.Where(q => q.Diff == difficulty));           

            if (amount > questionList.Count)
                throw new IndexOutOfRangeException("The amount " + amount + " is bigger than the size of the list, " + questionList.Count);

            indexRandom = random.Next(questionList.Count);
            for (int i = 0; i < amount; i++)
            {
                QuestionsList.Add(questionList[indexRandom]);
                questionList.RemoveAt(indexRandom);
                indexRandom = random.Next(questionList.Count);
            }
        }
        #endregion

       #region (Outdated) DB queries to create QuestionsList
  /*      private void QueryFromMCTable()
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
        }*/
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
