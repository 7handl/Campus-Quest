
using UnityEngine;

public class VirusFrightened : VirusBehavior
{
    public SpriteRenderer body;

    public SpriteRenderer eyes;

    public SpriteRenderer blue;

    public SpriteRenderer white;

    public bool gegessen {  get; private set; }

    public override void Enable(float dauer)
    {
        base.Enable(dauer);

        this.body.enabled = false;

        this.eyes.enabled = false;

        this.blue.enabled = true;

        this.white.enabled = false;

        Invoke(nameof(Flash), dauer / 2.0f);
    }

    public override void Disable() 
    {
        base.Disable();

        this.body.enabled = true;

        this.eyes.enabled = true;

        this.blue.enabled = false;

        this.white.enabled = false;
    }
    
    private void Flash()
    {
        if (!this.gegessen)
        {
            this.blue.enabled = false;

            this.white.enabled = true;

            this.white.GetComponent<AnimatedSprite>().Restart();
        }
        
    }

    private void Gegessen()
    {
        this.gegessen = true;

        Vector3 position = this.virus.home.inside.position;

        position.z = this.virus.transform.position.z;
        
        this.virus.transform.position = position;

        this.virus.home.Enable(this.dauer);

        this.body.enabled = false;

        this.eyes.enabled = true;

        this.blue.enabled = false;

        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.virus.movement.speedMultiplier = 0.5f;

        this.gegessen = false;
    }

    private void OnDisable()
    {
        this.virus.movement.speedMultiplier = 1.0f;

        this.gegessen = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Zelle"))
        {
            if (this.enabled)
            {
                Gegessen();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;

            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);

                float distance = (this.virus.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;

                    maxDistance = distance;
                }
            }

            this.virus.movement.SetDirection(direction);
        }
    }
}
