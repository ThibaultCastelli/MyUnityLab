using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#region Enums
enum ValueToModify
{
    Position,
    Rotation,
    Scale,
    Color,
    TextColor,
}
enum AnimationType
{
    EaseIn,
    EaseOut,
    EaseInOut,
    SpecialEase,
    Mirror
}
enum EaseIn
{
    Linear,

    EaseInQuad,
    EaseInCubic,
    EaseInQuart,
    EaseInQuint,
    EaseInCirc,
}
enum EaseOut
{
    EaseOutQuad,
    EaseOutCubic,
    EaseOutQuart,
    EaseOutQuint,
    EaseOutCirc,
}
enum EaseInOut
{
    EaseInOutQuad,
    EaseInOutCubic,
    EaseInOutQuart,
    EaseInOutQuint,
    EaseInOutCirc,
}
enum SpecialEase
{
    EaseInBack,
    EaseOutBack,
    EaseInOutBack,
    EaseOutBounce,
}
enum MirorType
{
    Linear,
    EaseQuad,
    EaseCubic,
    EaseQuart,
    EaseQuint,
    EaseCirc,
}
#endregion

public class Easing : MonoBehaviour
{
    #region Variables
    [Header("ANIMATION CHOICE")]
    [SerializeField] ValueToModify valueToModify;
    [SerializeField] AnimationType animationType;
    [SerializeField] EaseIn easeInType;
    [SerializeField] EaseOut easeOutType;
    [SerializeField] EaseInOut easeInOutType;
    [SerializeField] MirorType mirorType;
    [SerializeField] SpecialEase specialEaseType;
    [Space]

    [Header("INFOS")]
    [SerializeField] bool playOnAwake;
    [SerializeField] [Range(0.1f, 20)] float duration = 1;
    [SerializeField] Vector3 endPosition;
    [SerializeField] bool useLocalPosition;
    [SerializeField] Vector3 endRotation;
    [SerializeField] Vector3 endScale;
    [SerializeField] Color endColor = Color.white;

    Func<IEnumerator> animationToPlay;
    Func<float, float> easeFunc;
    float elapsedTime = 0;

    Vector3 _previousStartPos;
    Vector3 _previousStartRot;
    Vector3 _previousStartScale;
    Color _previousStartColor;
    Color _previousStartTextColor;

    bool _isInTransition;
    bool _hasPlayed;

    #endregion

    #region Animation Choice
    private void Awake()
    {
        switch (valueToModify)
        {
            case ValueToModify.Position:
                animationToPlay = EasePos;
                break;
            case ValueToModify.Rotation:
                animationToPlay = EaseRot;
                break;
            case ValueToModify.Scale:
                animationToPlay = EaseScale;
                break;
            case ValueToModify.Color:
                animationToPlay = EaseColor;
                break;
            case ValueToModify.TextColor:
                animationToPlay = EaseTextColor;
                break;
        }

        switch (animationType)
        {
            case AnimationType.EaseIn:
                switch (easeInType)
                {
                    case EaseIn.Linear:
                        easeFunc = Linear;
                        break;
                    case EaseIn.EaseInQuad:
                        easeFunc = EaseInQuad;
                        break;
                    case EaseIn.EaseInCubic:
                        easeFunc = EaseInCubic;
                        break;
                    case EaseIn.EaseInQuart:
                        easeFunc = EaseInQuart;
                        break;
                    case EaseIn.EaseInQuint:
                        easeFunc = EaseInQuint;
                        break;
                }
                break;

            case AnimationType.EaseOut:
                switch (easeOutType)
                {
                    case EaseOut.EaseOutQuad:
                        easeFunc = EaseOutQuad;
                        break;
                    case EaseOut.EaseOutCubic:
                        easeFunc = EaseOutCubic;
                        break;
                    case EaseOut.EaseOutQuart:
                        easeFunc = EaseOutQuart;
                        break;
                    case EaseOut.EaseOutQuint:
                        easeFunc = EaseOutQuint;
                        break;
                }
                break;

            case AnimationType.EaseInOut:
                switch (easeInOutType)
                {
                    case EaseInOut.EaseInOutQuad:
                        easeFunc = EaseInOutQuad;
                        break;
                    case EaseInOut.EaseInOutCubic:
                        easeFunc = EaseInOutCubic;
                        break;
                    case EaseInOut.EaseInOutQuart:
                        easeFunc = EaseInOutQuart;
                        break;
                    case EaseInOut.EaseInOutQuint:
                        easeFunc = EaseInOutQuint;
                        break;
                }
                break;

            case AnimationType.SpecialEase:
                break;

            case AnimationType.Mirror:
                switch (mirorType)
                {
                    case MirorType.Linear:
                        easeFunc = LinearMirror;
                        break;
                    case MirorType.EaseQuad:
                        easeFunc = MirrorQuad;
                        break;
                    case MirorType.EaseCubic:
                        easeFunc = MirrorCubic;
                        break;
                    case MirorType.EaseQuart:
                        easeFunc = MirrorQuart;
                        break;
                    case MirorType.EaseQuint:
                        easeFunc = MirrorQuint;
                        break;
                    case MirorType.EaseCirc:
                        easeFunc = MirrorCirc;
                        break;
                }
                break;
        }
    }
    #endregion

