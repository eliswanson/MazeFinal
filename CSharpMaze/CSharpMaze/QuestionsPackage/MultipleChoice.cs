using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharpMaze.QuestionsPackage
{
	[Serializable]
    class MultipleChoice : Ques_Ans, ISerializable
    {
        private string strAns1;
        private string strAns2;
        private string strAns3;
        private string strAns4;
        private GroupBox myGB;
        //private Label lblQues;
        private TextBlock txbQues;
        private RadioButton[] rdAns = new RadioButton[4];

        #region get/set
        public string Ans1
        {
            get { return this.strAns1; }
            set { this.strAns1 = value; }
        }

        public string Ans2
        {
            get { return this.strAns2; }
            set { this.strAns2 = value; }
        }

        public string Ans3
        {
            get { return this.strAns3; }
            set { this.strAns3 = value; }
        }
        public string Ans4
        {
            get { return this.strAns4; }
            set { this.strAns4 = value; }
        }
        #endregion

        #region constructor
        public MultipleChoice(GroupBox gb)
        {
            for (int i = 0; i < 4; i++)
                rdAns[i] = new RadioButton();
            this.myGB = gb;

        }
        public MultipleChoice()
        {

        }
        #endregion

        public override string Display()
        {
            Grid myGrid = myGB.Content as Grid;
            myGB.Visibility = Visibility.Visible;

            //Display question
            //lblQues = myGrid.Children[0] as Label;
            //lblQues.Content = Ques;
            txbQues = myGrid.Children[0] as TextBlock;
            txbQues.Text = Ques;
            //Display answer
            rdAns[0] = myGrid.Children[1] as RadioButton;
            rdAns[0].Content = Ans1;

            rdAns[1] = myGrid.Children[2] as RadioButton;
            rdAns[1].Content = Ans2;

            rdAns[2] = myGrid.Children[3] as RadioButton;
            rdAns[2].Content = Ans3;

            rdAns[3] = myGrid.Children[4] as RadioButton;
            rdAns[3].Content = Ans4;

            return this.Final;
        }

	    public void GetObjectData(SerializationInfo info, StreamingContext context)
	    {
		    info.AddValue("MCAnswer1", strAns1);
		    info.AddValue("MCAnswer2", strAns2);
		    info.AddValue("MCAnswer3", strAns3);
		    info.AddValue("MCAnswer4", strAns4);
	    }

	    public MultipleChoice(SerializationInfo info, StreamingContext context)
	    {
		    strAns1 = (String)info.GetValue("MCAnswer1", typeof(String));
		    strAns2 = (String)info.GetValue("MCAnswer2", typeof(String));
		    strAns3 = (String)info.GetValue("MCAnswer3", typeof(String));
		    strAns4 = (String)info.GetValue("MCAnswer4", typeof(String));
	    }
	}
}
