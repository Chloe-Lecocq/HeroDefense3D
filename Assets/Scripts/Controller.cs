using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    //Menu
    public WheelMenuMB WheelMenuPrefab;
    protected WheelMenuMB WheelMenuInstance;

    public static Controller instance;

    [HideInInspector]
    public ControllerMode Mode;
    public float CameraDistance;
    public Button startBtn;
    public Button quitBtn;
    public Button restart;
    public Text GameOver;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;

        SetMode(ControllerMode.StartMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode== ControllerMode.StartMenu)
        {
            Time.timeScale = 0;
            startBtn.gameObject.SetActive(true);
            quitBtn.gameObject.SetActive(true);
            startBtn.onClick.AddListener(setPlay);
            quitBtn.onClick.AddListener(doExitGame);


        }
        else
        {
            Time.timeScale = 1;
            startBtn.gameObject.SetActive(false);
            quitBtn.gameObject.SetActive(false);
        }
        if (Mode == ControllerMode.Lost)
        {
            Time.timeScale = 0;
            GameOver.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            GameOver.gameObject.SetActive(false);
        }

        if (Mode == ControllerMode.Play)
        {
            CameraDistance = Mathf.Clamp(CameraDistance + Input.mouseScrollDelta.y, 0, 10);
        }
            if (Mode == ControllerMode.Play)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetMode(ControllerMode.Menu);
                WheelMenuInstance = Instantiate(WheelMenuPrefab, FindObjectOfType<Canvas>().transform);
                WheelMenuInstance.callback = MenuClick;
            }
        }

        else if (Mode == ControllerMode.Menu)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetMode(ControllerMode.Play);
            }
        }

    }

    private void MenuClick(string path)
    {
        var paths = path.Split('/');
        SetMode(ControllerMode.Play);
        GetComponent<PlaceTower>().SetPrefab(int.Parse(paths[1]));
    }

    public void SetMode(ControllerMode mode)
    {
        Mode = mode;

        if (mode != ControllerMode.Menu && WheelMenuInstance != null)
            Destroy(WheelMenuInstance.gameObject);

        switch (mode)
        {
            case ControllerMode.StartMenu:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case ControllerMode.Menu:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case ControllerMode.Play:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

    void setPlay()
    {
        SetMode(ControllerMode.Play);
    }
    public void setLost()
    {
        SetMode(ControllerMode.Lost);
    }

    void doExitGame()
    {
        Application.Quit();
    }

    public enum ControllerMode
    {
        StartMenu,
        Play,
        Menu,
        Lost
    }
}
