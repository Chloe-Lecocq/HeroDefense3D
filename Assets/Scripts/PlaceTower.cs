using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    public Tower[] TowerLib;
    protected Tower PrefabTower;
    public Material TransparentMat;
    protected Material TowerMat;

    protected Controller Controller;
    protected Tower CurrentTower;
    protected bool PositionOk;
    public Camera camera;

    void Awake()
    {
        Controller = GetComponent<Controller>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PrefabTower = TowerLib[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentTower != null)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                CurrentTower.transform.position = transform.position + transform.forward * 3 + transform.up * 1.5f;

            }
        }


        if (Input.GetMouseButtonDown(0) && CurrentTower != null)
        {
            CurrentTower.Collider.enabled = true;
            CurrentTower.SetMaterial(TowerMat);
            CurrentTower.SetEtat("built");
            var rot = CurrentTower.transform.rotation;
            CurrentTower = null;
        }

        if (Input.GetKeyDown(KeyCode.A))
            CurrentTower.transform.Rotate(Vector3.forward, 90);

    }

    public void SetNextBrick()
    {
        CurrentTower = Instantiate(PrefabTower);
        CurrentTower.Collider.enabled = false;
        CurrentTower.SetMaterial(TransparentMat);
    }

    public void SetPrefab(int tower)
    {
        PrefabTower = TowerLib[tower];
        SetNextBrick();
    }
}
