using UnityEngine;
using System.Collections;

public class PipeField {
	
	public Pipe[,] field;
	public Vector2 startPos;

	public PipeField(int width, int height, int solution)
	{
		field = new Pipe[width, height];
	}

	public bool CheckSolution()
	{
		int x = (int)startPos.x;
		int y = (int)startPos.y;
		Pipe current = field[x, y];
		Pipe next;
		while (current.End != Pipe.Direction.end)
		{
			switch(current.End)
			{
			case Pipe.Direction.down: y--; break;
			case Pipe.Direction.up: y++; break;
			case Pipe.Direction.left: x--; break;
			case Pipe.Direction.right: x++; break;
			}
			next = field[x,y];
			if(next.Start != current.End)
				return false;
			current = next;
		}
		return true;
	}

	public void AttachPipe(Pipe pipe, int x, int y)
	{
		field[x, y] = pipe;
	}
}
