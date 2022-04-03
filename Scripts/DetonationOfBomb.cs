using System.Collections;
using UnityEngine;

public class DetonationOfBomb : MonoBehaviour
{
    [SerializeField] private DeathDetector _deathDetector;
    
    public IEnumerator ExplodeBomb(GameObject bomb, GameField gameField)
    {
        while (bomb.transform.localScale.x < 3.5)
        {
            bomb.transform.localScale += new Vector3(0.03f, 0.03f, 0);
            yield return null;
        }
        
        _deathDetector.CheckFieldsAround(gameField);

        Destroy(bomb);
    }
}
