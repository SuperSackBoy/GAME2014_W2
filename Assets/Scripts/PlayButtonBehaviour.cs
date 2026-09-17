using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonBehaviour : MonoBehaviour {
	[SerializeField] private int desiredSceneID;
	void Start() { }

	
	void Update() { }

	public void OnPlayButtonClick() {
		SceneManager.LoadScene(desiredSceneID);
	}
}