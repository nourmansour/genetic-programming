using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

	public GameObject botPrefab;
	public GameObject left;
	public GameObject right;
	//public GameObject ball;
	//public GameObject target;

	public int populationSize = 50;
	List<GameObject> population = new List<GameObject>();
	public static float elapsed = 0;
	public float trialTime = 5;
	int generation = 1;
	int numScored = 0;
	int lastNumScored = 0;

	GUIStyle guiStyle = new GUIStyle();
	void OnGUI()
	{
		guiStyle.fontSize = 25;
		guiStyle.normal.textColor = Color.white;
		GUI.BeginGroup(new Rect(10, 10, 250, 150));
		GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
		GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
		GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
		GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
		GUI.Label(new Rect(10, 100, 200, 30), "Last Gen Score: " + lastNumScored, guiStyle);
		GUI.Label(new Rect(10, 125, 200, 30), "Curr Gen Score: " + numScored, guiStyle);
		GUI.EndGroup();
	}
	void Awake()
	{
		//for (int i = 0; i < populationSize; i++)
		//{
		//	Vector3 startingPos = new Vector3(this.transform.position.x + i * 10, 0, 0);
		//	GameObject b = Instantiate(botPrefab, startingPos, this.transform.rotation);
		//	// init the brain which in turn inits the DNA for the arm lengths
		//	b.GetComponent<Brain>().Init();
		//	b.GetComponent<Brain>().DrawArms();
		//	//left = (b.transform.GetChild(2).gameObject).transform.GetChild(0).gameObject;
		//	//right = (b.transform.GetChild(2).gameObject).transform.GetChild(1).gameObject;

		//	//left.transform.localScale = new Vector3(1, Random.Range(0f, 10f), 1);
		//	//right.transform.localScale = new Vector3(1, Random.Range(0f, 10f), 1);
		//	population.Add(b);
		//}
		// the following arranges them in a grid
		float zPos = 0;
		for (int i = 0; i < populationSize/10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				Vector3 startingPos = new Vector3(this.transform.position.x + j * 10, 0, zPos);
				GameObject b = Instantiate(botPrefab, startingPos, this.transform.rotation);
				// init the brain which in turn inits the DNA for the arm lengths
				b.GetComponent<Brain>().Init();
				b.GetComponent<Brain>().DrawArms();
				population.Add(b);
			}
			zPos = zPos + 10;
		}
		
	}

	GameObject Breed(GameObject parent1, GameObject parent2, float xPos, float zPos)
	{
		Vector3 startingPos = new Vector3(xPos, 0, zPos);
		GameObject offspring = Instantiate(botPrefab, startingPos, this.transform.rotation);
		Brain b = offspring.GetComponent<Brain>();
		if (Random.Range(0, 100) == 1)
		{
			b.Init();
			b.dna.Mutate();
			b.DrawArms();
		}
		else
		{
			b.Init();
			b.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
			b.DrawArms();
		}
		return offspring;
	}

	void BreedNewPopulation()
	{
		List<GameObject> sortedList = population.OrderBy(o => (o.GetComponent<Brain>().timeToBox)).ToList();

		population.Clear();
		float position = 0;
		float zPos = 0;
		//breed upper half of sorted list
		for (int i = 0; i < (int)(sortedList.Count / 2.0f); i++)
		{
			if (position != 0 && position % 10 == 0)
			{
				zPos = zPos + 10;
				position = 0;
			}
			population.Add(Breed(sortedList[i], sortedList[i + 1], 10 * position, zPos));
			population.Add(Breed(sortedList[i + 1], sortedList[i], 10 * (position + 1), zPos));
			position = position + 2;
		}

		for (int i = 0; i < sortedList.Count; i++)
		{
			if (sortedList[i].GetComponent<Brain>().hitBox)
			{
				lastNumScored++;
			}
		}

		// destroy all parents and previous population
		for (int i = 0; i < sortedList.Count; i++)
		{
			Debug.Log("PM Destroy");
			Destroy(sortedList[i]);
		}
		generation++;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		for (int i = 0; i < population.Count; i++)
		{
			if (population[i].GetComponent<Brain>().hitBox && !population[i].GetComponent<Brain>().hitOnce)
			{
				numScored++;
				population[i].GetComponent<Brain>().hitOnce = true;
			}
		}
		if (elapsed >= trialTime)
		{
			numScored = 0;
			lastNumScored = 0;
			BreedNewPopulation();
			elapsed = 0;
			
		}
	}
}
