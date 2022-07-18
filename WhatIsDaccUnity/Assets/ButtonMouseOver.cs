using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMouseOver : MonoBehaviour, IPointerClickHandler
{
    public ReactToMouseOver main;

	//when clicking button set the corresponding value to true in the main script
    public void OnPointerClick(PointerEventData eventData) {
		//disable all outlines and set button colors
		DeactivateOutlines();
		//SetButtonColors();

		switch (this.name) {
			case "ButtonAirIn":
				//main.overAirIn = true;
				SetAllFalse();
				main.overAirIn = main.CurrentState >= ReactToMouseOver.STATE.None;
				main.voiceOver.Stop();
                main.voiceOver.PlayOneShot(main.sounds[0]);
				break;
			case "ButtonSorbent":
				//main.overSorbent = true;
				SetAllFalse();
				main.overSorbent = main.CurrentState >= ReactToMouseOver.STATE.AirIn;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[1]);
				break;
			case "ButtonWater":
				//main.overWater = true;
				SetAllFalse();
				main.overWater = main.CurrentState >= ReactToMouseOver.STATE.Sorbent;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[2]);
				break;
			case "ButtonVaccum":
				//main.overVaccum = true;
				SetAllFalse();
				main.overVaccum = main.CurrentState >= ReactToMouseOver.STATE.Water;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[3]);
				break;
			case "ButtonAirOut":
				//main.overAirOut = true;
				SetAllFalse();
				main.overAirOut = main.CurrentState >= ReactToMouseOver.STATE.CO2;
				main.voiceOver.Stop();
				main.voiceOver.PlayOneShot(main.sounds[4]);
				break;
		}
		ActivateOutlines();
	}

	//set the value to false when leaving the button
    public void OnPointerExit(PointerEventData eventData) {
		DeactivateOutlines();
	}

	private void ActivateOutlines() {
		//activate current button outline
		switch (this.name) {
			case "ButtonAirIn":
				foreach(GameObject i in main.button1Objects) {
					i.GetComponent<Outline>().enabled = true;
                }
				break;
			case "ButtonSorbent":
				foreach (GameObject i in main.button2Objects) {
					i.GetComponent<Outline>().enabled = true;
				}
				break;
			case "ButtonWater":
				foreach (GameObject i in main.button3Objects) {
					i.GetComponent<Outline>().enabled = true;
				}
				break;
			case "ButtonVaccum":
				foreach (GameObject i in main.button4Objects) {
					i.GetComponent<Outline>().enabled = true;
				}
				break;
			case "ButtonAirOut":
				foreach (GameObject i in main.button5Objects) {
					i.GetComponent<Outline>().enabled = true;
				}
				break;
		}
    }

	private void DeactivateOutlines() {
		//deactivate all button objects' outlines
		foreach (GameObject i in main.button1Objects) {
			i.GetComponent<Outline>().enabled = false;
		}
		foreach (GameObject i in main.button2Objects) {
			i.GetComponent<Outline>().enabled = false;
		}
		foreach (GameObject i in main.button3Objects) {
			i.GetComponent<Outline>().enabled = false;
		}
		foreach (GameObject i in main.button4Objects) {
			i.GetComponent<Outline>().enabled = false;
		}
		foreach (GameObject i in main.button5Objects) {
			i.GetComponent<Outline>().enabled = false;
		}

		/*		switch (this.name) {
					case "ButtonAirIn":
						foreach (GameObject i in main.button1Objects) {
							i.GetComponent<Outline>().enabled = false;
						}
						break;
					case "ButtonSorbent":
						foreach (GameObject i in main.button2Objects) {
							i.GetComponent<Outline>().enabled = false;
						}
						break;
					case "ButtonWater":
						foreach (GameObject i in main.button3Objects) {
							i.GetComponent<Outline>().enabled = false;
						}
						break;
					case "ButtonVaccum":
						foreach (GameObject i in main.button4Objects) {
							i.GetComponent<Outline>().enabled = false;
						}
						break;
					case "ButtonAirOut":
						foreach (GameObject i in main.button5Objects) {
							i.GetComponent<Outline>().enabled = false;
						}
						break;
				}*/
	}

	private void SetButtonColors() {
		//set this button's color to active color
		GetComponent<Image>().color = main.activeButtonColor;

		//set all other button colors to deactivated color
		foreach(GameObject i in main.buttons) {
			i.GetComponent<Image>().color = main.deactivatedButtonColor;
		}
	}

	private void SetAllFalse() {
		main.overAirIn = false;
		main.overSorbent = false;
		main.overWater = false;
		main.overVaccum = false;
		main.overAirOut = false;
	}
}
