using UnityEngine;
 
public class SimpleSpawner : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject objetASpanner; // Glisse ton Prefab ici
    public Vector3 offset = new Vector3(0, 0.2f, 0); // Pour spawn un peu plus haut
 
    public void SpawnObjet()
    {
        if (objetASpanner != null)
        {
            // Calcule la position (position de la tasse + le petit décalage)
            Vector3 positionDeSpawn = transform.position + offset;
            // Crée le clone
            GameObject nouveauVerre = Instantiate(objetASpanner, positionDeSpawn, transform.rotation);
            // Sécurité : on s'assure qu'il est bien activé
            nouveauVerre.SetActive(true);
        }
        else
        {
            Debug.LogError("Il manque le Prefab dans le script sur : " + gameObject.name);
        }
    }
}
 
