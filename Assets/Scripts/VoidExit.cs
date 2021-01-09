using UnityEngine;

public class VoidExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Application.Quit();
        }
    }
}
