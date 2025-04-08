namespace Collectable
{
    using Level;
    using System;
    using UnityEngine;
    using System.Linq;
    using System.Collections.Generic;

    public class CollectablesController : MonoBehaviour
    {
        public event Action<Queue<AbstractCollectable>> OnCollectablesSpawned = delegate { };

        [SerializeField] protected AbstractCollectable[] collectables = null;
        [SerializeField] protected LevelController levelController = null;
        [SerializeField, Min(1)] protected int collectablesAmount = 1;
        [SerializeField] protected LevelView levelView = default;

        protected int tempCollectableId = 0;
        protected Vector2 horOffset = default;
        protected Vector2 verOffset = default;
        protected LevelCell[] availableCell = null;
        protected AbstractCollectable tempCollectable = default;
        protected Queue<AbstractCollectable> availableCollectables = new Queue<AbstractCollectable>();

        protected virtual void OnEnable()
            => levelView.OnWallsGenerated += SpawnCollectables;

        protected virtual void OnDisable()
            => levelView.OnWallsGenerated -= SpawnCollectables;

        protected virtual void SpawnCollectables(List<LevelCell> cells)
        {
            ResetCollectables();
            availableCell = cells.Where(x => x.State == LevelCellState.walkable).ToArray();
            for (int i = 0; i < collectablesAmount; ++i)
            {
                tempCollectableId = UnityEngine.Random.Range(0, collectables.Length);
                SpawnCollectableByCell(availableCell[UnityEngine.Random.Range(0, availableCell.Length)], collectables[tempCollectableId]);
                availableCollectables.Enqueue(tempCollectable);
            }
            OnCollectablesSpawned(availableCollectables);
        }

        protected virtual void SpawnCollectableByCell(LevelCell currentCell, AbstractCollectable collectablePrefab)
        {
            horOffset = Vector2.right * currentCell.Position.y * 0.75f;
            verOffset = Vector2.down * currentCell.Position.y * 1.3f;
            tempCollectable = Instantiate(collectablePrefab, currentCell.Position * 2.0f + horOffset + verOffset, Quaternion.identity);
            tempCollectable.transform.Translate(Vector2.left * levelController.LevelWidth * 1.3f + Vector2.down * levelController.LevelHeight * 0.5f);
        }

        protected virtual void ResetCollectables()
        {
            foreach (AbstractCollectable collectable in availableCollectables)
            {
                Destroy(collectable);
            }
            availableCollectables.Clear();
        }
    }
}