using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
    bool filled;
    float value;

    public void IncrementCylinder(float _value)
    {
        value += _value;
        if(value > 1)
        {
            float leftV = value - 1;
            transform.localScale = new Vector3(0.5f,transform.localScale.y,0.5f);
            PlayerController.current.CreateCylinder(leftV);
        }
        else if(value < 0)
        {
            PlayerController.current.DestroyCylinder(this);
        }
        else
        {
            transform.localScale = new Vector3(value/2,transform.localScale.y,value/2);
            if(_value < 0)
            {
                transform.GetComponent<Renderer>().material.color = Color.red;
            }
            else if(_value > 0)
            {
                transform.GetComponent<Renderer>().material.color = new Color32(255, 128, 0, 255);
            }
        }
    }
}
