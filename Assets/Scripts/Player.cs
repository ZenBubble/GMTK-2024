using pattern;
using UnityEngine;

public class Player : CollisionSubject
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        // ripped from GitHub, lmao
        if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * (speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left * (speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W)) transform.position += Vector3.up * (speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S)) transform.position += Vector3.down * (speed * Time.deltaTime);
    }
}