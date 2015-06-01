using UnityEngine;
using System.Collections;

// It is like a particle emmitter.
public class PaperEmitter : MonoBehaviour {

	public int paperPerSec = 5;
	public float lifeTime = 10;// in sec. see in triangle paper
	public float offsetXNeg = 10;
	public float offsetXPos = 10;
	public int maxPaper = 200;

	public float minForceX = 0;
	public float maxForceX = 0;
	public float minForceY = 500;
	public float maxForceY = 1000;

	public float minMass = 1;
	public float maxMass = 1;

	private float pastTime = 0;
	private float pastTimeScore = 0;

	private Game.GameStatus status;

	public float timeToBeMoreDifficult = 10;
	public int morePaperPerSec = 1;
	public float forceMovement = 100;

	public float scoreInterval = 0.2f;
	// Use this for initialization
	void Start () {
		status = Game.status;
	}
	
	// Update is called once per frame
	void Update () {
		bool statusChanged = false;
		if (Game.status != status) {
			statusChanged = true;
			status = Game.status;
		}

		if (statusChanged) {
			if (status == Game.GameStatus.Enjoy) {
				Invoke ("invodeDifficultAddition", timeToBeMoreDifficult);
//				GameObject obj = GameObject.Find("Group1");
//				obj.SetActive(false);
			}
		}

		if (Game.status == Game.GameStatus.Enjoy || Game.status == Game.GameStatus.Welcome || Game.status == Game.GameStatus.Farewell) {
			pastTime += Time.deltaTime;
			GameObject[] objects = GameObject.FindGameObjectsWithTag("paper");
			
			if (objects.Length < maxPaper) {
				if (pastTime > 1.0f / paperPerSec) {
					pastTime -= 1.0f / paperPerSec;
					
					Vector3 vOffset = new Vector3(Random.Range(transform.position.x - offsetXNeg, transform.position.x + offsetXPos), transform.position.y, transform.position.z);
					
					GameObject paper = (GameObject) Instantiate ((GameObject) Resources.Load ("TrianglePaper"), vOffset, new Quaternion());
					paper.transform.SetParent(transform);

					Rigidbody2D rigidbody = paper.GetComponent<Rigidbody2D> ();
					rigidbody.mass = Random.Range(minMass, maxMass);
					rigidbody.AddForce(new Vector2(0, Random.Range(-minForceY, -maxForceY)));
				}
			}
		}

		if (Game.status == Game.GameStatus.Enjoy) {
			pastTimeScore += Time.deltaTime;

			if (pastTimeScore > scoreInterval) {
				pastTimeScore = 0;

				Vector3 vOffset = new Vector3(Random.Range(transform.position.x - offsetXNeg, transform.position.x + offsetXPos), transform.position.y, transform.position.z);

				GameObject paper = (GameObject) Instantiate ((GameObject) Resources.Load ("Item Score"), vOffset, new Quaternion());
				paper.transform.SetParent(transform);
				
				Rigidbody2D rigidbody = paper.GetComponent<Rigidbody2D> ();
				rigidbody.AddForce(new Vector2(0, Random.Range(-minForceY, -maxForceY)));
			}
		}
	}

	void invodeDifficultAddition() {
		paperPerSec += morePaperPerSec;
		minForceY += forceMovement;
		maxForceY += forceMovement;
		Invoke ("invodeDifficultAddition", timeToBeMoreDifficult);
	}
}
