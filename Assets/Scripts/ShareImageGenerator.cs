using UnityEngine;
using System.Collections;


public class ShareImageGenerator : MonoBehaviour {

	public Transform tCamera;
	private bool takeHiResShot = false;

	int resWidth = 720;
	int resHeight = 1280;

	int imageWidth = 594;
	int imageHeight = 656;

	// Use this for initialization
	void Start () {
		Debug.Log ("datapath = " + Application.dataPath);
		Debug.Log ("persistent datapath = " + Application.persistentDataPath);

	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void TakeHiResShot() {
		takeHiResShot = true;
	}

	void LateUpdate () {
		if (takeHiResShot) {
			//string path = Application.dataPath.Substring (0, Application.dataPath.Length - 20 )+"/Documents";
			RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
			Camera camera = tCamera.GetComponent<Camera>();
			camera.targetTexture = rt;
			Texture2D screenShot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
			camera.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect((resWidth - imageWidth) / 2, (resHeight - imageHeight) / 2, imageWidth, imageHeight), 0, 0);
			camera.targetTexture = null;
			RenderTexture.active = null; // JC: added to avoid errors
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = Application.persistentDataPath + "/screenshot.png";
			System.IO.File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			takeHiResShot = false;
		}
	}

	void GenerateShareImage() {

	}
}
