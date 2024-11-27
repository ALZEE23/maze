using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class test : MonoBehaviour
{
    // Reference to the Input Action
    // public TextMeshProUGUI debugText;
    public InputActionAsset inputActions;
    private InputAction gyroAction;

    private Vector3 lastAccelerationValue;

    // Threshold to detect significant movement (to avoid very small changes triggering updates)
    private float movementThreshold = 0.01f;

    void Start()
    {
        InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
        Vector3 accelerationValue = gyroAction.ReadValue<Vector3>();
        // debugText.text = $"Acceleration Changed - X: {accelerationValue.x}, Y: {accelerationValue.y}, Z: {accelerationValue.z}";
    }
    private void OnEnable()
    {
        gyroAction = inputActions.FindAction("Gyro");
        gyroAction.Enable();

        // Initialize last acceleration value
        lastAccelerationValue = Vector3.zero;
    }

    private void OnDisable()
    {
        gyroAction.Disable();
    }

    private void Update()
    {
        // Get current accelerometer value
        Vector3 accelerationValue = gyroAction.ReadValue<Vector3>();

        // Check if the change in acceleration is significant
        if (Vector3.Distance(accelerationValue, lastAccelerationValue) > movementThreshold)
        {
            // Print debug only when there's significant movement
            // debugText.text = $"Acceleration Changed - X: {accelerationValue.x}, Y: {accelerationValue.y}, Z: {accelerationValue.z}";
            Debug.Log($"Acceleration Changed - X: {accelerationValue.x}, Y: {accelerationValue.y}, Z: {accelerationValue.z}");
            // Update lastAccelerationValue to the new value
            lastAccelerationValue = accelerationValue;
        }
    }
}
