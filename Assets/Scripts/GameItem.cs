using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GameItem : MonoBehaviour {
	public Transform terminal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -20) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Hero Ball") {
//			HOTween.To(this.transform, 1, new TweenParms()
//			           .Prop("position", terminal.position, false)
//			           .Ease(EaseType.EaseOutCubic)
//			           ); 

//			HOTween.To (this.transform, 1, "position", new Vector3(2.53f, 7.77f, 0));
		}
	}
}
