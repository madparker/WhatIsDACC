using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMouseOver : MonoBehaviour
{

	public Material mouseOverMat;
	public Material regularMat;

	MeshRenderer mr;
	
	// Use this for initialization
	void Start ()
	{
		mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
//	void OnMouseOver()
//	{
//		mr.material = mouseOverMat;
//	}
//
//	void OnMouseExit()
//	{
//		mr.material = regularMat;
//	}
}
