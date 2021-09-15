using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public GameObject ball;
    public ballScript bs;
    public float Radius;
    public Sprite rarrow;
    public Sprite barrow;
    public SpriteRenderer sr;
    private Vector2 _centre;

    private float angle;

    private int ta;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ball = GameObject.Find("Ball");
        bs = ball.GetComponent<ballScript>();
        angle = 0;
        _centre = new Vector2(0, 0);
        ta = bs.angle;
        if (bs.lastPoint == 2)
        {
            sr.sprite = rarrow;
        }
        if (bs.lastPoint == 1)
        {
            sr.sprite = barrow;
        }
        StartCoroutine("spin",ta);
    }

    // Update is called once per frame
    void Update()
    {
        var offset = new Vector2(Mathf.Sin((angle * Mathf.PI)/180), Mathf.Cos((angle * Mathf.PI)/180)) * Radius;
        transform.position = _centre + offset;
        transform.right = new Vector3(0, 0,0) - transform.position;
    }

    IEnumerator spin(int targetAngle)
    {
        Debug.Log(targetAngle);
        for (int i = 0; i < 5; i++)
        {
            if (i < 4)
            {
                for (int j = 0; j < 361; j+=3)
                {
                    angle = j;
                    yield return new WaitForSeconds(.00001f);
                }
            }

            if (i == 4)
            {
                for (int j = 0; j < targetAngle; j++)
                {
                    angle = j;
                    yield return new WaitForSeconds(.0001f);
                }
            }
        }
        Destroy(this.gameObject);
    }
    
}
