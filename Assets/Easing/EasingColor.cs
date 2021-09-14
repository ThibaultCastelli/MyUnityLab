using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EasingColor : EasingBase
{
    #region Variables
    [SerializeField] Color endColor = Color.white;

    protected Color defaultStartColor;
    protected Color newStartColor;

    protected Color newEndColor;

    new Renderer renderer = null;
    Image image = null;
    #endregion

    #region Animation Choice
    protected override void Awake()
    {
        base.Awake();

        // Select the animation and intialize default values
        animationToPlay = EaseColor;
        if (!TryGetComponent<Renderer>(out renderer))
        {
            if (!TryGetComponent<Image>(out image))
            {
                Debug.LogError("ERROR : Can't find the renderer or the image on this gameobject.");
                return;
            }
            else
                defaultStartColor = image.color;
        }
        else
            defaultStartColor = renderer.material.color;

        newStartColor = defaultStartColor;
        newEndColor = endColor;

        // Select which special ease function will be used
        if (animationType == AnimationType.SpecialEase)
        {
            Debug.LogError("ERROR : Can't change the color with specials ease.");
            animationToPlay = NullAnimation;
        }
    }
    #endregion

    #region Functions
    public override void PlayAnimationInOut()
    {
        newEndColor = newEndColor == endColor ? defaultStartColor : endColor;
        newStartColor = renderer != null ? renderer.material.color : image.color;

        base.PlayAnimationInOut();
    }

    IEnumerator EaseColor()
    {
        while (true)
        {
            if (renderer != null)
                renderer.material.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));
            else if (image != null)
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
    #endregion
}
