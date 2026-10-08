using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	[SerializeField] private float speed;
	
	[SerializeField] private Boundary screenBounds;

	private void Move() {
		transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
	}

	private void CheckBounds() {
		if (transform.position.y < screenBounds.min) {
			BulletManager.instance.ReturnBullet(gameObject);
		}
	}
	
	private void Update() {
		Move();
		CheckBounds();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			BulletManager.instance.ReturnBullet(gameObject);
		}
	}
}