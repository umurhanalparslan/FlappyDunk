using UnityEngine;

public class FP_BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private float startX;
    private float width;

    private void Start()
    {
        startX = transform.position.x;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Kamera X konumunu al
        float camX = Camera.main.transform.position.x;

        // Kamera çok uzaklaşırsa sprite'ı sağa kaydır
        if (camX - transform.position.x > width)
        {
            transform.position += Vector3.right * width * 2f;
        }
    }
}
