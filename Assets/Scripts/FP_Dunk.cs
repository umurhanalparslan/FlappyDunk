using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Dunk : MonoBehaviour
{
    private GameObject playerGameObject;
    private FP_Player player;
    private bool touchedBad = false;
    private bool gotPoint = false;

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

                if (player.combo == 3)
                    player.ShowComboMessage();

                Debug.Log("✅ Skor alindi! Combo: " + player.combo + " | Skor: " + player.score);
            }
        }
    }
}
