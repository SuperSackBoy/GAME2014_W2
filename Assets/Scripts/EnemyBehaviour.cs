using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour {

	[Header("References")]
	[SerializeField] private Transform bulletSpawnPoint;
	
	[Header("Bounds")]
	[SerializeField] private Boundary movementBounds;
	[SerializeField] private Boundary startingXBounds;
	[SerializeField] private Boundary startingYBounds;

	[Header("Stats")]
	[SerializeField] private IntBoundary shotDelay;

	
	
	private Vector2 startingPoint;
	private float randomSpeed;
	private int currentShotDelay;
	
	private int fixedFrame;
	
	void Start() {
		randomSpeed = Random.Range(movementBounds.min, movementBounds.max);

		startingPoint = new Vector2(
			Random.Range(startingXBounds.min, startingXBounds.max),
			Random.Range(startingYBounds.min, startingYBounds.max)
		);

		transform.position = startingPoint;
		
		currentShotDelay = Random.Range(shotDelay.min, shotDelay.max);
	}

	void Update() {
		transform.position = new Vector2(
			Mathf.PingPong(Time.time * randomSpeed + startingPoint.x, startingXBounds.max - startingXBounds.min) + startingXBounds.min,
			transform.position.y);
	}

	private void FixedUpdate() {
		fixedFrame++;
		
		if (fixedFrame >= currentShotDelay) {
			GameObject bullet = BulletManager.instance.GetBullet(bulletSpawnPoint.position);
			fixedFrame = 0;
			currentShotDelay = Random.Range(shotDelay.min, shotDelay.max);
		}
	}
}