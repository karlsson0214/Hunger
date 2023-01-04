using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject wormPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int yMax = (int)Camera.main.orthographicSize;
        int xMax = (int)(Camera.main.orthographicSize * Camera.main.aspect);

        for (int x = -xMax; x < xMax + 1; ++x)
        {
            // upper wall
            Instantiate(wallPrefab, new Vector2(x, yMax), Quaternion.identity);
            // lower wall
            Instantiate(wallPrefab, new Vector2(x, -yMax), Quaternion.identity);
        }
        for (int y = -yMax + 1; y < yMax; ++y)
        {
            // left wall
            Instantiate(wallPrefab, new Vector2(-xMax, y), Quaternion.identity);
            // right wall
            Instantiate(wallPrefab, new Vector2(xMax, y), Quaternion.identity);
        }
        // add water inside of walls
        for (int x = -xMax + 1; x < xMax; ++ x )
        {
            for (int y = -yMax + 1; y < yMax; ++y)
            {
                Instantiate(waterPrefab, new Vector2(x, y), Quaternion.identity);
            }
        }
        // add worms at random position
        int numberOfWorms = 100;
        for (int i = 0; i < numberOfWorms; ++i)
        {
            float x = Random.Range((float)(-xMax + 1), xMax - 1);
            float y = Random.Range((float)(-yMax + 1), yMax - 1);
            Instantiate(wormPrefab, new Vector2(x, y), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
