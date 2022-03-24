using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CurrentObjective : MonoBehaviour
{
    public List<Objective> objectives;
    public List<Objective> secondLevelObjectives;
    public List<Text> objectiveText;
    
    private void Update()
    {
        if (objectives.Count == 0)
        {
            print("No more objectives!");
            return;
        }
        
        var currentObjective = objectives[0];
        
        if (currentObjective.IsCompleted())
        {
            objectives.RemoveAt(0);         
        }

        for (int i = 0; i < objectiveText.Count; i++)
        {
            if (i < objectives.Count)
            {
                if (i == 0)
                {
                    objectiveText[i].text = objectives[i].modifiedText;
                }
                else
                {
                    objectiveText[i].text = objectives[i].objectName;
                }
                objectiveText[i].color = i == 0 ? Color.white : new Color(0, 0, 0, 0.4f);
                objectiveText[i].gameObject.transform.localScale =
                    i == 0 ? Vector3.one * 0.4528294f : Vector3.one * 0.3955917f;
            }
            else
            {
                objectiveText[i].text = "";
            }
        }
    }
    [ContextMenu("Second Level")]
    public void SecondLevelSet()
    {
        objectives = secondLevelObjectives;
    }
}

[System.Serializable]
public class Objective
{
    public enum ObjectiveTypes
    {
        EnemiesRemaining, 
        KillBoss,
        Leave
    }

    public ObjectiveTypes objectiveType;
    public string objectName;
    public string modifiedText;
    public GameObject usableObject;
    public GameObject usableObject2;
    public List<EnemyStats> allEnemies;

    public bool IsCompleted()
    {
        switch (objectiveType)
        {
            case(ObjectiveTypes.EnemiesRemaining):
                modifiedText = objectName + " (" + EnemiesRemaining() + " remaining)";
                return EnemiesRemaining() == 0;
            case(ObjectiveTypes.Leave):
                var player = usableObject.transform.position;
                float distance = Vector3.Distance(player, usableObject2.transform.position);
                modifiedText = objectName;
                return distance <= 5;
            case(ObjectiveTypes.KillBoss):
                modifiedText = objectName;
                usableObject.SetActive(true);
                var boss = GameObject.FindGameObjectWithTag("Boss");
                if (!boss)
                    return false;
                return boss.GetComponent<EnemyStats>().health > 0;
        }
        return false;
    }

    private int EnemiesRemaining()
    {
        return allEnemies.Count(i => i.health > 0);
    }

}
