using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FP_GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var selfEnum = GetComponent<FP_EnumType>();
        var otherEnum = other.GetComponent<FP_EnumType>();

        if (selfEnum == null || otherEnum == null) return;

        if (selfEnum.selectedTag == FP_EnumType.Tags.GameOverCollider &&
            otherEnum.selectedTag == FP_EnumType.Tags.Player)
        {
            Debug.Log("✅ GameOver: Flappy collider'a carpti!");

            // High score kontrolu
            FP_Player player = other.GetComponent<FP_Player>();
            if (player != null)
            {
                player.CheckHighScore();
            }

            StartCoroutine(RestartScene());
        }
    }

    IEnumerator RestartScene()
    {
        Debug.Log("▶️ Coroutine BASLADI!");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("🔁 Sahne yukleniyor...");
        SceneManager.LoadScene("FlappyDunk");
    }
}
