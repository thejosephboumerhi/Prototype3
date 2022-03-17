using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookScript: MonoBehaviour
{
    //Code source in wiki
    //Customizable Settings, select the wanted objects, and tweak values
    [SerializeField] private CharacterController _player;
    [SerializeField] private LineRenderer _grappleLine;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _grappleHook;
    [SerializeField] private Transform _hookEndPoint;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private LayerMask _grappleLayer;
    [SerializeField] private float _maxGrappleRange;
    [SerializeField] private float _grappleSpeed;
    [SerializeField] private Vector3 _placeOffset;

    //To see if you are using and used the grapple
    private bool isGrappling, isLaunching; 
    private Vector3 _grapplePoint;
    
    // Start is called before the first frame update
    //Makes sure everything false to start off, so that it can be properly used
    private void Start()
    {
        isGrappling = false;
        isLaunching = false;
        _grappleLine.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //Keeps the hook itself in front of you whenever, after you use it
        if (_grappleHook.parent == _handPosition)
        {
            _grappleHook.localPosition = Vector3.zero;
            _grappleHook.localRotation = Quaternion.Euler(new Vector3(0, 90, 90));
        }
        //You shoot the hook
        if (Input.GetMouseButtonDown(0))
        {
            LaunchHook();
        }
        //When you shoot to grapple, it brings to you the point, while disabling your controls for a moment, and putting the grapple back in your hands
        if (isGrappling)
        {
            
            _grappleHook.position = Vector3.Lerp(_grappleHook.position,_grapplePoint,_grappleSpeed * Time.deltaTime);

            _player.enabled = false;
            _playerBody.position = Vector3.Lerp(_playerBody.position, _grapplePoint - _placeOffset, _grappleSpeed * Time.deltaTime);
            if (Vector3.Distance(_grappleHook.position,_grapplePoint) < 0.5f)
            {
                _player.enabled = true;
                isGrappling = false;
                _grappleHook.SetParent(_handPosition);
                _grappleLine.enabled = false;
            }
        }
    }

    //Draws the line towards the point grappled to
    private void LateUpdate()
    {
        if (_grappleLine.enabled)
        {
            _grappleLine.SetPosition(0,_hookEndPoint.position);
            _grappleLine.SetPosition(1,_handPosition.position);

        }
    }

    //Looks at the boolean, raycast and links to a point that has the "Grapple" layer on it
    private void LaunchHook()
    {
        if (isGrappling || isLaunching) return;

        isLaunching = true;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast (ray, out hit, _maxGrappleRange,_grappleLayer))
        {
            _grapplePoint = hit.point;
            isGrappling = true;
            _grappleHook.parent = null;
            _grappleHook.LookAt(_grapplePoint);
            print("Connected!");
            _grappleLine.enabled = true;
        }

        isLaunching = false;
    }
}
