using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public GameObject cam1;
    public GameObject cam2;
    public GameObject TextFinish;
    public InputActionAsset inputActions;
    private InputAction gyroAction;
    public float speed = 10.0f;
    public Rigidbody2D rb;

    void Start()
    {
        InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
    }

    void Update()
    {
        Vector3 acceleration = gyroAction.ReadValue<Vector3>();

        Vector2 movement = new Vector2(acceleration.x, acceleration.y) * speed;

        rb.velocity = movement;

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        StartCoroutine(SwitchCameras());
    }

    IEnumerator SwitchCameras()
    {
        yield return new WaitForSeconds(5f);
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    private void OnEnable()
    {
        gyroAction = inputActions.FindAction("Gyro");
        gyroAction.Enable();

    }

    private void OnDisable()
    {
        gyroAction.Disable();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            TextFinish.SetActive(true);
            StartCoroutine(finish());
            IEnumerator finish()
            {
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene("Play");
            }
        }
    }

    void Next()
    {
        SceneManager.LoadScene("Play");
    }
}
