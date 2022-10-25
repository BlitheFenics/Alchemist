using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTime
{
    public int hour;
    public int minute;

    public GameTime(int hour, int minute)
    {
        this.hour = hour;
        this.minute = minute;
    }

    public GameTime(GameTime timeStamp)
    {
        this.hour = timeStamp.hour;
        this.minute = timeStamp.minute;
    }

    public void UpdateClock()
    {
        minute++;

        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if(hour >= 24)
        {
            hour = 0;
        }
    }

    public static int HourToMinute(int hour)
    {
        return hour * 60;
    }

    public static int CompareTimeStamps(GameTime timestamp1, GameTime timestamp2)
    {
        //int timestamp1Minutes = HourToMinute(timestamp1.hour) + timestamp1.hour;
        //int timestamp2Minutes = HourToMinute(timestamp2.hour) + timestamp2.hour;
        int difference = timestamp1.hour - timestamp2.hour;
        return Mathf.Abs(difference);
    }
}
