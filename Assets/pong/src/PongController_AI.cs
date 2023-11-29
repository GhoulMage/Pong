using UnityEngine;

public class PongController_AI : Pong {
	private PongBall _target;

	public void Set(Vector2 position, float speed, PongBall target, float topBounds, float bottomBounds, float timeout) {
		_initialPosition = transform.position = position;
		Speed = speed;
		_target = target;
		_timeout = timeout;
		SetMaxBounds(bottomBounds, topBounds);
	}

	private void Update() {
		Direction = Vector2.zero;
		Vector2 distanceToTarget = _target.transform.position - transform.position;

		if (distanceToTarget.x + _target.Direction.x > distanceToTarget.x) {
			return;
		}

		if (distanceToTarget.y < _collider.size.y * 0.5f && distanceToTarget.y > -_collider.size.y * 0.5f) {
			Direction = Vector2.zero;
			return;
		}

		if (distanceToTarget.y > 0)
			Direction.y = 1;

		if (distanceToTarget.y < 0)
			Direction.y = -1;
	}
}
