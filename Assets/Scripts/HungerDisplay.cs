using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerDisplay : MonoBehaviour
{
    [SerializeField] private GameObject circleRedPrefab;
    [SerializeField] private GameObject circleGreenPrefab;
    [SerializeField] private GameObject circleYellowPrefab;
    private List<GameObject> circles = new List<GameObject>();
    private int circlesPerSection = 3;
    // Start is called before the first frame update
    void Start()
    {
        

        float x = transform.position.x;
        float y = transform.position.y;
        for (int i = 0; i < circlesPerSection; ++i)
        {
            circles.Add(Instantiate(circleRedPrefab, new Vector2(x, y), Quaternion.identity));
            x = x + 0.5f;
        }
        for (int i = 0; i < circlesPerSection; ++i)
        {
            circles.Add(Instantiate(circleGreenPrefab, new Vector2(x, y), Quaternion.identity));
            x = x + 0.5f;
        }
        for (int i = 0; i < circlesPerSection; ++i)
        {
            circles.Add(Instantiate(circleYellowPrefab, new Vector2(x, y), Quaternion.identity));
            x = x + 0.5f;
        }
        SetValue(6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value)
    {
        if (value > 3 * circlesPerSection)
        {
            value = 3 * circlesPerSection;
            Debug.Log("Hunger Display value to high");
        }
        for (int i = 0; i < value; ++i)
        {
            circles[i].SetActive(true);
        }
        for (int i = value; i < circles.Count; ++i)
        {
            circles[i].SetActive(false);
        }
        if (value < 0)
        {
            if (value < circlesPerSection)
            {
                value = circlesPerSection;
                Debug.Log("Hunger Display Value to low.");
            }
            for (int i = 0; i > value; --i)
            {
                int index = circlesPerSection - i;
            }
        }
    }
}
