using Godot;
using System;

public static class Global
{
	public static SelectedSystemCard selectedSystemCard = null;
	public static SelectedCrewCard selectedCrewCard = null;
	public static Stats playerStats = null;
	public static Stats enemyStats = null;
	public static class Enums 
	{
		public enum CardType
		{
			ATTACK,
			DEFFENCE,
			SYSTEM,
			NEUTRAL,
			OTHER
		}
	}

	public static class State 
	{
		public readonly static string Player_Turn = "Player_Turn";
		public readonly static string Enemy_Turn = "Enemy_Turn";
	}
}
