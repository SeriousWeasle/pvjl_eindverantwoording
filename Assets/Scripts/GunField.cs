using UnityEngine;

public class GunField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<playermovement>().activateGun();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<playermovement>().deactivateGun();
        }
    }
}
