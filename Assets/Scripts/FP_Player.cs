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

    private void Update()
    {
        // Skor ve rekor gosterimi
        uiScore.text = "Skor: " + score + "\nRekor: " + PlayerPrefs.GetInt("HighScore", 0);

        if (Input.GetMouseButtonDown(0))
        {
            didFlap = true;
        }
    }

    private void FixedUpdate()
    {
        // Oyun basladiysa hareket aktif
        if (GameStateMachine.Instance.isGameStarted)
        {
            rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);

            if (didFlap)
            {
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
                didFlap = false;
            }
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
