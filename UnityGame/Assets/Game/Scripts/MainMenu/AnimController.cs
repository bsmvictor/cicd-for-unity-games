using UnityEngine;
using UnityEngine.UI;

public class AnimController : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] idle;
    public Sprite[] walking;
    public Sprite[] attacking;
    public Sprite[] jumping;
    public float framerate = 1f / 6f;

    private Image img;
    private int frame;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void ChangeAnimation(int x){
        switch (x)
        {
            case 1:
                sprites = walking;
            break;
            case 2:
                sprites = attacking;
            break;
            case 3:
                sprites = jumping;
            break;
            case 4:
                sprites = walking;
                img.transform.localScale = new(-0.5f,0.5f,0.5f);
            break;         
            default:
                sprites = idle;
                img.transform.localScale = new(0.5f,0.5f,0.5f);
            break;
        }
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Animate()
    {
        frame++;

        if (frame >= sprites.Length) {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length) {
            img.sprite = sprites[frame];
        }
    }

}