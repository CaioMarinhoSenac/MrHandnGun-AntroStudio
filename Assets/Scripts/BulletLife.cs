using UnityEngine;

public class BulletSimple : MonoBehaviour
{
    public float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colidiu com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ScoreManager.Instance.AddPoint(1);
            Destroy(collision.gameObject); // remove inimigo
            Destroy(gameObject);           // remove bala
        }
    }
}