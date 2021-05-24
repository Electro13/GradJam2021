using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredPot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pot());
    }

    public IEnumerator Pot()
    {
        yield return new WaitForSeconds(2f);

        GetComponent<Animator>().SetTrigger("shrink");

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
