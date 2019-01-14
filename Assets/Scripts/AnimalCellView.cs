using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine.SceneManagement;


public class AnimalCellView : EnhancedScrollerCellView
{
	//public GameObject WebCamButton = GameObject.FindGameObjectWithTag ("WebCamText");

	public Text appName;
	public Text devName;
	public Text categories;
	public Text description;
	public Text rating;
	public Text priceTag;
	public Image Image;
	public int GameID;
	public GameObject Globals;
	GameObject[] prefab;

	ScrollerController counter;

	public void SetData(ScrollerData data)
	{
		GameObject Globals = GameObject.FindGameObjectWithTag("GlobalVariables");
		string switchCase = Globals.GetComponent<GlobalVariables> ().StoreSourceName;



		if (switchCase == "WidgetsLib") {
			Debug.Log ("Counter " + counter);
		//	for(int i=0;i<counter;i++){
								
		//	}
	//		prefab [counter] = Resources.Load (data.ImageSrc) as GameObject;

		}

		appName.text = data.appName;
		devName.text = data.devName;
		categories.text = data.categories;
		description.text = data.description;
		rating.text = data.rating.ToString ();
		priceTag.text = data.priceTag.ToString ();
	

		Image.GetComponent<Image>().sprite = Resources.Load<Sprite>(data.ImageSrc);
		GameID = data.GameID;


			
		GetComponent<Button> ().onClick.AddListener (delegate{ShowHideDetails(data);});
	}

	public void UnloadStore(){		
		SceneManager.UnloadScene (7);

	}

	public void ShowHideDetails(ScrollerData data){


		GameObject Globals = GameObject.FindGameObjectWithTag("GlobalVariables");
		string switchCase = Globals.GetComponent<GlobalVariables> ().StoreSourceName;

			switch (switchCase) {

			case "GamesLib":
			case "GameFavorites":
			case "GamesStore":

				GameObject GamesDetails = GameObject.FindGameObjectWithTag ("ShowHideDetails");
				GamesDetails.GetComponent<ShowHideDetails> ().SwitchDetails (data,"GamesLib");
				break;

			case "Places":

				GameObject PlacesDetails = GameObject.FindGameObjectWithTag ("ShowHideDetails");
				PlacesDetails.GetComponent<ShowHideDetails> ().SwitchDetails (data,"Places");
				break;

			case "VideoLib":

				GameObject VideosDetails = GameObject.FindGameObjectWithTag ("ShowHideDetails");
				VideosDetails.GetComponent<ShowHideDetails> ().SwitchDetails (data,"VideoLib");
				break;

			case "WidgetsLib":

				GameObject WidgetsDetail = GameObject.FindGameObjectWithTag ("ShowHideDetails");
				WidgetsDetail.GetComponent<ShowHideDetails> ().SwitchDetails (data,"WidgetsLib");
				break;
		}
	}
		
}