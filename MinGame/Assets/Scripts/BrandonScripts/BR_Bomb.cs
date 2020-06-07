using System;
using System.Collections;
using UnityEngine;

public class BR_Bomb : MonoBehaviour
{
    [SerializeField]
    private float fuseStartTime = 5.0f;

    [SerializeField]
    private GameObject explosion;

    private Rigidbody2D rigidbody;
    private CircleCollider2D collider;
    private float currentFuseTime = 0.0f;
    private bool bombArmed = false;

    private bool exploding = false;
    private float explodingTime = 1.0f;

    public event EventHandler bombExploded;

    public void ArmBomb()
    {
        this.gameObject.SetActive(true);

        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        rigidbody.isKinematic = true;
        rigidbody.Sleep();
        

        currentFuseTime = 0.0f;
        bombArmed = true;
    }

    public void Throw(Vector3 throwVec)
    {
        rigidbody.WakeUp();
        rigidbody.isKinematic = false;
        rigidbody.velocity = throwVec;
        rigidbody.AddForce(throwVec);
        rigidbody.AddTorque(throwVec.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        if (bombArmed)
        {
            currentFuseTime += Time.deltaTime;

            if (currentFuseTime > fuseStartTime)
                Explode();
        }
        else if(exploding)
        {
            explosion.transform.localScale = new Vector3(explodingTime, explodingTime, 1.0f);

            explodingTime += Time.deltaTime * 50;

            if(explodingTime > 20.0f)
            {
                exploding = false;
                FireExplodeFinished();
            }
        }
    }

    private void Explode()
    {
        bombArmed = false;

        rigidbody.Sleep();
        rigidbody.isKinematic = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y), 5.0f);

        exploding = true;
        explosion.SetActive(true);

        foreach(Collider2D collider in hits)
        {
            BR_Player playerHit = collider.GetComponentInParent<BR_Player>();
            
            if (playerHit != null)
            {
                Vector2 distance = this.transform.position - collider.transform.position;

                playerHit.DamagePlayer(distance.magnitude * 2);

            }
        }

    }
       
    private void FireExplodeFinished()
    {
        var handler = bombExploded;
        if (handler != null)
            bombExploded(this, null);
    }
}
