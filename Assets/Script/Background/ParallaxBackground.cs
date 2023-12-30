using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float ParallaxEffect;

    private float xPositing;
    private float lenght;
 
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        xPositing = transform.position.x;
    }
 
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - ParallaxEffect);
        float distanceToMove = cam.transform.position.x * ParallaxEffect;

        transform.position = new Vector3(xPositing + distanceToMove, transform.position.y);

        if (distanceMoved > xPositing + lenght)
            xPositing = xPositing + lenght;
        else if (distanceMoved < xPositing - lenght)
            xPositing = xPositing - lenght;
    }
}
