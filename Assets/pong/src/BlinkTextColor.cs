using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BlinkTextColor : MonoBehaviour {
	private TMP_Text _uiText;
	public Color colorA = Color.white;
	public Color colorB = Color.white;
	public float timescale = 1.85f;

	public void Activate(bool active) {
		gameObject.SetActive(active);
	}

	private void OnEnable() {
		_uiText = GetComponent<TMP_Text>();
	}
	private void Update() {
		_uiText.color = Color.Lerp(colorA, colorB, Mathf.Sin(timescale * Time.time)); // * Mathf.PI would have been smart here...
	}
}
