using UnityEngine;

public abstract class PongBehaviour : MonoBehaviour {
	public bool IsPaused { get; private set; }
	public void SetPaused(bool paused) { IsPaused = paused; }
	public abstract void ResetEntity();
}
