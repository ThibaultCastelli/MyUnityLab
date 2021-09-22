using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasingTC
{
    public static class EasingFunctions
    {
        #region Standard Eases
        public static float Linear(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            return Mathf.Lerp(start, end, animTime);
        }

        #region Quad
        public static float EaseInQuad(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * animTime * animTime + start;
        }
        public static float EaseOutQuad(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return -end * animTime * (animTime - 2) + start;
        }
        public static float EaseInOutQuad(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * animTime * animTime + start;
            animTime--;
            return -end * 0.5f * (animTime * (animTime - 2) - 1) + start;
        }
        #endregion

        #region Cubic
        public static float EaseInCubic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * animTime * animTime * animTime + start;
        }
        public static float EaseOutCubic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime--;
            end -= start;
            return end * (animTime * animTime * animTime + 1) + start;
        }
        public static float EaseInOutCubic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * animTime * animTime * animTime + start;
            animTime -= 2;
            return end * 0.5f * (animTime * animTime * animTime + 2) + start;
        }
        #endregion

        #endregion

        #region Special Eases
        public static float Spring(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime = (Mathf.Sin(animTime * Mathf.PI * (0.2f + 2.5f * animTime * animTime * animTime)) * Mathf.Pow(1f - animTime, 2.2f) + animTime) * (1f + (1.2f * (1f - animTime)));
            return start + (end - start) * animTime;
        }
        #endregion
    }
}
