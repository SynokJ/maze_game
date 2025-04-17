namespace Collectable
{
    using Level;
    using UnityEngine;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class CollectablesController : MonoBehaviour
    {
        public event Action<List<LevelCell>> OnCollectablesGenerated = delegate { };

        [SerializeField] protected CollectablesDataSO collectablesContainer = default;
        [SerializeField] protected AbstractCollectable[] collectables = null;
        [SerializeField] protected LevelController levelController = null;
        [SerializeField, Min(1)] protected int collectablesAmount = 1;
        [SerializeField] protected LevelView levelView = default;

        protected int tempCollectableId = 0;
        protected Vector2 horOffset = default;
        protected Vector2 verOffset = default;
        protected LevelCell[] availableCell = null;
        protected LevelCell tempLevelCell = default;
        protected AbstractCollectable tempCollectable = default;

        protected virtual void OnEnable()
            => levelView.OnWallsGenerated += SpawnCollectables;

        protected virtual void OnDisable()
            => levelView.OnWallsGenerated -= SpawnCollectables;

        protected virtual void SpawnCollectables(List<LevelCell> cells)
        {
            availableCell = cells.Where(x => x.State == LevelCellState.walkable).ToArray();
            for (int i = 0; i < collectablesAmount; ++i)
            {
                tempCollectableId = UnityEngine.Random.Range(0, collectables.Length);

                tempLevelCell = availableCell[UnityEngine.Random.Range(0, availableCell.Length)];
                SpawnCollectableByCell(tempLevelCell, collectables[tempCollectableId]);
                tempLevelCell.SetPositionState(LevelCellState.collectables);
                collectablesContainer.AddCollectable(tempCollectable);
            }

            OnCollectablesGenerated(cells);
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