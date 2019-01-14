using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.IO;
using System;


public class readRSS : MonoBehaviour {
	public Image voidIcon;
	public Text temperature;
	public Text city;

	public Sprite cloud_0to20;
	public Sprite cloud_20to40;
	public Sprite cloud_40to60;
	public Sprite cloud_60to80;
	public Sprite cloud_80to100;

	IEnumerator Start()
	{
		
		string url = "http://www.crosscreations.de/rlau.xml";
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null){

			StringReader stringReader = new StringReader(www.text);
			stringReader.Read(); // skip BOM
			XmlReader reader = XmlReader.Create(stringReader);

		

			Debug.Log("Loaded following XML " + www.text);
			XmlDocument xmlDoc = new XmlDocument();
		
			xmlDoc.Load (reader);

			//xmlDoc.LoadXml(reader.ToString());	

			Debug.Log (xmlDoc.SelectNodes("feed/entry/").Count);
		}
		else
		{
			Debug.Log("ERROR: " + www.error);

		}

	}

}