using UnityEngine;

[CreateAssetMenu(menuName = "Data/Pong/Pong data")]
public class PongData : ScriptableObject {
	public Sprite sprite;
	public Vector2 colliderSize = new Vector2(0.5f, 3f);
	public float scale = 1f;

	public GameObject CreateGameObject(string name) {
		GameObject result = new GameObject(name);
		result.AddComponent<SpriteRenderer>().sprite = sprite;
		result.AddComponent<BoxCollider2D>().size = colliderSize;

		Rigidbody2D rigidbody = result.AddComponent<Rigidbody2D>();
		rigidbody.isKinematic = true;
		rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

		return result;
	}
}
