using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if ((transform.position - GameObject.Find("Ruby").GetComponent<Transform>().position).magnitude > 10)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void Launch(Vector2 direction, float speed)
    {
        rb.AddForce(direction * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EmeryContorler emeryContorler = collision.gameObject.GetComponent<EmeryContorler>();
        if (emeryContorler != null)
        {
            emeryContorler.Fixed();
            BoxCollider2D boxcollider2d = collision.gameObject.GetComponent<BoxCollider2D>();
            boxcollider2d.enabled = false;
            EmeryContorler.instance.fixedNum++;
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy (gameObject);
    }
}
