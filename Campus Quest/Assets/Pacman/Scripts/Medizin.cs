
using UnityEngine;

public class Medizin : Molekül
{
    public float dauer = 8.0f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().MedizinGegessen(this);
    }
}
