using UnityEngine;

public class Moving3 : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sr;
    public bool isGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        pmove(Input.GetAxis("Horizontal"));
        jump();
    }
    void pmove(float x)
    {
        rigid.linearVelocity = new Vector2(x * 4, rigid.linearVelocity.y);

        if (x < 0)
            sr.flipX = true;
        else if (x > 0)
            sr.flipX = false;

    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigid.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

        
}
