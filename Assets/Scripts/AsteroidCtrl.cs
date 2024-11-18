using System.Collections.Generic;
using UnityEngine;

public class AsteroidCtrl : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Transform tr;
    public float Speed = 0f;
    public GameObject ParticleSystem;


    void Start()
    {
        tr = transform;
        Speed = Random.Range(3.0f, 9.0f);
    }


    void Update()
    {
        tr.Translate(Vector3.left * Speed * Time.deltaTime);
        if (tr.position.x < -25.0f)
            Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            GameObject particle = Instantiate(ParticleSystem, tr.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(particle, 0.4f);
            Destroy(collision.gameObject);
        }
    }
}
