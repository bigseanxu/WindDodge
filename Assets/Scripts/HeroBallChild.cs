using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class HeroBallChild : MonoBehaviour {

	public float distance = 0.6f;

	private Vector3 lastPosition = new Vector3();
	private Vector3 translation;

	public string namea;

	public Vector3 follower;
	private Vector3 direction;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.parent != null) {
			HOTween.To(transform, 0.2f, new TweenParms()
			           .Prop("position", transform.parent.GetComponent<HeroBallChild>().follower)
			           //.Prop("position", t)
			           .Ease(EaseType.EaseOutCubic)
			           );

		//			transform.Translate(transform.parent.GetComponent<HeroBallChild>().getTranslation());
		}
	}


	void LateUpdate() {
		translation = transform.position - lastPosition;

		lastPosition.x = transform.position.x;
		lastPosition.y = transform.position.y;


		//follower = transform.position - translation.normalized * 0.5f;
		if (translation.x * translation.x + translation.y * translation.y > 0.01f) {
			direction = translation;
		}

		follower = transform.position - direction.normalized * 0.5f;
	}

	public Vector3 getLastPosition() {
		return lastPosition;
	}

	public Vector3 getTranslation() {
		return translation;
	}

}
