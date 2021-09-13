using System;
using System.Collections;
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
    [SerializeField] bool loop;
    [SerializeField] [Range(0.1f, 20)] float duration = 1;
    [SerializeField] bool useLocalPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] Vector3 endRotation;
    [SerializeField] Vector3 endScale;
    [SerializeField] Color endColor = Color.white;

    // Delegates to store the animation to play and the ease type to use
    Func<IEnumerator> animationToPlay;
    Func<float, float> easeFunc;

    // Use to know how much time is passed since the begining of the animation
    float elapsedTime = 0;

    // Starting Points
    Vector3 defaultStartPos;
    Vector3 newStartPos;

    Vector3 defaultStartRot;
    Vector3 newStartRot;

    Vector3 defaultStartScale;
    Vector3 newStartScale;

    Color defaultStartColor;
    Color newStartColor;

    // Ending Points
    Vector3 newEndPos;
    Vector3 newEndRot;
    Vector3 newEndScale;
    Color newEndColor;

    // Color
    new Renderer renderer = null;
    Image image = null;

    // Text Color
    Text text = null;
    TextMeshProUGUI TMP = null;

    // Flags
    bool _isInTransition;
    bool _hasPlayed;

    // Special eases
    const float s = 1.70158f;
    float t;
    #endregion

    #region Animation Choice
    private void Awake()
    {
        // Select the animation to play based on the value the user want to modify
        // Also initialize defaults variables
        switch (valueToModify)
        {
            case ValueToModify.Position:
                animationToPlay = EasePos;
                defaultStartPos = useLocalPosition ? transform.localPosition : transform.position;
                newStartPos = defaultStartPos;
                newEndPos = endPosition;
                break;

            case ValueToModify.Rotation:
                animationToPlay = EaseRot;
                defaultStartRot = transform.rotation.eulerAngles;
                newStartRot = defaultStartRot;
                newEndRot = endRotation;
                break;

            case ValueToModify.Scale:
                animationToPlay = EaseScale;
                defaultStartScale = transform.localScale;
                newStartScale = defaultStartScale;
                newEndScale = endScale;
                break;

            case ValueToModify.Color:
                animationToPlay = EaseColor;
                if (!TryGetComponent<Renderer>(out renderer))
                {
                    if (!TryGetComponent<Image>(out image))
                        Debug.LogError("ERROR : Can't find the renderer or the image on this gameobject.");
                    else
                        defaultStartColor = image.color;
                }
                else
                    defaultStartColor = renderer.material.color;

                newStartColor = defaultStartColor;
                newEndColor = endColor;
                break;

            case ValueToModify.TextColor:
                animationToPlay = EaseTextColor;
                if (!TryGetComponent<TextMeshProUGUI>(out TMP))
                {
                    if (!TryGetComponent<Text>(out text))
                        Debug.LogError("ERROR : Can't find the TextMeshPro or the Text on this gameobject.");
                    else
                        defaultStartColor = text.color;
                }
                else
                    defaultStartColor = TMP.color;

                newStartColor = defaultStartColor;
                newEndColor = endColor;
                break;
        }

        // Select which ease function will be used
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
                switch (specialEaseType)
                {
                    case SpecialEase.EaseInBack:
                        switch (valueToModify)
                        {
                            case ValueToModify.Position:
                                animationToPlay = EaseInBackPos;
                                break;
                            case ValueToModify.Rotation:
                                animationToPlay = EaseInBackRot;
                                break;
                            case ValueToModify.Scale:
                                animationToPlay = EaseInBackScale;
                                break;
                            case ValueToModify.Color:
                                Debug.LogError("ERROR : Can't change the color with specials ease.");
                                animationToPlay = NullAnimation;
                                break;
                            case ValueToModify.TextColor:
                                Debug.LogError("ERROR : Can't change the text color with specials ease.");
                                animationToPlay = NullAnimation;
                                break;
                        }
                        break;

                    case SpecialEase.EaseOutBack:
                        switch (valueToModify)
                        {
                            case ValueToModify.Position:
                                animationToPlay = EaseOutBackPos;
                                break;
                            case ValueToModify.Rotation:
                                animationToPlay = EaseOutBackRot;
                                break;
                            case ValueToModify.Scale:
                                animationToPlay = EaseOutBackScale;
                                break;
                            case ValueToModify.Color:
                                Debug.LogError("ERROR : Can't change the color with specials ease.");
                                animationToPlay = NullAnimation;
                                break;
                            case ValueToModify.TextColor:
                                Debug.LogError("ERROR : Can't change the text color with specials ease.");
                                animationToPlay = NullAnimation;
                                break;
                        }
                        break;

                    case SpecialEase.EaseOutBounce:
                        switch (valueToModify)
                        {
                            case ValueToModify.Position:
                                animationToPlay = EaseOutBouncePos;
                                break;
                            case ValueToModify.Rotation:
                                animationToPlay = EaseOutBounceRot;
                                break;
                            case ValueToModify.Scale:
                                animationToPlay = EaseOutBounceScale;
                                break;
                            case ValueToModify.Color:
                                Debug.LogError("ERROR : Can't change the color with specials ease.");
                                animationToPlay = NullAnimation;
                                break;
                            case ValueToModify.TextColor:
                                Debug.LogError("ERROR : Can't change the text color with specials ease.");
                                animationToPlay = NullAnimation;
                                break;
                        }
                        break;
                }
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

    #region Start & Update
    private void Start()
    {
        // Play the animation as soon as the game start if playOnAwake is selected on the editor
        if (playOnAwake)
            PlayAnimation();
    }

    private void Update()
    {
        // Replay automatically the animation if loop is selected on the editor
        if (loop && !_isInTransition && _hasPlayed)
            PlayAnimationInOut();
    }
    #endregion

    #region Functions
    // Reset the variables and play the animation selected on the Awake
    public void PlayAnimation()
    {
        _isInTransition = true;
        elapsedTime = 0;

        StartCoroutine(animationToPlay?.Invoke());
        _hasPlayed = true;
    }

    // On first call, will play the animation with input values
    // On the second call, will play the animation in reverse
    // etc etc...
    public void PlayAnimationInOut()
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
        else if (_isInTransition)
        {
            StopAllCoroutines();
        }
        
        switch (valueToModify)
        {
            case ValueToModify.Position:
                newEndPos = newEndPos == endPosition ? defaultStartPos : endPosition;
                newStartPos = useLocalPosition ? transform.localPosition : transform.position;
                break;

            case ValueToModify.Rotation:
                newEndRot = newEndRot == endRotation ? defaultStartRot : endRotation;
                newStartRot = transform.rotation.eulerAngles;
                break;

            case ValueToModify.Scale:
                newEndScale = newEndScale == endScale ? defaultStartScale : endScale;
                newStartScale = transform.localScale;
                break;
            case ValueToModify.Color:
                newEndColor = newEndColor == endColor ? defaultStartColor : endColor;
                newStartColor = renderer != null ? renderer.material.color : image.color;
                break;
            case ValueToModify.TextColor:
                newEndColor = newEndColor == endColor ? defaultStartColor : endColor;
                newStartColor = TMP != null ? TMP.color : text.color;
                break;
        }
        
        PlayAnimation();
    }
    #endregion

    #region Standard Eases
    IEnumerator EasePos()
    {   
        while (true)
        {
            if (useLocalPosition)
                transform.localPosition = Vector3.Lerp(newStartPos, newEndPos, easeFunc(elapsedTime / duration));
            else
                transform.position = Vector3.Lerp(newStartPos, newEndPos, easeFunc(elapsedTime / duration));

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
        Quaternion startRot = Quaternion.Euler(newStartRot);
        Quaternion endRot = Quaternion.Euler(newEndRot);

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
        while (true)
        {
            transform.localScale = Vector3.Lerp(newStartScale, newEndScale, easeFunc(elapsedTime / duration));

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
        while (true)
        {
            if (renderer != null)
                renderer.material.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));
            else
                image.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));

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
        while (true)
        {
            if (TMP != null)
                TMP.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));
            else
                text.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));

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

    #region Back In
    IEnumerator EaseInBackPos()
    {
        Vector3 endPos = newEndPos - newStartPos;

        while (true)
        {
            t = elapsedTime / duration;

            if (useLocalPosition)
                transform.localPosition = endPos * t * t * ((s + 1) * t - s) + newStartPos;
            else
                transform.position = endPos * t * t * ((s + 1) * t - s) + newStartPos;
            
            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseInBackRot()
    {
        Vector3 endRot = newEndRot - newStartRot;

        while (true)
        {
            t = elapsedTime / duration;

            transform.rotation = Quaternion.Euler(endRot * t * t * ((s + 1) * t - s) + newStartRot);

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseInBackScale()
    {
        Vector3 endScale = newEndScale - newStartScale;

        while (true)
        {
            t = elapsedTime / duration;

            transform.localScale = endScale * t * t * ((s + 1) * t - s) + newStartScale;

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

    #region Back Out
    IEnumerator EaseOutBackPos()
    {
        Vector3 endPos = newEndPos - newStartPos;

        while (true)
        {
            t = elapsedTime / duration - 1;

            if (useLocalPosition)
                transform.localPosition = endPos * (t * t * ((s + 1) * t + s) + 1) + newStartPos;
            else
                transform.position = endPos * (t * t * ((s + 1) * t + s) + 1) + newStartPos;

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseOutBackRot()
    {
        Vector3 endRot = newEndRot - newStartRot;

        while (true)
        {
            t = elapsedTime / duration - 1;

            transform.rotation = Quaternion.Euler(endRot * (t * t * ((s + 1) * t + s) + 1) + newStartRot);

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseOutBackScale()
    {
        Vector3 endScale = newEndScale - newStartScale;

        while (true)
        {
            t = elapsedTime / duration - 1;

            transform.localScale = endScale * (t * t * ((s + 1) * t + s) + 1) + newStartScale;

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

    #region Bounce
    IEnumerator EaseOutBouncePos()
    {
        Vector3 endPos = newEndPos - newStartPos;

        while (true)
        {
            t = elapsedTime / duration;
            if (t < (1 / 2.75f))
            {
                if (useLocalPosition)
                    transform.localPosition = endPos * (7.5625f * t * t) + newStartPos;
                else
                    transform.position = endPos * (7.5625f * t * t) + newStartPos;
            }
            else if (t < (2 / 2.75f))
            {
                t -= (1.5f / 2.75f);

                if (useLocalPosition)
                    transform.localPosition = endPos * (7.5625f * (t) * t + .75f) + newStartPos;
                else
                    transform.position = endPos * (7.5625f * (t) * t + .75f) + newStartPos;
            }
            else if (t < (2.5 / 2.75))
            {
                t -= (2.25f / 2.75f);

                if (useLocalPosition)
                    transform.localPosition = endPos * (7.5625f * (t) * t + .9375f) + newStartPos;
                else
                    transform.position = endPos * (7.5625f * (t) * t + .9375f) + newStartPos;
            }
            else
            {
                t -= (2.625f / 2.75f);

                if (useLocalPosition)
                    transform.localPosition = endPos * (7.5625f * (t) * t + .984375f) + newStartPos;
                else
                    transform.position = endPos * (7.5625f * (t) * t + .984375f) + newStartPos;
            }

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseOutBounceRot()
    {
        Vector3 endRot = newEndRot - newStartRot;

        while (true)
        {
            t = elapsedTime / duration;

            if (t < (1 / 2.75f))
                transform.rotation = Quaternion.Euler(endRot * (7.5625f * t * t) + newStartRot);

            else if (t < (2 / 2.75f))
            {
                t -= (1.5f / 2.75f);

                transform.rotation = Quaternion.Euler(endRot * (7.5625f * (t) * t + .75f) + newStartRot);
            }
            else if (t < (2.5 / 2.75))
            {
                t -= (2.25f / 2.75f);

                transform.rotation = Quaternion.Euler(endRot * (7.5625f * (t) * t + .9375f) + newStartRot);
            }
            else
            {
                t -= (2.625f / 2.75f);

                transform.rotation = Quaternion.Euler(endRot * (7.5625f * (t) * t + .984375f) + newStartRot);
            }

            if (elapsedTime == duration)
            {
                _isInTransition = false;
                yield break;
            }

            yield return null;
            elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
        }
    }
    IEnumerator EaseOutBounceScale()
    {
        Vector3 endScale = newEndScale - newStartScale;

        while (true)
        {
            t = elapsedTime / duration;

            if (t < (1 / 2.75f))
                transform.localScale = endScale * (7.5625f * t * t) + newStartScale;

            else if (t < (2 / 2.75f))
            {
                t -= (1.5f / 2.75f);

                transform.localScale = endScale * (7.5625f * (t) * t + .75f) + newStartScale;
            }
            else if (t < (2.5 / 2.75))
            {
                t -= (2.25f / 2.75f);

                transform.localScale = endScale * (7.5625f * (t) * t + .9375f) + newStartScale;
            }
            else
            {
                t -= (2.625f / 2.75f);

                transform.localScale = endScale * (7.5625f * (t) * t + .984375f) + newStartScale;
            }

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

    IEnumerator NullAnimation() { yield break; }
}
