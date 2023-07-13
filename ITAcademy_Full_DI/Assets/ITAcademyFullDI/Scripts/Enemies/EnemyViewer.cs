using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ITAcademy.FullDI
{
    public class EnemyViewer : MonoBehaviour
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public List<EnemyController> Enemies { get; set; }

        [Range(0, 1)] [SerializeField] private float _range;

        [MenuItem("Tools/Snap", true)]
        public static bool SnapToRoundValidation()
        {
            return Selection.gameObjects.Length > 0;
        }

        [MenuItem("Tools/Snap")]
        public static void SnapToRound()
        {
            Selection.gameObjects.ToList().ForEach(enemy =>
            {
                var enemyPos = enemy.transform.position;
                enemy.transform.position = new Vector3(Mathf.RoundToInt(enemyPos.x), Mathf.RoundToInt(enemyPos.y),
                    Mathf.RoundToInt(enemyPos.z));
            });
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (var enemy in Enemies)
            {
                var originPos = transform.position;
                var enemyPos = enemy.GetPosition();
                float halfHeight = (originPos.y - enemyPos.y) * 0.5f;
                Vector3 offset = Vector3.up * halfHeight;
                Handles.DrawBezier(originPos, enemyPos, originPos - offset,
                    enemyPos + offset, Color.white, EditorGUIUtility.whiteTexture, 1f);
            }
        }
#endif
    }

    [CustomEditor(typeof(EnemyViewer))]
    public class EnemyViewerEditor : Editor
    {
        public enum IdTypes
        {
            Enemy,
            Ogr,
            Something
        }

        private string _range;

        public override VisualElement CreateInspectorGUI()
        {
            _range = PlayerPrefs.GetString("range");
            return base.CreateInspectorGUI();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var enemyViewerScript = (EnemyViewer)target;
            if (GUILayout.Button("Collect Enemies"))
            {
                enemyViewerScript.Enemies = FindObjectsOfType<EnemyController>().ToList();
            }

            _range = GUILayout.TextField(_range);
            PlayerPrefs.SetString("range", _range);
            if (GUILayout.Button("Collect Close Enemies"))
            {
                var enemies = FindObjectsOfType<EnemyController>().ToList();
                enemyViewerScript.Enemies = enemies.Where(enemy =>
                        Vector3.Distance(enemyViewerScript.transform.position, enemy.GetPosition()) <
                        float.Parse(_range))
                    .ToList();
            }

            Ids = (IdTypes)EditorGUILayout.EnumPopup(Ids);
            enemyViewerScript.ID = Ids.ToString();
        }

        public IdTypes Ids { get; set; }
    }
}