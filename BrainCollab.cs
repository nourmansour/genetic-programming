using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCollab : MonoBehaviour
{

	int DNALength = 2;
	public float timeToBox;
	public DNA dna;
	public int team;
	//public GameObject ball;
	//public GameObject bot;
	//bool alive = false; // not sure i need this
	public bool hitBox = false;
	public bool hitOnce = false;
	GameObject left;
	GameObject right;

	//void OnCollisionEnter(Collision obj)
	//{
	//	if (obj.gameObject.tag == "Hit")
	//	{
	//		hitBox = true;
	//		timeToBox = PopulationManager.elapsed;
	//	}
	//}

	public void Init(int teamNum)
	{
		// initialize DNA: left and right lengths
		dna = new DNA(DNALength, 10);
		//timeToBox = 10;
		//alive = false;
		hitBox = false;
		team = teamNum;

		//left = (this.transform.GetChild(2).gameObject).transform.GetChild(0).gameObject;
		//right = (this.transform.GetChild(2).gameObject).transform.GetChild(1).gameObject;

		//left.transform.localScale = new Vector3(1, dna.GetGene(0), 1);
		//right.transform.localScale = new Vector3(1, dna.GetGene(1), 1);
	}

	public void DrawArms()
	{
		left = (this.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;
		right = (this.transform.GetChild(0).gameObject).transform.GetChild(1).gameObject;

		left.transform.localScale = new Vector3(1, dna.GetGene(0), 1);
		right.transform.localScale = new Vector3(1, dna.GetGene(1), 1);
	}

	// Update is called once per frame
	void Update()
	{
		if (this.CompareTag("botNoBox"))
		{
			timeToBox = (this.transform.GetChild(2).gameObject).transform.GetComponent<BallCollides>().getTime();
			hitBox = (this.transform.GetChild(2).gameObject).transform.GetComponent<BallCollides>().getScored();
		}
		
	}

}
