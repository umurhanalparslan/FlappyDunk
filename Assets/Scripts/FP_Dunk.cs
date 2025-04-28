using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Dunk : MonoBehaviour
{
    [Header("Baglanti yapilacak Player GameObject")]
    public GameObject playerGameobject;

    [Header("Enum Secimi")]
    public FP_EnumType.Tags selectedTag = FP_EnumType.Tags.Player;

    private FP_Player player;
    private bool touchedBad = false;
    private bool gotPoint = false;

    private void Start()
    {
        if (playerGameobject == null)
        {
            Debug.Log("Player GameObject baglanmamis");
            return;
        }
        player = playerGameobject.GetComponent<FP_Player>();
        if (player == null)
        {
            Debug.Log("playerScript component bulunamadi!");
            return;
        }
        Debug.Log("Player ve playerScript bulundu!");

    }
    private void Update()
    {
        if (player == null) return;
        float distance = player.transform.position.x - transform.position.x;
        if (distance > 2f && distance < 3f)
        {
            if (!gotPoint)
            {
                player.gameOver = true;
                Debug.Log("Game Over");
            }
        }
        if (distance > 4f)
        {
            if (gotPoint)
            {
                gotPoint = false;
                touchedBad = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(selectedTag.ToString()))
        {
            if (player == null) return;
            player.combo = 1;
            touchedBad = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(selectedTag.ToString()))
        {
            if (player == null) return;
            if (!gotPoint)
            {
                gotPoint = true;
                if (!touchedBad)
                {
                    player.combo++;

                }
                player.score += player.combo;

            }
            
        }
    }
}
