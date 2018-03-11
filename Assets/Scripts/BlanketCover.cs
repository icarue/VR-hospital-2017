using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlanketCover : MonoBehaviour, IPointerUpHandler,IPointerDownHandler, IDragHandler{

	bool shouldblanketGoDown = false;
	Vector2 target = new Vector2 (0, -30);


	public bool isBlanketUp { get; private set; }
	[SerializeField]
	float blanketHeightAsUp;

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.position.y < 1200) {
			GetComponent<RectTransform> ().offsetMax = new Vector2 (0, eventData.position.y);
		}
	}

	public void OnPointerDown(PointerEventData eventData) {
		shouldblanketGoDown = false;
	}

	public void OnPointerUp(PointerEventData eventData){
		shouldblanketGoDown = true;
	}

	void Update() {
		if (shouldblanketGoDown) {
			Vector2 current = GetComponent<RectTransform> ().offsetMax;
			GetComponent<RectTransform> ().offsetMax = Vector2.MoveTowards (current, target, Time.deltaTime * 4000);
			if (current == target) {
				shouldblanketGoDown = false;
			}
		}

		if (getBlanketYPoint() > blanketHeightAsUp) {
			isBlanketUp = true;
		} else {
			isBlanketUp = false;
		}
	}


	float getBlanketYPoint() {
		return GetComponent<RectTransform> ().offsetMax.y;
	}
}
