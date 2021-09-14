using System.Collections;
using UnityEngine;

namespace EasingTC
{
    public class EasingPosition : EasingBase
    {
        #region Variables
        public bool useLocalPosition;
        public Vector3 endPosition;

        Vector3 defaultStartPos;
        Vector3 newStartPos;

        Vector3 newEndPos;
        #endregion

        #region Animation Choice
        protected override void Awake()
        {
            base.Awake();

            // Select the animation and intialize default values
            animationToPlay = EasePos;
            defaultStartPos = useLocalPosition ? transform.localPosition : transform.position;
            newStartPos = defaultStartPos;
            newEndPos = endPosition;

            // Select which special ease function will be used
            if (animationType == AnimationType.SpecialEase)
            {
                switch (specialEaseType)
                {
                    case SpecialEase.EaseInBack:
                        animationToPlay = EaseInBackPos;
                        break;

                    case SpecialEase.EaseOutBack:
                        animationToPlay = EaseOutBackPos;
                        break;

                    case SpecialEase.EaseOutBounce:
                        animationToPlay = EaseOutBouncePos;
                        break;
                }
            }
        }
        #endregion

        #region Functions
        public override void PlayAnimationInOut()
        {
            newEndPos = newEndPos == endPosition ? defaultStartPos : endPosition;
            newStartPos = useLocalPosition ? transform.localPosition : transform.position;

            base.PlayAnimationInOut();
        }

        protected IEnumerator EasePos()
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

        protected IEnumerator EaseOutBouncePos()
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
        #endregion
    }
}