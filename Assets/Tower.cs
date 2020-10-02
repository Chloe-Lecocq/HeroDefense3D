using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider Collider;
    [HideInInspector]
    public LODGroup TowerMeshes;

    public void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        TowerMeshes = GetComponent<LODGroup>();
    }

    public void SetMaterial(Material mat)
    {
        var lods = TowerMeshes.GetLODs();
        for (var i = 0; i < lods.Length; i++)
        {
            for (var j = 0; j < lods[i].renderers.Length; j++)
            {
                lods[i].renderers[j].material = mat;
            }
        }
    }
}