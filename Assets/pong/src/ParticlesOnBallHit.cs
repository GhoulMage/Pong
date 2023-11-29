using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnBallHit : MonoBehaviour {
	public ParticleSystem Particles;

	private void OnEnable() {
		PongBall.OnCollision += SpawnBallHit;
	}
	private void OnDisable() {
		PongBall.OnCollision -= SpawnBallHit;
	}

	private void SpawnBallHit(Vector2 obj, bool againstPong) {
		Instantiate(Particles, obj, Quaternion.identity);
	}
}
