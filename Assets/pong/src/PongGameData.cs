using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Pong Game")]
public class PongGameData : ScriptableObject {
	public Color backgroundColor = Color.black;
	public PongSprite backgroundSprite = new PongSprite(veryTransparentReddishDarkGrey, new Vector2(31, 23));
	public PongSprite separationSprite = new PongSprite(new Color(1f, 1f, 1f, 0.1568f), new Vector2(3.2f, 15f));

	public PongBall ballPrefab;
	public PongData controlledPong;
	public PongData pongAI;

	[Range(-180, 180)]
	public float ballStartingAngle = 65;
	public float ballSpeed = 60;
	public float controlledPongSpeed = 15;
	public float aiPongSpeed = 15;
	public float gameTimeout = 2.5f;

	private static Color veryTransparentReddishDarkGrey = new Color(0.2f, 0.1607f, 0.1607f, 0.0980f);

	public Quaternion BallStartingRotation {
		get {
			return Quaternion.Euler(0, 0, ballStartingAngle);
		}
	}

	public PongBall CreateBall(Vector2 position) {
		PongBall result = ballPrefab.Create(position, ballStartingAngle, ballSpeed, gameTimeout);

		return result;
	}
	public PongController_Player CreatePlayerPong(Vector2 position, float topBounds, float bottomBounds) {
		GameObject pong = controlledPong.CreateGameObject("Player Pong");
		
		PongController_Player result = pong.AddComponent<PongController_Player>();
		result.Set(position, controlledPongSpeed, topBounds, bottomBounds, gameTimeout);
		pong.transform.localScale *= controlledPong.scale;

		return result;
	}
	public PongController_AI CreateAIPong(Vector2 position, PongBall ball, float topBounds, float bottomBounds) {
		GameObject pong = pongAI.CreateGameObject("AI Pong");
		
		PongController_AI result = pong.AddComponent<PongController_AI>();
		result.Set(position, aiPongSpeed, ball, topBounds, bottomBounds, gameTimeout);
		pong.transform.localScale *= pongAI.scale;

		return result;
	}
}
