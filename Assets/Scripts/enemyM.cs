using UnityEngine;

public class enemy_move : MonoBehaviour
{
    [Header("이동 설정")]
    public float speed = 2f;          // 적의 이동 속도
    public float changeDirectionTime = 0.5f; // 몇 초마다 방향을 바꿀 것인가? (예: 3초에 한 번씩 왕복)

    private Rigidbody2D rigid;
    private int direction = -1;       // 처음엔 왼쪽(-1)으로 이동 시작
    private float timer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        timer = changeDirectionTime; // 타이머 초기화
    }

    void Update()
    {
        // 시간을 계속 흐르게 만듭니다.
        timer -= Time.deltaTime;

        // 설정한 시간이 지나면?
        if (timer <= 0)
        {
            direction *= -1;            // 방향을 반대로 바꾸고
            timer = changeDirectionTime; // 타이머를 다시 채웁니다.
            Flip();                     // 고개도 돌려줍니다.
        }
    }

    void FixedUpdate()
    {
        // 적에게 현재 방향으로 속도를 줍니다. (★Y축 중력은 그대로 유지!)
        rigid.linearVelocity = new Vector2(direction * speed, rigid.linearVelocity.y);
    }

    // 적의 이미지 좌우를 뒤집어주는 함수
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}