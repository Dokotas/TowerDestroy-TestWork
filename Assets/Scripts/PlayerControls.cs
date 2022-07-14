using UnityEngine;

public class PlayerControls : Shooter
{
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        CurrentHealth = health;
    }
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchPos = _mainCamera.ScreenToWorldPoint(touch.position);
            cannon.Rotate(touchPos);

            if (touch.phase == TouchPhase.Began)
                cannon.StartShooting();

            if (touch.phase == TouchPhase.Ended)
                cannon.StopShooting();
        }
    }
}