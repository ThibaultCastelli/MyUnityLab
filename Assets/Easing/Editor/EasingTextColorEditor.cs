using UnityEditor;
using UnityEngine;

namespace EasingTC
{
    [CustomEditor(typeof(EasingTextColor))]
    public class EasingTextColorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EasingTextColor _target = (EasingTextColor)target;

            EditorGUILayout.LabelField("ANIMATION CHOICE", EditorStyles.boldLabel);

            _target.animationType = (AnimationType)EditorGUILayout.EnumPopup(new GUIContent("Animation Type", "Ease In : Start slow.\nEase Out : End slow.\nEase In Out : Start and end slow.\nMirror : Go back and forth.\nSpecial Ease : Bounce or back effect."), _target.animationType);

            switch (_target.animationType)
            {
                case AnimationType.EaseIn:
                    _target.easeInType = (EaseIn)EditorGUILayout.EnumPopup(new GUIContent("Ease In Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.easeInType);
                    break;

                case AnimationType.EaseOut:
                    _target.easeOutType = (EaseOut)EditorGUILayout.EnumPopup(new GUIContent("Ease Out Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.easeOutType);
                    break;

                case AnimationType.EaseInOut:
                    _target.easeInOutType = (EaseInOut)EditorGUILayout.EnumPopup(new GUIContent("Ease In Out Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.easeInOutType);
                    break;

                case AnimationType.Mirror:
                    _target.mirorType = (MirorType)EditorGUILayout.EnumPopup(new GUIContent("Mirror Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.mirorType);
                    break;

                case AnimationType.SpecialEase:
                    _target.specialEaseType = (SpecialEase)EditorGUILayout.EnumPopup(new GUIContent("Special Ease Type", "Back In : Back effect at start and go to end value.\nBack Out : Go to end value and back effect.\nBounce Out : Go to end value and bounce effect."), _target.specialEaseType);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("ANIMATION INFOS", EditorStyles.boldLabel);

            _target.playOnAwake = EditorGUILayout.Toggle(new GUIContent("Play On Awake", "Select if the animation should automatically start when the game start."), _target.playOnAwake);
            _target.loop = EditorGUILayout.Toggle(new GUIContent("Loop", "Select if the animation should automatically loop."), _target.loop);

            EditorGUILayout.Space();

            _target.endColor = EditorGUILayout.ColorField(new GUIContent("End Color", "Set the value that the object will reach."), _target.endColor);
            _target.duration = EditorGUILayout.Slider(new GUIContent("Duration", "Set the duration of the animation. (in s)"), _target.duration, 0.01f, 20f);
        }
    }
}