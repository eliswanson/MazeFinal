namespace CSharpMaze
{
	struct RoomState
	{
		public int Door1 { get; set; }
		public int Door2 { get; set; }
		public int Door3 { get; set; }
		public int Door4 { get; set; }

	    public const int Closed = 0;
	    public const int Open = 1;
	    public const int Locked = 2;
	    public const int Hidden = 3;
	}
}
