using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums
enum ValueToModify
{
    Position,
    Rotation,
    Scale,
    Color,
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
    [SerializeField] [Range(0, 20)] float duration;
    [SerializeField] Vector3 endPosition;
    [SerializeField] Vector3 endRotation;
    [SerializeField] Vector3 endScale;
    [SerializeField] Color endColor;

    Func<IEnumerator> animationToPlay;
    Func<float, float> easeFunc;
    float elapsedTime = 0;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayAnimation();
    }

    public void PlayAnimation()
    {
        StartCoroutine(animationToPlay?.Invoke());
    }

    
    #region Standard Eases
    IEnumerator EasePos()
    {
        elapsedTime = 0;
        Vector3 startPosition = transform.position;

        while (true)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseRot()
    {
        elapsedTime = 0;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(endRotation);

        while (true)
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseScale()
    {
        elapsedTime = 0;
        Vector3 startScale = transform.localScale;

        while (true)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseColor()
    {
        elapsedTime = 0;

        Renderer renderer;
        if (!TryGetComponent<Renderer>(out renderer))
        {
            Debug.LogError("ERROR : Can't find the renderer on this gameobject.");
            yield break;
        }

        Color startColor = renderer.material.color;

        while (true)
        {
            renderer.material.color = Color.Lerp(startColor, endColor, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
                yield break;

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
