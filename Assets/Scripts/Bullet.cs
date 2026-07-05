using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public bool isClone = false;

    // 플레이어 저장용 변수 plr
    GameObject plr;

    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");

        if (isClone == true)
        {
            GetComponent<SpriteRenderer>().enabled = true;

            float dirX = 1f;

            if (plr.GetComponent<SpriteRenderer>().flipX == true) // 플레이어 방향
            {
                dirX = -1f;
            }
            else if (plr.transform.localScale.x < 0)
            {
                dirX = -1f;
            }

            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(dirX * speed, 0f);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void LateUpdate()
    {
        if (isClone == false)
        {
            if (plr != null)
            {

                transform.position = plr.transform.position;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject b = Instantiate(gameObject, plr.transform.position, Quaternion.identity);
                b.GetComponent<Bullet>().isClone = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isClone == true)
        {
            if (col.CompareTag("Enemy") || col.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }
}