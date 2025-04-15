namespace LevelMap
{
    using Collectable;
    using Level;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelMap : MonoBehaviour
    {
        [SerializeField] protected LevelController levelController = default;
        [SerializeField] protected CollectablesController controller = default;

        protected virtual void OnEnable()
            => controller.OnCollectablesGenerated += DrawLevelMap;

        protected virtual void OnDisable()
            => controller.OnCollectablesGenerated -= DrawLevelMap;

        protected virtual void DrawLevelMap(List<LevelCell> data)
        {
            string strMap = DataToString(data);
            Debug.Log(strMap);
        }

        protected virtual string DataToString(List<LevelCell> data)
        {
            string res = default;
            data.ForEach(x =>
            {
                res += x + " ";
                if (x.Position.x == levelController.LevelWidth - 1)
                {
                    res += '\n';
                }
            });
            return res;
        }
    }
}
