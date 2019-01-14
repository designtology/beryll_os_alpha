using UnityEngine;
using System.Collections;

public class ScrollerData : MonoBehaviour {

	public string appName;
	public string devName;
	public string categories;
	public string description;
	public float rating;
	public float priceTag;
	public string ImageSrc;
	public int GameID;

	public ScrollerData(string newAppName, string newDevName, string newCategories, string newDescription, float newRating, float newPriceTag, string newImage,int newID){
		appName = newAppName;
		devName = newDevName;
		categories = newCategories;
		description = newDescription;
		rating = newRating;
		priceTag = newPriceTag;
		ImageSrc = newImage;
		GameID = newID;
	}
}
