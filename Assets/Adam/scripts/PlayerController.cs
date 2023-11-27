using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject head;

    private float _headTilt = 0;

    private CharacterController _characterController;

    private Vector3 _movementX;
    private Vector3 _movementZ;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movement = (_movementX + _movementZ).normalized;

        _characterController.Move(Physics.gravity * Time.deltaTime);
        _characterController.Move(movement * Time.deltaTime * 4);
    }

    public void TiltHead(float mouseYValue)
    {
        _headTilt -= mouseYValue;
        head.transform.localRotation = Quaternion.Euler(_headTilt, 0, 0);
    }

    public void RotateY(float mouseXValue)
    {
        transform.Rotate(0, mouseXValue, 0);
    }

    public void SetMovementX(float horizontalValue)
    {
        _movementX = transform.right * horizontalValue;
    }

    public void SetMovementZ(float verticalValue)
    {
        _movementZ = transform.forward * verticalValue;
    }
}