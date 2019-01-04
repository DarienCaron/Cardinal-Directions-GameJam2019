using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUi : MonoBehaviour {

    Text TextLabel;
	void Start () {
        TextLabel = GetComponent<Text>();
	}

    private void Update()
    {
        int TextTime = (int)GlobalClock.CurrentTime;
        TextLabel.text = TextTime.ToString();
    }
}
