using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class getBattery : MonoBehaviour {

	public Text BatteryLife;

	void Start(){
		int temp = Convert.ToInt32(GetBatteryLevel());
		BatteryLife.GetComponent<Text>().text = temp.ToString () + " %";
	
	}

	int GetBatteryLevel()
	{
		try {
			string CapacityString = System.IO.File.ReadAllText ("/sys/class/power_supply/battery/capacity");
			return int.Parse (CapacityString);
		} catch (Exception e) {
			Debug.Log ("Failed to read battery power; " + e.Message);
		}

		return -1;
	}

}
