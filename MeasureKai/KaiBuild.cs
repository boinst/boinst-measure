namespace MeasureKai
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;

    using Boinst.Measure;

    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Mono.Cecil.Rocks;

    using FieldAttributes = Mono.Cecil.FieldAttributes;
    using MethodAttributes = Mono.Cecil.MethodAttributes;
    using MethodBody = Mono.Cecil.Cil.MethodBody;
    using TypeAttributes = Mono.Cecil.TypeAttributes;

    public class KaiBuild
    {
        private Measures measures;

        Assembly reflectionAssembly;
        AssemblyDefinition cecilAssembly;

        /*
        public struct Magnitude
        {
            public string Name;

            public double Value;
        }
        /*
        private readonly Magnitude[] magnitudes = 
        { 
            new Magnitude { Name = "Deca", Value = 1e1 },
            new Magnitude { Name = "Hecto", Value = 1e2 },
            new Magnitude { Name = "Kilo", Value = 1e3 },
            new Magnitude { Name = "Mega", Value = 1e6 },
            new Magnitude { Name = "Giga", Value = 1e9 },
            new Magnitude { Name = "Tera", Value = 1e12 },
            new Magnitude { Name = "Peta", Value = 1e15 },
            new Magnitude { Name = "Exa", Value = 1e18 },
            new Magnitude { Name = "Zetta", Value = 1e21 },
            new Magnitude { Name = "Yotta", Value = 1e24 },
            new Magnitude { Name = "Deci", Value = 1e-1 },
            new Magnitude { Name = "Centi", Value = 1e-2 },
            new Magnitude { Name = "Milli", Value = 1e-3 },
            new Magnitude { Name = "Micro", Value = 1e-6 },
            new Magnitude { Name = "Nano", Value = 1e-9 },
            new Magnitude { Name = "Pico", Value = 1e-12 },
            new Magnitude { Name = "Femto", Value = 1e-15 },
            new Magnitude { Name = "Atto", Value = 1e-18 },
            new Magnitude { Name = "Zepto", Value = 1e-21 },
            new Magnitude { Name = "Yocto", Value = 1e-24 }
        };*/



        public void ReadClasses()
        {
            string filename = "measureclasses.xml";
            using (var stream = File.OpenRead(filename))
            {
                var serializer = new XmlSerializer(typeof(Measures));
                this.measures = (Measures)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Base types for each dimension.
        /// </summary>
        private Dictionary<string, TypeDefinition> dimensionTypeMap;

        private void ReadDimensions()
        {
            if (cecilAssembly == null) throw new NullReferenceException("CecilAssembly not set. Call LoadAssemblies first.");
            if (reflectionAssembly == null) throw new NullReferenceException("ReflectionAssembly not set. Call LoadAssemblies first.");

            dimensionTypeMap = new Dictionary<string, TypeDefinition>();
            var types = cecilAssembly.MainModule.Types;
            foreach (TypeDefinition typeDefinition in types)
            {
                Type reflectionType = reflectionAssembly.GetType(typeDefinition.FullName);
                if (reflectionType == null) continue;
                DimensionAttribute attrib = reflectionType.GetCustomAttribute<DimensionAttribute>();
                if (attrib == null) continue;

                dimensionTypeMap.Add(attrib.Name, typeDefinition);
            }
        }

        private void LoadAssemblies()
        {
            string assemblyPath = Path.GetFullPath("Boinst.Measure.dll");
            reflectionAssembly = System.Reflection.Assembly.LoadFile(assemblyPath);
            cecilAssembly = Mono.Cecil.AssemblyDefinition.ReadAssembly(assemblyPath);
        }

        public void Extend()
        {
            ReadClasses();
            this.LoadAssemblies();
            this.ReadDimensions();

            var types = cecilAssembly.MainModule.Types;

            foreach (MeasureClass measureClass in measures)
            {
                TypeDefinition baseClass;
                if (!dimensionTypeMap.TryGetValue(measureClass.Dimension, out baseClass))
                    throw new Exception("Cannot find a base class for dimension " + measureClass.Dimension);

                TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;
                TypeDefinition t = new TypeDefinition("Boinst.MeasureKai.Extended", measureClass.Name, typeAttributes, baseClass);
                cecilAssembly.MainModule.Types.Add(t);

                MethodDefinition ctor = this.MakeConstructor(t);
                
                t.Methods.Add(ctor);

                this.AddFieldForConversionRate(t, measureClass);
                this.OverrideToStandardUnits(t);
                this.OverrideFromMethod(t);
            }

            foreach (TypeDefinition typeDefinition in types)
            {
                Type reflectionType = reflectionAssembly.GetType(typeDefinition.FullName);

                if (typeDefinition.Name != "Metres") continue;

                Debug.WriteLine("Class: " + typeDefinition.Name);

                Debug.WriteLine(typeDefinition);

                foreach (var field in typeDefinition.Fields)
                {
                    Debug.WriteLine("Field: " + field.FieldType.Name + " " + field.Name);
                }

                foreach (MethodDefinition md in typeDefinition.Methods)
                {
                    Debug.WriteLine("Method: " + md.Name);
                    
                    if (md.Body != null)
                    foreach (var instruction in md.Body.Instructions) Debug.WriteLine(instruction);
                }
            }

            // Rename everything from "Measure" to "MeasureKai"
            cecilAssembly.Name = new AssemblyNameDefinition("Boinst.MeasureKai", cecilAssembly.Name.Version);
            cecilAssembly.MainModule.Name = cecilAssembly.MainModule.Name.Replace("Boinst.Measure", "Boinst.MeasureKai");
            foreach (TypeDefinition typeDefinition in types)
            {
                if (typeDefinition.Namespace.StartsWith("Boinst.MeasureKai")) continue;
                typeDefinition.Namespace = typeDefinition.Namespace.Replace("Boinst.Measure", "Boinst.MeasureKai");
            }

            string outpath = Path.GetFullPath("../../Boinst.MeasureKai.dll");
            Console.WriteLine("Writing assembly "  + outpath);
            cecilAssembly.Write(outpath);
        }

        /// <summary>
        /// Make a Constructor that just calls the base constructor, with a single parameter of type double.
        /// </summary>
        /// <param name="forType"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public MethodDefinition MakeConstructor(TypeDefinition forType)
        {
            var baseType = forType.BaseType.Resolve();

            const MethodAttributes constructorAttributes = MethodAttributes.Public
                | MethodAttributes.HideBySig
                | MethodAttributes.SpecialName
                | MethodAttributes.RTSpecialName;
            TypeReference returnType = cecilAssembly.MainModule.Import(typeof(void));
            MethodDefinition ctor = new MethodDefinition(".ctor", constructorAttributes, returnType);

            // add the single parameter to the constructor
            ParameterDefinition parameterDefinition = new ParameterDefinition(cecilAssembly.MainModule.Import(typeof(double)));
            parameterDefinition.Name = "value";
            ctor.Parameters.Add(parameterDefinition);

            var baseConstructor = baseType.GetConstructors().First();

            ILProcessor cil = ctor.Body.GetILProcessor();
            cil.Append(cil.Create(OpCodes.Ldarg_0));  // "this"
            cil.Append(cil.Create(OpCodes.Ldarg_1));  // double value
            cil.Append(cil.Create(OpCodes.Call, baseConstructor)); // base(value)
            cil.Append(cil.Create(OpCodes.Ret));      // return "this"

            return ctor;
        }

        public void AddFieldForConversionRate(TypeDefinition type, MeasureClass measure)
        {
            var field = new FieldDefinition("conversion", FieldAttributes.Private, type.Module.Import(typeof(double)))
                { 
                    InitialValue = BitConverter.GetBytes(measure.Factor) 
                };
            type.Fields.Add(field);
        }

        /// <summary>
        /// Override the "ToStandardUnits" method.
        /// </summary>
        /// <param name="type"></param>
        public void OverrideToStandardUnits(TypeDefinition type)
        {
            var method = type.Override("ToStandardUnits");

            var getter = type.GetBaseMethod("get_Value");

            if (method.Body == null) method.Body = new MethodBody(method);

            var conversionField = type.Fields.First(f => f.Name == "conversion");

            ILProcessor cil = method.Body.GetILProcessor();
            cil.Append(cil.Create(OpCodes.Ldarg_0));                // load "this"
            cil.Append(cil.Create(OpCodes.Call, getter));           // call base.get_Value
            cil.Append(cil.Create(OpCodes.Ldfld, conversionField)); // load the field "conversion"
            cil.Append(cil.Create(OpCodes.Div));                    // base.Value / this.conversion
            // cil.Append(cil.Create(OpCodes.Stloc_0)); // pop the value from the stack to local storage
            // cil.Append(cil.Create(OpCodes.Br_S)); // transfer control to target instruction
            // cil.Append(cil.Create(OpCodes.Ldloc_0)); // load the value from local storage to the stack
            cil.Append(cil.Create(OpCodes.Ret));                    // return, pushing the value from the to of the stack to the callee's stack
        }

        /// <summary>
        /// Override the "From" method.
        /// </summary>
        /// <param name="type">
        /// The type to add the override to.
        /// </param>
        public void OverrideFromMethod(TypeDefinition type)
        {
            var method = type.Override("From");

            var constructor = type.GetConstructors().First();
            
            if (method.Body == null) method.Body = new MethodBody(method);

            var conversionField = type.Fields.First(f => f.Name == "conversion");

            var toStandardUnits = type.GetBaseType("Measure").GetMethods().First(m => m.Name == "ToStandardUnits");

            ILProcessor cil = method.Body.GetILProcessor();
            cil.Append(cil.Create(OpCodes.Ldarg_1));                      // push the argument to the stack
            cil.Append(cil.Create(OpCodes.Callvirt, toStandardUnits));    // call "ToStandardUnits" on the provided argument
            cil.Append(cil.Create(OpCodes.Ldfld, conversionField));       // load the field "conversion"
            cil.Append(cil.Create(OpCodes.Mul));                          // arg.ToStandardUnits * conversion
            cil.Append(cil.Create(OpCodes.Newobj, constructor));          // create a new instance
            // cil.Append(cil.Create(OpCodes.Stloc_0)); // pop the value from the stack to local storage
            // cil.Append(cil.Create(OpCodes.Br_S)); // transfer control to target instruction
            // cil.Append(cil.Create(OpCodes.Ldloc_0)); // load the value from local storage to the stack
            cil.Append(cil.Create(OpCodes.Ret));                    // return, pushing the value from the to of the stack to the callee's stack
        }
    }



    public static class TypeExtensions
    {
        public static T GetCustomAttribute<T>(this Type type, bool inherit = false) where T : class
        {
            var attribs = type.GetCustomAttributes(typeof(T), inherit);
            return attribs == null || attribs.Length == 0 ? null : attribs[0] as T;
        }
    }

    public static class TypeDefinitionExtensions
    {
        /// <summary>
        /// Override a method.
        /// </summary>
        /// <param name="typeDefinition"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static MethodDefinition Override(this TypeDefinition typeDefinition, string methodName)
        {
            var baseType = typeDefinition.BaseType.Resolve();
            if (baseType == null) throw new Exception("No base class");

            var baseMethod = baseType.Methods.First(m => m.Name == methodName);
            if (baseMethod == null) throw new Exception("No method " + methodName + " to override.");

            var module = typeDefinition.Module;

            var returnType = module.Import(baseMethod.ReturnType);
            var methodDefinition = new MethodDefinition(methodName, baseMethod.Attributes, returnType);

            // copy attributes
            methodDefinition.ImplAttributes = baseMethod.ImplAttributes;
            methodDefinition.SemanticsAttributes = baseMethod.SemanticsAttributes;

            // copy any parameters
            foreach (var parameterDefinition in baseMethod.Parameters)
            {
                var paramType = module.Import(parameterDefinition.ParameterType);

                var paramDef = new ParameterDefinition(paramType);

                methodDefinition.Parameters.Add(paramDef);
            }

            // "override": remove the 'NewSlot' and add 'ReuseSlot' attribute 
            methodDefinition.Attributes = baseMethod.Attributes & ~MethodAttributes.NewSlot;
            methodDefinition.Attributes |= MethodAttributes.ReuseSlot;

            // Add the new method to the type
            typeDefinition.Methods.Add(methodDefinition);

            // Give the method a body if it does not have one.
            if (methodDefinition.Body == null) methodDefinition.Body = new MethodBody(methodDefinition);

            return methodDefinition;
        }

        public static MethodDefinition GetBaseMethod(this TypeDefinition typeDefinition, string methodName)
        {
            var baseType = typeDefinition.BaseType.Resolve();
            while (baseType != null)
            {
                var baseMethod = baseType.GetMethods().FirstOrDefault(m => m.Name == methodName);
                if (baseMethod != null) return baseMethod;
                baseType = baseType.BaseType.Resolve();
            }

            throw new Exception("No base method " + methodName);
        }

        public static TypeDefinition GetBaseType(this TypeDefinition typeDefinition, string typeName)
        {
            var type = typeDefinition;
            do
            {
                if (type.Name == typeName) return type;
                type = type.BaseType == null ? null : type.BaseType.Resolve();
            }
            while (type != null);
            throw new Exception("No base type " + typeName);
        }

        public static object GetAttributeValue(this TypeDefinition type, string attributeFullName, string propertyName)
        {
            foreach (var attribute in type.CustomAttributes)
            {
                if (attribute.AttributeType.FullName != attributeFullName) continue;

                var prop = attribute.Properties.First(a => a.Name == propertyName);

                return prop.Argument.Value;
            }

            return null;
        }

        public static bool HasInterface(this TypeDefinition type, Type @interface)
        {
            string interfaceFullName = @interface.FullName;
            TypeDefinition ty = type;
            bool any = ty.Interfaces.Any(i => i.FullName.Equals(interfaceFullName));

            if (any) return true;

            var baseType = type.BaseType;
            if (baseType == null) return false;

            var bt = baseType.Resolve();

            return HasInterface(bt, @interface);
        }
    }
}