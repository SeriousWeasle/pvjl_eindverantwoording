using UnityEngine;

public class ObjectLifeSpan : MonoBehaviour
{
    public float lifespan = 2.5f;
    float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}
