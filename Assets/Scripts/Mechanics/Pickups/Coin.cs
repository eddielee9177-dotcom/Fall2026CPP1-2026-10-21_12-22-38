using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] private int scoreValue = 10;

    public override void OnPlayerPickup(PlayerController player)
    {
        if (player != null)
        {
            player.score += scoreValue;
        }
    }
}
