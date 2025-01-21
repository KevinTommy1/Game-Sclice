using UnityEngine;

public class CoinFling : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private GeoUi geoUi;
    [SerializeField] private float MinimumFlingForce;
    [SerializeField] private float MaximumFlingForce;
   
    void Start()
    {
        geoUi = GameObject.Find("GeoUi").GetComponent<GeoUi>();
        Vector2 flingDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float flingForce = UnityEngine.Random.Range(MinimumFlingForce, MaximumFlingForce);
        rb.AddForce(flingDirection * flingForce);
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            geoUi.AddGeo(1);
            Destroy(gameObject);
        }
    }
}
