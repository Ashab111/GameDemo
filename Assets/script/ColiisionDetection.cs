using UnityEngine;

public class ColiisionDetection : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
         EnemyMovement enemy = gameObject.GetComponent<EnemyMovement>();
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.GetMovement().magnitude > enemy.enemyHealth)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            enemy.FindNewDir();
        }
    }
}
