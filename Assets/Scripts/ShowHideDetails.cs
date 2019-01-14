using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowHideDetails : MonoBehaviour {
	public GameObject GamesDetailPanel;
	public GameObject PlacesDetailPanel;
	public GameObject PlacesDetailwoRadial;
	public GameObject VideosDetailPanel;
	public GameObject WidgetsDetailPanel;

	public LoadGames loadGameScene;

	public	GameObject SoundController;

	public GameObject MainCanvas;

	public GameObject adjustGlobals;

	public Button btnPlay;
	public Button btnVisit;

	public GameObject StoreScroller;

	public Text appName;
	public Text devName;
	public Text categories;
	public Text description;
	public Text rating;
	public Image Image;


	public Text DET_appName;
	public Text DET_devName;
	public Text DET_categories;
	public Text DET_description;
	public Text DET_rating;
	public Image DET_Image;


	public Text VID_appName;
	public Text VID_devName;
	public Text VID_categories;
	public Text VID_description;
	public Text VID_rating;
	public Image VID_Image;


	public int GameID;
	// Use this for initialization

	public void SwitchDetails(ScrollerData data,string ChooseDetailPanel){
		Debug.Log ("SwitchDetails(): " + ChooseDetailPanel);


		switch (ChooseDetailPanel){

		case "GamesLib":
		case "GameFavorites":
		case "GamesStore":
			if (GamesDetailPanel.activeSelf && data == null) {
				StoreScroller.SetActive (true);		
				GamesDetailPanel.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [0].SetActive (true);
			} else {
				appName.text = data.appName;
				devName.text = data.devName;
				categories.text = data.categories;
				description.text = data.description;
				rating.text = data.rating.ToString ();
				Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> (data.ImageSrc + "_Large");

				GamesDetailPanel.SetActive (true);
				StoreScroller.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [0].SetActive (false);
				GameID = data.GameID;
			}
			break;
		
		case "Places":
			Debug.Log ("case Places");
			if (PlacesDetailPanel.activeSelf && data == null) {
				StoreScroller.SetActive (true);		
				PlacesDetailPanel.SetActive (false);
			adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [1].SetActive (true);
			} else {
				DET_appName.text = data.appName;
				DET_devName.text = data.devName;
				DET_categories.text = data.categories;
				DET_description.text = data.description;
				DET_rating.text = data.rating.ToString ();
				//DET_Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> (data.ImageSrc + "_Large");

				btnVisit.GetComponent<Button> ().onClick.AddListener (delegate {
					Camera.allCameras [0].GetComponent<Skybox> ().material = Resources.Load<Material> (data.ImageSrc);
					Camera.allCameras [1].GetComponent<Skybox> ().material = Resources.Load<Material> (data.ImageSrc);
					Debug.Log(GameID);
					SoundController.GetComponent<audioController>().playSound(GameID);
					Debug.Log("Should be playing,..");
				});

				PlacesDetailPanel.SetActive (true);
				StoreScroller.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [1].SetActive (false);

				GameID = data.GameID;
			}
			break;

		case "VideoLib":
			if (VideosDetailPanel.activeSelf && data == null) {
				StoreScroller.SetActive (true);		
				VideosDetailPanel.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [2].SetActive (true);
			} else {
				VID_appName.text = data.appName;
				VID_devName.text = data.devName;
				VID_categories.text = data.categories;
				VID_description.text = data.description;
				VID_rating.text = data.rating.ToString ();
				VID_Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> (data.ImageSrc + "_Large");

				btnVisit.GetComponent<Button> ().onClick.AddListener (delegate {
					Camera.allCameras [0].GetComponent<Skybox> ().material = Resources.Load<Material> ("panoTheatre");
					Camera.allCameras [1].GetComponent<Skybox> ().material = Resources.Load<Material> ("panoTheatre");
				});

				Debug.Log ("VideoLib Show/Hide");

				VideosDetailPanel.SetActive (true);
				StoreScroller.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [2].SetActive (false);

				GameID = data.GameID;
			}
			break;

		case "WidgetsLib":
			if (WidgetsDetailPanel.activeSelf && data == null) {
				StoreScroller.SetActive (true);		
				WidgetsDetailPanel.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [3].SetActive (true);
			} else {
				VID_appName.text = data.appName;
				VID_devName.text = data.devName;
				VID_categories.text = data.categories;
				VID_description.text = data.description;
				VID_rating.text = data.rating.ToString ();
				VID_Image.GetComponent<Image> ().sprite = Resources.Load<Sprite> (data.ImageSrc + "_Large");


				btnVisit.GetComponent<Button> ().onClick.AddListener (delegate {
					
				});

				Debug.Log ("WidgetsLib Show/Hide");

				WidgetsDetailPanel.SetActive (true);
				StoreScroller.SetActive (false);
				adjustGlobals.GetComponent<adjustGlobals> ().SwitchRadials [3].SetActive (false);

				GameID = data.GameID;
			}
			break;

		}
	}

	public void HidePanoDetails(){
		Color temp = PlacesDetailPanel.GetComponent<Image> ().color;

		if (PlacesDetailPanel.GetComponent<Image>().color.a != 0) {
			PlacesDetailwoRadial.SetActive (false);
			Color tempA = PlacesDetailPanel.GetComponent<Image> ().color;
			tempA.a = 0;
			PlacesDetailPanel.GetComponent<Image> ().color = tempA;

		} else {
			PlacesDetailwoRadial.SetActive (true);
			Color tempA = PlacesDetailPanel.GetComponent<Image> ().color;
			tempA.a = 110.0f;
			PlacesDetailPanel.GetComponent<Image> ().color = tempA;
		}
	}

	public void HideItemList(){
		if (MainCanvas.activeSelf)
			MainCanvas.SetActive (false);
		else
			MainCanvas.SetActive (true);		
	}

	public void LoadGame(int ID){
		loadGameScene.LoadGame (GameID);
	}

	public void LoadAdditiveF(int ID){
		LoadAdditive load = new LoadAdditive ();
		load.ToggleAdditiveScene (GameID);
	}

	public void SwitchDetailsVideo(){
		if (VideosDetailPanel.activeSelf)
			VideosDetailPanel.SetActive (false);
		else
			VideosDetailPanel.SetActive(true);
	}
}
