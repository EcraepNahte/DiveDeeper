using UnityEngine;

public class AirBubblePickup : Pickup
{
    public int AirAdded = 25;
    public int BubbleSpeed = 3;
    public AudioClip BubbleCreatedSound;
    public AudioClip BubblePopSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        // float height = (Mathf.Sin(relativePosition) + 1.5f + Mathf.Sin(relativePosition * 1.75f) + 1f) / 4f;
        float sideMovement = Mathf.Sin(transform.position.y);

        transform.Translate(new Vector2(sideMovement, BubbleSpeed) * Time.fixedDeltaTime);
    }

    public override void TriggerPickup(DiverController player)
    {
        player.AddAir(AirAdded);
        SoundManager.PlayOneShot(BubblePopSound);
        Destroy(this.gameObject);
    }
}
