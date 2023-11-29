using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Pong : PongBehaviour {
	protected Rigidbody2D _rigidbody;
	protected BoxCollider2D _collider;
	protected float Speed;
	protected Vector2 Direction;
	private float _minY, _maxY;
	protected float _timeout;

	protected float Lifetime;

	private Vector2 _velocity;
	protected Vector2 _initialPosition;

	protected virtual void OnEnable() {
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<BoxCollider2D>();
	}

	protected virtual void FixedUpdate() {
		if (IsPaused)
			return;

		if (_timeout > Lifetime)
			Lifetime += Time.fixedDeltaTime;

		Vector2 targetVelocity = Direction * Speed * Time.fixedDeltaTime;
		_velocity = Vector2.Lerp(_velocity, targetVelocity, 0.25f);

		Vector2 targetPos = _rigidbody.position + _velocity;
		targetPos.y = Mathf.Clamp(targetPos.y, _minY, _maxY);

		_rigidbody.MovePosition(targetPos);
	}
	public void SetMaxBounds(float minY, float maxY) {
		_minY = minY + _collider.size.y * 0.5f;
		_maxY = maxY - _collider.size.y * 0.5f;
	}
	public override void ResetEntity() {
		Lifetime = 0;
		SetPaused(false);
		transform.position = _initialPosition;
	}
}
