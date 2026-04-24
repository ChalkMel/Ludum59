using UnityEngine;
using UnityEngine.UI;

public class MemoryNotification : MonoBehaviour
{
    [SerializeField] private Button okButton;
    
    private OverloadSystem _overload;
    
    public void Setup(OverloadSystem ovld)
    {
        _overload = ovld;
        
        okButton.onClick.AddListener(() => Destroy(gameObject));
        
        Destroy(gameObject, 6f);
    }
}