using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_EnumType : MonoBehaviour
{
    public enum Tags
    {
        Player,
        GameOverCollider,
    }

    public Tags selectedTag; // Inspector'da gorulecek
}
