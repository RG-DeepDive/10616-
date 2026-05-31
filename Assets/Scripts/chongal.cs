using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // 총알 속도
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        // 총알의 오른쪽 방향(오브젝트의 앞방향)으로 속도를 줍니다.
        rigid.linearVelocity = transform.right * bulletSpeed;

        // 화면 밖으로 벗어난 총알이 무한히 쌓이지 않도록 5초 뒤 자동 삭제
        Destroy(gameObject, 5f);
    }

    // 적이나 벽에 부딪히면 총알이 파괴되게 하는 함수
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 대상이 적(Enemy)이거나 벽(Wall)이면 총알 삭제
        if (collision.CompareTag("Enemy") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}