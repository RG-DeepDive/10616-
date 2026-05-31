using UnityEngine;

public class player_move : MonoBehaviour //맹세컨대 챗지피티 안썼븐디다. ㅆㅓ도 제미나이 쓰지
{
    [Header("이동 및 속도 설정")]
    public float speed = 20f;
    public float maxspeed = 5f;

    [Header("점프 설정")]
    public float jumpForce = 10f;

    [Header("공격 설정 (총알 프리팹을 여기에 넣으세요)")]
    public GameObject bulletPrefab;  // 총알 프리팹 저장용 변수
    public Transform firePoint;     // 총알이 발사될 위치 (선택 사항, 없으면 주인공 중심에서 발사)

    private Rigidbody2D rigid;
    private float h;
    private bool isGrounded = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");

        // 점프 제어
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        //  Q 키를 누르면 마우스 방향으로 총알 발사!
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. 마우스의 화면 공간 좌표를 게임 속 2D 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 니까 Z축은 0으로 고정

        // 2. 발사할 위치 정하기 (firePoint가 지정 안 되어 있으면 플레이어 본인 위치)
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;

        // 3. 발사 위치에서 마우스 위치까지의 방향(각도) 계산하기
        Vector2 direction = (mousePosition - spawnPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. 계산된 각도로 총알 오브젝트 생성!
        Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));
    }

    void FixedUpdate()
    {
        // 이동 및 감속 로직 (기존과 동일)
        rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Force);

        if (rigid.linearVelocity.x > maxspeed)
            rigid.linearVelocity = new Vector2(maxspeed, rigid.linearVelocity.y);
        else if (rigid.linearVelocity.x < -maxspeed)
            rigid.linearVelocity = new Vector2(-maxspeed, rigid.linearVelocity.y);

        if (h == 0)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x * 0.1f, rigid.linearVelocity.y);
        }
    }

    void OnCollisionStay2D(Collision2D collision) { isGrounded = true; }
    void OnCollisionExit2D(Collision2D collision) { isGrounded = false; }
}