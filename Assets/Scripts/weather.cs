using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System;


public class weather : MonoBehaviour {
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

		string url = "http://api.openweathermap.org/data/2.5/find?q=Berlin,de&type=accurate&mode=xml&lang=nl&units=metric&appid=4a94e7970b91531ba7783271a8000205";
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null){

			Debug.Log("Loaded following XML " + www.text);
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(www.text);	
		
			city.text = xmlDoc.SelectSingleNode ("cities/list/item/city/@name").InnerText;
			temperature.text = xmlDoc.SelectSingleNode ("cities/list/item/temperature/@value").InnerText + "° C";

			if(System.Int32.Parse ( xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText) < 20)
			{
				voidIcon.GetComponent<Image> ().sprite = cloud_0to20;
			}
			else if(System.Int32.Parse ( xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText) < 40){
				voidIcon.GetComponent<Image> ().sprite = cloud_20to40;
			}
			else if(System.Int32.Parse ( xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText) < 60){
				voidIcon.GetComponent<Image> ().sprite = cloud_40to60;
			}
			else if(System.Int32.Parse ( xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText) < 80){
				voidIcon.GetComponent<Image> ().sprite = cloud_60to80;
			}
			else if(System.Int32.Parse ( xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText) < 100){
				voidIcon.GetComponent<Image> ().sprite = cloud_80to100;
			}
		}
		else
		{
			Debug.Log("ERROR: " + www.error);

		}

	}


}