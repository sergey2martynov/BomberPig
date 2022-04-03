using UnityEngine;

public class ProjectStarter : MonoBehaviour
{
    [SerializeField] private MapController _mapController;
    void Awake()
    {
        _mapController.CreateMap();
    }
}
