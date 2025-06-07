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
                //Debug.Log("Tile supprimée à " + cellPos);
            }
        }
    }
}
