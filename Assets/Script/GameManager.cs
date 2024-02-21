using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{

    private float cameraHeight;
    private float cameraWidth;
    private Vector2 cameraBottomLeft;
    private Vector2 cameraTopRight;
    private Vector2 cameraTopLeft;
    private Vector2 cameraBottomRight;
    private Vector2 cameraCenter;

    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public TMP_Text restartText;

    private static GameManager instance;

    public float CameraHeight => cameraHeight;
    public float CameraWidth { get { return cameraWidth; } }
    public Vector2 CameraBottomLeft { get { return cameraBottomLeft; } }
    public Vector2 CameraBottomRight { get { return cameraBottomRight; } }
    public Vector2 CameraTopLeft { get { return cameraTopLeft; } }
    public Vector2 CameraTopRight { get { return cameraTopRight; } }
    public Vector2 CameraCenter { get {  return cameraCenter; } }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Found more than one Manager");
        }
        instance = this;
    }

    public static GameManager Instance()
    {
        return instance;
    }

    void Start()
    {
        CalculateCameraBounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (SceneManager.GetActiveScene().name != "FinalScene")
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    public void GameOver()
    {
        Camera cam = Camera.main;
        gameOverPanel.SetActive(true);
        gameOverText.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 1f, 0);
        restartText.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y-1f, 0);
    }

    void CalculateCameraBounds()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        if (cam.orthographic)
        {
            cameraHeight = cam.orthographicSize * 2;
            cameraWidth = cameraHeight * cam.aspect;

            cameraBottomLeft = new Vector2(cam.transform.position.x - cameraWidth / 2, cam.transform.position.y - cam.orthographicSize);
            cameraTopRight = new Vector2(cam.transform.position.x + cameraWidth / 2, cam.transform.position.y + cam.orthographicSize);

            cameraTopLeft = new Vector2(cameraBottomLeft.x, cameraTopRight.y);
            cameraBottomRight = new Vector2(cameraTopRight.x, cameraBottomLeft.y);

            Debug.Log($"Camera Bounds:\nTop Left: {cameraTopLeft}\nTop Right: {cameraTopRight}\nBottom Left: {cameraBottomLeft}\nBottom Right: {cameraBottomRight}\n Height: {cameraHeight}\n Width: {cameraWidth}");
        }
        else
        {
            Debug.LogError("Camera is not orthographic.");
        }
    }


}
