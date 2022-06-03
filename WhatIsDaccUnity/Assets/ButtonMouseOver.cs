using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ReactToMouseOver main;

	//when mousing over the button set the corresponding value to true in the main script
    public void OnPointerEnter(PointerEventData eventData) {

		switch (this.name) {
			case "ButtonAirIn":
				//main.overAirIn = true;
				main.overAirIn = main.CurrentState >= ReactToMouseOver.STATE.None;
				break;
			case "ButtonSorbent":
				//main.overSorbent = true;
				main.overSorbent = main.CurrentState >= ReactToMouseOver.STATE.AirIn;
				break;
			case "ButtonWater":
				//main.overWater = true;
				main.overWater = main.CurrentState >= ReactToMouseOver.STATE.Sorbent;
				break;
			case "ButtonVaccum":
				//main.overVaccum = true;
				main.overVaccum = main.CurrentState >= ReactToMouseOver.STATE.Water;
				break;
			case "ButtonAirOut":
				//main.overAirOut = true;
				main.overAirOut = main.CurrentState >= ReactToMouseOver.STATE.CO2;
				break;
		}
	}

	//set the value to false when leaving the button
    public void OnPointerExit(PointerEventData eventData) {
		switch (this.name) {
			case "ButtonAirIn":
				main.overAirIn = false;
				break;
			case "ButtonSorbent":
				main.overSorbent = false;
				break;
			case "ButtonWater":
				main.overWater = false;
				break;
			case "ButtonVaccum":
				main.overVaccum = false;
				break;
			case "ButtonAirOut":
				main.overAirOut = false;
				break;
		}
	}
}
