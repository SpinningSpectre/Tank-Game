using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    [SerializeField] private List<string> attributes = new List<string>();

    public bool HasAttribute(string att)
    {
        return attributes.Contains(att);
    }

    public List<string> GetAttributes()
    {
        return attributes;
    }

    public static bool HasAttribute(Component obj, string att)
    {
        if (obj.TryGetComponent(out Attributes attr) && attr.HasAttribute(att))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool HasAttribute(GameObject obj, string att)
    {
        return HasAttribute(obj.transform, att);
    }

    public static bool AddAttribute(Component obj, string att)
    {
        if (obj.TryGetComponent(out Attributes attr))
        {
            attr.attributes.Add(att);
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool AddAttribute(GameObject obj, string att)
    {
        return AddAttribute(obj.transform, att);
    }
    public static bool RemoveAttribute(Component obj, string att)
    {
        if (obj.TryGetComponent(out Attributes attr))
        {
            attr.attributes.Remove(att);
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool RemoveAttribute(GameObject obj, string att)
    {
        return RemoveAttribute(obj.transform, att);
    }
}
