using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	[SerializeField] private RectTransform safeZoneContainer;
	[SerializeField] private TMP_Text screenLabel;
	[SerializeField] private TMP_Text livesLabel;
	[SerializeField] private TMP_Text scoreLabel;
	
	[SerializeField] private int lives = 3;
	private int score = 0;

	public static GameController instance;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		UpdateScore();
		UpdateLives();
	}
	
	private void UpdateScore() {
		scoreLabel.text = "Score: " + score;
	}

	private void UpdateLives() {
		livesLabel.text = "Lives: " + lives;
	}

	public void RemoveLife() {
		lives--;
		if (lives <= 0) {
			SceneManager.LoadScene("EndScreen");
		}
		else {
			UpdateLives();
		}
	}

	public void AddScore(int points) {
		score += points;
		UpdateScore();
	}
	
	
	private void Update() {
		Vector2 screenSize = new Vector2(Screen.width, Screen.height); 
		Vector2 minSafeArea = Screen.safeArea.min / screenSize;
		Vector2 maxSafeArea = Screen.safeArea.max / screenSize;
		

		safeZoneContainer.anchorMin = minSafeArea;
		safeZoneContainer.anchorMax = maxSafeArea;

		switch (Screen.orientation) {
			case (ScreenOrientation.LandscapeLeft):
				screenLabel.text = "LandscapeLeft";
				break;
			case (ScreenOrientation.LandscapeRight):
				screenLabel.text = "LandscapeRight";
				break;
			case (ScreenOrientation.Portrait):
				screenLabel.text = "Portrait";
				break;
			case (ScreenOrientation.PortraitUpsideDown):
				screenLabel.text = "PortraitUpsideDown";
				break;
			default:
				screenLabel.text = "Unknown";
				break;
		}
	}
}