using TMPro;
using UnityEngine;

public class Mio : MonoBehaviour
{
    [SerializeField] TMP_Text t;

    void Update()
    {
        t.text = "Ninguno";

        if (Input.GetAxis("Horizontal") != 0)
            t.text = Input.GetAxis("Horizontal").ToString();

        if (Input.GetAxis("Vertical") != 0)
            t.text = Input.GetAxis("Vertical").ToString();

        if (Input.GetButtonDown("Jump"))
        {
            print("Jump");
        }
    }


}
