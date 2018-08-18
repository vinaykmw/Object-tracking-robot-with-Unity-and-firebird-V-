using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System;
using System.Threading;


public class ardunio : MonoBehaviour {
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
	}



	void portWriter()
	{
		//print (" running");
		try
		{	
			// Check that the port is open. If not skip and do nothing
			if (sp.IsOpen && object_visiblity&&(Mathf.Abs(dx)>0.5f))
			{ float x = dx;
                veL = 217.6414f + 2.007815f * x - 0.3077718f * Mathf.Pow(x, 2) + 0.001890171f * Mathf.Pow(x, 3) + 0.0002709233f * Mathf.Pow(x, 4) + 0.000001641281f * Mathf.Pow(x ,5);
                veL = Mathf.Clamp(veL, 0, 250);
                veR = 207.4261f - 0.9849778f * x - 0.1270757f * Mathf.Pow(x, 2) - 0.006143416f * Mathf.Pow(x, 3);
                veR = Mathf.Clamp(veR, 0, 250);
                GiveVelocity(Mathf.RoundToInt(veL), Mathf.RoundToInt(veR));
            }
			else{
				Stop();
				}
		}
		catch (Exception ex)
		{
			// This could be thrown if we close the port whilst the thread 
			// is reading data. So check if this is the case!
			if (sp.IsOpen) {
				// Something has gone wrong!
				Debug.Log (ex.Message.ToString ());
				Stop ();
			} else
				print ("Port closed");
		}

	}
	public void Stop()
	{
		print("stoping");


		veL=0;
		veR=0;
		GiveVelocity(Mathf.RoundToInt(veL), Mathf.RoundToInt(veR));
	}
    //void OnTriggerEnter(Collider other)
    public void GiveVelocity(int vl, int vr)
	{//	print ("Giving velocity");
		if (sp.IsOpen) {
			sp.Write (vl.ToString () + 'l');
			sp.Write (vr.ToString () + 'r');
		}
    }
}


