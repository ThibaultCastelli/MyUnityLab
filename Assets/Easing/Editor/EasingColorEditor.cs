using UnityEditor;

namespace EasingTC
{
    [CustomEditor(typeof(EasingColor))]
    public class EasingColorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EasingColor _target = (EasingColor)target;

            EditorGUILayout.LabelField("ANIMATION CHOICE", EditorStyles.boldLabel);

            _target.animationType = (AnimationType)EditorGUILayout.EnumPopup("Animation Type", _target.animationType);

            switch (_target.animationType)
            {
                case AnimationType.EaseIn:
                    _target.easeInType = (EaseIn)EditorGUILayout.EnumPopup("Ease In Type", _target.easeInType);
                    break;

                case AnimationType.EaseOut:
                    _target.easeOutType = (EaseOut)EditorGUILayout.EnumPopup("Ease Out Type", _target.easeOutType);
                    break;

                case AnimationType.EaseInOut:
                    _target.easeInOutType = (EaseInOut)EditorGUILayout.EnumPopup("Ease In Out Type", _target.easeInOutType);
                    break;

                case AnimationType.Mirror:
                    _target.mirorType = (MirorType)EditorGUILayout.EnumPopup("Mirror Type", _target.mirorType);
                    break;

                case AnimationType.SpecialEase:
                    _target.specialEaseType = (SpecialEase)EditorGUILayout.EnumPopup("Special Ease Type", _target.specialEaseType);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("ANIMATION INFOS", EditorStyles.boldLabel);

            _target.playOnAwake = EditorGUILayout.Toggle("Play On Awake", _target.playOnAwake);
            _target.loop = EditorGUILayout.Toggle("Loop", _target.loop);
            _target.duration = EditorGUILayout.Slider("Duration", _target.duration, 0.01f, 20f);

            _target.endColor = EditorGUILayout.ColorField("End Color", _target.endColor);
        }
    }
}