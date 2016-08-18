using System;
using UnityEngine;

[Serializable]
public class VisitorDefinition
{
	public string name;
	public VisitorTypes type;
	public int popularityValue;
	public int minSpawnRange;
	public int maxSpawnRange;
	public Color color;
	public VisitorSpriteDefinition[] SpriteDefinitions;
}