using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, 0.01f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(gameObject);
        }
    }
}
