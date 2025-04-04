namespace Level
{
    using UnityEngine;

    [RequireComponent(typeof(LevelController))]
    public class LevelView : MonoBehaviour
    {
        [SerializeField] protected WallSO verWallPref = default;
        [SerializeField] protected WallSO horWallPref = default;

        protected LevelController controller = default;

        protected virtual void Awake()
        {
            controller = GetComponent<LevelController>();
        }

        protected virtual void OnEnable()
            => controller.OnLevelPathGenerated += GenerateWalls;

        protected virtual void OnDisable()
            => controller.OnLevelPathGenerated -= GenerateWalls;

        protected virtual void GenerateWalls(LevelCell lastCell)
        {
            GenerateWallsByCell(lastCell);
        }

        protected virtual void GenerateWallsByCell(LevelCell currentCell)
        {
            if (currentCell == null) return;

            GenerateWallByData(currentCell, horWallPref, Vector2.down);
            GenerateWallByData(currentCell, horWallPref, Vector2.up);
            GenerateWallByData(currentCell, verWallPref, Vector2.right);
            GenerateWallByData(currentCell, verWallPref, Vector2.left);

            GenerateWallsByCell(currentCell.RootNode);
        }

        protected virtual void GenerateWallByData(LevelCell currentCell, WallSO wallData, Vector2 posOffset)
        {
            var tempWall = wallData.InstantiateWall(currentCell.Position * 4.0f + posOffset * wallData.PosOffset, Quaternion.identity);
            tempWall.name = ((Vector2)tempWall.transform.position).ToString();
            tempWall.transform.Translate((Vector2.left * controller.LevelWidth + Vector2.down * controller.LevelHeight) * 2.0f);
        }
    }
}
