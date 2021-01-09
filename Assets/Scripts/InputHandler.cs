using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //get if the jump button is pressed
    public bool isJumping()
    {
        return Input.GetButton("Jump");
    }

    //get 2-component vector for player walking direction
    public Vector3 walkDirection() {
        //get X and Y inputs (a, d and w, s)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //return a 3 component vector of x and z, clamped at 1.25
        return Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1f);
    }

    //Get if jump button is pressed
    public bool isRunning()
    {
        return Input.GetButton("Run");
    }

    //get if fire button is pressed
    public bool isAttacking()
    {
        return Input.GetButton("Fire1");
    }

    //get scrollwheel input for weapon switching
    public float scrollDirection()
    {
        //get scrollwheel input
        return Input.GetAxis("Mouse ScrollWheel");
    }

    public bool isUsing()
    {
        //get if use button is pressed
        return Input.GetButton("Use");
    }
    
    public bool isCrouching()
    {
        return Input.GetButton("Crouch");
    }

    public bool isQuitting()
    {
        return Input.GetButton("Cancel");
    }
}
