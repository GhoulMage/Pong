using System;
using UnityEngine;

[Serializable]
public class PongSprite {
	public Sprite sprite;
	public Color color;
	public Vector2 size;

	public PongSprite(Color color, Vector2 size) {
		this.color = color;
		this.size = size;
	}
}
