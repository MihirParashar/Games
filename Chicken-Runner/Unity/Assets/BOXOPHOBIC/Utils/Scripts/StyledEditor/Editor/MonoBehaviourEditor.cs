using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : Editor
    {
        private bool hideScriptField;

        private void OnEnable()
        {
            hideScriptField = target.GetType().GetCustomAttributes(typeof(HideScriptField), false).Length > 0;
        }

        public override void OnInspectorGUI()
        {
            if (hideScriptField)
            {
                //GUILayout.Space(10);
                serializedObject.Update();
                EditorGUI.BeginChangeCheck();
                DrawPropertiesExcluding(serializedObject, "m_Script");
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
                //GUILayout.Space(10);
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}