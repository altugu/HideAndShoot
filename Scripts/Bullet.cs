using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // life: the time the bullet disappears after initialized
    public float life = 3;
    public float speed = 8f;

    private void Start() {

        Destroy(gameObject, life);
    }
    private void FixedUpdate()
    {
        // bullet position calculation in each frame
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
