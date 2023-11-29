using UnityEngine;
using GhoulMage.BGM;

public class PongSounds : MonoBehaviour {
	public BGMPlayer BGMPlayer;
	public BGMAsset PongTheme;
	public AudioClip BallHitSound, BallHitPongSound;

	private void OnEnable() {
		BGMPlayer.PlayBGM(PongTheme.ToBGM());

		PongGame.OnGameStart += PlayIntro;
		PongGame.OnGameEnd += PlayIntro;
		PongGame.OnGettingWarmedUp += PlayUpbeat;
		PongGame.OnGoingAllOut += PlayClimax;
		PongBall.OnCollision += PlayHitSound;
		PongBall.OnBallCalmedDown += PlayOneLess;
		PongBall.OnBallGoingNuts += PlayOneMore;
	}

	private void PlayHitSound(Vector2 arg1, bool arg2) {
		if(arg2)
			AudioSource.PlayClipAtPoint(BallHitPongSound, arg1, 0.8f);
		else
			AudioSource.PlayClipAtPoint(BallHitSound, arg1, 0.65f);
	}

	private void OnDisable() {
		PongGame.OnGameStart -= PlayIntro;
		PongGame.OnGameEnd -= PlayIntro;
		PongGame.OnGettingWarmedUp -= PlayUpbeat;
		PongGame.OnGoingAllOut -= PlayClimax;
		PongBall.OnBallCalmedDown -= PlayOneLess;
		PongBall.OnBallGoingNuts -= PlayOneMore;
	}

	private void PlayIntro() {
		BGMPlayer.ScheduleSection(1);
	}
	private void PlayUpbeat() {
		BGMPlayer.ScheduleSection(2);
	}
	private void PlayClimax() {
		BGMPlayer.ScheduleSection(3);
	}
	private void PlayOneMore() {
		BGMPlayer.ScheduleSection(BGMPlayer.CurrentSectionIndex + 1);
	}
	private void PlayOneLess() {
		BGMPlayer.ScheduleSection(BGMPlayer.CurrentSectionIndex - 1);
	}
}
