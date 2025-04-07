namespace Enemy
{
    using Level;
    using UnityEngine;
    using System.Linq;
    using System.Collections.Generic;

    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected LevelController levelController = default;
        [SerializeField] protected LevelView levelView = default;
        [SerializeField] protected GameObject enemyPrefab = default;
        [SerializeField, Min(1)] protected int enemyCount = 1;

        protected Vector2 horOffset = default;
        protected Vector2 verOffset = default;
        protected Vector2 spawPos = default;
        protected LevelCell[] availableCell = null;
        protected GameObject tempEnemy = default;

        protected virtual void OnEnable()
            => levelView.OnWallsGenerated += CalculateSpawPos;

        protected virtual void OnDisable()
            => levelView.OnWallsGenerated -= CalculateSpawPos;

        protected virtual void CalculateSpawPos(List<LevelCell> cells)
        {
            availableCell = cells.Where(x => x.State == LevelCellState.walkable).ToArray();
            for(int i = 0; i < enemyCount; ++i)
            {
                GenerateWallByData(availableCell[Random.Range(0, availableCell.Length)]);
            }
        }

        protected virtual void GenerateWallByData(LevelCell currentCell)
        {
            horOffset = Vector2.right * currentCell.Position.y * 0.75f;
            verOffset = Vector2.down * currentCell.Position.y * 1.3f;
            tempEnemy = Instantiate(enemyPrefab, currentCell.Position * 2.0f + horOffset + verOffset, Quaternion.identity);
            tempEnemy.transform.Translate(Vector2.left * levelController.LevelWidth * 1.3f + Vector2.down * levelController.LevelHeight * 0.5f);
        }
    }
}
