using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float ThrowForce;
    [SerializeField] float ThrowForceRange;
    [SerializeField] float PositionXRange = 4;
    [SerializeField] float PositionYRange = -1.6f;
    [SerializeField] int pointValue;
    [SerializeField] ParticleSystem explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ThrowTarget();
    }
    void  ThrowTarget()
    {
        transform.position = new Vector3(Random.Range(-PositionXRange, PositionXRange), PositionYRange, 0);
        rb.AddForce(RandomVectorForce() * ThrowForce * Random.Range(0.8f, ThrowForceRange));
        rb.AddTorque(RadomFloat(), RadomFloat(), RadomFloat());
    }
    [SerializeField] float RangeForceMomel = 10f;
    float RadomFloat()
    {
        return Random.Range(-RangeForceMomel, RangeForceMomel);
    }
    [SerializeField] [Range(0,1)] float RangeForceDirection = 0.3f;
    Vector3 RandomVectorForce()
    {
        Vector3 res = new Vector3(Random.Range(-RangeForceDirection, RangeForceDirection), 1, 0);
        return res;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (pointValue != 0)
            GameManager.instance.UploadScore(pointValue);
        else
            GameManager.instance.UploadScore(Random.Range(-12,15));
        Instantiate(explosionEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!this.gameObject.CompareTag("Bad"))
        {
            GameManager.instance.GameOverApper();
        }
        Destroy(gameObject);
    }
}
