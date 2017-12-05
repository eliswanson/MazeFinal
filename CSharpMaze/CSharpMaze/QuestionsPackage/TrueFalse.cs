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
    class TrueFalse : Ques_Ans, ISerializable
    {
        private string strAns1;
        private string strAns2;       

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

        #endregion

        #region constructor

        public TrueFalse(GroupBox gb) : base (gb) { }
        #endregion

        public override string Display()
        {
            Grid myGrid = MyBox.Content as Grid;
            RadioButton[]  rdAns = new RadioButton[2];
            for (int i = 0; i < 2; i++)
                rdAns[i] = new RadioButton();
            //Display question
            TextBlock txbQues = new TextBlock();
            txbQues = myGrid.Children[0] as TextBlock;
            txbQues.Text = Ques;

            //Display answer
            rdAns[0] = myGrid.Children[1] as RadioButton;
            rdAns[0].Content = Ans1;

            rdAns[1] = myGrid.Children[2] as RadioButton;
            rdAns[1].Content = Ans2;

            myGrid.Visibility = Visibility.Visible;

            return this.Final;
        }

	    public void GetObjectData(SerializationInfo info, StreamingContext context)
	    {
		    info.AddValue("TFAnswer1", strAns1);
		    info.AddValue("TFAnswer2", strAns2);
	    }

	    public TrueFalse(SerializationInfo info, StreamingContext context)
	    {
		    strAns1 = (String)info.GetValue("TFAnswer1", typeof(String));
		    strAns2 = (String)info.GetValue("TFAnswer2", typeof(String));
	    }
	}
}
