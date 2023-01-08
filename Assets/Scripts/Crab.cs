using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    private float speed = 4;
    // lower speed when hungry
    private const float SPEED_HUNGRY = 2;
    // normal speed when in between hungry and over full
    private const float SPEED_NORMAL = 4;
    // lower speed when over full
    private const float SPEED_WHEN_FULL = 2;
    private float angularVelocity = 360;
    private Rigidbody2D rb;
    private int food = 4;
    private HungerDisplay hungerDisplay = null;
    // time to next removal of food
    private const float TIME_TO_FOOD = 2f;
    private float timeToNextFood = TIME_TO_FOOD;
    [SerializeField] private GameObject gameOverDisplay;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hungerDisplay == null)
        {
            
            GameObject hungerDisplayGameObject = GameObject.Find("HungerDisplay");
            if (hungerDisplayGameObject != null)
            {
                hungerDisplay = hungerDisplayGameObject.GetComponent<HungerDisplay>();
                hungerDisplay.SetValue(food);
            }         
        }
        if (Input.GetKey(KeyCode.W))
        {
            // walk
            float angleRadians = rb.rotation * Mathf.PI / 180f;
            float xSpeed = Mathf.Cos(angleRadians) * speed;
            float ySpeed = Mathf.Sin(angleRadians) * speed;
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
        {
            // do not move
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }      

        if (Input.GetKey(KeyCode.A))
        {
            // and turn left
            rb.rotation += angularVelocity * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // and turn right
            rb.rotation -= angularVelocity * Time.deltaTime;
        }
        timeToNextFood -= Time.deltaTime;
        if (timeToNextFood < 0)
        {
            timeToNextFood = TIME_TO_FOOD;
            --food;
            if (food <= 3)
            {
                speed = SPEED_HUNGRY;
            }
            else if (food <= 6)
            {
                speed = SPEED_NORMAL;
            }
            else
            {
                speed = SPEED_WHEN_FULL;
            }
            if (hungerDisplay != null)
            {
                hungerDisplay.SetValue(food);
            }            
            if (food <= 0)
            {

                gameOverDisplay.SetActive(true);

                Time.timeScale = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // timeScale is float
            // check if zero
            if (Time.timeScale < 0.001f)
            {
                Time.timeScale = 1;
                // restart
                food = 4;
                rb.rotation = 0;
                rb.velocity = Vector2.zero;
                rb.position = Vector2.zero;
                if (hungerDisplay != null)
                {
                    hungerDisplay.SetValue(food);
                }
                GameObject worldGameObject = GameObject.Find("World");
                // get World script object
                World world = worldGameObject.GetComponent<World>();
                world.ResetWorms();

                gameOverDisplay.SetActive(false);

            }
            else
            {
                Time.timeScale = 0;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Worm"))
        {
            ++food;
            int foodMax = 9;
            if (food > foodMax)
            {
                food = foodMax;
            }
            else
            {
                if (hungerDisplay != null)
                {
                    hungerDisplay.SetValue(food);
                }
                Destroy(collision.gameObject);
            }         
        }
    }
}
