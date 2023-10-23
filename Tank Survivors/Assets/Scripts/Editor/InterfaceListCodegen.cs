using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityCodeGen;
using UnityEngine;

[Generator]
public class InterfaceEditorGenerator : ICodeGenerator
{
    public void Execute(GeneratorContext context)
    {
        IEnumerable<Type> interface_types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(
                a => a.GetTypes().Where(t => t.IsDefined(typeof(InterfaceEditorAttribute)))
            );

        foreach (Type interface_type in interface_types)
        {
            GenerateSerializedInterface(context, interface_type);
            GenerateSerializedInterfaceEditor(context, interface_type);
        }
    }

    private void GenerateSerializedInterface(GeneratorContext context, Type interface_type)
    {
        string interface_name = interface_type.Name;
        string ns = interface_type.Namespace;

        IEnumerable<Type> implemented_for_types = ClassNamesImplementingInterface(interface_type);
        string usings = ComposeUsings(implemented_for_types, ns);

        string fields = implemented_for_types
            .Select(
                ty =>
                    $"        [SerializeField]\r\n        private {ty.Name} {PascalToCamelCase(ty.Name)};"
            )
            .Aggregate("", (x, y) => x + (x.Length == 0 ? "" : "\r\n\r\n") + y);

        string switch_variants = implemented_for_types
            .Select(ty => $"                \"{ty.Name}\" => {PascalToCamelCase(ty.Name)},")
            .Aggregate("", (x, y) => x + (x.Length == 0 ? "" : "\r\n") + y);

        string template_path =
            Application.dataPath + "/Scripts/Editor/SerializedInterfaceTemplate.txt";
        string code = File.ReadAllText(template_path);
        code = code.Replace("|=USINGS=|", usings);
        code = code.Replace("|=NAMESPACE=|", ns);
        code = code.Replace("|=INTERFACE_NAME=|", interface_name);
        code = code.Replace("|=INTERFACE_CLASS_NAME=|", interface_name[1..]);
        code = code.Replace("|=FIELDS=|", fields);
        code = code.Replace("|=SWITCH_VARIANTS=|", switch_variants);

        context.OverrideFolderPath("Assets/Scripts/Generated");
        context.AddCode($"Serialized{interface_name[1..]}.cs", code);
    }

    private void GenerateSerializedInterfaceEditor(GeneratorContext context, Type interface_type)
    {
        string interface_name = interface_type.Name;
        string ns = interface_type.Namespace;

        IEnumerable<Type> implemented_for_types = ClassNamesImplementingInterface(interface_type);
        string usings = ComposeUsings(implemented_for_types, "");

        string implementing_type_names = implemented_for_types
            .Select(t => '"' + t.Name + '"')
            .Aggregate("", (x, y) => x + (x.Length == 0 ? "" : ", ") + y);

        string template_path =
            Application.dataPath + "/Scripts/Editor/SerializedInterfaceEditorTemplate.txt";
        string code = File.ReadAllText(template_path);

        code = code.Replace("|=USINGS=|", usings);
        code = code.Replace("|=INTERFACE_CLASS_NAME=|", interface_name[1..]);
        code = code.Replace("|=IMPLEMENTING_TYPE_NAMES=|", implementing_type_names);

        context.OverrideFolderPath("Assets/Scripts/Generated");
        context.AddCode($"Serialized{interface_name[1..]}Editor.cs", code);
    }

    private IEnumerable<Type> ClassNamesImplementingInterface(Type interface_type)
    {
        return Assembly
            .GetAssembly(interface_type)
            .GetTypes()
            .Where(type => interface_type.IsAssignableFrom(type) && !type.IsInterface);
    }

    private string PascalToCamelCase(string pascal)
    {
        return pascal[..1].ToLower() + pascal[1..];
    }

    private string ComposeUsings(IEnumerable<Type> types, string current_namespace)
    {
        return types
            .Select(t => t.Namespace)
            .Where(ns => ns != current_namespace)
            .Distinct()
            .Select(ns => $"using {ns};\r\n")
            .Aggregate("", (x, y) => x + y);
    }
}
