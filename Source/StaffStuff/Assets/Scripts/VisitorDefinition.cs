using System;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using UnityEngine.SceneManagement;

[Serializable]
public class VisitorDefinition
{
	public string name;
	public VisitorTypes type;
	public int popularityValue;
	public int minSpawnRange;
	public int maxSpawnRange;
	public Color color;
}
