using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class PlayerBehaviour : MonoBehaviour {
	[SerializeField] private float horizontalForce;
	[SerializeField] private Boundary bounds;
	[SerializeField, Range(0f, 1f)] private float decay;
	
	[SerializeField] private Transform bulletSpawnPoint;
	[SerializeField] private IntBoundary shotDelay;
	
	private Rigidbody2D rb;
	private int fixedFrame;
	private float currentShotDelay;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		currentShotDelay = Random.Range(shotDelay.min, shotDelay.max);
	}

	private float GetTouchDirection() {
		if (Touchscreen.current == null) return 0.0f;

		TouchControl touch = Touchscreen.current.primaryTouch;

		if (!touch.press.isPressed) {
			return 0.0f;
		}

		Vector2 pos = touch.position.ReadValue();

		if (pos.x < Screen.width / 2.0f) {
			return -1.0f;
		}
		
		return 1.0f;
	}
	
	
	private void Move() {
		float x = GetTouchDirection();
		rb.AddForce(new Vector2(x * horizontalForce, 0.0f));
	}


	private void CheckBounds() {
		if (transform.position.x < bounds.min) {
			transform.position = new Vector2(bounds.min, transform.position.y);
			if (rb.linearVelocity.x < 0.0f) rb.linearVelocity = new Vector2(0.0f, rb.linearVelocity.y);
			
		} else if (transform.position.x > bounds.max) {
			transform.position = new Vector2(bounds.max, transform.position.y);
			if (rb.linearVelocity.x > 0.0f) rb.linearVelocity = new Vector2(0.0f, rb.linearVelocity.y);
		}
		
	}
	
	
	private void Update() {
		Move();
		CheckBounds();
	}

	private void FixedUpdate() {
		rb.linearVelocity *= decay;
		
		fixedFrame++;
		
		if (fixedFrame >= currentShotDelay) {
			GameObject bullet = ObjectPool.instance.GetPlayerBullet(bulletSpawnPoint.position);
			fixedFrame = 0;
			currentShotDelay = Random.Range(shotDelay.min, shotDelay.max);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("EnemyBullet")) {
			GameController.instance.RemoveLife();
		}
	}
}