using UnityEngine;
using UnityEngine.UI;

public class playermovement : MonoBehaviour
{
    CharacterController controller;
    InputHandler inputHandler;

    public Transform respawnPoint;
    public Transform groundChecker;
    public Transform ceilChecker;

    public float speed = 7f;
    public float runmult = 2f;
    public float gravity = 25f;
    public float groundDist = 0.25f;
    public float ceilDist = 0.25f;
    public float jumpHeight = 2f;
    public float verticalOOB = -25f;

    public float standHeight = 2f;
    public float crouchHeight = 1f;

    public float fireDelay = 0.5f;

    float fireTimer = 0f;

    public GameObject gun;
    public Transform rayOrigin;
    bool gunActive = false;

    public float hitDist = 100f;

    public float crouchSpeedMult = 0.5f;

    public LayerMask envMask;
    public LayerMask hitMask;

    public AudioSource fire;
    public AudioSource lower;
    public AudioSource raise;

    public ParticleSystem flash;

    public GameObject targetHitFX;
    public GameObject wallHitFx;
    public GameObject damnum;

    public Image crosshair;

    Vector3 movement;
    Vector3 velocity;

    bool isGrounded;
    bool hitCeiling = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDist, envMask);
        hitCeiling = Physics.CheckSphere(ceilChecker.position, groundDist, envMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }

        if (hitCeiling && velocity.y > 0)
        {
            velocity.y = 0f;
        }

        movement = inputHandler.walkDirection();

        if (inputHandler.isRunning())
        {
            movement *= runmult;
        }

        if (isGrounded && inputHandler.isJumping() && !hitCeiling)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            groundChecker.transform.localPosition = new Vector3(0, -1f, 0);
        }

        if(inputHandler.isCrouching())
        {
            controller.height = crouchHeight;
            groundChecker.transform.localPosition = new Vector3(0, -crouchHeight / 2, 0);
            movement *= crouchSpeedMult;
        }

        else
        {
            controller.height = standHeight;
            groundChecker.transform.localPosition = new Vector3(0, -standHeight / 2, 0);
        }

        velocity.y += gravity * Time.deltaTime * -1f;

        controller.Move(velocity * Time.deltaTime);

        controller.Move(movement * speed * Time.deltaTime);

        if (groundChecker.transform.position.y < verticalOOB)
        {
            gameObject.transform.position = respawnPoint.position;
            velocity = new Vector3(0, 0, 0);
        }

        if (inputHandler.isQuitting())
        {
            Application.Quit();
        }

        if (gunActive && inputHandler.isAttacking() && fireTimer <= 0f)
        {
            fire.Play();
            fireTimer = fireDelay;
            flash.Play();
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, hitDist, hitMask))
            {
                if(hit.collider.gameObject.tag == "hitable")
                {
                    GameObject dn = Instantiate(damnum);
                    dn.transform.position = rayOrigin.position + rayOrigin.transform.forward * hit.distance;
                    GameObject hfx = Instantiate(targetHitFX);
                    hfx.transform.position = rayOrigin.position + rayOrigin.transform.forward * hit.distance;
                }

                else
                {
                    GameObject hfx = Instantiate(wallHitFx);
                    hfx.transform.position = rayOrigin.position + rayOrigin.transform.forward * hit.distance;
                }
            }
        }

        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void activateGun()
    {
        raise.Play();
        crosshair.gameObject.SetActive(true);
        gun.SetActive(true);
        gunActive = true;
    }

    public void deactivateGun()
    {
        lower.Play();
        crosshair.gameObject.SetActive(false);
        gun.SetActive(false);
        gunActive = false;
    }
}
