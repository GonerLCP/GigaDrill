using UnityEngine;
using UnityEngine.Tilemaps;

public class TileColliderSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tileTriggerPrefab;

    void Start()
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                Vector3 worldPos = tilemap.GetCellCenterWorld(pos);
                Instantiate(tileTriggerPrefab, worldPos, Quaternion.identity, transform);
            }
        }
    }
}
