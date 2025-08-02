using UnityEngine;

public class spawnEnemyScript : MonoBehaviour

{
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    void Spawn()
    {
        GameObject enemy = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
         EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.enemyHealth = Random.Range(1, 15); // 51 is exclusive, so this gives 5-50
            Debug.Log(enemyMovement.enemyHealth);
    }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, 5f); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
