using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int enemyHealth;
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDirection = GetRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        FindNewDir();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newDirection;
        int oldQuadrant = GetQuadrant(moveDirection);

        // Keep picking a new direction until it lands in a different quadrant
        do
        {
            newDirection = GetRandomDirection();
        } while (GetQuadrant(newDirection) == oldQuadrant);

        moveDirection = newDirection;
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        return dir.normalized;
    }

    private int GetQuadrant(Vector3 dir)
    {
        // XZ plane: Quadrants 1 to 4 based on the signs of X and Z
        if (dir.x >= 0 && dir.z >= 0) return 1;
        if (dir.x < 0 && dir.z >= 0) return 2;
        if (dir.x < 0 && dir.z < 0) return 3;
        return 4; // dir.x >= 0 && dir.z < 0
    }
    public void FindNewDir()
    { 
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }
}
