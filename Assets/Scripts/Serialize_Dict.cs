using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
WHY DOES THIS NOT WORK
I HAVE BEEN TRYING FOR SO LONG BUT IT DOES NOT WORK I GIVE UP



*/
public class CustomData
{
    public string item_tag {get ; set; }
    public Vector3 item_pos {get; set; }

    public Quaternion item_rot {get; set; }


    public CustomData (string tag, Vector3 pos, Quaternion rot) {
        item_tag = tag;
        item_pos = pos;
        item_rot = rot;
    }
}



[System.Serializable]
public class Serialize_Dict<TKey, CustomData> : Dictionary<TKey, CustomData>, ISerializationCallbackReceiver
{

    [SerializeField] private List<TKey> data_keys = new List<TKey>();
    [SerializeField] private List<CustomData> data_values = new List<CustomData>();

    public void CopyDict(Serialize_Dict<TKey, CustomData> other_dict) {
        this.Clear();

        if (other_dict != null) {
            for (int i = 0; i < other_dict.data_keys.Count; i++) {
                this.Add(other_dict.data_keys[i], other_dict.data_values[i]);
            }
        }
    }
    public void OnBeforeSerialize() {
        data_keys.Clear();
        data_values.Clear();

        foreach (KeyValuePair<TKey, CustomData> data_pair in this) {
            data_keys.Add(data_pair.Key);
            data_values.Add(data_pair.Value);
        }
    }

    // Load dict from list
    public void OnAfterDeserialize() {
        for (int i = 0; i < data_keys.Count; i++) {
            this.Add(data_keys[i], data_values[i]);
        }
    }
}
