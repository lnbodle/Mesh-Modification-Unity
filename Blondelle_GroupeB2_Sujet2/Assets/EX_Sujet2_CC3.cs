using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_Sujet2_CC3 : MonoBehaviour
{

    [SerializeField] Color couleur_de_selection;


    Color[] colors;
    [SerializeField] Color couleur_objet;

    [SerializeField] List<int[]> triangles_selectionne;
   
    void Start()
    {
        triangles_selectionne = new List<int[]>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            supprimer_triangle_selectionne();
 
        }

        if (Input.GetMouseButtonDown(0))
        {
            selectionner_triangle();
        }
    }

    void selectionner_triangle()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            int triangle_index = hit.triangleIndex;

            print(hit.triangleIndex); //Debug

            //Ajouter le triangle ala liste de triangle selectionnés
            ajouter_triangle_liste(triangle_index);
            mise_a_jour_couleurs_triangles_slectionnes();
        }

    }

    void ajouter_triangle_liste(int triangle_index)
    {
        Mesh maillage = GetComponent<MeshFilter>().mesh;
        int[] triangles = maillage.triangles;

        int p0, p1, p2;
        p0 = triangles[triangle_index * 3 + 2];
        p1 = triangles[triangle_index * 3 + 1];
        p2 = triangles[triangle_index * 3 + 0];

        int[] triangle = {p0, p1, p2 };
        triangles_selectionne.Add(triangle);
    }

    private void supprimer_triangle_selectionne()
    {
        Mesh maillage = GetComponent<MeshFilter>().mesh;

        //List<int> triangles = maillage.GetTriangles(maillage.subMeshCount);

        List<int> triangles = new List<int>(maillage.triangles);

        List<Vector3> sommets = new List<Vector3>(maillage.vertices);


        for (int i = 0 ; i < triangles_selectionne.Count ; i++)
        {
            for (int j = 0; j <= 2 ; j++)
            {
                int index = triangles_selectionne[i][j];
                //if (triangles.Contains(index))

                //{
///sommets.RemoveAt(index);
                    triangles.RemoveAt(index);
                    
                //}
                //sommets.RemoveAt(index);
            }
        }

        maillage.Clear();
        maillage.vertices = sommets.ToArray();
        maillage.triangles = triangles.ToArray();

        maillage.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = maillage;
        GetComponent<MeshCollider>().sharedMesh = maillage;

        triangles_selectionne.Clear();
        mise_a_jour_couleurs_triangles_slectionnes();
    }

    void mise_a_jour_couleurs_triangles_slectionnes()
    {
        Mesh maillage = GetComponent<MeshFilter>().mesh;

        Color[] colors = new Color[maillage.vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = couleur_objet;
        }

        for (int i = 0; i < triangles_selectionne.Count; i++)
        {
            colors[triangles_selectionne[i][0]] = couleur_de_selection;
            colors[triangles_selectionne[i][1]] = couleur_de_selection;
            colors[triangles_selectionne[i][2]] = couleur_de_selection;
        }

        maillage.colors = colors;
        GetComponent<MeshFilter>().mesh = maillage;
    }
}
