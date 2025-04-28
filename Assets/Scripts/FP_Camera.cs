using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Camera : MonoBehaviour
{
    [Header("Baglanti yapilacak Player objesi")]
    public Transform player;

    [Header("Enum Bilgisi Tutulan Script")]
    public FP_EnumType enumTypeScript; // <<< Buradan Enum'a ulasacagiz

    float offsetX;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform baglanmamis!");
            return;
        }

        if (enumTypeScript == null)
        {
            Debug.LogWarning("Enum Script baglanmamis!");
            return;
        }

        Debug.Log("Secilen Enum: " + enumTypeScript.selectedTag.ToString());
        offsetX = transform.position.x - player.position.x;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 pos = transform.position;
            pos.x = player.position.x + offsetX;
            transform.position = pos;
        }
    }
}
