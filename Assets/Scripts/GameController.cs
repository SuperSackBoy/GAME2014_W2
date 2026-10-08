using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
	[SerializeField] private RectTransform safeZoneContainer;
	[SerializeField] private TMP_Text screenLabel;
	
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start() {
	}

	// Update is called once per frame
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