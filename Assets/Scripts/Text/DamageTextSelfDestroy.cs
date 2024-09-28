using UnityEngine;

public class DamageTextSelfDestroy : MonoBehaviour
{
    public void DeleteText()
    {
        Destroy(gameObject.transform.parent.gameObject);
        
    }
}
