using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour {
	public Vector2 Size = new Vector2(1, 1);

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireCube(Center, Size);
	}

	public Vector2 Center {
		get {
			return transform.position;
		}
	}

	public Vector2 Left {
		get {
			return new Vector2(Center.x - Size.x * 0.5f, Center.y);
		}
	}
	public Vector2 Right {
		get {
			return new Vector2(Center.x + Size.x * 0.5f, Center.y);
		}
	}

	public void CreateAreaColliders() {
		Vector2 right = new Vector2(Size.x, 0.5f);
		Vector2 up = new Vector2(0.4f, Size.y);

		ColliderAt(new Vector2(Center.x + Size.x * 0.5F, Center.y), up);
		ColliderAt(new Vector2(Center.x - Size.x * 0.5F, Center.y), up);
		ColliderAt(new Vector2(Center.x, Center.y + Size.y * 0.5F), right);
		ColliderAt(new Vector2(Center.x, Center.y - Size.y * 0.5F), right);
	}

	private void ColliderAt(Vector2 position, Vector2 size) {
		GameObject result = new GameObject("bounds");
		BoxCollider2D collider = result.AddComponent<BoxCollider2D>();
		collider.size = size;
		result.transform.position = position;
	}
}
