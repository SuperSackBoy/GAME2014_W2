using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	[SerializeField] private int enemyBulletNumber = 50;
	[SerializeField] private GameObject enemyBulletPrefab;

	[SerializeField] private int enemyNumber = 25;
	[SerializeField] private GameObject enemyPrefab;
	
	[SerializeField] private int playerBulletNumber = 50;
	[SerializeField] private GameObject playerBulletPrefab;
	
	private Pool enemyBulletPool;
	private Pool enemyPool;
	private Pool playerBulletPool;

	public static ObjectPool instance;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		BuildEnemyBulletPool();
		BuildEnemyPool();
		BuildPlayerBulletPool();
	}

	#region EnemyBullets
	private void BuildEnemyBulletPool() {
		enemyBulletPool = new Pool(enemyBulletNumber, enemyBulletPrefab, transform, "EnemyBullet");
	}

	public GameObject GetEnemyBullet(Vector3 position) {
		return enemyBulletPool.GetObject(position);
	}

	public void ReturnEnemyBullet(GameObject bullet) {
		enemyBulletPool.ReturnObject(bullet);
	}
	#endregion

	#region Enemy
	private void BuildEnemyPool() {
		enemyPool = new Pool(enemyNumber, enemyPrefab, transform, "Enemy");
	}

	public GameObject GetEnemy(Vector3 position) {
		return enemyPool.GetObject(position);
	}

	public void ReturnEnemy(GameObject enemy) {
		enemyPool.ReturnObject(enemy);
	}
	#endregion

	#region PlayerBullets
	private void BuildPlayerBulletPool() {
		playerBulletPool = new Pool(playerBulletNumber, playerBulletPrefab, transform, "PlayerBullet");
	}

	public GameObject GetPlayerBullet(Vector3 position) {
		return playerBulletPool.GetObject(position);
	}

	public void ReturnPlayerBullet(GameObject bullet) {
		playerBulletPool.ReturnObject(bullet);
	}
	#endregion

	private class Pool {
		private int objectCount;
		private readonly GameObject objectPrefab;
		private readonly Transform parent;
		private readonly string objectName;
		
		private Queue<GameObject> objectPool;
		
		public Pool(int objectCount, GameObject objectPrefab, Transform parent, string objectName = "") {
			this.objectCount = objectCount;
			this.objectPrefab = objectPrefab;
			this.parent = parent;
			this.objectName = objectName;
			
			SetupPool();
		}

		private void SetupPool() {
			objectPool = new Queue<GameObject>();

			for (int i = 0; i < objectCount; i++) {
				GameObject newObj = Instantiate(objectPrefab, parent);
				newObj.SetActive(false);
				newObj.name = objectName + " " + i;
				objectPool.Enqueue(newObj);
			}
		}

		private void AddNewObjectToPool() {
			GameObject newObj = Instantiate(objectPrefab, parent);
			newObj.SetActive(false);
			newObj.name = objectName + " " + objectCount;
			objectPool.Enqueue(newObj);
			objectCount++;
		}

		public GameObject GetObject(Vector3 position) {
			if (objectPool.Count <= 0) AddNewObjectToPool();
			GameObject obj = objectPool.Dequeue();
		
			obj.transform.position = position;
			obj.SetActive(true);
		
			return obj;
		}

		public void ReturnObject(GameObject obj) {
			obj.SetActive(false);
			objectPool.Enqueue(obj);
		}
		
	}
	
	
	
	
	
}