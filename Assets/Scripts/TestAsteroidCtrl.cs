using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestAsteroidCtrl : MonoBehaviour
{  
    private Vector3 direction;
    private float speed = 10f;
    private Transform tr;
    private void OnEnable()
    {
        transform.position = new Vector2(6, Random.Range(-1.5f, 3.5f));
        StartCoroutine("DisableObject");
        tr = GetComponent<Transform>();

    }
    private void Update()
    {
        tr.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void Start()
    {
        
        
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(4.0f);
        gameObject.SetActive(false);
    }
}
