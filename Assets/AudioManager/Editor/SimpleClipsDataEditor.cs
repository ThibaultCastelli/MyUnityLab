using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AudioThibaultCastelli
{
    [CustomEditor(typeof(SimpleClipsData))]
    public class SimpleClipsDataEditor : Editor
    {
        AudioSource _previewer;

        private void OnEnable()
        {
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

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Preview", GUILayout.Height(50)))
                ((SimpleClipsData)target).Preview(_previewer);
        }
    }
}
