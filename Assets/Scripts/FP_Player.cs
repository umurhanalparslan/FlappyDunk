using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    public Transform wingLeft;
    public Transform wingRight;

    [Header("Combo UI")]
    public TextMeshProUGUI comboMessageText; // Combo mesaj yazisi

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Skor, rekor ve combo gosterimi
        uiScore.text = "Skor: " + score + "\nRekor: " + PlayerPrefs.GetInt("HighScore", 0);
        //Combo gozukmesi icin
        //+"\nCombo: " + combo bunu yazcaz.

        if (Input.GetMouseButtonDown(0))
        {
            didFlap = true;
            AnimateWings();
            AnimateJumpEffect(); // Ziplama animasyonu burada tetiklenir
        }
    }

    private void FixedUpdate()
    {
        if (isPlaying && GameStateMachine.Instance.isGameStarted)
        {
            rb.simulated = true; // Fizik aktif

            // Ileri ve yukari hareket
            rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);

            if (didFlap)
            {
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
                didFlap = false;
            }
        }
        else
        {
            rb.simulated = false; // Fizik tamamen durdurulur
        }
    }

    private void AnimateWings()
    {
        if (wingLeft == null || wingRight == null) return;

        float upAngle = 40f;
        float downAngle = -10f;
        float speed = 0.1f;

        // Sol kanat
        wingLeft.DOLocalRotate(new Vector3(0, 0, upAngle), speed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                wingLeft.DOLocalRotate(new Vector3(0, 0, downAngle), speed)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        wingLeft.DOLocalRotate(Vector3.zero, speed).SetEase(Ease.OutQuad);
                    });
            });

        // Sag kanat (ayna)
        wingRight.DOLocalRotate(new Vector3(0, 0, -upAngle), speed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                wingRight.DOLocalRotate(new Vector3(0, 0, -downAngle), speed)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        wingRight.DOLocalRotate(Vector3.zero, speed).SetEase(Ease.OutQuad);
                    });
            });
    }

    private void AnimateJumpEffect()
    {
        // Topu biraz squash-stretch yaparak ziplama hissi ver
        transform.DOKill(); // Onceki scale animasyonlarini iptal et

        Sequence jumpSeq = DOTween.Sequence();
        jumpSeq.Append(transform.DOScale(new Vector3(1.15f, 0.85f, 1f), 0.08f).SetEase(Ease.OutQuad)) // squash
               .Append(transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack)); // eski haline don
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

    // Combo mesajini goster
    public void ShowComboMessage()
    {
        AudioManager.instance.Play("ComboSound");
        comboMessageText.text = "COMBO X10! PERFECT";
        comboMessageText.gameObject.SetActive(true);

        comboMessageText.transform.localScale = Vector3.zero;
        comboMessageText.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);

        DOVirtual.DelayedCall(2f, () =>
        {
            comboMessageText.transform.DOScale(0f, 0.3f).OnComplete(() =>
            {
                comboMessageText.gameObject.SetActive(false);
            });
        });
    }
}
