using UnityEngine;

public class TaserProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStun stun = collision.gameObject.GetComponent<EnemyStun>();
            Destroy(gameObject);

            if (stun != null)
            {
                stun.Stun(5f);
            }
        }

       Invoke("DestroyObj", 0.5f);
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}