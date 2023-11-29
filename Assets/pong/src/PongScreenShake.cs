using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PongScreenShake : MonoBehaviour {
	public float HorizontalStrength = 2;
	public float VerticalStrength = 2;
	public float Duration = 0.2f;

	private Vector3 _originalCameraPos;
	private float _shakeTime;
	private Camera _camera;

	private void OnEnable() {
		_camera = GetComponent<Camera>();
		_originalCameraPos = _camera.transform.position;
		_shakeTime = Duration;
		PongBall.OnCollision += ScreenShake;
	}
	private void OnDisable() {
		PongBall.OnCollision -= ScreenShake;
	}

	private void ScreenShake(Vector2 arg1, bool againstPong) {
		if (againstPong) {
			ShakeScreen();
		}
	}

	private void ShakeScreen() {
		_shakeTime = 0f;
	}

	private void LateUpdate() {
		if(_shakeTime <= Duration) {
			float horizontalShake = Random.Range(-1f, 1f) * HorizontalStrength;
			float verticalShake = Random.Range(-1f, 1f) * VerticalStrength;
			Vector3 shake = new Vector3(horizontalShake, verticalShake) * (1f-(_shakeTime/Duration));

			_camera.transform.position = _originalCameraPos + shake;

			_shakeTime += Time.deltaTime;
		} else {
			_camera.transform.position = _originalCameraPos;
		}
	}
}
