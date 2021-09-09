using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EaseType
{
    Linear,

    EaseIn,
    EaseInCubic,
    EaseInQuart,
    EaseInQuint,
    EaseInCirc,
    EaseInBack,

    EaseOut,
    EaseOutCubic,
    EaseOutQuart,
    EaseOutQuint,
    EaseOutCirc,
    EaseOutBack,

    EaseInOut,
    EaseInOutCubic,
    EaseInOutQuart,
    EaseInOutQuint,
    EaseInOutCirc,
    EaseInOutBack,
}
enum MirorType
{
    Linear,
    Ease,
    EaseCubic,
    EaseQuart,
    EaseQuint,
    EaseCirc,
}

public class Easing : MonoBehaviour
{
    [SerializeField] bool playOnAwake;
    [SerializeField] bool mirrored;
    [SerializeField] Vector3 endPos;
    [SerializeField] [Range(0, 20)] float duration;
    [SerializeField] EaseType easeType;
    [SerializeField] MirorType mirorType;

    Func<float, float> easeFunc;
    float elapsedTime = 0;

    private void Awake()
    {
        if (mirrored)
        {
            switch(mirorType)
            {
                case MirorType.Linear:
                    easeFunc = LinearMirror;
                    break;
                case MirorType.Ease:
                    easeFunc = Mirror;
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
            }
        }
        else
        {
            switch (easeType)
            {
                case EaseType.Linear:
                    easeFunc = Linear;
                    break;

                // Ease In
                case EaseType.EaseIn:
                    easeFunc = EaseIn;
                    break;
                case EaseType.EaseInCubic:
                    easeFunc = EaseInCubic;
                    break;
                case EaseType.EaseInQuart:
                    easeFunc = EaseInQuart;
                    break;
                case EaseType.EaseInQuint:
                    easeFunc = EaseInQuint;
                    break;
                case EaseType.EaseInCirc:
                    easeFunc = EaseInCirc;
                    break;
                case EaseType.EaseInBack:
                    easeFunc = EaseInBack;
                    break;

                // Ease Out
                case EaseType.EaseOut:
                    easeFunc = EaseOut;
                    break;
                case EaseType.EaseOutCubic:
                    easeFunc = EaseOutCubic;
                    break;
                case EaseType.EaseOutQuart:
                    easeFunc = EaseOutQuart;
                    break;
                case EaseType.EaseOutQuint:
                    easeFunc = EaseOutQuint;
                    break;
                case EaseType.EaseOutCirc:
                    easeFunc = EaseOutCirc;
                    break;

                // Ease In Out
                case EaseType.EaseInOut:
                    easeFunc = EaseInOut;
                    break;
                case EaseType.EaseInOutCubic:
                    easeFunc = EaseInOutCubic;
                    break;
                case EaseType.EaseInOutQuart:
                    easeFunc = EaseInOutQuart;
                    break;
                case EaseType.EaseInOutQuint:
                    easeFunc = EaseInOutQuint;
                    break;
                case EaseType.EaseInOutCirc:
                    easeFunc = EaseInOutCirc;
                    break;
            }
        }
    }

    private void Start()
    {
        if (playOnAwake)
            StartCoroutine(EaseVector3());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(EaseVector3());
    }

    IEnumerator EaseVector3()
    {
        Vector3 startPos = transform.position;

        while (true)
        {
            transform.position = Vector3.Lerp(startPos, endPos, easeFunc(elapsedTime / duration));

            if (elapsedTime == duration)
                yield break;

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }

    #region Easing Types
    float Linear(float t) => t;

    float EaseIn(float t) => Square(t);
    float EaseInCubic(float t) => Cubic(t);
    float EaseInQuart(float t) => Quart(t);
    float EaseInQuint(float t) => Quint(t);
    float EaseInCirc(float t) => Flip(Mathf.Sqrt(Flip(Square(t))));

    float EaseOut(float t) => Flip(Square(Flip(t)));
    float EaseOutCubic(float t) => Flip(Cubic(Flip(t)));
    float EaseOutQuart(float t) => Flip(Quart(Flip(t)));
    float EaseOutQuint(float t) => Flip(Quint(Flip(t)));
    float EaseOutCirc(float t) => Mathf.Sqrt(Flip(Square(t - 1)));

    float EaseInOut(float t) => Mathf.Lerp(EaseIn(t), EaseOut(t), t);
    float EaseInOutCubic(float t) => Mathf.Lerp(EaseInCubic(t), EaseOutCubic(t), t);
    float EaseInOutQuart(float t) => Mathf.Lerp(EaseInQuart(t), EaseOutQuart(t), t);
    float EaseInOutQuint(float t) => Mathf.Lerp(EaseInQuint(t), EaseOutQuint(t), t);
    float EaseInOutCirc(float t) => Mathf.Lerp(EaseInCirc(t), EaseOutCirc(t), t);
    #endregion

    #region Mirror
    float LinearMirror(float t)
    {
        if (t < 0.5)
            return Linear(t / 0.5f);

        return Linear(Flip(t) / 0.5f);
    }
    float Mirror(float t)
    {
        if (t < 0.5f)
            return EaseIn(t / 0.5f);

        return EaseIn(Flip(t) / 0.5f);
    }
    float MirrorCubic(float t)
    {
        if (t < 0.5f)
            return EaseInCubic(t / 0.5f);

        return EaseInCubic(Flip(t) / 0.5f);
    }
    float MirrorQuart(float t)
    {
        if (t < 0.5f)
            return EaseInQuart(t / 0.5f);

        return EaseInQuart(Flip(t) / 0.5f);
    }
    float MirrorQuint(float t)
    {
        if (t < 0.5f)
            return EaseInQuint(t / 0.5f);

        return EaseInQuint(Flip(t) / 0.5f);
    }
    #endregion

    #region Specials Eases
    float EaseInBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = 2.5949095f;

        return Square(t) * ((c1 + 1) * t - c1);
    }

    #endregion

    #region Tools
    float Flip(float t) => 1 - t;
    float Square(float x) => x * x;
    float Cubic(float x) => x * x * x;
    float Quart(float x) => x * x * x * x;
    float Quint(float x) => x * x * x * x * x;
    #endregion
}
