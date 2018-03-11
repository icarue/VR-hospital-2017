using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectUIClick : MonoBehaviour, IPointerDownHandler {
	public void OnPointerDown(PointerEventData eventData) {
		gameObject.SetActive (false);
	}
}
