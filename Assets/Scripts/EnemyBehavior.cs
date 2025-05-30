using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public float autoDespawnTime = 3f;

    void Start()
    {
        Destroy(gameObject, autoDespawnTime);
    }
}