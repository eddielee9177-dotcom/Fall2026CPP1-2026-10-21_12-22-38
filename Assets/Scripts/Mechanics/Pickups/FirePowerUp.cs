using UnityEngine;

public class FirePowerUp : Pickup
{
    //touching this power-up lets Mario shoot fireballs - Pickup destroys the power-up afterwards
    public override void OnPlayerPickup(PlayerController player)
    {
        if (player != null)
        {
            player.hasFirePower = true;
        }
    }
}
