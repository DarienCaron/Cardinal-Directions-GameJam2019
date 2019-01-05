using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalClock : MonoBehaviour {

    [SerializeField]
    public static float CurrentTime;

    public float TimeStep = 60f;

    public float TimeofDayToStartAt = 7;

    public static bool DayPassed = false;
    private void Awake()
    {
        CurrentTime = TimeofDayToStartAt;
    }


    private void Update()
    {
        if (!DayPassed)
        {
            CurrentTime += Time.deltaTime / TimeStep;

            CurrentTime = Mathf.Clamp(CurrentTime, 0, 24);
            if (CurrentTime >= 24)
            {
                DayPassed = true;
                CurrentTime = 0;
            }
        }
      

        if(DayPassed)
        {
            
            CurrentTime = Mathf.Lerp(CurrentTime, TimeofDayToStartAt, Time.deltaTime);

            if(CurrentTime >= TimeofDayToStartAt)
            {
                DayPassed = false;
                CurrentTime = TimeofDayToStartAt;
            }
        }
    }


    public static void SetGlobalTime(float time)
    {
        CurrentTime = time;
    }
}
