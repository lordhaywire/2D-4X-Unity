using TMPro;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayAndTimeText;
    [SerializeField] private TextMeshProUGUI currentSpeedText;
    [SerializeField] private GameObject paused;
    [SerializeField] private int ticks;

    private MapControls mapControls;
    private int modifiedTimeScale; // Getter Setter / Property

    private float seconds;
    private int mins;
    private int hours;
    private int days = 0;

    private int oldTimeSpeed;

    private void Awake()
    {
        mapControls = new MapControls();  // This sets up a new control scheme for this script. This sentence doesn't mean anything to me.
        ModifiedTimeScale = 1;
    }
    private void Start()
    {
        mapControls.Keyboard.Spacebar.started += _ => PauseWithSpacebar();
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

    private void Clock() // Used to calculate sec, min and hours
    {
        seconds += Time.fixedDeltaTime * ticks; // multiply time between fixed update by tick.

        // We probably could get rid of seconds, but just in case we need it later.

        if (seconds >= 60) // 60 sec = 1 min
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60) //60 min = 1 hr
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24) //24 hr = 1 day
        {
            hours = 0;
            days += 1;
        }
        dayAndTimeText.text = "Day " + days + " " + string.Format("{0:00}:{1:00}", hours, mins);
        
        //Debug.Log("Time: " + string.Format("{0:00}:{1:00}", hours, mins));
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

    private void PauseWithSpacebar()
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
        Debug.Log("Space bar was pressed.");
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
