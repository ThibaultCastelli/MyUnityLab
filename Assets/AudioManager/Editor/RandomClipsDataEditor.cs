using UnityEngine;
using UnityEditor;

namespace AudioTC
{
    [CustomEditor(typeof(RandomClipsData))]
    public class RandomClipsDataEditor : Editor
    {
        RandomClipsData _target;
        AudioSource _previewer;

        private void OnEnable()
        {
            _target = (RandomClipsData)target;

            // Create an audio source to play the preview
            _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(_previewer.gameObject);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Preview Button
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Preview", GUILayout.Height(50)))
                _target.Preview(_previewer);
        }
    }
}
