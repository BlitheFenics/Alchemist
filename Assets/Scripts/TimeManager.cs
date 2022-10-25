using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField]
    GameTime timeStamp;
    public float timeScale = 1.0f;

    List<TimeTracker> listeners = new List<TimeTracker>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeStamp = new GameTime(6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1/timeScale);
            Tick();
        }
    }

    public void Tick()
    {
        timeStamp.UpdateClock();

        foreach(TimeTracker listener in listeners)
        {
            listener.ClockUpdate(timeStamp);
        }
    }

    public GameTime GetGameTime()
    {
        return new GameTime(timeStamp);
    }

    public void RegisterTracker(TimeTracker listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker(TimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
