using UnityEngine;
using UnityEngine.UI;

public class MemoryNotification : MonoBehaviour
{
    [SerializeField] private Button okButton;
    
    private OverloadSystem overload;
    
    public void Setup(OverloadSystem ovld)
    {
        overload = ovld;
        
        okButton.onClick.AddListener(() => Destroy(gameObject));
        
        Destroy(gameObject, 6f);
    }
}