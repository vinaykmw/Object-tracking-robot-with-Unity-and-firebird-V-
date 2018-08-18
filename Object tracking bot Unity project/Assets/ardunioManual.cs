using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System;
using System.Threading;


public class ardunioManual  : MonoBehaviour {
	public GameObject vrboxreferance;
	private string rdata;
	private string[] vec3;
	private Vector3 accelnew ;
	private Vector3 accel ;
	private Vector3 dir;
	public float veL, veR;

	bool object_visiblity;
	public GameObject targetRef;
	public float dx; 
	//public Rigidbody rb;

	SerialPort sp = new SerialPort("COM3", 9600); //Set the port (com4) and the baud rate (9600, is standard on most devices) 

	void Start () {
		veL = 0;
		veR = 0;
		sp.Open ();
		//	dx = Mathf.RoundToInt(targetRef.GetComponent<DistanceFinder> ().thetax);

	}


	void Update ()
	{	dx = targetRef.GetComponent<DistanceFinder> ().thetax;
		object_visiblity = vrboxreferance.GetComponent<MeshRenderer> ().isVisible;
		portWriter ();
		/*if (sp.IsOpen)
			GiveVelocity(Mathf.RoundToInt(veL), Mathf.RoundToInt(veR));
		else
			sp.Open();
			*/
	}



	void portWriter()
	{
		//print (" running");
		try
		{	
			// Check that the port is open. If not skip and do nothing
			if (sp.IsOpen )
			{ 
				GiveVelocity(Mathf.RoundToInt(veL), Mathf.RoundToInt(veR));
				print("giving velocity ");
			}
			else{
				sp.Open();
				GiveVelocity(0,0);			}
		}
		catch (Exception ex)
		{
			// This could be thrown if we close the port whilst the thread 
			// is reading data. So check if this is the case!
			if (sp.IsOpen) {
				// Something has gone wrong!
				Debug.Log (ex.Message.ToString ());
			} else
				print ("Port closed");
		}

	}
	//void OnTriggerEnter(Collider other)
	public void GiveVelocity(int vl, int vr)
	{
		sp.Write(vl.ToString() + 'l');
		sp.Write(vr.ToString() + 'r');
	}
}


