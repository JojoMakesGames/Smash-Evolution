using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger");
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Trigger Exit");
    }
}
