using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Arcanoid
{
    public class Platform2InputControls : MonoBehaviour
    {
        [SerializeField] private GameManager GameManager;
        public PlatformControls1 controls;
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

        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var value = controls.ActionMap.MovePlatform2.ReadValue<Vector2>();
            Vector3 direction = new Vector3(0, value.y, -value.x);
            GetComponent<Rigidbody>().AddForce(direction, GameManager._platformAcceleration);
        }
    }
}
