using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    //Menu
    public WheelMenuMB WheelMenuPrefab;
    protected WheelMenuMB WheelMenuInstance;
    [HideInInspector]
    public ControllerMode Mode;

    // Use this for initialization
    void Start()
    {
        SetMode(ControllerMode.Play);
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(path);
        var paths = path.Split('/');
        SetMode(ControllerMode.Play);

        //GetComponent<PlaceTower>().SetPrefab(int.Parse(paths[1]), int.Parse(paths[2]));
    }

    public void SetMode(ControllerMode mode)
    {
        Mode = mode;

        if (mode != ControllerMode.Menu && WheelMenuInstance != null)
            Destroy(WheelMenuInstance.gameObject);

        switch (mode)
        {
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

    public enum ControllerMode
    {
        Play,
        Menu
    }
}
