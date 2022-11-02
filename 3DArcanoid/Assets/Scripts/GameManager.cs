using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Arcanoid
{
    public class GameManager : MonoBehaviour
    {
        [Tooltip("Incr. speed platform"), SerializeField]
        public UnityEngine.ForceMode _platformAcceleration = (ForceMode)1;

        //Здесь заданы препятствия всех уровней
        [Header("Obstacles massive"), SerializeField]
        public Transform[] _obstaclesOnLevel;

        //Задать сферу
        [Header("Sphere"), SerializeField]
        public Transform _sphere;

        //Определить ворота
        [Header("Gates"), SerializeField]
        private List<GateControls> _gates;


        //Счётчик жизней
        [Header("Life Count"), SerializeField]
        private int _numberOfLife = 10;

        //Камеры
        [Header("Cameras"), SerializeField]
        public Transform _camera1;
        public Transform _camera2;

        private byte _currentLevel = 1;

        void Start()
        {
            RotateObstacles();
            ObstacleEventHandler();
            InitGates();
        }

        private void RotateObstacles()
        {
            foreach (var _obstacle in _obstaclesOnLevel)
            {
                _obstacle.rotation = Random.rotation;
            }
        }

        private void DesactivateObstacleInMassive(Transform _transform)
        {
            switch (_currentLevel)
            {
                case 1:
                    DesactivateObstacle(_transform, _obstaclesOnLevel);
                    LevelCheck(_obstaclesOnLevel);
                    break;
            }
        }

        private void DesactivateObstacle(Transform _transform, Transform[] _obstacles)
        {
            foreach (var _obstacle in _obstacles)
            {
                if (_transform == _obstacle)
                {
                    _transform.gameObject.SetActive(false);
                    //Увеличение скорость движения шара
                    _sphere.GetComponent<SphereControls>().IncreaseSpeed();
                    return;
                }
            }
        }

        private void LevelCheck(Transform[] _obstacles)
        {
            foreach (var _obstacle in _obstacles)
            {
                if (_obstacle.gameObject.activeSelf == true)
                {
                    return;
                }
            }
        }

        private void ObstacleEventHandler()
        {
            _sphere.GetComponent<SphereControls>().DesactivateObstacleEvent += DesactivateObstacleInMassive;
        }

        private void InitGates()
        {
            _gates.ForEach(gate => gate.OnDetect += UpdateLifeCount);
        }

        //Счёт жизней
        private void UpdateLifeCount()
        {
            _numberOfLife -= 1;
            if (_numberOfLife > 0)
            {
                Debug.Log("Life Count: " + _numberOfLife);
            }
            else
            {
                Debug.Log("Life Count: " + _numberOfLife + " GAME OVER.");
                EditorApplication.isPaused = true;
            }
            //Возврат и остановка шара
            _sphere.GetComponent<SphereControls>().SphereReturn();
        }
    }
}