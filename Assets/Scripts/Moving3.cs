using UnityEngine;

public class Moving3 : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sr;
    public bool isGround;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        pmove(Input.GetAxisRaw("Horizontal"));
    }

    void pmove(float x)
    {
        rigid.linearVelocity = new Vector2(x * 7, rigid.linearVelocity.y);

        if (x < 0)
            sr.flipX = true;
        else if (x > 0)
            sr.flipX = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        }
    }
}