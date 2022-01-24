using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject weapon;

    private Vector2 direction;
    private Vector2 pastDirection;
    private Vector2 selfPosByCam;
    private Vector3 mousePosition;

    void Start()
    {
        CallStart();
    }

    void Update()
    {
        CallUpdate();
        RotateArm();
    }

    private void FixedUpdate()
    {
        Movements();
    }

    protected override void Movements()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"),
                                Input.GetAxis("Vertical"));
        this.body.AddForce(direction * characterStats.speed);
    }

    private void RotateArm()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        selfPosByCam = GameManager.Instance.getMainCamera().WorldToScreenPoint(this.transform.position);

        mousePosition.x -= selfPosByCam.x;
        mousePosition.y -= selfPosByCam.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
