using UnityEngine;
using UnityEngine.UI;

public class ButtonOpenDoor : MonoBehaviour
{
    public GameObject openButton;
    private bool isHovering = false, isOpen;
    private Color initialColor;
    [SerializeField] GameObject cartelAbrir;
    [SerializeField] Animator animatorLeftDoor;
    [SerializeField] Animator animatorRightDoor;

    void Start()
    {
        openButton.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        initialColor = openButton.GetComponent<Image>().color;
    }

    void Update()
    {
        if (isHovering && Input.GetKeyDown(KeyCode.E))
        {
            openButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHovering = true;
            openButton.GetComponent<Image>().color = Color.red;
            cartelAbrir.SetActive(true);
            openButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHovering = false;
            openButton.GetComponent<Image>().color = initialColor;
            cartelAbrir.SetActive(false);
            openButton.SetActive(false);
            animatorRightDoor.SetTrigger("Toggle");
            animatorLeftDoor.SetTrigger("Toggle");
            isOpen = false;
        }
    }

    void OnButtonClick()
    {
        if (!isOpen)
        {
            animatorLeftDoor.enabled = true;
            animatorRightDoor.enabled = true;
            animatorLeftDoor.SetTrigger("Toggle");
            animatorRightDoor.SetTrigger("Toggle");
            isOpen = true;
        }
    }
}
