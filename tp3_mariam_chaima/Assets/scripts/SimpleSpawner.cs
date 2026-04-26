using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject objetASpanner; // Glissez votre PREFAB de verre ici

    public void SpawnLeVerre()
    {
        // Cette ligne crée une COPIE du prefab à chaque clic
        Instantiate(objetASpanner, transform.position, transform.rotation);
    }
}
