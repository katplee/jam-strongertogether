using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        //code to create the saves directory
        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        //create the path in which a stream will be opened
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

        //open the stream using the path which was just created
        FileStream stream = File.Create(path);

        //with the stream created, data can now be serialized
        //serialization is done with the formatter
        formatter.Serialize(stream, saveData);

        //always close the file stream after serialization
        stream.Close();

        //to indicate a successful save
        return true;
    }

    public static object Load(string path)
    {
        //checks if the path is empty
        if (!File.Exists(path)) { return null; }

        //do the requirements to deserialize with a formatter
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream stream = File.Open(path, FileMode.Open);

        try
        {
            object data = formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            stream.Close();
            return null;
        }        
    }

    //this is where the serialization surrogats will be set
    private static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
