using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnyKey : MonoBehaviour {
    private void Update() {
        if(Input.anyKeyDown)
            Destroy(gameObject);
    }
}
