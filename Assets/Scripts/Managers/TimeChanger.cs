using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : MonoBehaviour, IInteractable
{
    DayNightCycle dayNightCycle;
    [Range(0f, 1f)] public float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        dayNightCycle = GameManager.Instance.DayNightCycle;
    }

    private void ChangeTime(DayNightCycle dayNightCycle)
    {
        dayNightCycle.time = targetTime;
    }

    public string GetInteractMsg()
    {
        return string.Format("Time Change {0}", targetTime);
    }

    public void OnInteract(Player player)
    {
        if (dayNightCycle != null)
        {
            ChangeTime(dayNightCycle);
        }
    }



}
