using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    //
    public float width;

    void Start()
    { 
        boxCollider2D = GetComponent<BoxCollider2D>();
        width = boxCollider2D.size.x;
       // transform = transform.GetComponent<Transform>();
        StartCoroutine(backgroundMove());
    }

    
    IEnumerator backgroundMove()
    {
        while (true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= width * -1.5f)
            {
                Vector2 newposition = new Vector2(width * 2.0f, 0f);
                transform.position = (Vector2)transform.position + newposition;
            }
            yield return new WaitForSeconds(0.02f);
        }
    
    }
}
