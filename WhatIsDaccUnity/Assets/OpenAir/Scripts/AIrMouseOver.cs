using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIrMouseOver : MonoBehaviour
{

	public GameObject UIBox;
	public string text;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseOver()
	{
		UIBox.SetActive(true);
	}

	void OnMouseExit()
	{
		UIBox.SetActive(false);
	}
}
