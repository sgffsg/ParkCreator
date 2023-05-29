using UnityEngine;
//using UnityEngine.EventSystems;

public class TouchEventSystem : MonoBehaviour
{
    public delegate void ZoomSwype(float velocity);
    public event ZoomSwype zoomSwypeMessage;
    public delegate void DoubleTouch();
    public event DoubleTouch doubleTouchMessage;
    public delegate void OcnceTouch();
    public event OcnceTouch touchMessage;
    [SerializeField, Range(1f, 50f)] private float zumSpeed;
    [SerializeField, Range(0, 5f)] private float treshold;
    private Vector2 touch0start;
    private Vector2 touch1start;
    private int TapCount=0;
    [SerializeField, Range(0.1f, 5f)] private float MaxDubbleTapTime;
    private float NewTime;
    public static TouchEventSystem instance;
    private void Start() {
        instance = this;   
    }
    void Update()
    {
        if (GlobalSettings.instance.Play || !GlobalSettings.instance.Play && GlobalSettings.instance.Build || !GlobalSettings.instance.Play && !GlobalSettings.instance.Build)
            CheckZoomSwype();
        if (!GlobalSettings.instance.Play)
        {
            
            CheckDoubleTouch();
            CheckTouch();
        }
        
    }

    private void CheckZoomSwype()
    {
        if (!GlobalSettings.instance.Mobile)
        {
            //Приближение колесиком мыши [PC]
            GlobalSettings.instance.Zoom = true;
            float mouseWhellCount = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWhellCount != 0)
            {
                zoomSwypeMessage?.Invoke(-mouseWhellCount * 2000);
            }
            else
            {
                GlobalSettings.instance.Zoom = false;
            }
        }
        else
        {
            //Приближение при помощи жеста [Mobile]
            if (Input.touchCount == 2)
            {
                GlobalSettings.instance.Zoom = true;
                if (touch0start == Vector2.zero && touch1start == Vector2.zero)
                {
                    touch0start = Input.GetTouch(0).position;
                    touch1start = Input.GetTouch(1).position;
                }

                //Обновление позиций
                Vector2 f0position = Input.GetTouch(0).position;
                Vector2 f1position = Input.GetTouch(1).position;

                float deltaTouch = Vector2.Distance(touch0start, touch1start) - Vector2.Distance(f0position, f1position);

                float dir = Mathf.Sign(deltaTouch);

                
                if (deltaTouch > treshold || deltaTouch < -treshold)
                    zoomSwypeMessage?.Invoke(zumSpeed * dir);

                
                touch0start = f0position;
                touch1start = f1position;

                return;
            }
            else //Обнуление координат после окончания свайпа
            {
                GlobalSettings.instance.Zoom = false;
                touch0start = Vector2.zero;
                touch1start = Vector2.zero;
            }
            return;
        }
    }

    private void CheckDoubleTouch()
    {
        if (!GlobalSettings.instance.Mobile)
        {
            //Двойное нажатие [PC]
            if (Input.GetMouseButtonDown(0))
            {
                TapCount += 1;

                if (TapCount == 1)
                {
                    NewTime = Time.time + MaxDubbleTapTime;
                }
                else if (TapCount == 2 && Time.time <= NewTime)
                {
                    doubleTouchMessage?.Invoke();
                    TapCount = 0;
                }

                if (Time.time > NewTime)
                    TapCount = 0;
            }
        }
        else
        {
            //Двойное нажатие [Mobile]
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                    TapCount += 1;

                if (TapCount == 1)
                {
                    NewTime = Time.time + MaxDubbleTapTime;
                }
                else if (TapCount == 2 && Time.time <= NewTime)
                { 
                    doubleTouchMessage?.Invoke();
                    TapCount = 0;
                }
            }

            if (Time.time > NewTime)
                TapCount = 0;
        }
    }

    private void CheckTouch()
    {
        if (!GlobalSettings.instance.Mobile)
        {
            //Одинарное нажатие [PC]
            if (Input.GetMouseButtonDown(0))
            {
                touchMessage?.Invoke();
            }
        }
        else
        {
            //Одинарное нажатие [Mobile]
            if (Input.touchCount == 1)
            {
                touchMessage?.Invoke();
            }
        }
    }
}
