using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControls : MonoBehaviour
{

    //Объявить делегат
    public delegate void SphereLeftDelegate();

    //Объявление события
    public event SphereLeftDelegate SphereLeftEvent;

    [Header("Main Sphere"), SerializeField]
    public Transform _sphere;

    private void OnTriggerEnter(Collider _mustBeSphereCollider)
    {
        if ((_sphere.GetComponent<Collider>() == _mustBeSphereCollider) && (SphereLeftEvent != null))
        {
            SphereLeftEvent();
        }

    }
}
