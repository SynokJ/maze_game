namespace LevelMap
{
    using Collectable;
    using Level;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelMap : MonoBehaviour
    {
        [SerializeField] protected LevelController levelController = default;
        [SerializeField] protected CollectablesController controller = default;
        [SerializeField] protected Image levelMapImage = default;

        protected virtual void OnEnable()
            => controller.OnCollectablesGenerated += DrawLevelMap;

        protected virtual void OnDisable()
            => controller.OnCollectablesGenerated -= DrawLevelMap;

        protected virtual void DrawLevelMap(List<LevelCell> data)
            => levelMapImage.material.mainTexture = GenerateTexture(data);

        protected virtual Texture2D GenerateTexture(List<LevelCell> data)
        {
            Texture2D texture = new Texture2D(levelController.LevelWidth, levelController.LevelHeight);

            foreach (LevelCell cell in data)
            {
                Color color = CalculateColor(cell);
                texture.SetPixel((int)cell.Position.x, (int)cell.Position.y, color);
            }

            texture.Apply();
            return texture;
        }

        protected virtual Color CalculateColor(LevelCell cell)
        {
            switch (cell.State)
            {
                case LevelCellState.unwalkable: return Color.white;
                case LevelCellState.walkable: return Color.black;
                case LevelCellState.collectables: return Color.yellow;
                default: return Color.black;
            }
        }
    }
}
