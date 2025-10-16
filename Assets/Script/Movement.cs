using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical).normalized;
      
        transform.position += direction * speed * Time.deltaTime;
        AnimateMovement(direction);
    }
    void AnimateMovement(Vector3 direction)
    {
        if(animator!= null)
        {
            bool isMoving = direction.magnitude > 0;
            animator.SetBool("isMoving", isMoving);
        }
        if(spriteRenderer != null)
        {
            if(direction.x > 0)
            {
                spriteRenderer.flipX = false; //qua phai
            }
            else if(direction.x <0)
            {
                spriteRenderer.flipX = true; //qua trai
            }
        }
    }
}
