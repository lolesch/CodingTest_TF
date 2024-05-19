using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace CodingTest.Utility.AttributeRefs
{
    [CustomPropertyDrawer(typeof(SceneRefAttribute))]
    public sealed class SceneRefDrawer : PropertyDrawer
    {
        private List<string> SceneList = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if ((EditorBuildSettings.scenes.Length + 1) != SceneList.Count)
                LoadSceneList();

            if (SceneList.Count == 0)
                SceneList.Add("");

            var index = SceneList.IndexOf(property.stringValue);

            if (index == -1)
                index = 0;

            var goalIndex = EditorGUI.Popup(position, property.displayName, index, SceneList.ToArray());
            property.stringValue = SceneList[goalIndex];
        }

        private void LoadSceneList()
        {
            SceneList = new List<string>
            {
                ""
            };

            foreach (var scene in EditorBuildSettings.scenes)
            {
                var scenePathElements = scene.path.Split('/');
                var sceneName = scenePathElements[^1];
                sceneName = sceneName.Remove(sceneName.Length - 6, 6);

                SceneList.Add(sceneName);
            }
        }
    }
}
#endif
