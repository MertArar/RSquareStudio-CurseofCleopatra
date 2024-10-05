using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] sections;
    public Transform player;
    public float deletionDistance = 200f;
    public float sectionLength = 100f;
    public int maxSections = 10;

    [SerializeField] static public float moveSpeed = 12f;
    public float maxSpeed = 25f;
    [SerializeField] private float currentSpeed; 

    private float speedIncreaseAmount = 0.75f;
    private float speedIncreaseInterval = 25f;
    private bool accesToMove = false;

    private List<GameObject> generatedSections = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateInitialSections();
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("YÜRÜTÜRLDÜ");
            accesToMove = true;
            StartCoroutine(IncreaseSpeedRoutine());
        }
        if (accesToMove == true)
        {
            MoveSections();
            DeleteSectionsBehindPlayer();
            if (generatedSections.Count < maxSections || generatedSections[generatedSections.Count - 1].transform.position.z < player.position.z + deletionDistance)
            {
                GenerateSection();
            }
        }

        
        currentSpeed = moveSpeed;
    }

    void GenerateInitialSections()
    {
        Debug.Log("Oluştu.");
        GameObject initialSection = Instantiate(sections[0], Vector3.zero, Quaternion.identity);
        generatedSections.Add(initialSection);

        for (int i = 1; i < maxSections; i++)
        {
            GenerateSection();
        }
    }

    void GenerateSection()
    {
        int randomIndex = Random.Range(0, sections.Length);
        Vector3 position = Vector3.zero;

        if (generatedSections.Count > 0)
        {
            position = generatedSections[generatedSections.Count - 1].transform.position + new Vector3(0, 0, sectionLength);
        }

        GameObject newSection = Instantiate(sections[randomIndex], position, Quaternion.identity);
        generatedSections.Add(newSection);
    }

    void MoveSections()
    {
        foreach (GameObject section in generatedSections)
        {
            section.transform.position -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }
    }

    void DeleteSectionsBehindPlayer()
    {
        for (int i = 0; i < generatedSections.Count; i++)
        {
            if (generatedSections[i].transform.position.z < 0 - deletionDistance)
            {
                Destroy(generatedSections[i]);
                generatedSections.RemoveAt(i);
                i--;
            }
        }
    }

    IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            moveSpeed += speedIncreaseAmount;

            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }
}
