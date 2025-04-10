namespace Level
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(LevelController))]
    public class LevelView : MonoBehaviour
    {
        public event Action<List<LevelCell>> OnWallsGenerated = delegate { };

        [SerializeField] protected WallSO wallPrefHor = default;
        [SerializeField] protected WallSO wallPrefVer = default;

        protected Vector2 horOffset = default;
        protected Vector2 verOffset = default;
        protected Wall tempWall = default;
        protected LevelController controller = default;
        protected HashSet<Wall> instantiatedWalls = new HashSet<Wall>();

        protected virtual void Awake()
            => controller = GetComponent<LevelController>();

        protected virtual void OnEnable()
            => controller.OnLevelPathGenerated += GenerateWalls;

        protected virtual void OnDisable()
            => controller.OnLevelPathGenerated -= GenerateWalls;

        protected virtual void GenerateWalls(List<LevelCell> lastCell)
        {
            ClearGeneratedWalls();
            lastCell.ForEach(GenerateUnwalkableCells);
            OnWallsGenerated(lastCell);
        }

        protected virtual void ClearGeneratedWalls()
        {
            foreach (Wall item in instantiatedWalls)
            {
                Destroy(item);
            }
            instantiatedWalls.Clear();
        }

        protected virtual void GenerateUnwalkableCells(LevelCell cell)
        {
            if (cell.State == LevelCellState.unwalkable)
                GenerateWallByData(cell, cell.Type == LevelCellType.vertical ? wallPrefVer : wallPrefHor);
        }

        protected virtual void GenerateWallByData(LevelCell currentCell, WallSO wallData)
        {
            horOffset = Vector2.right * currentCell.Position.y * wallData.PosOffset;
            verOffset = Vector2.down * currentCell.Position.y * 1.3f;
            tempWall = wallData.InstantiateWall(currentCell.Position * 2.0f + horOffset + verOffset);
            tempWall.transform.Translate(Vector2.left * controller.LevelWidth * 1.3f + Vector2.down * controller.LevelHeight * 0.5f);
            instantiatedWalls.Add(tempWall);
        }
    }
}
