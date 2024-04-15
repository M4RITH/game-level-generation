using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    Camera cam;
    float cameraSize, defaultCameraSize;
    Vector3 position;
    void Start()
    {
        cam = GetComponent<Camera>();
        defaultCameraSize = cam.orthographicSize;
        position = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraSize = cam.orthographicSize;
        if (Input.GetKey(KeyCode.Z))
        {
            cameraSize += 1;
        }
        if (Input.GetKey(KeyCode.X))
        {
            cameraSize -= 1;
        }

        if(cameraSize > 82)
        {
            cameraSize = 82;
        }
        if(cameraSize < 1)
        {
            cameraSize = 1;
        }

        cam.orthographicSize = cameraSize;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 1f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, -1f, 0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(1f, 0f, 0f);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetCamera()
    {
        cam.orthographicSize = defaultCameraSize;
        cam.transform.position = position;
    }
}
