using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentTriggers : MonoBehaviour
{
	public GameObject[] networkTriggers;
	// Use this for initialization
	void Start ()
	{
		SortGameObjects ();
	}
	
	// Update is called once per frame
	public void Trigger (int index)
	{
		TriggerScript trigger = networkTriggers [index].GetComponent<TriggerScript> ();
		trigger.Triggered = !trigger.Triggered;
	}

	public void SortGameObjects()
	{
		int children = transform.childCount;
		for(int x = 0; x < children; x++)
		{
			string name = transform.GetChild (x).name;
			if(!name.Contains("|"))
				continue;
			string[] line = name.Split('|');
			int suffixValue = 0;
			switch (line [2]) {
			case "left":
				break;
			case "right":
				suffixValue = 1;
				break;
			case "top":
				suffixValue = 2;
				break;
			case "bottom":
				suffixValue = 3;
				break;
			default:
				break;
			}
			int triggerID = (int.Parse (line [1]) - 1) * 4 + suffixValue;
			networkTriggers[triggerID] = transform.GetChild (x).gameObject;
		}
	}
}
