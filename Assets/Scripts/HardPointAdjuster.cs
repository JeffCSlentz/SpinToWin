using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HardPointAdjuster : MonoBehaviour {
    public GameObject hardPoint;
    public bool isInUI = false;
    private PlayerStats playerStats;

    private void Awake()
    {

    }

    public void CreateHardPoints(int numHardPoints, float playerRadius)
    {
        DestroyChildren();

        for (int i = 0; i < numHardPoints; i++)
        {
            float playerRadiusCopy = playerRadius;
            GameObject hardPointCopy = Instantiate(hardPoint, gameObject.transform);

            int degreesBetweenHardPoints = 360 / numHardPoints;
            int degreesAround = degreesBetweenHardPoints * i;

            Vector2 hardPointPosition = MathHelper.DegreeToVector2(degreesAround);

            if (isInUI)
            {
                playerRadiusCopy *= 100;
            }

            hardPointPosition *= playerRadiusCopy;

            hardPointCopy.GetComponent<Transform>().localPosition = hardPointPosition;
            hardPointCopy.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, degreesAround));
        }
    }

    private void DestroyChildren()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        foreach (var child in children)
        {
            Destroy(child.gameObject);
        }
        children.Clear();
    }

    // Update is called once per frame
    void Update () {
        /*
        if (hardPoint != null && numHardPoints != lastNumHardPoints)
        {
            List<Transform> children = new List<Transform>();

            foreach (Transform child in transform)
            {
                children.Add(child);
            }
            foreach (var child in children)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < numHardPoints; i++)
            {
                Debug.Log("MAKING HARDPOINT #" + i + "on gameobject" + gameObject);
                GameObject hardPointCopy = Instantiate(hardPoint, gameObject.transform);
                float circleEdge = playerStats.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                int degreesBetweenHardPoints = 360 / numHardPoints;
                int degreesAround = degreesBetweenHardPoints * i;

                Vector2 hardPointPosition = DegreeToVector2(degreesAround);

                if (isInUI)
                {
                    circleEdge *= 100;
                }

                hardPointPosition *= circleEdge;

                hardPointCopy.GetComponent<Transform>().localPosition = hardPointPosition;
                hardPointCopy.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0,0, degreesAround));
            }
            lastNumHardPoints = numHardPoints;
        }
        */
    }

    
}
