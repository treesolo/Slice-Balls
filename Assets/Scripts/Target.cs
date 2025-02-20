using UnityEditor;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private Rigidbody targetRB;
    public GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 12;
    private float torque = 10;
    private float xSpawnRange = 4;
    private float ySpawn = 1;
    public int pointValue;
    private int fallLiveDMG = 1;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
    }
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if(Input.GetKey(KeyCode.Mouse0) && gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.lives > 0)
        {
            gameManager.UpdateLivesScore(-fallLiveDMG);
        }
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }
    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), -ySpawn);
    }

}
