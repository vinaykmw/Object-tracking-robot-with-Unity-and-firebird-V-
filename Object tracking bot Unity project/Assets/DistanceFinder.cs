using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceFinder : MonoBehaviour {

	public Text a;
	//public GameObject cameraPos;
	//public GameObject ImageTarget;
	// Use this for initialization
	Vector3 camP;
	//public Vector3 imageP;
	public float modifier;
	public float distance;
	public Vector3 direction;
	public Text directionText;
	public float thetax,thetay;
	public Text angleText;
	void Start () {
		//camP = cameraPos.GetComponent<Transform> ().position;
		//imageP = ImageTarget.GetComponent<Transform> ().position;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Vector3 diff= camP - cubeP; 


		direction = transform.position;
		distance = transform.position.magnitude*modifier*30/20;
		directionText.text = "x=   "+ direction.x.ToString ()+ "   y =   "+ direction.y.ToString () +"   z=   "+ direction.z.ToString ();
		a.text = distance.ToString ()+"cm";
		thetax = 100f*Mathf.Atan (direction.x / distance);
		thetay = 100f*Mathf.Atan (direction.y / distance);
		angleText.text = " x degree = " + thetax.ToString () + "  y  degree =  " + thetay.ToString ();  
	}
}
