using UnityEngine;
using UnityEngine.Events;

using Action = System.Action;

public class PongGame : Game {
	public Camera Camera;
	public SpriteRenderer BackgroundSprite;
	public SpriteRenderer SeparationSprite;
	public PongGameData gameData;
	public GameArea gameArea;
	public float distanceWinCondition = 0.1f;

	public UnityEvent OnReset, OnNewMatch;
	public UnityEvent<int> OnScoreRightSide, OnScoreLeftSide;
	public UnityEvent<string> OnWin;

	private int rightSideScore, leftSideScore;
	private PongBall ball;
	public PongEntitites gameEntities;

	public static event Action OnGameStart;
	public static event Action OnGameEnd;
	public static event Action OnGettingWarmedUp;
	public static event Action OnGoingAllOut;

	public override void Begin() {
		gameEntities = new PongEntitites();
		Camera.backgroundColor = gameData.backgroundColor;
		gameArea.CreateAreaColliders();

		CreateEntities();

		SetBackgroundSprite();
		SetSeparationSprite();
		OnGameStart?.Invoke();
		PongBall.OnCollision += CheckIfScore;
	}

	private void CheckIfScore(Vector2 arg1, bool arg2) {
		if (Mathf.Abs(arg1.x - gameArea.Left.x) < distanceWinCondition) {
			OnScoreRightSide?.Invoke(++rightSideScore);
			NewMatch(Random.Range(-65, 65));

			if (rightSideScore >= 9) {
				OnWin?.Invoke("Right");
				EndGame();
			}
		}

		if (Mathf.Abs(arg1.x - gameArea.Right.x) < distanceWinCondition) {
			OnScoreLeftSide?.Invoke(++leftSideScore);
			NewMatch(Random.Range(120, 235));

			if (leftSideScore >= 9) {
				OnWin?.Invoke("Left");
				EndGame();
			}
		}
	}

	public override void Tick() {
		if (Input.GetKeyDown(KeyCode.R)) {
			ResetGame();
		}

		if (gameEntities.IsEmpty)
			return;

		if (Input.GetKeyDown(KeyCode.Space)) {
			Pause(!Paused);
		}
	}
	
	public override void Pause(bool pause) {
		gameEntities.SetPauseAll(pause);
		base.Pause(pause);
	}
	public override void PhysicsTick() { }

	public void NewMatch(float newBallDirectionAngle) {
		gameEntities.ResetAll();
		ball.SetDirection(newBallDirectionAngle);

		OnNewMatch?.Invoke();

		if (rightSideScore + leftSideScore == 3) {
			OnGettingWarmedUp?.Invoke();
		}
		if(rightSideScore + leftSideScore == 8) {
			OnGoingAllOut?.Invoke();
		}
	}

	public override void ResetGame() {
		gameEntities.ClearAll();

		rightSideScore = leftSideScore = 0;
		CreateEntities();
		Pause(false);

		OnReset?.Invoke();
		OnGameStart?.Invoke();
	}

	public void EndGame() {
		gameEntities.ClearAll();
		OnGameEnd?.Invoke();
	}

	private void CreateEntities() {
		float top = gameArea.Size.y*0.5f;
		float bottom = -gameArea.Size.y*0.5f;

		ball = gameData.CreateBall(gameArea.Center);
		gameEntities.Register(ball);
		gameEntities.Register(gameData.CreatePlayerPong(gameArea.Right + Vector2.left, top, bottom));
		gameEntities.Register(gameData.CreateAIPong(gameArea.Left + Vector2.right, ball, top, bottom));
	}

#if !UNITY_EDITOR
	private void OnApplicationFocus(bool focus) {
		if (gameEntities == null || gameEntities.IsEmpty)
			return;

		if (!focus)
			Pause(true);
	}
#endif

	private void SetBackgroundSprite() {
		if (gameData.backgroundSprite.sprite != null) {
			BackgroundSprite.sprite = gameData.backgroundSprite.sprite;
			Vector3 backgroundSpriteScale = gameData.backgroundSprite.size;
			backgroundSpriteScale.z = 1f;
			BackgroundSprite.transform.localScale = backgroundSpriteScale;
			BackgroundSprite.color = gameData.backgroundSprite.color;
		}
	}

	private void SetSeparationSprite() {
		if (gameData.separationSprite.sprite != null) {
			SeparationSprite.sprite = gameData.separationSprite.sprite;
			Vector3 separationSpriteScale = gameData.separationSprite.size;
			separationSpriteScale.z = 1f;
			SeparationSprite.transform.localScale = separationSpriteScale;
			SeparationSprite.color = gameData.separationSprite.color;
		}
	}
}
