using System.Collections.Generic;
using UnityEngine;

public class PongEntitites {
	public List<PongBehaviour> Entities = new List<PongBehaviour>(3);

	public bool IsEmpty {
		get {
			return Entities.Count == 0;
		}
	}
	public void Register(PongBehaviour entity) {
		Entities.Add(entity);
	}
	public void Clear(PongBehaviour entity) {
		Entities.Remove(entity);
		Object.Destroy(entity.gameObject);
	}

	public void ClearAll() {
		foreach(PongBehaviour entity in Entities) {
			Object.Destroy(entity.gameObject);
		}
		Entities.Clear();
	}
	public void ResetAll() {
		foreach(PongBehaviour entity in Entities) {
			entity.ResetEntity();
		}
	}
	public void SetPauseAll(bool value) {
		foreach(PongBehaviour entity in Entities) {
			entity.SetPaused(value);
		}
	}
}
