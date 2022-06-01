using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToTransparent : MonoBehaviour
{
	public bool fadeIn = false;
	
	Color[] ogColors;
	Color[] finalColor;

	MeshRenderer[] mrs;

	public float delay = 1;
	public float fadeSpeed = 1;

	float delayTime = 0;
	float currentFade = 0;

	// Use this for initialization
	void Start()
	{
		mrs = GetComponentsInChildren<MeshRenderer>();
		ogColors = new Color[mrs.Length];
		finalColor = new Color[mrs.Length];

		for (int i = 0; i < mrs.Length; i++)
		{
			MeshRenderer mr = mrs[i];
			ogColors[i] = mr.material.GetColor("_Diffuse");
			finalColor[i] = mr.material.GetColor("_Diffuse");
			finalColor[i].a = 0;
		}

		if (fadeIn)
		{
			Color[] temp = ogColors;
			ogColors = finalColor;
			finalColor = temp;
			
			Reset();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (delayTime < delay)
		{
			delayTime += Time.deltaTime;
		} else {
			currentFade += Time.deltaTime/fadeSpeed;
			for (int i = 0; i < mrs.Length; i++)
			{
				MeshRenderer mr = mrs[i];
				mr.material.SetColor("_Diffuse", Color.Lerp(ogColors[i], finalColor[i], currentFade));
			}
		}
	}

	public void Reset()
	{
		fadeIn = false;
		currentFade = 0;
		delayTime = 0;
			
		for (int i = 0; i < mrs.Length; i++)
		{
			MeshRenderer mr = mrs[i];
			mr.material.SetColor("_Diffuse", ogColors[i]);
		}
	}
}
