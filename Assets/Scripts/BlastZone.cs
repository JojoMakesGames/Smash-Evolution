using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public GameObject explosion;
    void OnTriggerEnter2D(Collider2D other) {
        explosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
        AudioSource.PlayClipAtPoint(explosion.GetComponent<AudioSource>().clip, other.transform.position);
        Destroy(other.gameObject);
        Destroy(explosion, 0.5f);
    }
}
