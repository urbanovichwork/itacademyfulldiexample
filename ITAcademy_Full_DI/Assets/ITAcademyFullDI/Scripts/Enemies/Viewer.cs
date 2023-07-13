using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ITAcademy.FullDI
{
    public class Viewer : EditorWindow
    {
        [MenuItem("Tools/Open Viewer &V")]
        public static void OpenViewer()
        {
            GetWindow<Viewer>("Viewer");
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("SNAPPER");
            if (GUILayout.Button("Snap"))
            {
                SnapToRound();
            }

            GUILayout.EndHorizontal();
        }

        private static void SnapToRound()
        {
            Selection.gameObjects.ToList().ForEach(enemy =>
            {
                var enemyPos = enemy.transform.position;
                enemy.transform.position = new Vector3(Mathf.RoundToInt(enemyPos.x), Mathf.RoundToInt(enemyPos.y),
                    Mathf.RoundToInt(enemyPos.z));
            });
        }
    }
}