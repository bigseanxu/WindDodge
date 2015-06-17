using UnityEngine;
using System.Collections;

public class TrianglePaper : MonoBehaviour {

	[SerializeField] public float minSize = 0.5f;
	[SerializeField] public float maxSize = 1.5f;
	[SerializeField] public Color color = new Color (70.0f / 255, 200.0f / 255, 247.0f / 255, 1.0f);
	[SerializeField] public float minAngle = 20;  // in degree
	[SerializeField] public float maxAngle = 160; // in degree

	private float bornTime;
	private float lifeTime = 1;
	// Use this for initialization
	void Start () {
		// Get two length of side and the angle randomly.
		float lengthA = Random.Range (minSize, maxSize);
		float lengthB = Random.Range (minSize, maxSize);
		float angle = Random.Range (minAngle, maxAngle);

		// vertices
		Vector3[] vertices = new Vector3[3];
		vertices[0] = new Vector3(0, 0, 0);
		vertices[1] = new Vector3(lengthA, 0, 0);
		vertices [2] = Quaternion.AngleAxis (angle, Vector3.forward) * new Vector3(lengthB, 0, 0);
	
		// indices
		int[] triangles = new int[3] {0, 1, 2};

		Color c = new Color (color.r, color.g, color.b, color.a);

		if (Game.status == Game.GameStatus.Welcome) {
			c.a = Random.Range(0.5f, 1);
		}

		// vertex colors
		Color[] tColor = new Color[3] { c, c, c};

		// mesh apply
		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh;
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = tColor;

		// set collide edge
		PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D> ();
		Vector2[] path = new Vector2[3];
		path [0] = new Vector2 (vertices[0].x, vertices[0].y);
		path [1] = new Vector2 (vertices[1].x, vertices[1].y);
		path [2] = new Vector2 (vertices[2].x, vertices[2].y);
		polygonCollider.SetPath (0, path);

		bornTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Time.time - bornTime > lifeTime) {
//			Destroy(gameObject);
//			Debug.Log ("hahhaha");
//		}
		if (transform.position.y < -20) {
			//Destroy(gameObject);
			transform.parent.gameObject.GetComponent<PaperEmitter>().gameObjectPool.Destroy(gameObject);
		}

	}
}
