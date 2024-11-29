using System;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigitbody;
    [SerializeField] Camera camera;


    private float _speed = 5;
    private int _counterOfFlyTrails = 0;
    private float _cameraHight;
    private float _cameraWidth;
    void Update()
    {

        rigitbody.linearVelocity = new Vector2(-_speed, 0);
        ////if (this.transform.position.x > _cameraWidth + 3)
        ////{
        ////    _counterOfFlyTrails++;
        ////}
        ////if (_counterOfFlyTrails > 5)
        ////{
        ////    Destroy(this);
        ////}
        //if (this.transform.position.x > -_cameraWidth + 3)
        //    rigitbody.linearVelocity = new Vector2(0, 0);
    }
}
