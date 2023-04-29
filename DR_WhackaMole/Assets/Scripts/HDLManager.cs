using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HDLManager : MonoBehaviour
{
    [SerializeField] HitDetection hitDet;

    private Rigidbody rb;
    public float high_score;
    private float high_point;
    private bool check_score;
    private float start_height;
    public TMP_Text UIFeedback;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        high_point = 0.0f;
        start_height = transform.localPosition.y;
        check_score = false;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude != 0 && transform.localPosition.y > start_height)
        {
            high_point = Mathf.Max(high_point, transform.localPosition.y);
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
        UIFeedback.text = high_score.ToString("0.00");
        rb.useGravity = true;
    }

    void Update() {
        if (high_score <= hitDet.vel.y) {
            high_score = hitDet.vel.y;
            UIFeedback.text = high_score.ToString("0.00");
        }
    }
}
