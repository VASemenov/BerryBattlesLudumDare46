using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spread = 2f;
    public int number;
    public GameObject commander;
    private List<GameObject> _objects = new List<GameObject>();
    public List<EnvironmentGenerator> biomes;
    public bool grass = true;
    public EnvironmentGenerator top;
    public EnvironmentGenerator bottom;
    public EnvironmentGenerator left;
    public EnvironmentGenerator right;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        for (var i = 0; i < number; i++)
        {
            _objects.Add(Instantiate(
                prefabs[Random.Range(0, prefabs.Length)],
                transform.position + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0),
                Quaternion.Euler(Vector3.zero),
                transform
            ));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (commander == null) return;
        // if (Vector2.Distance(commander.transform.position, transform.position) > 10)
        // {
        //     CreateNewBiome();
        // }
        foreach (var prefab in _objects)
        {
            if (prefab == null)
            {
                _objects.Remove(prefab);
                break;
            }
            if (Vector2.Distance(commander.transform.position, prefab.transform.position) > 10)
            {
                prefab.SetActive(false);
                
            }
            else
            {
                prefab.SetActive(true);
            }
        }
        
    }

    
    private void CreateNewBiome()
    {
        if (Mathf.Abs(transform.position.x - commander.transform.position.x) >
            Mathf.Abs(transform.position.y - commander.transform.position.y))
        {
            if (commander.transform.position.x - transform.position.x < -10)
            {
                if (left != null) return;
                Debug.Log("LEFT");
                left = Instantiate(biomes[Random.Range(0, biomes.Count)],
                    transform.position - Vector3.left * (spread),
                    Quaternion.Euler(Vector3.zero),
                    transform);
                left.commander = commander;
                left.right = this;
            }
            else if (commander.transform.position.x - transform.position.x > -10)
            {
                if (right != null) return;
                Debug.Log("RIGHT");
                right = Instantiate(biomes[Random.Range(0, biomes.Count)],
                    transform.position + Vector3.right * (spread),
                    Quaternion.Euler(Vector3.zero),
                    transform);
                right.commander = commander;
                right.left = this;
            }
        }
        else
        {
            if (commander.transform.position.y - transform.position.y < -10)
            {
                
                if (bottom != null) return;
                Debug.Log("BOTTOM");
                bottom = Instantiate(biomes[Random.Range(0, biomes.Count)],
                    transform.position - Vector3.left * (spread),
                    Quaternion.Euler(Vector3.zero),
                    transform);
                bottom.commander = commander;
                bottom.top = this;
            }
            else if (commander.transform.position.y - transform.position.y > 10)
            {
                if (top != null) return;
                Debug.Log("TOP");
                top = Instantiate(biomes[Random.Range(0, biomes.Count)],
                    transform.position + Vector3.left * (spread),
                    Quaternion.Euler(Vector3.zero),
                    transform);
                top.commander = commander;
                top.bottom = this;
            }
        }
        
        
    }
    
    private void Add()
    {
        _objects.Add(Instantiate(
            prefabs[Random.Range(0, prefabs.Length)],
            transform.position + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0),
            Quaternion.Euler(Vector3.zero),
            transform
        ));
    }
}
