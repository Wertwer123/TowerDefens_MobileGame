using System.Collections;
using General.Helper;
using UnityEngine;

namespace UI.Tweens
{
    [System.Serializable]
    public class UITween
    {
        [SerializeField] float duration;
        [SerializeField] protected TweenTransform from;
        [SerializeField] private TweenTransform to;
        [SerializeField] private RectTransform toAnimate;
        [SerializeField] private AnimationCurve tweenCurve;

        public void PlayTween(MonoBehaviour owner, bool reverse)
        {
            owner.StopCoroutine(Tween(reverse));
            owner.StartCoroutine(Tween(reverse));
        }
        private IEnumerator Tween(bool reverse)
        {
            WaitForEndOfFrame waitForEndOfFrame = new ();
            
            //Probably soon ill implement a timer class
            float t = reverse ? duration : 0.0f;

            while ((t < duration && !reverse) || (reverse && t > 0.0f))
            {
                if (reverse)
                {
                    t -= Time.deltaTime;
                }
                else
                {
                    t += Time.deltaTime;
                }

                float curve = tweenCurve.Evaluate(t / duration);
                
                Vector3 positionFrom = from.Position;
                Vector3 scaleFrom = from.Scale;
                Vector3 rotationFrom = from.EulerAngles;
                
                Vector3 positionTo = to.Position;
                Vector3 scaleTo = to.Scale;
                Vector3 rotationTo = to.EulerAngles;

                toAnimate.anchoredPosition = Vector3.Lerp(positionFrom, positionTo, curve);
                toAnimate.localScale = Vector3.Lerp(scaleFrom, scaleTo, curve);
                toAnimate.localEulerAngles = Vector3.Lerp(rotationFrom, rotationTo, curve);
                
                yield return waitForEndOfFrame;
            }
        }
    }
}