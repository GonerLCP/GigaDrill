using UnityEngine;
using UnityEngine.Tilemaps;

public class TileExplosion : MonoBehaviour
{
    public Tilemap targetTilemap;
    public float radius = 3f;
    public void ExplosionRadius(float radiuscall)
    {

        Vector3 center = transform.position;
        Vector3Int centerCell = targetTilemap.WorldToCell(center);
        int cellRadius = Mathf.CeilToInt(radiuscall / targetTilemap.cellSize.x);

        for (int x = -cellRadius; x <= cellRadius; x++)
        {
            for (int y = -cellRadius; y <= cellRadius; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);
                Vector3 worldPos = targetTilemap.GetCellCenterWorld(cell);

                if (Vector3.Distance(center, worldPos) <= radiuscall)
                {
                    if (targetTilemap.HasTile(cell))
                    {
                        targetTilemap.SetTile(cell, null);
                    }
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
