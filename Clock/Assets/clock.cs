using UnityEngine;
using System;

public class clock: MonoBehaviour {
    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;

    public bool continuous;

    const float degreePerHour = 30f;
    const float degreePerMinute = 6f;
    const float degreePerSecond = 6f;

    void Update() {
        if (continuous) {
            UpdateContinuous();
        } else {
            UpdateDiscrete();
        }
    }

    void UpdateContinuous() {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalHours * degreePerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * degreePerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalSeconds * degreePerSecond, 0f);
    }

    void UpdateDiscrete() {
        DateTime time = DateTime.Now;
        hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * degreePerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * degreePerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, time.Second * degreePerSecond, 0f);
    }
}