    private void Start()
    {
        if (playOnAwake)
            PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (!_isInTransition)
        {
            StartCoroutine(animationToPlay?.Invoke());
            _hasPlayed = true;
        }
    }

    public void PlayReverseAnimation()
    {
        if (animationType == AnimationType.Mirror)
        {
            Debug.LogError("ERROR : Can't play in reverse for a mirror animation type.");
            return;
        }
        else if (!_hasPlayed)
        {
            PlayAnimation();
            return;
        }
        /*else if (_isInTransition)
        {
            Debug.LogError("ERROR : You need to wait until the animation is finished to play another one.");
            return;
        }
        */
        switch (valueToModify)
        {
            case ValueToModify.Position:
                endPosition = _previousStartPos;
                break;
            case ValueToModify.Rotation:
                endRotation = _previousStartRot;
                break;
            case ValueToModify.Scale:
                    endScale = _previousStartScale;
                break;
            case ValueToModify.Color:
                    endColor = _previousStartColor;
                break;
            case ValueToModify.TextColor:
                    endColor = _previousStartTextColor;
                break;
        }

        PlayAnimation();
    }

    
    #region Standard Eases
    IEnumerator EasePos()
    {
        _isInTransition = true;
        elapsedTime = 0;

        Vector3 startPosition = useLocalPosition ? transform.localPosition : transform.position;
        _previousStartPos = startPosition;
        
        while (true)
        {
            if (useLocalPosition)
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, easeFunc(elapsedTime / duration));
            else
                transform.position = Vector3.Lerp(startPosition, endPosition, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseRot()
    {
        _isInTransition = true;
        elapsedTime = 0;

        Quaternion startRot = transform.rotation;
        _previousStartRot = startRot.eulerAngles;

        Quaternion endRot = Quaternion.Euler(endRotation);

        while (true)
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseScale()
    {
        _isInTransition = true;
        elapsedTime = 0;

        Vector3 startScale = transform.localScale;
        _previousStartScale = startScale;

        while (true)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseColor()
    {
        _isInTransition = true;
        elapsedTime = 0;

        Renderer renderer = null;
        Image image = null;
        Color startColor;

        if (!TryGetComponent<Renderer>(out renderer))
        {
            if (!TryGetComponent<Image>(out image))
            {
                Debug.LogError("ERROR : Can't find the renderer or the image on this gameobject.");

                _isInTransition = false;
                yield break;
            }
            else
                startColor = image.color;
        }
        else
            startColor = renderer.material.color;

        _previousStartColor = startColor;

        while (true)
        {
            if (renderer != null)
                renderer.material.color = Color.Lerp(startColor, endColor, easeFunc(elapsedTime / duration));
            else
                image.color = Color.Lerp(startColor, endColor, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseTextColor()
    {
        _isInTransition = true;
        elapsedTime = 0;

        Text text = null;
        TextMeshProUGUI TMP = null;
        Color startColor;

        if (!TryGetComponent<TextMeshProUGUI>(out TMP))
        {
            if (!TryGetComponent<Text>(out text))
            {
                Debug.LogError("ERROR : Can't find the TextMeshPro or the Text on this gameobject.");

                _isInTransition = false;
                yield break;
            }
            else
                startColor = text.color;
        }
        else
            startColor = TMP.color;

        _previousStartTextColor = startColor;

        while (true)
        {
            if (TMP != null)
                TMP.color = Color.Lerp(startColor, endColor, easeFunc(elapsedTime / duration));
            else
                text.color = Color.Lerp(startColor, endColor, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    #endregion

    #region Specials Eases

    #region Back
    IEnumerator EaseInBackVector3()
    {
        elapsedTime = 0;
        Vector3 startPos = transform.position;
        endPosition -= startPos;

        float s = 1.70158f;
        float t;

        while (true)
        {
            t = elapsedTime / duration;
            transform.position = endPosition * t * t * ((s + 1) * t - s) + startPos;
            
            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseOutBackVector3()
    {
        elapsedTime = 0;
        Vector3 startPos = transform.position;
        endPosition -= startPos;

        float s = 1.70158f;
        float t;

        while (true)
        {
            t = elapsedTime / duration - 1;
            transform.position = endPosition * (t * t * ((s + 1) * t + s) + 1) + startPos;

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    // NOT WORKING
    IEnumerator EaseInOutBackVector3()
    {
        elapsedTime = 0;
        Vector3 startPos = transform.position;
        endPosition -= startPos;

        float s = 1.70158f;
        float t;

        while (true)
        {
            t = elapsedTime;
            t /= duration / 2;
            Debug.Log(t);
            if ((t) < 1)
            {
                s *= (1.525f);
                transform.position = endPosition * 0.5f * (t * t * (((s) + 1) * t - s)) + startPos;
            }
            else
            {
                t -= 2;
                s *= (1.525f);
                transform.position = endPosition * 0.5f * ((t) * t * (((s) + 1) * t + s) + 2) + startPos;
            }

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    #endregion

    #region Bounce
    IEnumerator EaseOutBounceVector3()
    {
        elapsedTime = 0;
        Vector3 startPos = transform.position;
        endPosition -= startPos;

        float t;

        while (true)
        {
            t = elapsedTime / duration;
            if (t < (1 / 2.75f))
            {
                transform.position = endPosition * (7.5625f * t * t) + startPos;
            }
            else if (t < (2 / 2.75f))
            {
                t -= (1.5f / 2.75f);
                transform.position = endPosition * (7.5625f * (t) * t + .75f) + startPos;
            }
            else if (t < (2.5 / 2.75))
            {
                t -= (2.25f / 2.75f);
                transform.position = endPosition * (7.5625f * (t) * t + .9375f) + startPos;
            }
            else
            {
                t -= (2.625f / 2.75f);
                transform.position = endPosition * (7.5625f * (t) * t + .984375f) + startPos;
            }

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    #endregion

    #region Elastic

    #endregion
    #endregion

    #region Easing Types
    float Linear(float t) => t;

    float EaseInQuad(float t) => t * t;
    float EaseInCubic(float t) => t * t * t;
    float EaseInQuart(float t) => t * t * t * t;
    float EaseInQuint(float t) => t * t * t * t;
    float EaseInCirc(float t) => 1 - Mathf.Sqrt(1 - (t * t));

    float EaseOutQuad(float t) => t * (2 - t);
    float EaseOutCubic(float t) => (--t) * t * t + 1;
    float EaseOutQuart(float t) => 1 - (--t) * t * t * t;
    float EaseOutQuint(float t) => 1 + (--t) * t * t * t * t;
    float EaseOutCirc(float t) => Mathf.Sqrt(1 - (t - 1) * (t - 1));

    float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
    float EaseInOutQuart(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;
    float EaseInOutQuint(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;
    float EaseInOutCirc(float t) => t < 0.5f ? (1 - Mathf.Sqrt(1 - (2 * t) * (2 * t))) / 2 : (Mathf.Sqrt(1 - (-2 * t + 2) * (-2 * t + 2)) + 1) / 2;
    #endregion

    #region Mirror
    float LinearMirror(float t) => t < 0.5f ? Linear(t / 0.5f) : Linear((1 - t) / 0.5f);
    float MirrorQuad(float t) => t < 0.5f ? EaseInQuad(t / 0.5f) : EaseInQuad((1 - t) / 0.5f);
    float MirrorCubic(float t) => t < 0.5f ? EaseInCubic(t / 0.5f) : EaseInCubic((1 - t) / 0.5f);
    float MirrorQuart(float t) => t < 0.5f ? EaseInQuart(t / 0.5f) : EaseInQuart((1 - t) / 0.5f);
    float MirrorQuint(float t) => t < 0.5f ? EaseInQuint(t / 0.5f) : EaseInQuint((1 - t) / 0.5f);
    float MirrorCirc(float t) => t < 0.5f ? EaseInCirc(t / 0.5f) : EaseInCirc((1 - t) / 0.5f);
    #endregion
}
