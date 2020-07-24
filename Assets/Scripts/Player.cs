using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 600, sightSpeed, life = 100;
    [SerializeField] private GameObject sight, projectile, control, healthBar;
    [SerializeField] private bool isTurn = false;
    
    protected SpriteRenderer sprite;
    protected CapsuleCollider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            sight.SetActive(true);
            PlayerMovements();
            PlayerShoot();
        }
        else
        {
            sight.SetActive(false);
        }
    }

    private void PlayerMovements()
    {
        transform.position += new Vector3(0, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
        sight.transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * sightSpeed * Time.deltaTime));
    }

    private void PlayerShoot()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerCollider.enabled = false;
            GameObject missile = Instantiate(projectile, sight.transform.position, sight.transform.rotation);
            isTurn = false;
        }
    }

    public void TookDamage(int damage)
    {
        life -= damage;

        healthBar.transform.localScale = new Vector3(life, healthBar.transform.localScale.y, 0);

        if (life <= 0)
        {
            gameObject.SetActive(false);
            control.GetComponent<GameControl>().PlayerLost();
        }
        else
            StartCoroutine("TookDamageCoroutine");
    }

    public void SetTurn(bool state)
    {
        isTurn = state;
    }

    public void SetCollision(bool state)
    {
        playerCollider.enabled = state;
    }

    IEnumerator TookDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }


}
