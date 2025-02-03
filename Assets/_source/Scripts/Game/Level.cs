using UnityEngine;

public class Level : MonoBehaviour
{
    public Collider2D container;
    public float timeGame = 15;

    public int countBallon => GetComponentsInChildren<Balloon>().Length;

    private void OnValidate()
    {
        
    }
}
