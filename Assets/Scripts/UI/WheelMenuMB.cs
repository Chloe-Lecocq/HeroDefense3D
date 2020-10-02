using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMenuMB : MonoBehaviour
{
    public WheelMenu Data;
    public DefenseTower DefenseTowerPrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected DefenseTower[] Towers;
    protected WheelMenu Parent;
    [HideInInspector]
    public string Path;

    // Start is called before the first frame update
    void Start()
    {
        var stepLength = 360f / Data.Elements.Length;
        var iconDist = Vector3.Distance(DefenseTowerPrefab.Icon.transform.position, DefenseTowerPrefab.TowerPiece.transform.position);
        //Positionner l'élément
        Towers = new DefenseTower[Data.Elements.Length];
        for (int i = 0; i < Data.Elements.Length; i++)
        {
            Towers[i] = Instantiate(DefenseTowerPrefab, transform);
            Towers[i].transform.localPosition = Vector3.zero;
            Towers[i].transform.localRotation = Quaternion.identity;

            Towers[i].TowerPiece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            Towers[i].TowerPiece.transform.localPosition = Vector3.zero;
            Towers[i].TowerPiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + GapWidthDegree / 2f + i * stepLength);
            Towers[i].TowerPiece.color = new Color(1f, 1f, 1f, 0.5f);

            //set icon
            Towers[i].Icon.transform.localPosition = Towers[i].TowerPiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            Towers[i].Icon.sprite = Data.Elements[i].Icon;

        }
    }

    // Update is called once per frame
    void Update()
    {
        var stepLength = 360f / Data.Elements.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);
        for (int i = 0; i < Data.Elements.Length; i++)
        {
            if (i == activeElement)
                Towers[i].TowerPiece.color = new Color(1f, 1f, 1f, 0.75f);
            else
                Towers[i].TowerPiece.color = new Color(1f, 1f, 1f, 0.5f);
        }


        if (Input.GetMouseButtonDown(0))
        {
            var path = Path + "/" + Data.Elements[activeElement].Name;
            callback?.Invoke(path);
            gameObject.SetActive(false);
        }
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;
}
