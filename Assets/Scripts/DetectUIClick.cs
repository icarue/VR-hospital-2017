using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectUIClick : MonoBehaviour, IPointerDownHandler {
	public void OnPointerDown(PointerEventData eventData) {
		//AUDIO
		AudioController.instance.PLAY (AudioController.instance.AUDIO.ButtonClicks,TYPE.UI, 0.5f);
		gameObject.SetActive (false);
	}
}
