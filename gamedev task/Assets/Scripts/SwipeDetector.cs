using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    // !!This is not my script. I just modified a bit.!!

	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;
    public Bounce bns;
    public bool IsSwipe = false;
	public bool detectSwipeAfterRelease = false;

	public float SWIPE_THRESHOLD = 20f;

	// Update is called once per frame
	void Update ()
	{

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
                StartCoroutine(timer());
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved) {
				if (!detectSwipeAfterRelease) {
					fingerDownPos = touch.position;
					DetectSwipe ();
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended && IsSwipe) {
				fingerDownPos = touch.position;
				DetectSwipe ();
			}
		}
	}

	void DetectSwipe ()
	{
		
		if (VerticalMoveValue () > SWIPE_THRESHOLD && VerticalMoveValue () > HorizontalMoveValue ()) {
			Debug.Log ("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0) {
				OnSwipeUp ();
			} else if (fingerDownPos.y - fingerUpPos.y < 0) {
				OnSwipeDown ();
			}
			fingerUpPos = fingerDownPos;

		} else if (HorizontalMoveValue () > SWIPE_THRESHOLD && HorizontalMoveValue () > VerticalMoveValue ()) {
			Debug.Log ("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0) {
				OnSwipeRight ();
			} else if (fingerDownPos.x - fingerUpPos.x < 0) {
				OnSwipeLeft ();
			}
			fingerUpPos = fingerDownPos;

		} else {
			Debug.Log ("No Swipe Detected!");
		}
	}

	float VerticalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp ()
	{
        bns.swipedUp();
	}

	void OnSwipeDown ()
	{
		//Do something when swiped down
	}

	void OnSwipeLeft ()
	{
		//Do something when swiped left
	}

	void OnSwipeRight ()
	{
		//Do something when swiped right
	}
    IEnumerator timer()
    {
        IsSwipe = true;
        yield return new WaitForSeconds(0.3f);
        IsSwipe = false;
    }
}
