using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_Sujet2_CC1 : MonoBehaviour
{
    [SerializeField] GameObject objet_a_copier;
    [SerializeField] Material material_affichage_couleur;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;


    void Start()
    {
        //Creation des éléments de base pour afficher un maillage.
        meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        meshCollider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        //Affectation du material pour afficher les couleurs
        meshRenderer.material = material_affichage_couleur;

        //Appelle de la fonction 
        convertir_maillage_avec_sommets_non_partagé();




    }

    void convertir_maillage_avec_sommets_non_partagé()
    {
        Mesh mesh_a_copier = objet_a_copier.GetComponent<MeshFilter>().mesh;


        //Le maillage a copier
        int[] indices_a_copier = mesh_a_copier.GetTriangles(0);
        Vector3[] sommets_a_copier = mesh_a_copier.vertices;
        Vector2[] uvs_a_copier = mesh_a_copier.uv;

        //Le nouveau maillage sans les sommets partagés
        int[] indices = new int[indices_a_copier.Length];
        Vector3[] sommets = new Vector3[indices_a_copier.Length];
        Vector2[] uvs = new Vector2[indices_a_copier.Length];

        //On crée un nouveau sommet pour chaque indices de du maillage a copié.
        for (int i = 0; i < indices_a_copier.Length; i++)
        {
            indices[i] = i;
            sommets[i] = sommets_a_copier[indices_a_copier[i]];
            uvs[i] = uvs_a_copier[indices_a_copier[i]];
        }

        //On crée une nouvelle mesh et on lui ajoute info du maillage modifié.
        Mesh mesh_modifie = new Mesh();
        mesh_modifie.vertices = sommets;
        mesh_modifie.uv = uvs;
        mesh_modifie.SetTriangles(indices, 0);
        mesh_modifie.RecalculateNormals();
        //On assigne le maillage du collider avec le nouveau maillage.
        meshCollider.sharedMesh = mesh_modifie;

        //On remplace notre maillage avec le modifié.
        meshFilter.mesh = mesh_modifie;
        meshFilter.mesh.name = "maillage_dupliqué";
    }
}
