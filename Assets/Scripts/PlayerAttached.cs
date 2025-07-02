using UnityEngine;

public class PlayerAttached : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Moving"))
        {
            transform.parent = hit.gameObject.transform;
        }
      
     
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            transform.parent = null;
        }
        
    }
}
