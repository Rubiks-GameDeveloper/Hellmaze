using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float Horizontal;
    public float Vertical;

    private bool isTouchEnter;

    private int TouchID;

    private float TouchFieldCoefficent;
    private void Start()
    {
        TouchFieldCoefficent = Screen.width - GetComponent<RectTransform>().rect.width;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        for (int t = Input.touchCount - 1; t >= 0; t--)
        {
            if (Input.touches[t].position.x > TouchFieldCoefficent)
            {
                TouchID = Input.touches[t].fingerId;
                isTouchEnter = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouchEnter = false;

        TouchID = 20;

        Horizontal = 0;
        Vertical = 0;
    }

    private void FixedUpdate()
    {
        List<Touch> temp = new List<Touch>(Input.touches);
        if (TouchID <= Input.touchCount - 1)
        {
            //Debug.Log(TouchID.ToString() + " - TouchID" + temp.Contains(Input.touches[TouchID]) + " - Temp Contains" + Input.touches[TouchID].phase + " - Phase");
            if (temp.Contains(Input.touches[TouchID]) && Input.touches[TouchID].phase == TouchPhase.Moved && Input.touches[TouchID].position.x > TouchFieldCoefficent && isTouchEnter)
            {
                Horizontal = Input.touches[TouchID].deltaPosition.x;
                Vertical = Input.touches[TouchID].deltaPosition.y;

            }
            if (temp.Contains(Input.touches[TouchID]) && Input.touches[TouchID].phase == TouchPhase.Stationary /*|| Input.touches[TouchID].position.x <= TouchFieldCoefficent*/ && isTouchEnter)
            {
                Horizontal = 0;
                Vertical = 0;
            }
            if (Input.touches[TouchID].position.x < TouchFieldCoefficent)
            {
                Horizontal = 0;
                Vertical = 0;
            }
        }
    }
}
