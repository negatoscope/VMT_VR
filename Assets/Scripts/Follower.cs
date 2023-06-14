using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    float distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        speed = PlayerPrefs.GetFloat("speed");
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled);
    }
}
