using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	[SerializeField] private float velocity;
	[SerializeField] private Boundary screenBounds;
	[SerializeField] private BulletType type;
	

	private void Move() {
		transform.position += new Vector3(0.0f, velocity * Time.deltaTime, 0.0f);
	}

	private void CheckBounds() {
		if (transform.position.y < screenBounds.min || transform.position.y > screenBounds.max) {
			ObjectPool.Instance.ReturnBullet(gameObject, type);
		}
	}
	
	private void Update() {
		Move();
		CheckBounds();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if ((other.CompareTag("Enemy") && type == BulletType.Player) 
		    || (other.CompareTag("Player") && type == BulletType.Enemy)) {
			ObjectPool.Instance.ReturnBullet(gameObject, type);
		}
	}
}