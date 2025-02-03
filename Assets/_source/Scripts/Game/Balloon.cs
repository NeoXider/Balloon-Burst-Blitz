using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [Header("Settings")]
    [SerializeField] private float gravity = -0.5f;
    [SerializeField] private float maxFloatHeight = 2f;
    [SerializeField] private GameObject popEffect;

    [SerializeField]
    private SpringJoint2D tether;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rb.gravityScale = gravity;
    }

    private void Start()
    {
        if(ColorBallon.Instance != null)
            sr.color = ColorBallon.Instance.GetRandomColor();
    }

    private void Update()
    {
        tether.distance = maxFloatHeight;
    }

    //лопнуть шарик
    public void PopBalloon()
    {
        if (popEffect != null)
        {
            GameObject obj = Instantiate(popEffect, sr.transform.position, Quaternion.identity, transform.parent);
            
            if(obj.TryGetComponent(out BlobAnim blob))
            {
                blob.sr.color = sr.color;
            }
        }

        GameController.Instance.PopBallon();
        Destroy(gameObject);
    }
}