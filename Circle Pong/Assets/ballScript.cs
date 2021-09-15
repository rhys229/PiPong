using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ballScript : MonoBehaviour
{
    public int p1score;
    public int p2score;
    public Text p1text;
    public Text p2text;
    private Vector3 position;
    private Rigidbody2D rb;
    public int lastPoint;

    public float force;

    public int angle;

    public int state;

    private SpriteRenderer sr;

    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        lastPoint = 1;
        state = 0;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0, 0,0);
        StartCoroutine("launchBall");
    }

    // Update is called once per frame
    void Update()
    {
        p1text.text = p1score.ToString();
        p2text.text = p2score.ToString();
        
        if (state == 0)
        {
            sr.color = Color.white;
        }
        else if (state == 1)
        {
            sr.color = Color.red;
        }
        else if (state == 2)
        {
            sr.color = Color.blue;
        }
        if ( Vector2.Distance(transform.position, new Vector2(0,0)) > 10){
            if (state == 1)
            {
                p1score++;
                lastPoint = 1;
            }

            if (state == 2)
            {
                p2score++;
                lastPoint = 2;
            }

            StartCoroutine(launchBall());

        }

        checkVictory();
    }

    IEnumerator launchBall()
    {
        if (lastPoint == 1)
        {
            Debug.Log("state 2");
            state = 2;
        }
        if (lastPoint == 2)
        {
            state = 1;
        }
        transform.position = new Vector3(0, 0,0);
        rb.velocity = Vector2.zero;
        angle = Random.Range(0, 360);
        float xcomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        Debug.Log(angle);
        Instantiate(arrow);
        yield return new WaitForSeconds(3f);
        rb.AddForce(new Vector2(xcomponent,ycomponent));
        
    }

    void checkVictory()
    {
        if (p1score > 6)
        {
            SceneManager.LoadScene("redVictory");
        }

        if (p2score > 6)
        {
            SceneManager.LoadScene("blueVictory");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "paddle1")
        {
            state = 1;
        }
        if (other.gameObject.name == "paddle2")
        {
            state = 2;
        }
    }
}
