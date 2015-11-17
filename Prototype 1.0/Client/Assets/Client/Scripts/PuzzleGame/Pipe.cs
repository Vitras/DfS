using UnityEngine;
using System.Collections;

public class Pipe {
	public enum Direction{start, up, down, left, right, end}
	public Direction Start {get; set;}
	public Direction End {get; set;}
		
	public Pipe(Direction start, Direction end)
	{
		Start = start;
		End = end;
	}
}
