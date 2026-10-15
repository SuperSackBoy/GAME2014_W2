using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	[SerializeField] private int enemyBulletNumber = 50;

	[SerializeField] private int enemyNumber = 25;
	[SerializeField] private GameObject enemyPrefab;
	
	[SerializeField] private int playerBulletNumber = 50;
	
	private Pool enemyBulletPool;
	private Pool enemyPool;
	private Pool playerBulletPool;

	private static ObjectPool instance;

	public static ObjectPool Instance {
		get {
			if (!instance) {
				instance = FindAnyObjectByType<ObjectPool>();	
			}
			return instance;
		}
	}

	private BulletFactory bulletFactory;
	
	private void Awake() {
		instance = this;
	}

	private void Start() {
		bulletFactory = BulletFactory.Instance;
		BuildEnemyPool();
		BuildBulletPool(BulletType.Enemy);
		BuildBulletPool(BulletType.Player);
	}

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

	#region Bullets
	public GameObject GetBullet(Vector3 position, BulletType type) {
		return type switch {
			BulletType.Enemy => enemyBulletPool.GetObject(position),
			BulletType.Player => playerBulletPool.GetObject(position),
			_ => null
		};
	}

	public void ReturnBullet(GameObject bullet, BulletType type) {
		switch(type) {
			case(BulletType.Enemy): 
				enemyBulletPool.ReturnObject(bullet);
				break;
			case(BulletType.Player):
				playerBulletPool.ReturnObject(bullet);
				break;
		}
	}

	private void BuildBulletPool(BulletType type) {
		switch(type) {
			case(BulletType.Enemy):
				enemyBulletPool = new FactoryPool(enemyBulletNumber, transform, bulletFactory, type, "EnemyBullet");
				enemyBulletPool.SetupPool();
				break;
			case(BulletType.Player):
				playerBulletPool = new FactoryPool(playerBulletNumber, transform, bulletFactory, type, "PlayerBullet");
				playerBulletPool.SetupPool();
				break;
		}
	}
	#endregion

	private class Pool {
		protected int objectCount;
		protected readonly GameObject objectPrefab;
		protected readonly Transform parent;
		protected readonly string objectName;
		
		protected Queue<GameObject> objectPool;
		
		public Pool(int objectCount, GameObject objectPrefab, Transform parent, string objectName = "") {
			this.objectCount = objectCount;
			this.objectPrefab = objectPrefab;
			this.parent = parent;
			this.objectName = objectName;
			objectPool = new Queue<GameObject>();
		}

		public virtual void SetupPool() {
			for (int i = 0; i < objectCount; i++) {
				GameObject newObj = Instantiate(objectPrefab, parent);
				newObj.SetActive(false);
				newObj.name = objectName + " " + i;
				objectPool.Enqueue(newObj);
			}
		}

		protected virtual void AddNewObjectToPool() {
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

	private class FactoryPool : Pool {
		private BulletFactory factory;
		private BulletType type;
		
		public FactoryPool(int objectCount, Transform parent, BulletFactory factory, BulletType type, string objectName = "") : base(objectCount, null, parent, objectName) {
			this.factory = factory;
			this.type = type;
		}
		
		public override void SetupPool() {
			for (int i = 0; i < objectCount; i++) {
				GameObject newObj = factory.CreateBullet(type, parent);
				newObj.name = objectName + " " + i;
				objectPool.Enqueue(newObj);
			}
		}

		protected override void AddNewObjectToPool() {
			GameObject newObj = factory.CreateBullet(type, parent);
			newObj.name = objectName + " " + objectCount;
			objectPool.Enqueue(newObj);
			objectCount++;
		}
		
	}
}