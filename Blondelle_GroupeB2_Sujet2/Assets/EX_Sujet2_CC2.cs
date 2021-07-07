using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_Sujet2_CC2 : MonoBehaviour
{
    void Update()
    {
        //On change la couleur quand on appuie sur espace.
        if (Input.GetKeyDown("space"))
        {
            changer_couleur_aleatoire_triangles();
        }
    }

    void changer_couleur_aleatoire_triangles()
    {
        Mesh maillage = GetComponent<MeshFilter>().mesh;

        int[] triangles = maillage.triangles;
        Color[] colors = new Color[maillage.vertices.Length];

        //Tous les trois index on assigne une couleur au sommet. 
        for (int i = 0; i < maillage.triangles.Length ; i++)
        {
            Color couleur_aleatoire = new Color(0,0,0);

            if (i % 3 == 0)
            {
                couleur_aleatoire = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
            }

            colors[i] = couleur_aleatoire;
        }

        maillage.colors = colors;
        GetComponent<MeshFilter>().mesh = maillage;
    }
}
