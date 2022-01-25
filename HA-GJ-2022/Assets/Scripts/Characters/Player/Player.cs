using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [Header("Arm")]
    [SerializeField] private GameObject arm;
    [SerializeField] private SpriteRenderer armSprite;
    [Space]
    [SerializeField] private Vector2 rightArmPosition;
    [SerializeField] private Vector2 rightArmSpritePosition;
    [SerializeField] private Vector2 leftArmPosition;
    [SerializeField] private Vector2 leftArmSpritePosition;

    [Header("Misc")]
    [SerializeField] private GameObject weapon;
    [SerializeField] private SpriteRenderer sprite;

    private int armBaseLayer;

    private Vector2 direction;
    private Vector2 pastDirection;
    private Vector2 selfPosByCam;
    private Vector3 mousePosition;

    protected override void Start()
    {
        base.Start();
        armBaseLayer = armSprite.sortingOrder;
    }

    protected override void Update()
    {
        base.Update();

        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            RotateArm();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            Movements();
    }

    private void Movements()
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
        arm.transform.localPosition = faceRight ? rightArmPosition : leftArmPosition;
        armSprite.transform.localPosition = faceRight ? rightArmSpritePosition : leftArmSpritePosition;
    }

    public override void TakeDamages(int amount)
    {
        base.TakeDamages(amount);

        UIManager.Instance.FillBar(ref hpBar, characterStats.currentHP, characterStats.maxHP);
    }
}
