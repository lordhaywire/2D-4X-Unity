using System;
using TMPro;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    public event Action DayStart;
    public event Action WorkDayOver;

    public static TimeKeeper Instance;
    [SerializeField] private TextMeshProUGUI dayAndTimeText;
    [SerializeField] private TextMeshProUGUI currentSpeedText;
    [SerializeField] private GameObject pausedText;
    [SerializeField] private int ticks;

    public MapControls mapControls;
    private int modifiedTimeScale; // Getter Setter / Property

    public float foreverTimer; // This will eventually need to be reset.  I think.  It depends on if we run out of numbers.
    public float minutes;
    public int hours;
    public int days = 0;

    public int oldTimeSpeed;
    public int numberOfThingsPaused;

    public int Hours
    {
        get { return hours; }
        set
        {
            hours = value;

            // This will not trigger on day zero.
            if (hours == 0)
            {
                DayStart?.Invoke();
                //Debug.Log("Hour is ZERO!!!");
            }

            if (hours == 17)
            {
                WorkDayOver?.Invoke();
            }
        }
    }

    public int ModifiedTimeScale
    {
        get
        {
            return modifiedTimeScale;
        }
        set
        {
            modifiedTimeScale = value;
            Time.timeScale = modifiedTimeScale;
            currentSpeedText.text = "Speed: " + modifiedTimeScale;
            //Debug.Log($"ModifiedScale has changed to {modifiedTimeScale}");
            if (modifiedTimeScale == 0)
            {
                pausedText.SetActive(true);
            }
            else
            {
                pausedText.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        Instance = this;

        mapControls = new MapControls();
        ModifiedTimeScale = 1;
        numberOfThingsPaused = 0;

        if (Globals.Instance.startPaused == true)
        {
            PauseTime();
        }
    }
    private void Start()
    {
        mapControls.Keyboard.Spacebar.performed += _ => PauseandUnpause();
        oldTimeSpeed = 1;
    }

    private void OnEnable()
    {
        mapControls.Enable();
    }

    private void FixedUpdate()
    {
        Clock();
    }

    private void Clock() // Used to calculate sec, min and Hours
    {
        minutes += Time.fixedDeltaTime * ticks; // multiply time between fixed update by tick.
        foreverTimer += Time.fixedDeltaTime * ticks;
        //Debug.Log("Fixed Delta Time: " + Time.fixedDeltaTime);

        if (minutes >= 60) // 60 min = 1 hr
        {
            minutes = 0;
            Hours += 1;
        }

        if (Hours >= 24) // 24 hr = 1 day
        {
            Hours = 0;
            days += 1;
        }
        // To show Days, Hours and minutes.
        dayAndTimeText.text = "Day " + days + " " + string.Format("{0:00}:{1:00}", Hours, minutes);
    }

    public void TimeSpeedx0()
    {
        if(ModifiedTimeScale != 0)
        {
            oldTimeSpeed = ModifiedTimeScale;
            ModifiedTimeScale = 0;
            numberOfThingsPaused++;
        }  
    }

    public void TimeSpeedx1()
    {
        ModifiedTimeScale = 1;
        oldTimeSpeed = 1;
    }

    public void TimeSpeedx2()
    {
        ModifiedTimeScale = 2;
        oldTimeSpeed = 2;
    }

    public void TimeSpeedx4()
    {
        ModifiedTimeScale = 4;
        oldTimeSpeed = 4;
    }

    public void TimeSpeedx8()
    {
        ModifiedTimeScale = 8;
        oldTimeSpeed = 8;
    }

    public void PauseTime()
    {
        Debug.Log("Pause Time!");
        mapControls.Keyboard.Spacebar.Disable();
        numberOfThingsPaused++;
        if (ModifiedTimeScale != 0)
        {
            oldTimeSpeed = ModifiedTimeScale;
            ModifiedTimeScale = 0;
        }
    }

    public void UnpauseTime()
    {
        Debug.Log("Unpause Time!");
        mapControls.Keyboard.Spacebar.Enable();

        numberOfThingsPaused--;
        if (numberOfThingsPaused == 0)
        {
            ModifiedTimeScale = oldTimeSpeed;
        }
    }
    public void PauseandUnpause()
    {
        Debug.Log("Keyboard has been pressed!");
        if (ModifiedTimeScale > 0)
        {
            oldTimeSpeed = ModifiedTimeScale;
            ModifiedTimeScale = 0;
            numberOfThingsPaused++;
        }
        else
        {
            ModifiedTimeScale = oldTimeSpeed;
            numberOfThingsPaused--;
        }
        //Debug.Log($"Modified Time: {ModifiedTimeScale} and Old Time Speed: {oldTimeSpeed}.");
    }

    private void OnDisable()
    {
        mapControls.Disable();
    }
}
