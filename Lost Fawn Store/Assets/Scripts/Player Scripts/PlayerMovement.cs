using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rg;
    
    // Variables that help pass input into FixedUpdate
    private bool up = false;
    private bool down = false;
    private bool right = false;
    private bool left = false;

    private readonly float speed = 50f;

    public VectorValue startingPos;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Rigidbody2D>() == null)  // Checks if the object has a Rigidbody, adds one if not
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        rg = GetComponent<Rigidbody2D>();
        rg.drag = 10f;
        rg.gravityScale = 0; // Prevents object from falling to what Unity thinks is the bottom
        rg.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevents rotating when colliding with other rb's

        transform.position = startingPos.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Processes input and flips the bool to true only when not already at the speed limit
        if (Input.GetKey(KeyCode.W) == true && rg.velocity.y < speed)
        {
            up = true;
        }
        if (Input.GetKey(KeyCode.S) == true && rg.velocity.y > -speed)
        {
            down = true;
        }
        if (Input.GetKey(KeyCode.D) == true && rg.velocity.x < speed)
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.A) == true && rg.velocity.x > -speed)
        {
            left = true;
        }
    }

    private void FixedUpdate()
    {
        // The physical movement is done in FixedUpdate for consistency
        if (up)
        {
            rg.AddForce(new Vector2(0, speed));
            up = false;
        }
        if (down)
        {
            rg.AddForce(new Vector2(0, -speed));
            down = false;
        }
        if (right)
        {
            rg.AddForce(new Vector2(speed, 0));
            right = false;
        }
        if (left)
        {
            rg.AddForce(new Vector2(-speed, 0));
            left = false;
        }
    }
}
