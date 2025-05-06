using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FP_Dunk : MonoBehaviour
{
    private GameObject playerGameObject;
    private FP_Player player;
    private bool touchedBad = false;
    private bool gotPoint = false;

    [Header("Pota filesi (net)")]
    public Transform net; // Sallanacak kisim

    private void Start()
    {
        // Player objesini bulur
        foreach (FP_EnumType obj in FindObjectsOfType<FP_EnumType>())
        {
            if (obj.selectedTag == FP_EnumType.Tags.Player)
            {
                playerGameObject = obj.gameObject;
                break;
            }
        }

        if (playerGameObject != null)
            player = playerGameObject.GetComponent<FP_Player>();
    }

    private void Update()
    {
        if (player == null) return;

        float distance = player.transform.position.x - transform.position.x;

        // Oyuncu potayi gecti ama puan alamadiysa combo sifirlanir
        if (distance > 2f && distance < 3f)
        {
            if (!gotPoint)
            {
                player.combo = 0;
                Debug.Log("❌ Pota kacirildi! Combo sifirlandi.");
            }
        }

        // Reset ayarlari
        if (distance > 4f)
        {
            if (gotPoint)
            {
                gotPoint = false;
                touchedBad = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Player carptiysa combo sifirlanir
        FP_EnumType enumScript = other.gameObject.GetComponent<FP_EnumType>();
        if (enumScript != null && enumScript.selectedTag == FP_EnumType.Tags.Player)
        {
            if (player == null) return;
            player.combo = 1;
            touchedBad = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player potaya girdiyse
        FP_EnumType enumScript = other.gameObject.GetComponent<FP_EnumType>();
        if (enumScript != null && enumScript.selectedTag == FP_EnumType.Tags.Player)
        {
            if (player == null) return;

            if (!gotPoint)
            {
                AudioManager.instance.Play("ScoreSound");
                gotPoint = true;

                if (!touchedBad)
                    player.combo++;

                player.score += player.combo;

                if (player.combo == 10)
                    player.ShowComboMessage();

                AnimateNet(); // Sallanma animasyonu

                Debug.Log("✅ Skor alindi! Combo: " + player.combo + " | Skor: " + player.score);
            }
        }
    }

    private void AnimateNet()
    {
        if (net == null) return;

        net.DOKill(); // Var olan animasyonu durdur

        Vector3 originalPos = net.localPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(net.DOLocalMoveY(originalPos.y - 0.3f, 0.1f).SetEase(Ease.OutQuad))
           .Append(net.DOLocalMoveY(originalPos.y + 0.15f, 0.1f).SetEase(Ease.InOutSine))
           .Append(net.DOLocalMoveY(originalPos.y, 0.1f).SetEase(Ease.OutExpo));
    }
}
