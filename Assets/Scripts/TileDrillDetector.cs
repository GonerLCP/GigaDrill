using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDrillDetector : MonoBehaviour
{
    public Tilemap targetTilemap;
    public float coneAngle = 60f;
    public float coneDistance = 2f;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    void FixedUpdate()
    {
        if (!GameManager.Instance.ActivePlayer.drilling) return;

        Vector3 origin = transform.position;
        Vector3 forward = transform.up;
        Vector3Int centerCell = targetTilemap.WorldToCell(origin);
        int cellRange = Mathf.CeilToInt(coneDistance / targetTilemap.cellSize.x);

        for (int x = -cellRange; x <= cellRange; x++)
        {
            for (int y = -cellRange; y <= cellRange; y++)
            {
                Vector3Int cellPos = centerCell + new Vector3Int(x, y, 0);
                Vector3 worldPos = targetTilemap.GetCellCenterWorld(cellPos);
                Vector3 toCell = (worldPos - origin);
                float dist = toCell.magnitude;

                if (dist > coneDistance) continue;

                float angleToCell = Vector3.Angle(forward, toCell);
                if (angleToCell <= coneAngle * 0.5f)
                {
                    TileBase tile = targetTilemap.GetTile(cellPos);
                    if (tile != null)
                    {
                        gm.ActivePlayer.Impulsion -= gm.ActivePlayer.ImpulsionBlocReduce;
                        targetTilemap.SetTile(cellPos, null); // 💥 Supprime la tile
                    }
                }
            }
        }
    }

    // Gizmo pour visualiser le cône dans la scène
    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.up;

        Gizmos.color = Color.red;

        int segments = 20;
        float halfAngle = coneAngle * 0.5f;
        Vector3 previousPoint = origin;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -halfAngle + (i / (float)segments) * coneAngle;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 dir = rot * forward;

            Vector3 point = origin + dir.normalized * coneDistance;
            Gizmos.DrawLine(origin, point);

            if (i > 0)
                Gizmos.DrawLine(previousPoint, point);

            previousPoint = point;
        }
    }
}
