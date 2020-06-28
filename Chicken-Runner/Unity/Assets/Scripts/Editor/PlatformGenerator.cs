using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class PlatformGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelGenerator lvlGen = (LevelGenerator)target;

        if (GUILayout.Button("Generate Level/Platform"))
        {
            lvlGen.GenerateLevel();
            GameObject prefabInstance = (GameObject) PrefabUtility.InstantiatePrefab(lvlGen.gameObject);
            EditorUtility.SetDirty(target);
        }
    }
}
