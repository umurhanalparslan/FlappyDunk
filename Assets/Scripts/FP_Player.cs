using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FP_Player : MonoBehaviour
{
    public static FP_Player instance;
    public float jumpForce;
    public Rigidbody2D rb;
    public int combo = 1;
    public int score = 0;
    public TextMeshProUGUI uiScore;
    public float forwardSpeed;
    bool didFlap = false;
    public bool isPlaying = true;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Skor ve rekor gosterimi
        uiScore.text = "Skor: " + score + "\nRekor: " + PlayerPrefs.GetInt("HighScore", 0) + "\nCombo: " + combo;

        if (Input.GetMouseButtonDown(0))
        {
            didFlap = true;
        }
    }

    private void FixedUpdate()
    {
        if (isPlaying && GameStateMachine.Instance.isGameStarted)
        {
            rb.simulated = true; // Fizik aktif
            rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);

            if (didFlap)
            {
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
                didFlap = false;
            }
        }
        else
        {
            rb.simulated = false; // Fizik tamamen durur (ilerleme + dusme durur)
        }
    }

    // High Score kontrolu
    public void CheckHighScore()
    {
        int currentHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHigh)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log("🔥 Yeni rekor! " + score);
        }
    }
}
