
using UnityEngine;

public class Medizin : Molek�l
{
    public float dauer = 8.0f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().MedizinGegessen(this);
    }
}
