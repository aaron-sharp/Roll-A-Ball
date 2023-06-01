using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arkanoid_BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;   // Reference to the brick prefab
    public int rowCount = 5;         // Number of rows of bricks
    public int colCount = 8;         // Number of columns of bricks
    public float horizontalSpacing = 1f;   // Horizontal spacing between bricks
    public float verticalSpacing = 0.5f;   // Vertical spacing between bricks

    void Start()
    {
        SpawnBricks();
    }

    void SpawnBricks()
    {
        Vector3 spawnPosition = transform.position;

        // Iterate over rows and columns to spawn bricks
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                // Calculate the position for the current brick
                float posX = spawnPosition.x + col * (brickPrefab.transform.localScale.x + horizontalSpacing);
                float posY = spawnPosition.y + row * (brickPrefab.transform.localScale.y + verticalSpacing);
                Vector3 brickPosition = new Vector3(posX, posY, spawnPosition.z);

                // Instantiate the brick at the calculated position
                Instantiate(brickPrefab, brickPosition, Quaternion.identity, transform);
            }
        }
    }
}