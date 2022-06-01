using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToColor : MonoBehaviour {
	Color ogColors;
	public Color finalColor;

	MeshRenderer mr;

	public float delay = 1;
	public float fadeSpeed = 1;

	float delayTime = 0;
	float currentFade = 0;

	// Use this for initialization
	void Start()
	{
		mr = GetComponent<MeshRenderer>();

		ogColors	= mr.material.color;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (delayTime < delay)
		{
			delayTime += Time.deltaTime;
		} else {
			currentFade += Time.deltaTime/fadeSpeed;
	
			mr.material.color = Color.Lerp(ogColors, finalColor, currentFade);
		}
	}

	public void Reset()
	{
		currentFade = 0;
		delayTime = 0;
		mr.material.color = ogColors;
	}
}
