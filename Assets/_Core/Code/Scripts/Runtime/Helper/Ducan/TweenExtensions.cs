using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Helper
{
    public enum Ease
    {
        Unset,
        Linear,
        InSine,
        OutSine,
        InOutSine,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InBack,
        OutBack,
        InOutBack,
        InElastic,
        OutElastic,
        InOutElastic,
        InBounce,
        OutBounce,
        InOutBounce,
        Flash,
        InFlash,
        OutFlash,
        InOutFlash,
        INTERNAL_Zero,
        INTERNAL_Custom
    }

    public enum UpdateMethod
    {
        Update,
        LateUpdate,
        FixedUpdate
    }

    public static class TweenExtensions
    {
        private static LeanTweenType ToLeanTweenEase(Ease ease)
        {
            switch (ease)
            {
                case Ease.Linear:
                    return LeanTweenType.linear;
                case Ease.InSine:
                    return LeanTweenType.easeInSine;
                case Ease.OutSine:
                    return LeanTweenType.easeOutSine;
                case Ease.InOutSine:
                    return LeanTweenType.easeInOutSine;
                case Ease.InQuad:
                    return LeanTweenType.easeInQuad;
                case Ease.OutQuad:
                    return LeanTweenType.easeOutQuad;
                case Ease.InOutQuad:
                    return LeanTweenType.easeInOutQuad;
                case Ease.InCubic:
                    return LeanTweenType.easeInCubic;
                case Ease.OutCubic:
                    return LeanTweenType.easeOutCubic;
                case Ease.InOutCubic:
                    return LeanTweenType.easeInOutCubic;
                case Ease.InQuart:
                    return LeanTweenType.easeInQuart;
                case Ease.OutQuart:
                    return LeanTweenType.easeOutQuart;
                case Ease.InOutQuart:
                    return LeanTweenType.easeInOutQuart;
                case Ease.InQuint:
                    return LeanTweenType.easeInQuint;
                case Ease.OutQuint:
                    return LeanTweenType.easeOutQuint;
                case Ease.InOutQuint:
                    return LeanTweenType.easeInOutQuint;
                case Ease.InExpo:
                    return LeanTweenType.easeInExpo;
                case Ease.OutExpo:
                    return LeanTweenType.easeOutExpo;
                case Ease.InOutExpo:
                    return LeanTweenType.easeInOutExpo;
                case Ease.InCirc:
                    return LeanTweenType.easeInCirc;
                case Ease.OutCirc:
                    return LeanTweenType.easeOutCirc;
                case Ease.InOutCirc:
                    return LeanTweenType.easeInOutCirc;
                case Ease.InBack:
                    return LeanTweenType.easeInBack;
                case Ease.OutBack:
                    return LeanTweenType.easeOutBack;
                case Ease.InOutBack:
                    return LeanTweenType.easeInOutBack;
                case Ease.InElastic:
                    return LeanTweenType.easeInElastic;
                case Ease.OutElastic:
                    return LeanTweenType.easeOutElastic;
                case Ease.InOutElastic:
                    return LeanTweenType.easeInOutElastic;
                case Ease.InBounce:
                    return LeanTweenType.easeInBounce;
                case Ease.OutBounce:
                    return LeanTweenType.easeOutBounce;
                case Ease.InOutBounce:
                    return LeanTweenType.easeInOutBounce;
                case Ease.Unset:
                case Ease.Flash:
                case Ease.InFlash:
                case Ease.OutFlash:
                case Ease.InOutFlash:
                case Ease.INTERNAL_Zero:
                case Ease.INTERNAL_Custom:
                default:
                    return LeanTweenType.linear;
            }
        }

        public static TweenSequence Sequence()
        {
            return new TweenSequence();
        }

        public static LTDescr SetEase(this LTDescr tween, Ease ease)
        {
            if (tween == null)
            {
                return null;
            }

            return tween.setEase(ToLeanTweenEase(ease));
        }

        public static LTDescr SetEase(this LTDescr tween, AnimationCurve curve)
        {
            if (tween == null)
            {
                return null;
            }

            return tween.setEase(curve);
        }

        public static LTDescr OnComplete(this LTDescr tween, Action onComplete)
        {
            if (tween == null)
            {
                return null;
            }

            return tween.setOnComplete(onComplete);
        }

        public static void DOKill(this LTDescr tween)
        {
            if (tween == null)
            {
                return;
            }

            LeanTween.cancel(tween.uniqueId);
        }

        public static LTDescr DOVirtual(this GameObject tweenObject, float from, float to, float time, Action<float> onUpdate, Action onComplete = null, float delay = 0, bool unscaledTime = false, UpdateMethod updateMethod = UpdateMethod.Update)
        {
            if (tweenObject == null)
            {
                return null;
            }

            LTDescr tween = LeanTween
                .value(tweenObject, from, to, time)
                .setDelay(delay)
                .setOnUpdate((float value) => onUpdate?.Invoke(value));

            if (onComplete != null)
            {
                tween.setOnComplete(onComplete);
            }

            if (unscaledTime)
            {
                tween.setIgnoreTimeScale(true);
            }

            _ = updateMethod;
            return tween;
        }

        public static LTDescr DOVolume(this AudioSource tweenObject, float resultValue, float time, float delay = 0, bool unscaledTime = false, UpdateMethod updateMethod = UpdateMethod.Update)
        {
            if (tweenObject == null)
            {
                return null;
            }

            LTDescr tween = LeanTween
                .value(tweenObject.gameObject, tweenObject.volume, resultValue, time)
                .setDelay(delay)
                .setOnUpdate((float value) =>
                {
                    if (tweenObject != null)
                    {
                        tweenObject.volume = value;
                    }
                });

            if (unscaledTime)
            {
                tween.setIgnoreTimeScale(true);
            }

            // LeanTween does not expose explicit update loop selection for Update/LateUpdate/FixedUpdate.
            // Kept for API compatibility with existing calls.
            _ = updateMethod;

            return tween;
        }

        public static LTDescr DOLocalMove(this Transform target, Vector3 endValue, float duration)
        {
            if (target == null)
            {
                return null;
            }

            Vector3 start = target.localPosition;
            return LeanTween
                .value(target.gameObject, 0f, 1f, duration)
                .setOnUpdate((float t) =>
                {
                    if (target != null)
                    {
                        target.localPosition = Vector3.LerpUnclamped(start, endValue, t);
                    }
                });
        }

        public static LTDescr DOLocalRotate(this Transform target, Quaternion endValue, float duration)
        {
            if (target == null)
            {
                return null;
            }

            Quaternion start = target.localRotation;
            return LeanTween
                .value(target.gameObject, 0f, 1f, duration)
                .setOnUpdate((float t) =>
                {
                    if (target != null)
                    {
                        target.localRotation = Quaternion.SlerpUnclamped(start, endValue, t);
                    }
                });
        }

        public static LTDescr DOLocalRotate(this Transform target, Vector3 endValue, float duration)
        {
            return target.DOLocalRotate(Quaternion.Euler(endValue), duration);
        }

        public static LTDescr DOFade(this Image target, float endValue, float duration)
        {
            if (target == null)
            {
                return null;
            }

            Color startColor = target.color;
            return LeanTween
                .value(target.gameObject, startColor.a, endValue, duration)
                .setOnUpdate((float alpha) =>
                {
                    if (target == null)
                    {
                        return;
                    }

                    Color next = target.color;
                    next.a = alpha;
                    target.color = next;
                });
        }

        public static LTDescr DOAnchorPos(this RectTransform target, Vector2 endValue, float duration)
        {
            if (target == null)
            {
                return null;
            }

            Vector2 start = target.anchoredPosition;
            return LeanTween
                .value(target.gameObject, 0f, 1f, duration)
                .setOnUpdate((float t) =>
                {
                    if (target != null)
                    {
                        target.anchoredPosition = Vector2.LerpUnclamped(start, endValue, t);
                    }
                });
        }

        public static LTDescr DOScale(this RectTransform target, Vector3 endValue, float duration)
        {
            if (target == null)
            {
                return null;
            }

            Vector3 start = target.localScale;
            return LeanTween
                .value(target.gameObject, 0f, 1f, duration)
                .setOnUpdate((float t) =>
                {
                    if (target != null)
                    {
                        target.localScale = Vector3.LerpUnclamped(start, endValue, t);
                    }
                });
        }

        public static LTDescr DOScale(this Transform target, Vector3 endValue, float duration)
        {
            if (target == null)
            {
                return null;
            }

            Vector3 start = target.localScale;

            return LeanTween
                .value(target.gameObject, 0f, 1f, duration)
                .setOnUpdate((float t) =>
                {
                    if (target != null)
                    {
                        target.localScale =
                            Vector3.LerpUnclamped(start, endValue, t);
                    }
                });
        }

        public static LTDescr DOLocalScale(this Transform target, Vector3 endValue, float duration)
        {
            return target.DOScale(endValue, duration);
        }

        public static LTDescr DOShakePosition(this Transform target, float duration, float strength, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true)
        {
            if (target == null)
            {
                return null;
            }

            Vector3 startLocalPosition = target.localPosition;
            float safeDuration = Mathf.Max(0.01f, duration);
            int safeVibrato = Mathf.Max(1, vibrato);

            return LeanTween
                .value(target.gameObject, 0f, safeDuration, safeDuration)
                .setOnUpdate((float elapsed) =>
                {
                    if (target == null)
                    {
                        return;
                    }

                    float t = Mathf.Clamp01(elapsed / safeDuration);
                    float fadeMultiplier = fadeOut ? (1f - t) : 1f;
                    float pulse = Mathf.Sin(t * Mathf.PI * safeVibrato);

                    float randomAngle = UnityEngine.Random.Range(-randomness, randomness);
                    Vector2 shakeDir = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.right;
                    Vector3 offset = (Vector3)(shakeDir * (strength * pulse * fadeMultiplier));

                    if (snapping)
                    {
                        offset.x = Mathf.Round(offset.x);
                        offset.y = Mathf.Round(offset.y);
                        offset.z = Mathf.Round(offset.z);
                    }

                    target.localPosition = startLocalPosition + offset;
                })
                .setOnComplete(() =>
                {
                    if (target != null)
                    {
                        target.localPosition = startLocalPosition;
                    }
                });
        }
    }

    public sealed class TweenSequence
    {
        private float cursorTime;
        private float lastAppendStart;

        public TweenSequence Append(LTDescr tween)
        {
            tween.setDelay(cursorTime);
            lastAppendStart = cursorTime;
            cursorTime += Mathf.Max(0f, tween.time);
            return this;
        }

        public TweenSequence Join(LTDescr tween)
        {
            tween.setDelay(lastAppendStart);
            var endTime = lastAppendStart + Mathf.Max(0f, tween.time);
            if (endTime > cursorTime)
            {
                cursorTime = endTime;
            }

            return this;
        }

        public TweenSequence AppendInterval(float delay)
        {
            cursorTime += Mathf.Max(0f, delay);
            lastAppendStart = cursorTime;
            return this;
        }

        public TweenSequence AppendCallback(Action callback)
        {
            LeanTween.delayedCall(cursorTime, callback);
            lastAppendStart = cursorTime;
            return this;
        }

        public TweenSequence Play()
        {
            return this;
        }
    }
}
