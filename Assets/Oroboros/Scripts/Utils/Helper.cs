using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Helper
{
    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static byte[] SerializeObject<T>(T serializableObject)
    {
        T obj = serializableObject;

        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
    }

    public static T DeserializeObject<T>(byte[] serilizedBytes)
    {
        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(serilizedBytes))
        {
            return (T)formatter.Deserialize(stream);
        }
    }

}
