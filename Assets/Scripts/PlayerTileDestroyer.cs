using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileDestroyer : MonoBehaviour
{
    public Tilemap targetTilemap;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>().drilling == true)
        {
            Vector3 hitWorldPos = transform.position;
            Vector3Int cellPos = targetTilemap.WorldToCell(hitWorldPos);

            if (targetTilemap.HasTile(cellPos))
            {
                targetTilemap.SetTile(cellPos, null); // Supprime la tile
                Debug.Log("Tile supprim�e � " + cellPos);
            }
        }
        // V�rifie que le collider appartient bien � la tilemap (ou � un layer/tag particulier si tu pr�f�res)
        if (other.GetComponent<TilemapCollider2D>())
        {
            Vector3 hitWorldPos = transform.position;
            Vector3Int cellPos = targetTilemap.WorldToCell(hitWorldPos);

            if (targetTilemap.HasTile(cellPos))
            {
                targetTilemap.SetTile(cellPos, null); // Supprime la tile
                Debug.Log("Tile supprim�e � " + cellPos);
            }
        }
    }
}
