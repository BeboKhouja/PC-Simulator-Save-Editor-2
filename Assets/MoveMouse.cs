using UnityEngine;

public class MoveMouse : MonoBehaviour
{
    [SerializeField] public float speed = 2.0f;
    [SerializeField] private Camera camera;

    void Start() {
        Input.simulateMouseWithTouches = true; /*
            We dont want to accidentally trigger the mouse when using the touch screen while pressing 2 fingers,
            but then that has the side effect of not being able to click the buttons on the world.
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) {
            camera.transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * speed;
        }
    }
}
