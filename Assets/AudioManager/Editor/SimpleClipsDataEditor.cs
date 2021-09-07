using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AudioThibaultCastelli
{
    [CustomEditor(typeof(SimpleClipsData))]
    public class SimpleClipsDataEditor : Editor
    {
        SimpleClipsData _target;
        AudioSource _previewer;

        private void OnEnable()
        {
            _target = (SimpleClipsData)target;

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
