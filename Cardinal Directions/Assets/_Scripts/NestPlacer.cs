using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestPlacer : MonoBehaviour
{
    public GameObject Nest_PreFab;

    public Transform[] NestLocations;

    public Transform CurrentNest;

    [SerializeField]
    float Timer;

    private void Awake()
    {
        Nest_PreFab = Instantiate(Nest_PreFab) as GameObject;
        ChangeNestSpot();
    }



    private void Update()
    {
        Timer += Time.deltaTime / 2;

        if(Timer >= 24)
        {
            Timer = 0;
            ChangeNestSpot();
        }
    }

    void ChangeNestSpot()
    {
        int RandomNumber = Random.Range(0, NestLocations.Length - 1);

        Nest_PreFab.transform.position = NestLocations[RandomNumber].position;
        Debug.Log("NestChange " + NestLocations[RandomNumber].name);
        CurrentNest = NestLocations[RandomNumber];
    }
}
