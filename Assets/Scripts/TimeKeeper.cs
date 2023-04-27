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
    [SerializeField] private GameObject paused;
    [SerializeField] private int ticks;

    public MapControls mapControls;
    private int modifiedTimeScale; // Getter Setter / Property

    public float foreverTimer; // This will eventually need to be reset.  I think.  It depends on if we run out of numbers.
    public float minutes;
    public int hours;
    public int days = 0;

    public int oldTimeSpeed;
    public bool isAlreadyPaused;

    public int Hours
    {
        get { return hours; }
        set
        {
            hours = value;

            // This will not trigger on day zero.
            if(hours == 0)
            {
                DayStart?.Invoke();
                Debug.Log("Hour is ZERO!!!");
            }

            if(hours == 17)
            {
                WorkDayOver?.Invoke();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        mapControls = new MapControls();  // This sets up a new control scheme for this script. This sentence doesn't mean anything to me.
        ModifiedTimeScale = 1;
        isAlreadyPaused = false;
    }
    private void Start()
    {
        mapControls.Keyboard.Spacebar.started += _ => PauseandUnpause();
        oldTimeSpeed = 1;
    }

    private void OnEnable()
    {
        mapControls.Enable();
    }

    private void OnDisable()
    {
        mapControls.Disable();
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
        ModifiedTimeScale = 0;
        oldTimeSpeed = 0;
        Debug.Log("Speedx0 - modifiedScale has changed to " + ModifiedTimeScale);
    }

    public void TimeSpeedx1()
    {
        ModifiedTimeScale = 1;
        oldTimeSpeed = 1;
        Debug.Log("Speedx1 - modifiedScale has changed to " + ModifiedTimeScale);
    }

    public void TimeSpeedx2()
    {
        ModifiedTimeScale = 2;
        oldTimeSpeed = 2;
        Debug.Log("Speedx2 - modifiedScale has changed to " + ModifiedTimeScale);
    }

    public void TimeSpeedx4()
    {
        ModifiedTimeScale = 4;
        oldTimeSpeed = 4;
        Debug.Log("Speedx4 - modifiedScale has changed to " + ModifiedTimeScale);
    }

    public void TimeSpeedx8()
    {
        ModifiedTimeScale = 8;
        oldTimeSpeed = 8;
        Debug.Log("Speedx8 - modifiedScale has changed to " + ModifiedTimeScale);
    }

    public void PauseandUnpause()
    {
        if (ModifiedTimeScale > 0)
        {
            oldTimeSpeed = ModifiedTimeScale;
            ModifiedTimeScale = 0;

        }
        else
        {
            ModifiedTimeScale = oldTimeSpeed;
        }
        //Debug.Log($"Modified Time: {ModifiedTimeScale} and Old Time Speed: {oldTimeSpeed}.");
    }

    public void OnPanelEnable()
    {
        mapControls.Keyboard.Spacebar.Disable();
        if (Time.timeScale != 0)
        {
            PauseandUnpause();
            isAlreadyPaused = false;

        }
        else
        {
            isAlreadyPaused = true;
        }
    }

    public void OnPanelDisable()
    {
        mapControls.Keyboard.Spacebar.Enable();
        if (isAlreadyPaused == false)
        {
            ModifiedTimeScale = oldTimeSpeed;
        }
    }

    // Change this to an event, probably.
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
            if (modifiedTimeScale == 0)
            {
                paused.SetActive(true);
            }
            else
            {
                paused.SetActive(false);
            }
        }
    }
}
