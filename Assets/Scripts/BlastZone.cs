using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public GameObject explosion;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == (int)Layers.Player) {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Quaternion rotation = Quaternion.Euler(-1 * rb.velocity.x, -1 * rb.velocity.y, 0);
            explosion = Instantiate(explosion, other.transform.position, rotation);
            AudioSource.PlayClipAtPoint(explosion.GetComponent<AudioSource>().clip, other.transform.position);
            Destroy(other.gameObject);
            Destroy(explosion, 0.5f);
        }
    }
}
