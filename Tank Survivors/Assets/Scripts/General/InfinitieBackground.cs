using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace General
{
    [AddComponentMenu("Scripts/General/General.InfinityBackground")]
    internal class InfinityBackground : MonoBehaviour
    {
        [SerializeField]
        private Vector2 step;

        [Required]
        [SerializeField]
        private GameObject tilePrefab;

        [Required]
        [SerializeField]
        private Transform target;

        [SerializeField]
        private Vector2Int tilesAround;

        private Dictionary<Vector2Int, GameObject> tiles = new();

        [ReadOnly]
        [SerializeField]
        private Vector2Int lastPosition;

        private void Awake()
        {
            step *= tilePrefab.transform.localScale;
        }

        private void Start()
        {
            lastPosition = CalculateCenter();
            RecreateTiles(lastPosition);
        }

        private void Update()
        {
            Vector2Int center = CalculateCenter();

            if (center == lastPosition)
            {
                return;
            }

            RecreateTiles(center);
        }

        private Vector2Int CalculateCenter()
        {
            int tx = Mathf.CeilToInt(target.position.x / step.x);
            int ty = Mathf.CeilToInt(target.position.y / step.y);
            return new(tx, ty);
        }

        private void RecreateTiles(Vector2Int center)
        {
            List<Vector2Int> positions = new();

            IEnumerable<int> xs = Enumerable.Range(
                center.x - tilesAround.x,
                (tilesAround.x * 2) + 1
            );
            IEnumerable<int> ys = Enumerable.Range(
                center.y - tilesAround.y,
                (tilesAround.y * 2) + 1
            );
            foreach (int x in xs)
            {
                foreach (int y in ys)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }

            List<Vector2Int> intersection = tiles.Keys.Intersect(positions).ToList();
            List<Vector2Int> oldPositions = tiles.Keys.Except(intersection).ToList();
            foreach (Vector2Int old in oldPositions)
            {
                Destroy(tiles[old]);
                _ = tiles.Remove(old);
            }
            foreach (Vector2Int add in positions.Except(intersection))
            {
                GameObject newTile = Instantiate(
                    tilePrefab,
                    new(step.x * add.x, step.y * add.y, 0),
                    Quaternion.identity,
                    transform
                );
                tiles.Add(add, newTile);
            }
        }
    }
}
