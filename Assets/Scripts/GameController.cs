using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public static GameController S 				{ get; protected set; }
	public UnityEvent GameStart 				{ get; protected set; }
	public UnityEvent GameWon 					{ get; protected set; }
	public UnityEvent GameOver 					{ get; protected set; }
	List<TrainController> trainsInScene;

	[SerializeField] float fadeOutDuration =  	3f;
	[SerializeField] float fadeInDuration = 	3f;
	[SerializeField] AudioClip titleMusic;
	[SerializeField] AudioClip victoryMusic;
	[SerializeField] AudioClip gameOverMusic;
	[SerializeField] AudioClip stageMusic;
	[SerializeField] string gameSceneName;
	[SerializeField] Text victoryText;
	[SerializeField] Text gameOverText;

	ScreenFader screenFader;

	UnityAction victorySequence, deathSequence, resetGameState, fadeIntoGameScene, fadeIntoTitleScreen;
	AudioSource musicPlayer, sfxPlayer;
	AudioFader musicFader;
	PlayerTrain playerTrain;


	// Use this for initialization

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		GetSceneComponents();
		bool titleScreen = 			scene.name == "TitleScreen";

		if (titleScreen)
		{
			// For fading into the title screen
			if (screenFader.opacity > 0)
				screenFader.Fade(0, 1);

			// Make the start button fade into the game scene
			Button startButton = 	GameObject.Find("StartButton").GetComponent<Button>();
			startButton.onClick.AddListener(fadeIntoGameScene);
		}

		// Basically reset the game when loading into the game scene
		bool gameScene = 			scene.name == gameSceneName;

		if (gameScene)
		{
			

			// Find the player train and watch for the trains' deaths
			playerTrain = 			GameObject.FindObjectOfType<PlayerTrain>();
			trainsInScene = 		new List<TrainController>(GameObject.FindObjectsOfType<TrainController>());

			foreach (TrainController train in trainsInScene)
				train.Died.AddListener(OnTrainDied);
			

			screenFader.FadeBegin.RemoveAllListeners();
			screenFader.FadeEnd.RemoveAllListeners();

			if (screenFader.opacity > 0 && !screenFader.isFading)
				screenFader.Fade(0, 1);

			musicPlayer.Stop();
			musicPlayer.clip = 			stageMusic;
			musicPlayer.PlayDelayed(1f);
		}
		
		Debug.Log("On Scene Loaded!");

	}

	void Awake () 
	{
		if (S == null)
			S = 								this;
		else 
			Destroy(this.gameObject);

		GameStart = 							new UnityEvent();
		GameOver = 								new UnityEvent();
		GameWon = 								new UnityEvent();
		GetSceneComponents();
		
		fadeIntoGameScene = 					new UnityAction(FadeIntoGameScene);
		fadeIntoTitleScreen = 					new UnityAction(FadeIntoTitleScreen);

		UnityAction<Scene, LoadSceneMode> onSceneLoaded = 	OnSceneLoaded;
		SceneManager.sceneLoaded += 			onSceneLoaded;

		HideText();
	}

	void Start()
	{
		if (!screenFader.isFading)
			screenFader.Fade(0, 0);
	}

	void FadeIntoTitleScreen()
	{
		if (!screenFader.isFading)
		{
			musicFader.FadeIntoClip(titleMusic, 0.8f, 1f, 1f);
			screenFader.Fade(1, 1);
			screenFader.FadeEnd.AddListener(() => SceneManager.LoadScene("TitleScreen"));
		}
	}

	void FadeIntoGameScene()
	{
		if (!screenFader.isFading)
		{
			Debug.Log("Fading into game scene!");
			musicFader.FadeIntoClip(stageMusic, 1f, 1f, 1f);
			screenFader.Fade(1, 1);
			screenFader.FadeEnd.AddListener(() => SceneManager.LoadScene(gameSceneName));
			//screenFader.FadeEnd.AddListener(() => screenFader.Fade(0, fadeOutDuration));
		}
	}

	void GetSceneComponents()
	{
		screenFader = 							GameObject.FindObjectOfType<ScreenFader>();
		musicPlayer = 							GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
		musicFader = 							musicPlayer.GetComponent<AudioFader>();
	}

	void OnTrainDied(TrainController train)
	{
		if (playerTrain == null)
			return; 

		trainsInScene.Remove(train);

		if (train == playerTrain)
		{
			GameOverSequence();
			return;
		}

		if (trainsInScene.Count == 1)
		{
			TrainController lastTrain = trainsInScene[0];
			if (lastTrain == playerTrain)
				VictorySequence();
			else 
				GameOverSequence();
		}
	}

	void VictorySequence()
	{
		GameWon.Invoke();
		musicPlayer.clip = 			victoryMusic;
		musicPlayer.Play();

		// Show the victory text
		victoryText.enabled = 		true;

		Application.Quit();
		//FadeIntoTitleScreen();

	}

	void GameOverSequence()
	{
		GameOver.Invoke();
		musicPlayer.clip = 			gameOverMusic;
		musicPlayer.Play();

		// Show the game over text
		gameOverText.enabled = 		true;
		Application.Quit();
		//FadeIntoTitleScreen();
	}

	void HideText()
	{
		if (victoryText != null)
			victoryText.enabled = false;

		if (gameOverText != null)
			gameOverText.enabled = false;
	}
}
