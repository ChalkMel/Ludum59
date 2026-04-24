using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
  public class SetToTop : MonoBehaviour
  {
    private Button _clickArea;

    private void Awake()
    {
      _clickArea = GetComponent<Button>();
      var block = _clickArea.colors;
      block.pressedColor = _clickArea.colors.normalColor;
      _clickArea.colors = block;
      _clickArea.onClick.AddListener(BringTop);
    }
    
    public virtual void BringTop()
    {
      gameObject.transform.SetAsLastSibling();
    }
  }
