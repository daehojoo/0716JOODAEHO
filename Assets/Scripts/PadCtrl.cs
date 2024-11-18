using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PadCtrl : MonoBehaviour
{
    
    private RectTransform _touchPad;
    private Vector3 _startPos;
    public float _dragRadius = 125f;
    private int _touchPadID = -1;
    private bool isBtnPressed = false;

    public Vector3 diff;
    [SerializeField] private RocketCtrl _rocketCtrl;
    void Start()
    {
        _touchPad = GetComponent<RectTransform>();
        _startPos = _touchPad.position;
        _rocketCtrl = GameObject.FindWithTag("Player").GetComponent<RocketCtrl>();
    }
    public void ButtonDown()
    {
        isBtnPressed = true;

    }
    public void ButtonUp()
    {
        isBtnPressed = false;
        _touchPad.position = _startPos;
        _rocketCtrl.OnStickPos(Vector3.zero);

    }
    private void FixedUpdate()
    {                       
        if (Application.platform == RuntimePlatform.Android)
        {
            HandleTouchInput();
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            HandleInput(Input.mousePosition);
        }
    }
    void HandleTouchInput()
    {
        int i = 0;
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches) 
            {
                i++;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);
                if (touch.phase == TouchPhase.Began)
                {   
                    if (touch.position.x <= (_startPos.x + _dragRadius))
                    {
                        _touchPadID = i;
                    }
                    if (touch.position.y <= (_startPos.y + _dragRadius))
                    {
                        _touchPadID = i;
                    }
                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (_touchPadID == i)
                    {
                        HandleInput(touchPos);
                    }

                }
                if ((touch.phase == TouchPhase.Ended))
                {
                    if (_touchPadID == i)
                    {
                        _touchPadID = -1;
                        ButtonUp();
                    }
                }
            }
        }
    }
    void HandleInput(Vector3 input)
    {
        if (isBtnPressed)
        { 
            Vector3 diffVector = (input - _startPos);         
            if (diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                diffVector.Normalize();
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }
            else
            {
                _touchPad.position = _startPos;
            }
            diff = _touchPad.position - _startPos;
            Vector2 normalDiff = new Vector2(diff.x / _dragRadius, diff.y / _dragRadius);
            if (_rocketCtrl != null)
            {
                _rocketCtrl.OnStickPos(normalDiff);
            }
        }
        else 
        {
            _touchPad.position = _startPos; 
        }
    }
}
