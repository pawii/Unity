using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	private int score = 0;

	public const int gridRows = 2;
	public const int gridColls = 4;
	public const float offsetX = 2f;
	public const float offsetY = 2.5f;

	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] private TextMesh textMesh;

	private int[] imageIndexes = { 0, 0, 1, 1, 2, 2, 3, 3};

	private MemoryCard firstCard;
	private MemoryCard secondCard;

	// Use this for initialization
	void Start () {
		Vector3 startPos = originalCard.transform.position;
		ShuffleArray (ref imageIndexes);
		for (int i = 0; i < gridRows; i++)
			for (int j = 0; j < gridColls; j++) {
				MemoryCard card;
				if (i == 0 && j == 0)
					card = originalCard;
				else {
					card = Instantiate (originalCard) as MemoryCard;
				}

				int id = j + i * gridColls;
				card.SetCard (imageIndexes[id], images[imageIndexes[id]]);

				float posX = startPos.x + j * offsetX;
				float posY = startPos.y - i * offsetY;
				card.transform.position = new Vector3 (posX, posY, startPos.z);

			}
	}

	private void ShuffleArray(ref int[] array){
		for (int i = 0; i < array.Length; i++) {
			int tmp = array [i];
			int r = Random.Range (i, array.Length);
			array [i] = array[r];
			array [r] = tmp;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OpenCard(MemoryCard card)
	{
		if (firstCard == null && secondCard == null) {
			firstCard = card;
			firstCard.SetCardState (true);
		}
		else if (firstCard != null && secondCard == null) {
			secondCard = card;
			secondCard.SetCardState (true);
			StartCoroutine (Check ());
		}
		
	}

	private IEnumerator Check(){
		if (firstCard.id == secondCard.id) {
			score++;
			textMesh.text = "Score: " + score;
		} else {
			yield return new WaitForSeconds (1);
			firstCard.SetCardState (false);
			secondCard.SetCardState (false);
		}
		firstCard = null;
		secondCard = null;
	}

	public void Restart(){
		Application.LoadLevel ("Main");
	}
}
