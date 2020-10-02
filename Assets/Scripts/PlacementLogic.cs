using UnityEngine;

public static class PlacementLogic

{
    public static int LayerMaskTower = LayerMask.GetMask("Tower");
    public static readonly Vector3 Grid = new Vector3(0.2f, 0.1f, 0.2f);

    public static Vector3 SnapToGrid(Vector3 input)
    {
        return new Vector3(Mathf.Round(input.x / Grid.x) * Grid.x,
                           Mathf.Round(input.y / Grid.y) * Grid.y,
                           Mathf.Round(input.z / Grid.z) * Grid.z);
    }
}