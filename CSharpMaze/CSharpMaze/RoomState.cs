using System;

namespace CSharpMaze
{
	[Serializable]
	class RoomState
	{
	    public const int Closed = 0;
	    public const int Open = 1;
	    public const int Locked = 2;
	    public const int Hidden = 3;
        public int Door1State { get; set; }
		public int Door2State { get; set; }
		public int Door3State { get; set; }
		public int Door4State { get; set; }	
    }
}
