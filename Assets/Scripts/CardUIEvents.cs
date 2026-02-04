using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardUIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler,
    IEndDragHandler
{
    [SerializeField] private InputAction press, screenPos;
    private Vector3 curScreenPos;
    private bool isDragging;
    private Camera cam;
    private CardView cardView;
    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 startScale;
    private Vector3 _offset;
    private float _zDistanceToCamera;
    private float _originalZ;
    private float dragZOffset;
    private Collider col;

    private void Start()
    {
        cam = Camera.main;
        cardView = GetComponent<CardView>();
        col = GetComponent<Collider>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Hide();
        startPos = transform.position;
        startRot = transform.rotation.eulerAngles;
        startScale = transform.localScale;
        _originalZ = transform.position.z;

        _zDistanceToCamera = Mathf.Abs(cam.transform.position.z - transform.position.z);

        Vector3 mouseWorldPos = GetMouseWorldPosition(eventData.position);
        _offset = transform.position - mouseWorldPos;

        transform.position = new Vector3(transform.position.x, transform.position.y, _originalZ + dragZOffset);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.zero;

        col.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition(eventData.position);
        transform.position = mouseWorldPos + _offset;

        transform.position = new Vector3(transform.position.x, transform.position.y, _originalZ + dragZOffset);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        col.enabled = true;

        Collider[] other = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1));
        foreach (var obj in other)
        {
            if (ManaSystem.Instance.EnoughMana(cardView.GetCard().Cost) && obj.TryGetComponent(out IDropArea dropArea))
            {
                PlayCardGA playCardGA = new(cardView.GetCard());
                ActionSystem.Instance.Perform(playCardGA).Forget();
                return;
            }
        }

        float time = 0.2f;
        transform.DORotate(startRot, time);
        transform.DOMove(startPos, time);
        transform.DOScale(startScale, time);
    }

    // Helper to convert Screen Pixels (Input) to World 3D Coordinates
    private Vector3 GetMouseWorldPosition(Vector2 screenPos)
    {
        // We must preserve the object's Z distance, otherwise ScreenToWorldPoint fails
        Vector3 mousePoint = new Vector3(screenPos.x, screenPos.y, _zDistanceToCamera);
        return cam.ScreenToWorldPoint(mousePoint);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    private void Show()
    {
        //cardView.GetWrapper().SetActive(false);
        transform.localScale = new(1.2f, 1.2f, 1.2f);
        CardViewHoverSystem.Instance.Show(cardView.GetCard());
    }

    private void Hide()
    {
        //cardView.GetWrapper().SetActive(true);
        transform.localScale = Vector3.one;
        CardViewHoverSystem.Instance.Hide();
    }
}