using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileDestroyer : MonoBehaviour
{
    public Tilemap targetTilemap;
    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gm.ActivePlayer.drilling == true || gm.ActivePlayer.exploding == true)
        {
            Vector3 hitWorldPos = transform.position;
            Vector3Int cellPos = targetTilemap.WorldToCell(hitWorldPos);

            if (targetTilemap.HasTile(cellPos))
            {
                gm.ActivePlayer.Impulsion -= gm.ActivePlayer.ImpulsionBlocReduce;
                targetTilemap.SetTile(cellPos, null); // Supprime la tile
                Destroy(this);
                //Debug.Log("Tile supprimée à " + cellPos);
            }
        }
    }
}
