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
        // Sahnedeki Player enumlu objeyi bul
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

        // Oyuncu potayi gecti ama puan alamadiysa
        if (distance > 2f && distance < 3f)
        {
            if (!gotPoint)
            {
                // Artik gameOver buraya yazilmiyor
                Debug.Log("Pota kacirildi ama gameOver disarida kontrol edilecek.");
                player.combo = 0;
            }
        }

        // Oyuncu tamamen gectiyse resetle
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

        // Eger carpan objede Player Enum'u varsa
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
        Debug.Log("🔥 TRIGGER CALISTI! Temas eden: " + other.name);
        // Eger carpan objede Player Enum'u varsa
        FP_EnumType enumScript = other.gameObject.GetComponent<FP_EnumType>();
        if (enumScript != null && enumScript.selectedTag == FP_EnumType.Tags.Player)
        {
            if (player == null) return;

            if (!gotPoint)
            {
                gotPoint = true;
                if (!touchedBad)
                    player.combo++;
            }

            player.score += player.combo;
        }
    }
}
