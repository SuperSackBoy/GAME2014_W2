using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private IntBoundary spawnRate;
	
	private int fixedFrame = 0;
	private float currentSpawnDelay;
	
	private void FixedUpdate() {
		fixedFrame++;
		
		if (fixedFrame >= currentSpawnDelay) {
			GameObject enemy = ObjectPool.Instance.GetEnemy(new Vector3(0, 100, 0)); //sets its own position anyways, so set an offscreen pos for now
			enemy.GetComponent<EnemyBehaviour>()?.Setup();
			fixedFrame = 0;
			currentSpawnDelay = Random.Range(spawnRate.min, spawnRate.max);
		}
	}


}
