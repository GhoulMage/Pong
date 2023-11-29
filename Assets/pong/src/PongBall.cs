using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PongBall : PongBehaviour {
	private Vector2 _direction;
	private float _speed;
	private Rigidbody2D _rigidbody;
	private Vector2 _initialPosition;
	private Vector2 _initialDirection;
	private float _initialSpeed;
	private float _timeout;
	private float _time;
	private SpriteRenderer _spriteRenderer;
	private bool _isNuts;

	public UnityEvent OnReset;
	const float MAX_SPEED = 80f;

	public static event Action OnBallCalmedDown;
	public static event Action OnBallGoingNuts;
	public static event Action<Vector2, bool> OnCollision;

	public Vector2 position {
		get {
			return transform.position;
		}
	}
	public Vector2 Direction {
		get {
			return _direction;
		}
	}

	public PongBall Create(Vector2 position, float angleInDegrees, float speed, float timeout) {
		PongBall result = Instantiate(this, position, Quaternion.identity);
		result.SetDirection(angleInDegrees);
		result._speed = speed;

		result._initialPosition = position;
		result._initialDirection = result._direction;
		result._initialSpeed = speed;
		result._timeout = timeout;

		return result;
	}

	private void OnEnable() {
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate() {
		if (IsPaused)
			return;

        if (_timeout > _time)
			_time += Time.fixedDeltaTime;

		Vector2 velocity = _direction * _speed * Time.fixedDeltaTime;
		_rigidbody.MovePosition(_rigidbody.position + velocity);
		_speed += 1.5f * Time.fixedDeltaTime;

		if (_speed > MAX_SPEED) {
			_speed = MAX_SPEED;
		}

		BallColor();
		AdjustBallDirectionIfTooHorizontal();
		ResetIfTooFarAway();
	}

	private void BallColor() {
		float speedPercent = (_speed - _initialSpeed) / (MAX_SPEED - _initialSpeed);
		Color orange = Color.yellow * Color.red;

		_spriteRenderer.color = Color.white;
		if (Between(speedPercent, 0.25f, 0.5f)) {
			if (!_isNuts && speedPercent >= 0.35f) {
				OnBallGoingNuts?.Invoke();
				_isNuts = true;
			}
			_spriteRenderer.color = Color.Lerp(Color.white, orange, (speedPercent - 0.25f) / 0.25f);
		}
		if (Between(speedPercent, 0.5f, 0.75f)) {
			_spriteRenderer.color = Color.Lerp(orange, Color.red, (speedPercent - 0.5f) / 0.25f);
		}
		if (Between(speedPercent, 0.75f, 1f)) {
			_spriteRenderer.color = Color.Lerp(Color.red, Color.blue, (speedPercent - 0.75f) / 0.25f);
		}
	}

	private void AdjustBallDirectionIfTooHorizontal() {
		if (nearlyHorizontal(_direction.x) && Between(_direction.y, -0.05f, 0.05f)) {
			_direction = _initialDirection;
		}
	}
	
	private void ResetIfTooFarAway() {
		if (_rigidbody.position.sqrMagnitude > 25*25f)
			ResetEntity();
	}

	private bool nearlyHorizontal(float x) {
		return Between(x, 0.95f, 1.05f) || Between(x, -0.95f, -1.05f);
	}

	private bool Between(float value, float min, float max) {
		return value >= min && value <= max;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		_direction = Vector2.Reflect(_direction, collision.GetContact(0).normal);
		OnCollision?.Invoke(collision.GetContact(0).point, collision.transform.GetComponent<Pong>() != null);
	}

	public void SetDirection(float angleInDegrees) {
		_direction = new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
	}
	public override void ResetEntity() {
		transform.position = _initialPosition;
		_direction = _initialDirection;
		_speed = _initialSpeed;
		if (_isNuts) {
			OnBallCalmedDown?.Invoke();
			_isNuts = false;
		}
		SetPaused(false);

		OnReset?.Invoke();
	}
}
