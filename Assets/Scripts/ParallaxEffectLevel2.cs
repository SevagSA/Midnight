using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffectLevel2 : MonoBehaviour
{
    private float length, startpos;
    public GameObject camera;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (camera.transform.position.x * parallaxEffect);
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
