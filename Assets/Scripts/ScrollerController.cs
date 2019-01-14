using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
	private List<ScrollerData> _data;
	public EnhancedScroller myScroller;
	public AnimalCellView animalCellViewPrefab;
	public GameObject Globals;

	void Start ()
	{
		_data = new List<ScrollerData>();



		// MITTELS FINDGAMEOBJECT ====
		GameObject Globals = GameObject.FindGameObjectWithTag("GlobalVariables");
		string switchCase = Globals.GetComponent<GlobalVariables> ().StoreSourceName;

		switch(switchCase)
		{

		case "GamesLib":
			_data.Add (new ScrollerData ("Forrest Mummy", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "ForestMummy", 2));
			_data.Add (new ScrollerData ("Chess VR", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "chessVR", 5));
			_data.Add (new ScrollerData ("Castle Crusher", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "CTC", 6));
			break;

		case "GameFavorites":

			_data.Add (new ScrollerData ("Faily Rider", "GamesHot", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 2.99f, "FailyRider", 99));
			_data.Add (new ScrollerData ("Criminal Case", "Pretty Simple", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 1.99f, "PHD", 99));
			_data.Add (new ScrollerData ("Titan Brawl", "Nodding Frog Ltd.", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 4.99f, "SkullWars", 99));
			_data.Add (new ScrollerData ("Emoji Blitz", "Disney", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "Emoji-Blitz", 99));
			break;

		case "GamesStore":
			_data.Add (new ScrollerData ("Design Home", "GamesStore", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 4.99f, "DesignHome", 99));
			_data.Add (new ScrollerData ("Maiden: Legacy", "Omnidrone", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 2.99f, "Maiden", 99));
			_data.Add (new ScrollerData ("Soccer Shootout", "Gamegou Limited", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "SoccerShooter", 99));
			_data.Add (new ScrollerData ("War Machcines 2", "Fun Games For Free", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 0.0f, "WarMachines", 99));
			break;

		case "VideoLib":
			_data.Add (new ScrollerData ("Wonder Woman - Trailer", "MediaLib", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "M_WonderWoman", 3));
			_data.Add (new ScrollerData ("Henry - Short", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "M_HenryShort", 3));
			break;

		case "Places":

			_data.Add (new ScrollerData ("Mountain View", "Google", "Nature, Mountains, Himalaya", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 0.99f, "panoMountain", 0));
			_data.Add (new ScrollerData ("Mercedes SLR", "Mercedes", "Sport, Racing, Benz", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 0.00f, "panoSLR", 0));
			_data.Add (new ScrollerData ("Main Floor", "Google", "Photographics, Interior, Architecture", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 0.99f, "panoHall", 0));
			_data.Add (new ScrollerData ("Living Room", "Oculus", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoRoom", 0));
			_data.Add (new ScrollerData ("Paris", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoParis", 0));
			_data.Add (new ScrollerData ("Berlin", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoBerlin", 0));
			_data.Add (new ScrollerData ("Museum", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoMuseum", 0));
			_data.Add (new ScrollerData ("Louvre", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoLouvre", 0));
			_data.Add (new ScrollerData ("Louvre 2", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoLouvre2", 0));
			_data.Add (new ScrollerData ("Waterfall", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoWaterfall", 1));
			_data.Add (new ScrollerData ("Ocean View", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoOceanview", 2));
			_data.Add (new ScrollerData ("Underwater Riff", "Google", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "panoUnderwater", 0));

			break;

		case "MediaStore":
			_data.Add (new ScrollerData ("Design Home", "MediaStore", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 4.99f, "DesignHome", 99));
			_data.Add (new ScrollerData ("Maiden: Legacy", "Omnidrone", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 2.99f, "Maiden", 99));
			_data.Add (new ScrollerData ("Soccer Shootout", "Gamegou Limited", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "SoccerShooter", 99));
			_data.Add (new ScrollerData ("War Machcines 2", "Fun Games For Free", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 0.0f, "WarMachines", 99));
			break;

		case "WidgetsLib":
			_data.Add (new ScrollerData ("Battery & Clock", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "W_Clock", 26));
			_data.Add (new ScrollerData ("Music Player", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "W_MusicPlayer", 30));
			_data.Add (new ScrollerData ("Video Player", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "W_Video_Player", 29));
			_data.Add (new ScrollerData ("Weather", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "W_Weather", 29));
			_data.Add (new ScrollerData ("Voice Chat", "BA 2016", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.9f, 0.00f, "W_VoiceChat", 29));
			break;

		case "WidgetsHot":

			_data.Add (new ScrollerData ("Faily Rider", "WidgetsHot", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 2.99f, "FailyRider", 99));
			_data.Add (new ScrollerData ("Criminal Case", "Pretty Simple", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 1.99f, "PHD", 99));
			_data.Add (new ScrollerData ("Titan Brawl", "Nodding Frog Ltd.", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 4.99f, "SkullWars", 99));
			_data.Add (new ScrollerData ("Emoji Blitz", "Disney", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "Emoji-Blitz", 99));
			break;

		case "WidgetsStore":
			_data.Add (new ScrollerData ("Design Home", "WidgetsStore", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 4.99f, "DesignHome", 99));
			_data.Add (new ScrollerData ("Maiden: Legacy", "Omnidrone", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 2.99f, "Maiden", 99));
			_data.Add (new ScrollerData ("Soccer Shootout", "Gamegou Limited", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 3.99f, "SoccerShooter", 99));
			_data.Add (new ScrollerData ("War Machcines 2", "Fun Games For Free", "CatGame, Racing", "Description Text comes here. Because Lorem ipsum is not enough, we will see what this Text is going to be in the futuristic Games.", 4.7f, 0.0f, "WarMachines", 99));
			break;

		}

		myScroller.Delegate = this;
		myScroller.ReloadData();

	}
		
	public int GetNumberOfCells(EnhancedScroller scroller)
	{
		Debug.Log (_data.Count);
		return _data.Count;
	}

	public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
	{
		return 53f;
	}

	public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int
		dataIndex, int cellIndex)
	{
		AnimalCellView cellView = scroller.GetCellView(animalCellViewPrefab) as
			AnimalCellView;
		cellView.SetData(_data[dataIndex]);
		return cellView;
	}
}