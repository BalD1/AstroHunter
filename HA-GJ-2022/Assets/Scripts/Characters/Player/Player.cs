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
    [SerializeField] private float hurtPortrait_CD;
    [SerializeField] private Animator animator;

    public enum Skins
    {
        Base,
        Amogus,
        Fanta,
        Antoine,
        Doge,
    }
    private Skins currentSkin;

    private float hurtPortrait_TIMER;

    private int armBaseLayer;

    private Vector2 direction;
    private Vector2 pastDirection;
    private Vector2 selfPosByCam;
    private Vector3 mousePosition;

    protected override void Start()
    {
        base.Start();
        armBaseLayer = armSprite.sortingOrder;
        GameManager.Instance.ev_ReloadEvent.AddListener(PlayerReload);
        this.hpBar = UIManager.Instance.GetPlayerHPBar().gameObject;
        ChangeSkin(Skins.Base);
    }

    protected override void Update()
    {
        base.Update();

        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            RotateArm();

        if (hurtPortrait_TIMER > 0)
            hurtPortrait_TIMER -= Time.deltaTime;
        else
            UIManager.Instance.SetPlayerPortrait(false);
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

        weapon.GetComponent<Pistol>().Flip(faceRight);
    }

    public override void TakeDamages(int amount)
    {
        base.TakeDamages(amount);
        source.PlayOneShot(AudioManager.Instance.GetAudioClip(AudioManager.ClipsTags.playerHurt));

        UIManager.Instance.FillBar(ref hpBar, characterStats.currentHP, characterStats.maxHP);
        UIManager.Instance.SetPlayerPortrait(true);
        hurtPortrait_TIMER = hurtPortrait_CD;
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);

        UIManager.Instance.FillBar(ref hpBar, characterStats.currentHP, characterStats.maxHP);
    }

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.GameState = GameManager.GameStates.GameOver;
    }

    public void PlayerReload()
    {
        Heal(characterStats.maxHP);
    }

    public void UpgradeWeapon(int wave)
    {
        weapon.GetComponent<Pistol>().UpgradeWeapon(wave);
    }

    public void Render(bool r)
    {
        sprite.enabled = r;
        armSprite.enabled = r;
    }

    public void ChangeSkin(Skins _skin)
    {
        currentSkin = _skin;
        animator.SetBool("base", false);
        animator.SetBool("antoine", false);

        switch (currentSkin)
        {
            case Skins.Base:
        animator.SetBool("base", true);
                break;

            case Skins.Fanta:
                break;

            case Skins.Antoine:
                animator.SetBool("antoine", true);
                break;

            case Skins.Doge:
                break;
        }
    }
}
