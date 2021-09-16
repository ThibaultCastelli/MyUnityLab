using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MusicTC
{
    public class MusicEventEditor : Editor
    {
        AudioSource _previewer;

        private void OnEnable()
        {
            _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(_previewer);
        }

        public override void OnInspectorGUI()
        {
            MusicEvent _target = (MusicEvent)target;

            base.OnInspectorGUI();

            EditorGUILayout.Space();

            // Only able to click when the game is not running
            GUI.enabled = !Application.isPlaying;


        }
    }
}
