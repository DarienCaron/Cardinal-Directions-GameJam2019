using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalClock : MonoBehaviour {

    [SerializeField]
    public static float CurrentTime;

    public float TimeStep = 60f;

    public float TimeofDayToStartAt = 7;
    private void Awake()
    {
        CurrentTime = TimeofDayToStartAt;
    }


    private void Update()
    {
        CurrentTime += Time.deltaTime / TimeStep;

        CurrentTime = Mathf.Clamp(CurrentTime, 0, 24);

        if(CurrentTime >= 24)
        {
            CurrentTime = 0;
        }
    }


    public static void SetGlobalTime(float time)
    {
        CurrentTime = time;
    }
}
