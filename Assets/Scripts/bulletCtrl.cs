using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCtrl : MonoBehaviour
{
    //public GameObject bulletprfab;
    public float speed = 300f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed);
        Destroy(gameObject, (3.0f));
    }
}
