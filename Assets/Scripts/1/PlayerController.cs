using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 fp;
    public GameObject hud;
    public int currentCoins;
    [HideInInspector]
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
            }
            if(touch.position.x > fp.x)
            {
                rb.AddForce(new Vector3(25f * Mathf.Clamp(touch.position.x - fp.x, -1, 1), 0, 0) * Time.deltaTime);
                fp = touch.position;
            }
            if(touch.position.x < fp.x)
            {
                rb.AddForce(new Vector3(25f * Mathf.Clamp(touch.position.x - fp.x, -1, 1), 0, 0) * Time.deltaTime);
                fp = touch.position;
            }
        }

        if (Input.GetAxis("Mouse X") > 0)
        {
            rb.AddForce(new Vector3(750f, 0, 0) * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            rb.AddForce(new Vector3(-750f, 0, 0) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Object"))
        {
            GameOver();
            hud.GetComponent<HUD>().isGameOver = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            currentCoins++;
            Destroy(other.gameObject);
        }
    }

    public void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("Hit");
    }
}
