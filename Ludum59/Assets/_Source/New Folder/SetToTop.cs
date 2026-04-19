using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
  public class SetToTop : MonoBehaviour
  {
    private Button clickArea;

    private void Awake()
    {
      clickArea = GetComponent<Button>();
      var block = clickArea.colors;
      block.pressedColor = clickArea.colors.normalColor;
      clickArea.colors = block;
      clickArea.onClick.AddListener(BringTop);
    }
    
    public virtual void BringTop()
    {
      gameObject.transform.SetAsLastSibling();
    }
  }
