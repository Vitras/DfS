using UnityEngine;
using System.Collections;

public class Menu
{
	public bool Done { get; set; }

	public Menu ()
	{
	}

	public virtual void Update ()
	{
	}

	public virtual void Continue ()
	{
	}
	public virtual void Suspend ()
	{
	}
	public virtual void Exit ()
	{
	}
}
