using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI text_display;
    public string[] sentences;
    public TextMeshProUGUI endtext;
    public TextMeshProUGUI storytext;
    public GameObject story_obj;
    public GameObject endcanvas;
    // Start is called before the first frame update

    private void Start()
    {

    }
    public void cstart()
    {
        //StopAllCoroutines(); 
        StartCoroutine(Write());
    }
    IEnumerator Write()
    {
        text_display.text = "";
        int index = Random.Range(0, 7);
        foreach (var a in sentences[index].ToCharArray())
        {
            text_display.text += a;
            yield return new WaitForSeconds(0.08f);
        }
    }
    // Update is called once per frame
    public void End()
    {
        endcanvas.SetActive(true);
        Invoke(nameof(startend), 0.3f);
    }
    public void startend()
    {
        StartCoroutine(EndWrite());
    }
    IEnumerator EndWrite()
    {
        text_display.text = "";
        string endsent = "In the dungeon's depths, the final gate reveals a heart-wrenching sight: Masha's mother, lifeless. Despair grips father and daughter as they exit. Yet, in shared grief, they find unity, forging a stronger family bond in the wake of tragedy.    ";
        foreach (var a in endsent.ToCharArray())
        {
            endtext.text += a;
            yield return new WaitForSeconds(0.08f);
        }
        yield return new WaitForSeconds(1.5f);
        GameObject.FindAnyObjectByType<MenuSystem>().LoadMainMenu();
    } public void Storyinite()
    {
        story_obj.SetActive(true);
        Invoke(nameof(story), 0.3f);
    }
    public void story()
    {
        StartCoroutine(storywrite());
    }
    IEnumerator storywrite()
    {
        text_display.text = "";
        string endsent = "In the big city of Elvaria, there exists the labyrinth of Jesha. Everyday countless adventurers enter the labyrinth to hunt down monsters and look for ancient treasures hidden within.\nIn the city of Jesha, fear grips hearts as the Labyrinth's dungeon unleashes terror. Suga, vanguard leader, mourns his wife's loss to goblins. Masha, resentful of her father's absence, ventures alone to save her mother. Witnessing her courage, Suga joins her. Together, they brave the dungeon to rescue Masha's mother.";
        foreach (var a in endsent.ToCharArray())
        {
            storytext.text += a;
            yield return new WaitForSeconds(0.08f);
        }
        story_obj.SetActive(false);
    }
    public void skip()
    {
        story_obj.SetActive(false) ;
    }
}
