using UnityEngine;

public class Damnum : MonoBehaviour
{
    public float dmg = 12f;
    public float randDiff = 2f;
    public float upspeed = 2f;
    public float lifespan = 2f;

    Transform camera;
    TextMesh text;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        float tdmg = Mathf.Round(Random.Range(dmg - randDiff, dmg + randDiff));
        camera = FindObjectOfType<Camera>().transform;
        text = GetComponent<TextMesh>();
        text.text = tdmg.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * upspeed * Time.deltaTime;
        gameObject.transform.LookAt(camera);
        gameObject.transform.localRotation *= Quaternion.Euler(0, 180, 0);
        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}