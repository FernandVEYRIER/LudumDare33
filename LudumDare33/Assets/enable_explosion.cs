using UnityEngine;
using System.Collections;

public class enable_explosion : MonoBehaviour {
	
	private bool rigid = false;
	void OnTriggerEnter2D(Collider2D col) {
		
		if (col.tag == "Monster" && !rigid && col.GetComponent<MonsterController>().getDash()) {
			this.transform.parent.GetComponent<destroy_plateform>().destruct();
		}
	}

	public bool getRigid() {
		return rigid;
	}

	public void setRigid(bool rig) {
		rigid = rig;
	}
}
