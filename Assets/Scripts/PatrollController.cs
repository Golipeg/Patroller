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
    private Vector3 _startPosition;// точка старта
    private Vector3 _endPosition;
    [SerializeField] float _pauseDuration=2f;
    private float _pauseBetweenMovingToNextPoint;//пауза перед началом движения к следующей точке
    private float _arrivalDuration;// длительность пути от точки до точки
    private int _currentPointIndex;// наш текущий индекс
    private float _currentTime;

    private void Awake()
    {
        SetNextPoint();
    }

    private void SetNextPoint()
    {
        var nextPointIndex = (_currentPointIndex + 1) % _points.Length;// узнаем номер следующей точки, куда пойдёт наш объект.
        _startPosition = _points[_currentPointIndex].position;
        _endPosition = _points[nextPointIndex].position;
        /// узнаем расстояние между точек 
        _arrivalDuration = Vector3.Distance(_startPosition, _endPosition) / _speed;// делим на speed т.к. Distance должна быть от 0 до 1;
        _currentPointIndex = nextPointIndex;
    }

    private void Update()
    {///
     /// сгенерили паузу между передвижением к след.точки
     
        _pauseBetweenMovingToNextPoint += Time.deltaTime;
        if ( _pauseBetweenMovingToNextPoint<= _pauseDuration)
        {
            return;
        }
        _currentTime += Time.deltaTime;
        var progress = _currentTime / _arrivalDuration;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, progress);
        if (_currentTime > _arrivalDuration)
        {
            _currentTime = 0f;
            _pauseBetweenMovingToNextPoint = 0f;
            SetNextPoint();
        }
    }
}