using UnityEngine;
using System.Collections;

//La clase EnemyFighter hereda de la clase Fighter
public class EnemyFighter : Fighter
{
    void Awake()
    {
        this.stats = new Stats(70, 70, 60, 50, 80, 35);
    }

    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = this.skills[Random.Range(0, this.skills.Length)];
        skill.SetEmitter(this);

        if (skill.needsManualTargeting)
        {
            Fighter[] targets = this.GetSkillTargets(skill);

            Fighter target = targets[Random.Range(0, targets.Length)];

            skill.AddReceiver(target);
        }
        else
        {
            this.AutoConfigureSkillTargeting(skill);
        }

        this.combatManager.OnFighterSkill(skill);
    }
}
