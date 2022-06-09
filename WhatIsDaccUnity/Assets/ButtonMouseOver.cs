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
				main.voiceOver.Stop();
                main.voiceOver.PlayOneShot(main.sounds[0]);
				break;
			case "ButtonSorbent":
				//main.overSorbent = true;
				main.overSorbent = main.CurrentState >= ReactToMouseOver.STATE.AirIn;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[1]);
				break;
			case "ButtonWater":
				//main.overWater = true;
				main.overWater = main.CurrentState >= ReactToMouseOver.STATE.Sorbent;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[2]);
				break;
			case "ButtonVaccum":
				//main.overVaccum = true;
				main.overVaccum = main.CurrentState >= ReactToMouseOver.STATE.Water;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[3]);
				break;
			case "ButtonAirOut":
				//main.overAirOut = true;
				main.overAirOut = main.CurrentState >= ReactToMouseOver.STATE.CO2;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[4]);
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
