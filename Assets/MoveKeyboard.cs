using UnityEngine;
using UnityEngine.InputSystem;

public class MoveKeyboard : MonoBehaviour
{
    [SerializeField] public float speed = 3.0f;
    [SerializeField] private Camera camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                camera.transform.position += camera.transform.right * (speed * 2) * Time.deltaTime;
            } else {
                camera.transform.position += camera.transform.right * speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                camera.transform.position += -camera.transform.right * (speed * 2) * Time.deltaTime;
            } else {
                camera.transform.position += -camera.transform.right * speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                camera.transform.position += camera.transform.forward * (speed * 2) * Time.deltaTime;
            } else {
                 camera.transform.position += camera.transform.forward * speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                camera.transform.position += -camera.transform.forward * (speed * 2) * Time.deltaTime;
            } else {
                camera.transform.position += -camera.transform.forward * speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.Q)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                camera.transform.position += -camera.transform.up * (speed * 2) * Time.deltaTime;
            } else {
                camera.transform.position += -camera.transform.up * speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.E)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                camera.transform.position += camera.transform.up * (speed * 2) * Time.deltaTime;
            } else {
                camera.transform.position += camera.transform.up * speed * Time.deltaTime;
            }
        }
    }
}
