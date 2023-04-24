using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDLManager : MonoBehaviour
{
    private Rigidbody rb;
    public float high_score;
    private float high_point;
    private bool check_score;
    private float start_height;
    public Text UIFeedback;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        high_point = 0.0f;
        start_height = transform.position.y;
        check_score = false;
        UIFeedback.text = "High Score: " + 0.0;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude != 0 && transform.position.y > start_height)
        {
            high_point = Mathf.Max(high_point, transform.position.y);
            check_score = true;
        }
        else if (check_score)
        {
            if (high_score < high_point)
            {
                HighScore();
            }
            check_score = false;
        }
    }
    void ResetScore()
    {
        high_point = 0.0f;
    }

    void HighScore()
    {
        high_score = high_point;
        Debug.Log("You reached a new High: " + high_point);
        UIFeedback.text = "High Score: " + high_score.ToString("0.00");

    }
}
