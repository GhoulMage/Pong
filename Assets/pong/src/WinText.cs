using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WinText : MonoBehaviour {
    private TMP_Text _uiText;
    private string _winner;

	private void OnEnable() {
        _uiText = GetComponent<TMP_Text>();

        if(string.IsNullOrEmpty(_winner)) {
            gameObject.SetActive(false);
        } else {
            _uiText.text = _winner + " side wins!";
        }
	}
	private void OnDisable() {
		_winner = string.Empty;
	}

	public void OnWin(string side) {
        _winner = side;
        _uiText.gameObject.SetActive(true);
    }
    public void OnReset() {
        _uiText.gameObject.SetActive(false);
    }
}
