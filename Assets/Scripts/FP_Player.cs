using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FP_Player : MonoBehaviour
{
    public float jumpForce;
    public Rigidbody2D rb;
    public int combo = 1;
    public int score = 0;
    public TextMeshProUGUI uiScore;
    public float forwardSpeed;
    bool didFlap = false;


    public bool gameOver = false;

    private void Update()
    {
        uiScore.text = score.ToString();
        if (!gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                didFlap = true;
            }
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * forwardSpeed);
        if (didFlap)
        {
            rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
            didFlap = false;
        }
    }
}
