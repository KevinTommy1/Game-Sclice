using UnityEngine;

public class SetGOToPos : MonoBehaviour
{
    [SerializeField] private bool activate;
    [SerializeField] private bool continuous;
     private Vector2 location;

    private void Start()
    {
        location = transform.position;
        activate = false;
        continuous = false;
    }

    private void FixedUpdate()
    {
        if (continuous)
        {
            transform.position = location;
            return;
        }

        if (activate)
        {
            activate = false;
            transform.position = location;
        }
    }
}
