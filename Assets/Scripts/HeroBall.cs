using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System;

public class HeroBall : MonoBehaviour {

	enum FollowMode {
		Follow, 
		Trace
	};

	enum HeroBallMode {
		Normal,
		Shield
	}

	public float traceTime = 0.1f;
	public Transform heroParticle;
	float mTraceTime;
	HeroBallMode ballMode = HeroBallMode.Normal;
	bool isSpeedUp = true;
	FollowMode mode = FollowMode.Follow;

	public event EventHandler GameOver;
	public event EventHandler GetScore;

	public Transform scoreParticle;

	// Use this for initialization
	void Start () {
		mTraceTime = traceTime;
	}

	void Awake() {
	}

	void OnEnable() {
		transform.position = new Vector3 (0, -4f, 0);
		heroParticle.gameObject.SetActive (false);
	}

	void OnDisable() {
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.status != Game.GameStatus.Enjoy) {
			return;
		}

//		if (Input.touchCount > 0) {
//			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//			worldPos.z = 0;
//			transform.position = worldPos;
//		}

		if (Input.GetMouseButton(0)) {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldPos.z = 0;
			worldPos.y += 1f;

//			if (Input.GetMouseButtonDown(0)) {
//				mode = FollowMode.Trace;
//				mTraceTime = traceTime;
//			}

//			if (mode == FollowMode.Trace) {
				HOTween.To(this.transform, mTraceTime, new TweenParms()
				           .Prop("position", worldPos)
				           .Ease(EaseType.EaseOutCubic)
				          // .OnComplete(resetFollowMode)
				           ); 
//				HOTween.To(this, 0.5f, new TweenParms()
//				           .Prop("mTraceTime", 0, false)
//				           .Ease(EaseType.Linear)
//				           ); 
//			} else if (mode == FollowMode.Follow){
//				transform.position = worldPos;
//			}

//			transform.position = worldPos;
		}


	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "paper") {
			if (collider.gameObject.name == "Item Score(Clone)") {
				if (GetScore != null) {
					GetScore(gameObject, EventArgs.Empty);
//					scoreParticle.gameObject.SetActive(true);
//					scoreParticle.position = collider.transform.position;

					Destroy(collider.gameObject);
				}
			} else {
				if (Game.status != Game.GameStatus.Enjoy) {
					return;
				}

				if (ballMode == HeroBallMode.Shield) {
					transform.FindChild("ShieldHeroBall").GetComponent<Animator>().Play("Shield no");
					ballMode = HeroBallMode.Normal;
				} else {
					heroParticle.gameObject.SetActive(true);
					heroParticle.position = transform.position;
					gameObject.SetActive(false);
					Invoke("gameLose", 2);
					if (GameOver != null) {
						GameOver(this.gameObject, EventArgs.Empty);
					}
				}
			}

		} else if (collider.gameObject.name == "Item Shield(Clone)") {
			ballMode = HeroBallMode.Shield;
			transform.FindChild("ShieldHeroBall").gameObject.SetActive(true);
		}

	}

	void gameLose() {
		Game.setGameStatus (Game.GameStatus.Farewell);
	}

	void resetFollowMode() {
//		mode = FollowMode.Follow;
	}
}
