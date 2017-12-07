using System;

namespace CSharpMaze
{
	[Serializable]
	struct RoomState
	{
	    public const int Closed = 0;
	    public const int Open = 1;
	    public const int Locked = 2;
	    public const int Hidden = 3;
	    public const string Door1String = "Door1";
	    public const string Door2String = "Door2";
	    public const string Door3String = "Door3";
	    public const string Door4String = "Door4";
        public int Door1State { get; set; }
		public int Door2State { get; set; }
		public int Door3State { get; set; }
		public int Door4State { get; set; }	
    }
}
