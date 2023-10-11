using SheetImporter;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public static class SOScripterGenerator
{
    private static string[] usings = new string[] { "System", "UnityEngine" };

    public static string GetSOSourceCode(string className, Dictionary<string, DataType> fields)
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append(GetUsingCode());
        stringBuilder.AppendLine();

        stringBuilder.Append(GetSOClassStart(className));
        stringBuilder.Append(GetVariables(fields));
        stringBuilder.Append(GetSOClassEnd());

        return stringBuilder.ToString();
    }

    private static string GetUsingCode()
    {
        StringBuilder stringBuilder = new StringBuilder();

        // using _____;

        for (int i = 0; i < usings.Length; i++)
        {
            stringBuilder.Append("using ");
            stringBuilder.Append(usings[i]);
            stringBuilder.Append(";\n");
        }

        return stringBuilder.ToString();
    }

    private static string GetSOClassStart(string className)
    {
        StringBuilder sb = new();

        sb.Append($"[CreateAssetMenu(menuName = \"SO/{className}SO\")]\n");
        sb.Append($"public class {className}SO : ScriptableObject\n");
        sb.Append("{\n");

        return sb.ToString();
    }

    private static string GetVariables(Dictionary<string, DataType> variables)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (var pair in variables)
        {
            // public type A;
            stringBuilder.Append("\tpublic ");
            stringBuilder.Append(GetLowerCamelCase(pair.Value.ToString()));
            stringBuilder.Append(" ");
            stringBuilder.Append(GetLowerCamelCase(pair.Key));
            stringBuilder.Append(";\n");
        }

        return stringBuilder.ToString();
    }

    private static string GetSOClassEnd()
    {
        return "}";
    }

    private static string GetLowerCamelCase(string str)
    {
        return str.FirstCharacterToLower();
    }
}
