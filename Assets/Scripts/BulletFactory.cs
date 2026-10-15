
using System;
using UnityEngine;

public class BulletFactory : MonoBehaviour {

	[SerializeField] private GameObject enemyBullet;
	[SerializeField] private GameObject playerBullet;
	
	private static BulletFactory instance;

	public static BulletFactory Instance {
		get {
			if (!instance) {
				SetupInstance();
			}
			return instance;
		}
	}
	
	private static void SetupInstance() {
		instance = FindAnyObjectByType<BulletFactory>();
		if (instance == null) {
			var gameObj = new GameObject("BulletFactory");
			instance = gameObj.AddComponent<BulletFactory>();
		}
		DontDestroyOnLoad(instance.gameObject);
	}

	private void Awake() {
		if (instance == null) {
			SetupInstance();
		}
		else {
			Destroy(gameObject);
		}
	}

	public GameObject CreateBullet(BulletType type, Transform parent) {
		GameObject bullet = (type == BulletType.Enemy) ? 
			Instantiate(enemyBullet) : 
			Instantiate(playerBullet);

		bullet.transform.parent = parent;
		bullet.SetActive(false);
		
		return bullet;
	}

}