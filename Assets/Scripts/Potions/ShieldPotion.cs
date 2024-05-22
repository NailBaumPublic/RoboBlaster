using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPotion : Item
{
	public ShieldPotion(Item_type _type, int _amount) : base(_type, _amount)
	{

	}
	public override void UseItem(PlayerInputController player)
	{
		PlayerManager.Instance.ActivateShieldPotion(player.isPlayerOne);
	}
}
