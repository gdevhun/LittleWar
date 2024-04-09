using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower4 : ArcherTowerBase
{
    // 스탯 조정
    private void Awake()
    {
        InitTower(500, 2.5f, 300);
    }

    // 타겟 공격
    // 아쳐타워4는 광역공격
    protected override IEnumerator Attack()
    {
        while (isTarget)
        {
            // 공격속도만큼 대기
            yield return new WaitForSeconds(attackSpeed);

            // 타워 애니메이션
            for(int i = 0; i < towerAnim.Count; i++)
            {
                towerAnim[i].SetTrigger("atkTrig");
            }

            // 사운드
            SoundManager.Instance.PlaySFX(SoundType.아쳐타워4화살, 0.5f);

            // 타워 애니메이션 실제 공격 타이밍과 싱크 맞춤
            yield return halfSeconds;

            // 타워 무기 가져오기
            GameObject towerWeapon = PoolManager.Instance.GetTowerWeapon(towerWeaponType);
            Rigidbody2D towerWeaponRigid = towerWeapon.GetComponent<Rigidbody2D>();

            // 위치 및 회전 초기화
            towerWeapon.transform.position = atkPos[0].transform.position;
            towerWeapon.transform.rotation = towerWeapon.transform.rotation;

            // 타워 무기 발사
            Vector2 direction = (target.position - towerWeapon.transform.position).normalized;
            towerWeaponRigid.velocity = direction * 15f;

            // 무기 발사 각도
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            towerWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // 광역 몬스터 체력 감소
            Collider2D[] hits = Physics2D.OverlapCircleAll(target.position, 1f);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    // 몬스터 Enemy 스크립트 접근해서 실제로 까야함
                    //hit.GetComponent<Enemy>().health -= basicDamage;
                    Debug.Log("광역 : " + hit.gameObject.name + ", 데미지 : " + basicDamage);
                }
            }

            // 디버깅용
            Debug.DrawLine(target.position + new Vector3(-1f, 0f, 0f), target.position + new Vector3(1f, 0f, 0f), Color.red, 2f);
            Debug.DrawLine(target.position + new Vector3(0f, -1f, 0f), target.position + new Vector3(0f, 1f, 0f), Color.red, 2f);
        }
    }
}
