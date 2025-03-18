using TMPro;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] float _pickUpRange = 5f;
    [SerializeField] float _pushStrength = 20f;
    [SerializeField] LayerMask _layerMask;

    //UI Elements
    [SerializeField] TextMeshProUGUI _pickUpPrompt;
    [SerializeField] TextMeshProUGUI _dropPrompt;

    public Transform pickUpLocation;

    private bool _isPicking = false;
    private Rigidbody _pickedRigidbody; 
    private Transform _pickedObject;

    private void Start()
    {
        _pickUpPrompt.gameObject.SetActive(false);
        _dropPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryPickUpOrDrop();
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowItem();
        }

        if (_isPicking && _pickedObject != null)
        {
            _pickedObject.position = Vector3.Lerp(_pickedObject.position, pickUpLocation.position, Time.deltaTime * 10);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _pickUpRange, _layerMask) && !_isPicking)
        {
            _pickUpPrompt.gameObject.SetActive(true);
        } else
        {
            _pickUpPrompt.gameObject.SetActive(false);
        }
    }

    void ThrowItem()
    {
        if(_isPicking)
        {
            _pickedRigidbody.AddForce(transform.forward * _pushStrength);
            TryPickUpOrDrop();
        } 
    }

    void TryPickUpOrDrop()
    {
        if (_isPicking)
        {
            _dropPrompt.gameObject.SetActive(false);
            _isPicking = false;

            if (_pickedRigidbody != null)
            {
                _pickedRigidbody.useGravity = true; 
                _pickedRigidbody.freezeRotation = false; 
                _pickedRigidbody = null;
            }

            _pickedObject = null;
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _pickUpRange, _layerMask))
        {
            _dropPrompt.gameObject.SetActive(true);
            _isPicking = true;
            _pickedObject = hit.transform;

            _pickedRigidbody = _pickedObject.GetComponent<Rigidbody>(); 
            if (_pickedRigidbody != null)
            {
                _pickedRigidbody.useGravity = false;
                _pickedRigidbody.freezeRotation = true; 
            }
        }
    }
}
