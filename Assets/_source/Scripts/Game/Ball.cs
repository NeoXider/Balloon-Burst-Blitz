using UnityEngine;

public class Ball : MonoBehaviour
{
    public int countCollisionEnter;

    //������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        countCollisionEnter++;
        AM.Instance.Play(4);
    }

    //������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Balloon balloon = collision.gameObject.GetComponentInParent<Balloon>();

        if(balloon != null)
        {
            balloon.PopBalloon();
        }
    }
}