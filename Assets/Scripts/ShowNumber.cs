using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowNumber : MonoBehaviour {

	public Sprite [] num = new Sprite[10];

	Vector3 tempV = new Vector3();
	// Use this for initialization
	void Start () {
	}

	void OnEnable() {
		int current = PlayerPrefs.GetInt ("current");
		int best = PlayerPrefs.GetInt ("best");

		if (current > 100) {
			current = 99;
		}

		int num1 = current / 10;
		int num2 = current % 10;

		GameObject number1Obj = transform.FindChild ("number1").gameObject;
		GameObject number2Obj = transform.FindChild ("number2").gameObject;
		GameObject number3Obj = transform.FindChild ("number3").gameObject;

		GameObject crownObj = transform.FindChild ("Crown").gameObject;

		if (num1 != 0) {
			number1Obj.SetActive(true);
			number2Obj.SetActive(true);
			number3Obj.SetActive(false);
			crownObj.SetActive(false);

			Image a = number1Obj.GetComponent<Image> ();
			a.sprite = num [num1];

			Image b = number2Obj.GetComponent<Image> ();
			b.sprite = num [num2];

		} else {
			number1Obj.SetActive(false);
			number2Obj.SetActive(false);
			number3Obj.SetActive(true);
			crownObj.SetActive(false);

			Image c = number3Obj.GetComponent<Image> ();
			c.sprite = num[num2];
		}

		if (current > best) {
			//PlayerPrefs.SetInt("best", current);
			crownObj.SetActive(true);
		}
		
		GameObject.Find ("BestScore").GetComponent<Text> ().text = PlayerPrefs.GetInt ("best").ToString ();
	}

	// Update is called once per frame
	void Update () {
	}
}
