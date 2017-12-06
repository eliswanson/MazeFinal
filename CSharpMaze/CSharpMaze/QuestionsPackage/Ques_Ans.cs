using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CSharpMaze.QuestionsPackage
{
	[Serializable]
	abstract class Ques_Ans
    {
        protected string strQues;
        protected string strFinal;
		[NonSerialized]
	    protected GroupBox myBox;

	    public GroupBox MyBox
	    {
		    get { return myBox; }
		    set { myBox = value; }
	    }

        protected Ques_Ans(GroupBox box)
        {
            MyBox = box;
        }

        protected Ques_Ans() { }

        public string Ques
        {
            get { return this.strQues; }
            set { this.strQues = value; }
        }

        public string Final
        {
            get { return this.strFinal; }
            set { this.strFinal = value; }
        }
        public abstract string Display();
    }
}
