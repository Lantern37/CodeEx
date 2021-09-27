using DG.Tweening;
using DG.Tweening.Core;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;


public static class DOTweenAsyncUtils
{
    // ________________________ Fade ________________________
    
    public static UniTask DoUniFade(this Graphic image, float endValue, float duration)
    {
        return new UniTask(() =>
        {
            var completionSource = new UniTaskCompletionSource();
            image.DOFade(endValue, duration)
                .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }); 
    }
    
    public static UniTask DoUniFade(this CanvasGroup canvasGroup, float endValue, float duration)
    {
        return new UniTask(() =>
        {
            var completionSource = new UniTaskCompletionSource();
            canvasGroup.DOFade(endValue, duration)
                .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }); 
    }
    
    public static UniTask DoUniFade(this AudioSource audioSource, float endValue, float duration)
    {
        return new UniTask(() =>
        {
            var completionSource = new UniTaskCompletionSource();
            audioSource.DOFade(endValue, duration)
                .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }); 
    }
    
    
    // ________________________ Transform ________________________
    
    public static UniTask DoUniScale(this Transform transform, Vector3 targetScale, float duration)
    {
        return new UniTask(() =>
        {
            var completionSource = new UniTaskCompletionSource();
            transform.DOScale(targetScale, duration)
                .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }); 
    }
    
    public static UniTask DoUniPunchScale(this Transform transform, Vector3 targetScale, float duration,
                                          int vibrato = 10, float elasticity = 1f)
    {
        return new UniTask(() =>
        {
            var completionSource = new UniTaskCompletionSource();
            transform.DOPunchScale(targetScale, duration, vibrato, elasticity)
                .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }); 
    }
    
    
    // ________________________ Common ________________________
    
    public static UniTask UniTo(DOGetter<float> getter, DOSetter<float> setter, float endValue, float duration)
    {
        return new UniTask(() =>
        {
            var completionSource = new UniTaskCompletionSource();
            DOTween.To(getter, setter, endValue, duration)
                .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }); 
    }
}


