using System.Collections;
using UnityEngine;

public class BlobAnim : MonoBehaviour
{
    public SpriteRenderer sr;
    public float timeAnim = 0.3f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0.RandomToValue(360));
        StartCoroutine(AnimScale(timeAnim));
    }

    private IEnumerator AnimScale(float timeAnim)
    {
        float timer = 0;
        float target = transform.localScale.x;
        transform.localScale = Vector3.zero;

        while (timer < timeAnim)
        {
            timer += Time.deltaTime;
            yield return null;
            transform.localScale = Vector3.one * Mathf.Lerp(0, target, timer / timeAnim);
        }
    }
}
