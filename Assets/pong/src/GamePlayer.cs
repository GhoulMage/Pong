using UnityEngine;

public class GamePlayer : MonoBehaviour {
	public Game Game;

	private bool playing;

	private void Update() {
		if (!playing) {
			if (Input.anyKeyDown) {
				Game.Begin();
				playing = true;
			}
			return;
		}

		Game.Tick();
	}

	private void FixedUpdate() {
		if (!playing)
			return;
		Game.PhysicsTick();
	}
}
