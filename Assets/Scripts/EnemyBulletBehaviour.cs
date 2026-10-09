using System;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour {
	[SerializeField] private float speed;
	
	[SerializeField] private Boundary screenBounds;

	private void Move() {
		transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
	}

	private void CheckBounds() {
		if (transform.position.y < screenBounds.min) {
			ObjectPool.instance.ReturnEnemyBullet(gameObject);
		}
	}
	
	private void Update() {
		Move();
		CheckBounds();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			ObjectPool.instance.ReturnEnemyBullet(gameObject);
		}
	}
}