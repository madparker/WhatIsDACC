using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    Camera cam;
    Vector3 screenPosition;
    Vector3 worldPosition;
    Transform _selection;

    GameManager gameManager;

    //Changable Variables
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameManager = this.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, maxDistance, layerMask))
        {
            worldPosition = hitData.point;
            var selection = hitData.transform;

            _selection = selection;
        } else
        {
            if (_selection != null)
            {
                _selection.gameObject.GetComponent<MoleculeBehavior>().DeactivateSelf();
                gameManager.DeactivateInteractive();
                _selection = null;
            }
        }

        //this.transform.position = new Vector3(worldPosition.x, this.transform.position.y, worldPosition.z);

        if (_selection != null)
        {
            _selection.gameObject.GetComponent<MoleculeBehavior>().ActivateSelf();

            //Might cause performance errors
            gameManager.ActivateInteractive(_selection.GetComponent<MoleculeBehavior>().Name, _selection.GetComponent<MoleculeBehavior>().Description);
        }
    }
}
