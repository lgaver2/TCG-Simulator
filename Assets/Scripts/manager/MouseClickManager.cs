using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MouseClickManager : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera; 
    private InputAction click;
    private IClickable clickedObject;

    void Awake() 
    {
        click = new InputAction(binding: "<Mouse>/leftButton");
        click.performed += ctx => {
            RaycastHit hit; 
            Vector3 coor = Mouse.current.position.ReadValue();
            if (Physics.Raycast(gameCamera.ScreenPointToRay(coor), out hit)) 
            {
                clickedObject = hit.collider.GetComponent<IClickable>();
                if (clickedObject != null)
                    clickedObject.OnClick();
            }
        };
        click.canceled += ctx =>
        {
            clickedObject.OnDeClick();
            clickedObject = null;
        };
        click.Enable();
    }
}
