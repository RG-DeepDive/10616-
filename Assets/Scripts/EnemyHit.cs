using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class EnemyHit : MonoBehaviour
{
    public float upForce = 5f;
    public float backForce = 7f;

    public int hp = 10;

    public TMP_Text hpText;

    bool isHit = false;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public Color normalColor = new Color(0.478f, 0.753f, 0.0f); // 7AC000 색깔
    public Color hitColor = Color.red;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = normalColor;

        UpdateHPUI();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            isHit = true;
            sr.color = hitColor;

            hp = hp - 1;

            UpdateHPUI();

            if (hp <= 0)
            {
                SceneManager.LoadScene("YouWin");
                return;
            }

            StartCoroutine(ResetColor());

            float bulletDirX = col.GetComponent<Rigidbody2D>().linearVelocity.x;
            float pushDir = 1f;

            if (bulletDirX < 0)
            {
                pushDir = -1f;
            }

            rb.linearVelocity = new Vector2(pushDir * backForce, upForce);
        }
    }

    void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = "Enemy HP: " + hp;
        }
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.2f);
        sr.color = normalColor;
    }
}