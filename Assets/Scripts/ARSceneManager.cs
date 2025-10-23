using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneManager : MonoBehaviour
{
    public void SahneGecis()
    {
        FaderController.instance.FadeOpen(1f);
        SceneManager.LoadScene("CountdownTimer");
    }
}
