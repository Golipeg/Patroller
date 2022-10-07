using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;


public class PatrollController : MonoBehaviour
{
    [SerializeField] private float _speed; // отвечает за скорость передвижения
    [SerializeField] private Transform[] _points; // массив точек, по которым будет передвигаться объект
    private int _point; // номер точки к которой будет идти наш объект
    private float _waitTime;
    [SerializeField] private float _startWaitTime;
    private float _currentTime;
    private void Start()
    {
        _waitTime = _startWaitTime;
        _point = 1;
    }

    private void Update()
    {
       // _currentTime += Time.deltaTime;
        //var progress = _currentTime / _speed;
        
        transform.position = Vector3.Lerp(transform.position, _points[_point].position, Time.deltaTime*_speed);
        if (Vector3.Distance(transform.position, _points[_point].position) < 0.2f)
        {
            if (_waitTime <= 0)
            {
                _point++;
                _waitTime = _startWaitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }

            if (_point == _points.Length)
            {
                _point = 0;
                //transform.position = _points[_point].position;
            }
        }
    }
}