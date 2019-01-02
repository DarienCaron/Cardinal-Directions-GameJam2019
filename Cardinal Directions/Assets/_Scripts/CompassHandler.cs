using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHandler : MonoBehaviour
{

    public GameObject Player;

    private void Update()
    {


        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Player.transform.localEulerAngles.y));
        
    }
}
