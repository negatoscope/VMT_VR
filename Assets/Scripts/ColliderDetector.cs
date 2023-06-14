using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    [HideInInspector]
    public bool collided = false;

    public float gridCounter;
    public float backtrackCounter;
    List<GameObject> enteredGrids = new List<GameObject>();

    // Start is called before the first frame updated
    void Start()
    {
        gridCounter = 0;
        backtrackCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //print(gridCounter);
        //print(backtrackCounter);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grid"))
        {
            if (enteredGrids.Contains(other.gameObject))
            {
                backtrackCounter += 1; //count backtracking
                return;
            }
            else
            {
                enteredGrids.Add(other.gameObject);
                gridCounter += 1; //add count
            }
        }
    }
}
