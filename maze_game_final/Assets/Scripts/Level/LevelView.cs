namespace Level
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(LevelController))]
    public class LevelView : MonoBehaviour
    {
        [SerializeField] protected WallSO wallPrefHor = default;
        [SerializeField] protected WallSO wallPrefVer = default;

        protected LevelController controller = default;

        protected virtual void Awake()
        {
            controller = GetComponent<LevelController>();
        }

        protected virtual void OnEnable()
            => controller.OnLevelPathGenerated += GenerateWalls;

        protected virtual void OnDisable()
            => controller.OnLevelPathGenerated -= GenerateWalls;

        protected virtual void GenerateWalls(List<LevelCell> lastCell)
        {
            lastCell.ForEach(cell =>
            {
                if (cell.State == LevelCellState.unwalkable)
                {
                    GenerateWallByData(cell, cell.Type == LevelCellType.vertical ? wallPrefVer : wallPrefHor);
                }
            });
        }

        protected virtual void GenerateWallByData(LevelCell currentCell, WallSO wallData)
        {
            Vector2 horOffset = Vector2.right * currentCell.Position.y * wallData.PosOffset;
            Vector2 verOffset = Vector2.down * currentCell.Position.y * 1.3f;
            var tempWall = wallData.InstantiateWall(currentCell.Position * 2.0f + horOffset + verOffset);
            tempWall.transform.Translate(Vector2.left * controller.LevelWidth * 1.3f + Vector2.down * controller.LevelHeight * 0.5f);
        }
    }
}
