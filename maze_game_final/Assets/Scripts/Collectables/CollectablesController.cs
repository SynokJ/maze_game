namespace Collectable
{
    using Level;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    public class CollectablesController : MonoBehaviour
    {
        [SerializeField] protected AbstractCollectable[] collectables = null;
        [SerializeField] protected LevelController levelController = null;
        [SerializeField] protected LevelView levelView = default;

        protected Vector2 horOffset = default;
        protected Vector2 verOffset = default;
        protected LevelCell[] availableCell = null;
        protected AbstractCollectable tempCollectable = default;

        protected virtual void OnEnable()
            => levelView.OnWallsGenerated += SpawnCollectables;

        protected virtual void OnDisable()
            => levelView.OnWallsGenerated -= SpawnCollectables;

        protected virtual void SpawnCollectables(List<LevelCell> cells)
        {
            availableCell = cells.Where(x => x.State == LevelCellState.walkable).ToArray();
            for (int i = 0; i < collectables.Length; ++i)
            {
                SpawnCollectableByCell(availableCell[Random.Range(0, availableCell.Length)], collectables[i]);
            }
        }

        protected virtual void SpawnCollectableByCell(LevelCell currentCell, AbstractCollectable collectablePrefab)
        {
            horOffset = Vector2.right * currentCell.Position.y * 0.75f;
            verOffset = Vector2.down * currentCell.Position.y * 1.3f;
            tempCollectable = Instantiate(collectablePrefab, currentCell.Position * 2.0f + horOffset + verOffset, Quaternion.identity);
            tempCollectable.transform.Translate(Vector2.left * levelController.LevelWidth * 1.3f + Vector2.down * levelController.LevelHeight * 0.5f);
        }
    }
}