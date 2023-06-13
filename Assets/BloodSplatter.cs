using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField]
    private  BloodParticle _particlePrefab;
    [SerializeField]
    private float _minForce = 1f;
    [SerializeField]
    private float _maxForce = 5f;

    public void Splatter()
    {

        int bloodAmount = (int)PlayerPrefs.GetFloat("blood");
        int bloodMax =  (int)(bloodAmount * 1.1f);
        int numParticles = Random.Range(bloodAmount, bloodMax);

        for (int i = 0; i < numParticles; i++)
        {
            BloodParticle particle = Instantiate(_particlePrefab, transform.position, Quaternion.identity);

            float forceMagnitude = Random.Range(_minForce, _maxForce);
            Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            particle.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
            
        }
    }
}
