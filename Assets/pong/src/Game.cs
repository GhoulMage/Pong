using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Game : MonoBehaviour {
	public UnityEvent<bool> OnPauseSet;

	public bool Paused {  get; private set; }
	public virtual void Pause(bool pause) {
		Paused = pause;
		OnPauseSet?.Invoke(Paused);

		Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
		Cursor.visible = pause;
	}

	public abstract void Begin();
	public abstract void PhysicsTick();
	public abstract void Tick();
	public abstract void ResetGame();
}
