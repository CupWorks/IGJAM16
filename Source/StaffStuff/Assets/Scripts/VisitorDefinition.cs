using System;

[Serializable]
public class VisitorDefinition
{
	public string name;
	public VisitorTypes type;
	public int popularityValue;
	public int minSpawnRange;
	public int maxSpawnRange;
	public VisitorSpriteDefinition[] SpriteDefinitions;
}