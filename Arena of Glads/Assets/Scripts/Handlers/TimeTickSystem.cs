using System;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    public static TimeTickSystem instance;

    public class OnTickEventArgs : EventArgs { public int tick; }

    public static event EventHandler<OnTickEventArgs> OnTick;
    public static event EventHandler<OnTickEventArgs> OnTick_100;
    public static event EventHandler<OnTickEventArgs> OnTick_1000;

    private const float TICK_TIMER_MAX = 0.01f; // Change Log - ( 0.5f -> 0.01f ) // 100 TICK = 1 Second

    private int tick;
    private float tickTimer;


    private void Awake()
    {
        tick = 0;
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;

            OnTick?.Invoke(this, new OnTickEventArgs { tick = tick });
            if (tick % 100 == 0) { OnTick_100?.Invoke(this, new OnTickEventArgs { tick = tick }); }
            if (tick % 1000 == 0) { OnTick_1000?.Invoke(this, new OnTickEventArgs { tick = tick }); }
        }
    }
}
