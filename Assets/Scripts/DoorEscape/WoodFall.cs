using UnityEngine;

public class WoodFall : MonoBehaviour
{
    public Rigidbody[] nailsRb;
    private Rigidbody rb;
    public bool woodIsFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool allNailsUnfrozen = true;

        foreach (Rigidbody nail in nailsRb)
        {
            if (nail.freezeRotation)
            {
                allNailsUnfrozen = false;
                break; // no need to keep checking
            }
        }

        if (allNailsUnfrozen)
        {
            rb.freezeRotation = false;
            rb.constraints = RigidbodyConstraints.None;
            woodIsFalling = true;
        }
    }
}
