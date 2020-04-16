using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {
	public GameObject botPrefab;
	List<float> genes = new List<float>();
	int dnaLength = 0;
	float maxValues = 0f;

	public DNA(int l, int v)
	{
		dnaLength = l;
		maxValues = v;
		SetRandom();
	}

	public void SetRandom()
	{
		genes.Clear();
		for (int i = 0; i < dnaLength; i++)
		{
			genes.Add(Random.Range(0f, maxValues));
		}
	}

	public void SetInt(int pos, int value)
	{
		genes[pos] = value;
	}

	public void Combine(DNA d1, DNA d2)
	{
		for (int i = 0; i < dnaLength; i++)
		{
			if (i < dnaLength / 2.0)
			{
				float c = d1.genes[i];
				genes[i] = c;
			}
			else
			{
				float c = d2.genes[i];
				genes[i] = c;
			}
		}
	}

	public void Mutate()
	{
		genes[Random.Range(0, dnaLength)] = Random.Range(0f, maxValues);
	}

	public float GetGene(int pos)
	{
		return genes[pos];
	}
}
