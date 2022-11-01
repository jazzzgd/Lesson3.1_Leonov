using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Arcanoid
{
    public class SphereControls : MonoBehaviour
    {
        Coroutine _moveSphereCoroutine = null;
        Quaternion _sphereStartRotation = new Quaternion();
        static Vector3 _sphereStartLocationLevel1 = new Vector3(6, 1, 1);
        public static Vector3 _sphereStartLocation = _sphereStartLocationLevel1;

        [SerializeField] private GameManager GameManager;

        [Tooltip("Speed sphere")] public float _moveSpeed = 1;
        [Tooltip("Max speed sphere")] public float _maxMoveSpeed = 5;

        public PlatformControls1 controls;
        public delegate void DesactivateObstacleDelegate(Transform _transform);
        public event DesactivateObstacleDelegate DesactivateObstacleEvent;

        private void Awake()
        {
            controls = new PlatformControls1();
        }

        private void OnEnable()
        {
            controls.ActionMap.Enable();
            controls.ActionMap.Start.performed += OnLaunch;
        }

        private void OnDisable()
        {
            controls.ActionMap.Start.performed -= OnLaunch;
            controls.ActionMap.Disable();
        }

        public void OnLaunch(CallbackContext context)
        {
            _moveSphereCoroutine = StartCoroutine(MoveForward());
        }

        public void Start()
        {
           _sphereStartRotation = transform.rotation;
        }

        private IEnumerator MoveForward()
        {
            while (true)
            {
                transform.position += _moveSpeed * Time.deltaTime * transform.forward;
                yield return null;
            }
        }

        public void OnCollisionEnter(Collision _collision)
        {
            Vector3 _beforeCollision = transform.forward;

            ContactPoint _contact = _collision.GetContact(0);

            Vector3 _afterCollision = Vector3.Reflect(_beforeCollision, _contact.normal);

            transform.forward = _afterCollision;

            if (DesactivateObstacleEvent != null)
            {
                DesactivateObstacleEvent(_collision.transform);
            }
        }

        public void SphereReturn()
        {
            transform.position = _sphereStartLocation;
            transform.rotation = _sphereStartRotation;
            StopCoroutine(_moveSphereCoroutine);
            Debug.Log("Get back sphere");
        }
        public void IncreaseSpeed()
        {
            if (_moveSpeed < _maxMoveSpeed)
            {
                _moveSpeed = _moveSpeed + 0.1f;
                Debug.Log("Obstacle destroyed, sphere speed increase " + _moveSpeed);
            }
            else
            {
                Debug.Log("Obstacle destroyed, increased max speed sphere");
            }
        }
    }
}
