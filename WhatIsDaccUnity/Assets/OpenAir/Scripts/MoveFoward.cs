using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{

	public float reset = 2;
	float speed;
	public Vector3 offset = Vector3.right;

	public bool smooth = false;

	Vector3 orgPos;
	float orgReset;

	public PanelCO2Manager panelCo2Manager;

	public bool pause;
	
	// Use this for initialization
	void Start ()
	{
		speed = reset;
		reset = 0;
		
		transform.localPosition = transform.localPosition - offset/2;
		
		orgPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		reset += Time.deltaTime/speed;

		Vector3 pos = transform.localPosition;
		
		if (reset >= 1 && !pause)
		{
			pos = orgPos;
			reset = 0;

//			GameObject[] CO2s = GameObject.FindGameObjectsWithTag("CO2");
			GameObject Ins = GameObject.Find("Ins");

//			foreach (var CO2 in CO2s)
//			{
			Ins.BroadcastMessage("Reset");
//			}

			panelCo2Manager.ActivateCo2();
		}
		else
		{
			Vector3 dest = orgPos + offset;

			if (!smooth)
			{
				pos = Vector3.Lerp(orgPos, orgPos + offset, reset);
			}
			else
			{
				pos = new Vector3(
					Mathf.SmoothStep(orgPos.x, dest.x, reset),
					Mathf.SmoothStep(orgPos.y, dest.y, reset),
					Mathf.SmoothStep(orgPos.z, dest.z, reset));
			}
		}
		
		transform.localPosition = pos;
	}
}
