using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

	[SerializeField] private int bulletNumber = 50;
	[SerializeField] GameObject bulletPrefab;

	private Queue<GameObject> pool;

	public static BulletManager instance;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		BuildBulletPool();
	}

	private void BuildBulletPool() {
		pool = new Queue<GameObject>();

		for (int i = 0; i < bulletNumber; i++) {
			GameObject bullet = Instantiate(bulletPrefab, transform);
			bullet.name = "Bullet " + i;
			bullet.SetActive(false);
			pool.Enqueue(bullet);
		}
		
	}

	private GameObject AddNewBulletToPool() {
		GameObject bullet = Instantiate(bulletPrefab, transform);
		bullet.name = "Bullet " + bulletNumber;
		bullet.SetActive(false);
		pool.Enqueue(bullet);
		
		bulletNumber++;
		return bullet;
	}

	public GameObject GetBullet(Vector3 position) {
		GameObject bullet = pool.Count > 0 ? pool.Dequeue() : AddNewBulletToPool();
		
		bullet.transform.position = position;
		bullet.SetActive(true);
		
		return bullet;
	}

	public void ReturnBullet(GameObject bullet) {
		bullet.SetActive(false);
		pool.Enqueue(bullet);
	}

}