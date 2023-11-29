using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController_Player : Pong {
	public void Set(Vector2 position, float speed, float topBounds, float bottomBounds, float timeout) {
		_initialPosition = transform.position = position;
		Speed = speed;
		_timeout = timeout;
		SetMaxBounds(bottomBounds, topBounds);
	}

	private void Update() {
		Direction = Vector2.zero;
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.O)) {
			Direction.y = 1;
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.L)) {
			Direction.y = -1;
		}
	}
}
