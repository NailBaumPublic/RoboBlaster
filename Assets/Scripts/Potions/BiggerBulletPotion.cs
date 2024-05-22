using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerBulletPotion : Item
{
	public BiggerBulletPotion(Item_type _type, int _amount):base(_type, _amount)
	{
		
	}
	public override void UseItem(PlayerInputController player)
	{
		PlayerManager.Instance.EnableBiggerBulletPotion(player.isPlayerOne);
	}
}
