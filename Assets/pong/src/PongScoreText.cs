using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PongScoreText : MonoBehaviour {
	public TMP_Text uiText;

	public void OnScore(int newScore) {
		uiText.text = newScore.ToString();
	}
}
