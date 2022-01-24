using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [SerializeField] private GameObject arm;
    [SerializeField] private SpriteRenderer armSprite;
    [SerializeField] private Vector2 rightArmPosition;
    [SerializeField] private Vector2 leftArmPosition;
    [SerializeField] private GameObject weapon;
    [SerializeField] private SpriteRenderer sprite;

    private int armBaseLayer;

    private Vector2 direction;
    private Vector2 pastDirection;
    private Vector2 selfPosByCam;
    private Vector3 mousePosition;

    void Start()
    {
        CallStart();
        armBaseLayer = armSprite.sortingOrder;
    }

    void Update()
    {
        CallUpdate();

        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            RotateArm();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
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

        Flip(angle > -90 && angle < 90);

        arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Flip(bool faceRight)
    {
        sprite.flipX = !faceRight;
        armSprite.flipY = !faceRight;
        //armSprite.sortingOrder = faceRight ? armBaseLayer : armBaseLayer - 2;
        arm.transform.position = faceRight ? rightArmPosition : leftArmPosition;
    }
}
