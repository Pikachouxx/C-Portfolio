using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript1 : MonoBehaviour
{
    public Text total;
    int totalint;
    public AudioSource newstack;
    public static int krk;
    // public List<Transform> Platforms = new List<Transform>();
    //  public GameObject TitlePlatform;
    public GameObject Unit;
    //  public List<GameObject> Unite = new List<GameObject>();
    //  public Transform spawnPoint = new Vector3(transform.position);
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<GameObject> tiles = new List<GameObject>();
    List<int> inoxes = new List<int>();
    List<int> checkListFirst = new List<int>();
    List<int> CheckListSecond = new List<int>();
    //  public int amountEnemies = 20;
    public GameObject block;
    // public int width = 10;
    //public int height = 4;
    // Start is called before the first frame update
    public Transform pos;
    public Vector3 gek;
    public Vector3 kak;
    public int itox;
    public int firstIndex;
    public int listchek;
    public int listsecndchek;
    public int countOfChekList;
    public int secndcountOfChekList;
    public string tagOfPrefab = "green";
    void Start()
    {
        total.text = "0";
        newstack = GetComponent<AudioSource>();
        GameObject prefabs = new GameObject();
        kak = gek;
        for (int o = 0; o < 100; ++o)
        {

            spawnPoints.Add(Unit.transform.GetChild(o));

            Debug.Log(Unit.transform.GetChild(o) + "position child");
        }
        SpawnEnemies();
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform spawnPointss = spawnPoints[i];
            Debug.Log(spawnPointss + " spawnpointss");
            Transform spawnPoint = spawnPointss;
            tiles.Add(SpawnEnemy(spawnPoint));
            tiles[i].gameObject.AddComponent<BoxCollider>();// for use ray hit
            Debug.Log(tiles + " = NAME OF tiles " + tiles.Count + " = COUNT OF tiles ");
        }
    }
    GameObject SpawnEnemy(Transform spawnPoint)
    {
        Quaternion formethod = new Quaternion();
        // Сюда можно дописать использование пула для врагов или более сложный алгоритм выбора противника
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        if (prefab.tag == "orange")
        {
            gek.y = 0.48f;
            formethod.SetLookRotation(frst, scnd);
        }
        else if (prefab.tag == "blue")
        {
            formethod.Set(0, 90, 0, 0);
            gek.y = 0.38f;
        }
        else if (prefab.tag == "purple") { gek.y = 0.5f; }
        else if (prefab.tag == "green") { gek.y = 0.54f; }
        else { gek = kak; }
        return Instantiate(prefab, spawnPoint.position + gek, formethod);
    }
    void GetObject() // получаем объект
    {
        GameObject obj = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            obj = hit.transform.gameObject;
            firstIndex = tiles.IndexOf(obj);
            Debug.Log(firstIndex + " INDEX OF BUTTON CLICK");
            tagOfPrefab = tiles[firstIndex].tag;
            inoxes.Add(firstIndex);
            // first call for near items
            if ((firstIndex + 1) % 10 != 0)
            {
                if (tiles[firstIndex].CompareTag(tiles[firstIndex].tag) & tiles[firstIndex + 1].CompareTag(tiles[firstIndex].tag)) { inoxes.Add(firstIndex + 1); }
            }
            if (firstIndex != 0 & firstIndex % 10 != 0)
            {
                if (tiles[firstIndex].CompareTag(tiles[firstIndex].tag) & tiles[firstIndex - 1].CompareTag(tiles[firstIndex].tag)) { inoxes.Add(firstIndex - 1); }
            }
            if (firstIndex < 90)
            {
                if (tiles[firstIndex].CompareTag(tiles[firstIndex].tag) & tiles[firstIndex + 10].CompareTag(tiles[firstIndex].tag))
                {
                    inoxes.Add(firstIndex + 10);
                }
                else { Debug.Log("HAHA 90+"); }
            }
            if (firstIndex > 9)
            {
                if (tiles[firstIndex].CompareTag(tiles[firstIndex].tag) & tiles[firstIndex - 10].CompareTag(tiles[firstIndex].tag))
                {
                    inoxes.Add(firstIndex - 10);
                }
                else { Debug.Log("AHAH 9-"); }
            }
            Debug.Log(inoxes.Count + " COUNT OF INDEXEX OF LIST");
            foreach (int i in inoxes) { Debug.Log(i + "NAME OF INDEX"); }
            countOfChekList = 0;
            for (int i = 0; i < 10; i++)//make two lists of items
            {
                if (countOfChekList == 0 || countOfChekList != secndcountOfChekList)
                {
                    ToChekListFromInoxes();
                    Debug.Log(checkListFirst.Count + "============================================================================");
                    ToInoxesFromChekList();
                    Debug.Log(inoxes.Count + "-----------------------------------------------------------------------------");
                    countOfChekList = checkListFirst.Count;
                    ToChekListFromInoxes();
                    Debug.Log(checkListFirst.Count + "============================================================================");
                    ToInoxesFromChekList();
                    Debug.Log(inoxes.Count + "-----------------------------------------------------------------------------");
                    secndcountOfChekList = checkListFirst.Count;
                }
            }

            Debug.Log(tiles.Count + " TILES COUNT BEFORE DESTROY");

            for (int a = 0; a <= inoxes.Count - 1; a++) //destroy chosed items and make new
            {
                if (a <= inoxes.Count & inoxes.Count > 2)
                {
                    totalint += 10 + ((inoxes.Count - 3) * 10);
                    total.text = totalint.ToString();
                    Destroy(tiles[inoxes[a]]);
                    tiles.RemoveAt(inoxes[a]);
                    Transform spawnPoint = spawnPoints[inoxes[a]];
                    tiles.Insert(inoxes[a], SpawnEnemyAgain(spawnPoint));
                    tiles[inoxes[a]].gameObject.AddComponent<BoxCollider>();
                }
            }
            Debug.Log(tiles.Count + " TILES COUNT AFTER DESTROY");
            Debug.Log(tiles.Count + " TILES COUNT AFTER NEW INSTANIATE");
            if (inoxes.Count > 2)//play audio
            {
                newstack.Play();
            }
            inoxes.Clear();
            checkListFirst.Clear();
        }
    }
    void ToChekListFromInoxes() // modify second list
    {
        if (checkListFirst.Contains(inoxes[0])) { }
        else { if (tiles[inoxes[0]].CompareTag(tiles[firstIndex].tag)) { checkListFirst.Add(inoxes[0]); } }
        foreach (int a in inoxes)
        {
            int secondIndex = a;
            if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag)) { checkListFirst.Add(secondIndex); }


            if (secondIndex != 99 & (secondIndex + 1) % 10 != 0)
            {
                if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex + 1].CompareTag(tiles[firstIndex].tag))
                {
                    if (checkListFirst.Contains(secondIndex + 1)) { }
                    else
                    {
                        if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex + 1].CompareTag(tiles[firstIndex].tag))
                        { checkListFirst.Add(secondIndex + 1); }
                    }
                }
            }
            if (secondIndex != 0 & secondIndex % 10 != 0)
            {
                if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex - 1].CompareTag(tiles[firstIndex].tag))
                {
                    if (checkListFirst.Contains(secondIndex - 1)) { }
                    else
                    {
                        if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex - 1].CompareTag(tiles[firstIndex].tag))
                        { checkListFirst.Add(secondIndex - 1); }
                    }
                }
            }
            if (secondIndex < 90 & secondIndex > -1 & secondIndex + 10 < 100)
            {
                if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex + 10].CompareTag(tiles[firstIndex].tag))
                {
                    if (checkListFirst.Contains(secondIndex + 10)) { }
                    else
                    {
                        if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex + 10].CompareTag(tiles[firstIndex].tag))
                        {
                            checkListFirst.Add(secondIndex + 10);
                        }
                    }
                }
            }
            if (secondIndex > 9 & secondIndex < 100 & secondIndex - 10 > -1)
            {
                if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex - 10].CompareTag(tiles[firstIndex].tag))
                {
                    if (checkListFirst.Contains(secondIndex - 10)) { }
                    else
                    {
                        if (tiles[secondIndex].CompareTag(tiles[firstIndex].tag) & tiles[secondIndex - 10].CompareTag(tiles[firstIndex].tag))
                        { checkListFirst.Add(secondIndex - 10); }
                    }
                }
            }
        }
    }
    void ToInoxesFromChekList() // copy items to first list
    {
        foreach (int g in checkListFirst)
        {
            if (inoxes.Contains(g)) { }
            else { inoxes.Add(g); }
        }
    } 
    public GameObject SpawnEnemyAgain(Transform spawnPoint) // second spawn
    {
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Quaternion formethod = new Quaternion();
        if (prefab.tag == "orange")
        {
            gek.y = 0.48f;
            formethod.SetLookRotation(frst, scnd);
        }
        else if (prefab.tag == "blue")
        {
            formethod.Set(0, 90, 0, 0);
            gek.y = 0.38f;
        }
        else if (prefab.tag == "purple") { gek.y = 0.5f; }
        else if (prefab.tag == "green") { gek.y = 0.54f; }
        else { gek = kak; }

        return Instantiate(prefab, spawnPoint.position + gek, formethod);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetObject();
            Debug.Log("hit.transform.name");
        }
    }
}