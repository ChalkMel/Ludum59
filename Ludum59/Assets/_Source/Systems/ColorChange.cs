using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class ColorChange : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private WindowSpawner manager;

    public void Change()
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1f, 2f).onComplete += () =>
        {
            canvasGroup.GetComponent<Image>().DOColor(Color.white, 1f);
            canvasGroup.DOFade(0, 2f);
            volume.profile.TryGet(out ColorAdjustments colorAdjustments);
            colorAdjustments.saturation.value = -100f;
            manager.spawnInterval -= 0.3f;
        };
    }
}
