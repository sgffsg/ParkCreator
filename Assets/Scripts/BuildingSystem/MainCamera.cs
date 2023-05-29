using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //Скрипт должен быть компонентом камеры     
    public static MainCamera instance;                                                                  //Ссылка на скрипт
    public TouchEventSystem touchEventSystem;                                                           //Система свайпов
    public Map map;                                                                                     //Карта

    
    [SerializeField, Range(1f, 20f)] private float minZumDistance;                                      //Минимальная дальность приближения
    [SerializeField, Range(1f, 35f)] private float maxZumDistance;                                      //Максимальная дальность приближения
    public Vector2 minCamDistance;                                                                      // [Левая]  и   [Нижняя]    Границы для перемещения камеры
    public Vector2 maxCamDistance;                                                                      // [Правая] и   [Верхняя]   Границы для перемещения камеры
    
    private void Start()
    {
        instance = this;
        touchEventSystem.zoomSwypeMessage += ZoomCam;
    }
    
    private void ZoomCam(float velocity)
    {
        transform.position = new Vector3(transform.position.x,
                                            Mathf.Clamp(transform.position.y + Time.deltaTime * velocity, minZumDistance, maxZumDistance),
                                            transform.position.z);
        GlobalSettings.instance.Zoom = false;
    }
}