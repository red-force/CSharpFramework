extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace RF
{
    public partial class GlobalClass
    {
        /// <summary>
        /// 常量類；Const
        /// </summary>
        public partial class Const
        {

            #region enum
            /// <summary>
            /// the result state of the method ValidateXMLWidthXSDResult
            /// </summary>
            /// <value>Failed</value>
            public enum ValidationResult
            {
                /// <summary>
                /// Failed to pass the validation.
                /// </summary>
                Failed = 0,
                /// <summary>
                /// Passed the validation.
                /// </summary>
                Passed = 1
            };
            #endregion

        }
        /// <summary>
        /// 工具類；Utils
        /// </summary>
        public partial class Utils
        {
            #region basic

            #region DO
            public partial class Do
            {
                #region Dynamical Call method of Class
                /// <summary>
                /// Dynamically Call Class Method
                /// </summary>
                /// <param name="typeofClass"></param>
                /// <param name="methodName"></param>
                /// <param name="args"></param>
                /// <param name="defaultValue"></param>
                /// <returns></returns>
				/// <example>
				/// <code language="C#" title="Dynamical Call Method o f Class">
				/// #region check the global translation method
				/// if (String.Empty == result || dictValue == result)
				/// {
				///     try
				///     {
				///         Object[] args = new Object[] { dictName, dictValue, domain, language };
				///         String methodName 
				///				= String.Join("_", (new string[] { "worldWide" }))
				///					.Replace("-", "__")
				///						.Replace("/", "_OR_").Replace(":", "_IS_");
				///         result 
				///				= Convert.ToString(
				///						RF.GlobalClass.Utils.Do.dynamicallyCallClassMethod(typeof(Dict)
				///						, methodName
				///						, args
				///						, defaultValue: result));
				///     }
				///     catch (Exception ex) { }
				/// }
				/// else { }
				/// #endregion
				/// </code>
				/// </example>
                public static object dynamicallyCallClassMethod(Type typeofClass, String methodName, Object[] args, object defaultValue = null)
                {
                    Object result = defaultValue;
                    //Object[] args = new Object[] { dictValue, domain, language };
                    //Type typeofClass = (Type)typeof(Dict);
                    Object obj = typeofClass.InvokeMember(null, System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.CreateInstance, null, null, null);
                    //String methodName = String.Join("_", (new string[] { dictName })).Replace("-", "__").Replace("/", "_OR_").Replace(":", "_IS_");
                    if (null != typeofClass.GetMethod(methodName))
                    {
                        result = (String)typeofClass.InvokeMember(methodName, System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.InvokeMethod, null, obj, args);
                    }
                    else { }
                    return result;
                }

                /// <summary>
                /// 動態的調用指定類的方法； invke dynamically the method of given class.
                /// </summary>
                /// <param name="assemblyName"></param>
                /// <param name="className"></param>
                /// <param name="methodName"></param>
                /// <param name="args"></param>
                /// <returns></returns>
                public static object dynamicalInvoke(String assemblyName,String className, String methodName, Object[] args )
                {
                    object result = DynaInvoke.InvokeMethod(assemblyName, className, methodName, args);
                    return result;
                }
                private class DynaInvoke
                {
                    // this way of invoking a function
                    // is slower when making multiple calls
                    // because the assembly is being instantiated each time.
                    // But this code is clearer as to what is going on
                    public static Object InvokeMethodSlow(string AssemblyName,
                           string ClassName, string MethodName, Object[] args)
                    {
                        // load the assemly
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(AssemblyName);

                        // Walk through each type in the assembly looking for our class
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.IsClass == true)
                            {
                                if (type.FullName.EndsWith("." + ClassName))
                                {
                                    // create an instance of the object
                                    object ClassObj = Activator.CreateInstance(type);

                                    // Dynamically Invoke the method
                                    object Result = type.InvokeMember(MethodName,
                                      System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                                           null,
                                           ClassObj,
                                           args);
                                    return (Result);
                                }
                            }
                        }
                        throw (new System.Exception("could not invoke method"));
                    }

                    // ---------------------------------------------
                    // now do it the efficient way
                    // by holding references to the assembly
                    // and class

                    // this is an inner class which holds the class instance info
                    public class DynaClassInfo
                    {
                        public Type type;
                        public Object ClassObject;

                        public DynaClassInfo()
                        {
                        }

                        public DynaClassInfo(Type t, Object c)
                        {
                            type = t;
                            ClassObject = c;
                        }
                    }


                    public static System.Collections.Hashtable AssemblyReferences = new System.Collections.Hashtable();
                    public static System.Collections.Hashtable ClassReferences = new System.Collections.Hashtable();

                    public static DynaClassInfo
                           GetClassReference(string AssemblyName, string ClassName)
                    {
                        if (ClassReferences.ContainsKey(AssemblyName) == false)
                        {
                            System.Reflection.Assembly assembly;
                            if (AssemblyReferences.ContainsKey(AssemblyName) == false)
                            {
                                AssemblyReferences.Add(AssemblyName,
                                      assembly = System.Reflection.Assembly.LoadFrom(AssemblyName));
                            }
                            else
                                assembly = (System.Reflection.Assembly)AssemblyReferences[AssemblyName];

                            // Walk through each type in the assembly
                            foreach (Type type in assembly.GetTypes())
                            {
                                if (type.IsClass == true)
                                {
                                    // doing it this way means that you don't have
                                    // to specify the full namespace and class (just the class)
                                    if (type.FullName.EndsWith("." + ClassName))
                                    {
                                        DynaClassInfo ci = new DynaClassInfo(type,
                                                           Activator.CreateInstance(type));
                                        ClassReferences.Add(AssemblyName, ci);
                                        return (ci);
                                    }
                                }
                            }
                            throw (new System.Exception("could not instantiate class"));
                        }
                        return ((DynaClassInfo)ClassReferences[AssemblyName]);
                    }

                    public static Object InvokeMethod(DynaClassInfo ci,
                                         string MethodName, Object[] args)
                    {
                        // Dynamically Invoke the method
                        Object Result = ci.type.InvokeMember(MethodName,
                          System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                               null,
                               ci.ClassObject,
                               args);
                        return (Result);
                    }

                    // --- this is the method that you invoke ------------
                    public static Object InvokeMethod(string AssemblyName,
                           string ClassName, string MethodName, Object[] args)
                    {
                        DynaClassInfo ci = GetClassReference(AssemblyName, ClassName);
                        return (InvokeMethod(ci, MethodName, args));
                    }
                }

                /// <summary>
                /// dynamically call method of the object
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="method"></param>
                /// <returns></returns>
                /// <remarks>
                /// http://snipplr.com/view/29683/converting-methodinfo-into-a-delegate-instance-to-improve-performance/
                /// </remarks>
                /// <example>
                /// MbrService.ResultMsg resultMsg = RF.GlobalClass.Utils.Do.MagicMethod<MbrService.MbrServiceSoapClient>(typeof(MbrService.MbrServiceSoapClient).GetMethod(p, new[] { typeof(string) }))(MSSC, jsonText) as MbrService.ResultMsg;
                /// </example>
                private static Func<T, object, object> MagicMethod2<T>(System.Reflection.MethodInfo method)
                {
                    Func<T, object, object> result = null;
                    try
                    {
                        var instance = System.Linq.Expressions.Expression.Parameter(typeof(T), "instance");
                        var argument = System.Linq.Expressions.Expression.Parameter(typeof(object), "argument");
                        System.Linq.Expressions.Expression[] expreAry =
                        method.GetParameters().Select(delegate(System.Reflection.ParameterInfo pi)
                        {
                            return System.Linq.Expressions.Expression.Convert(argument, pi.ParameterType);
                        }).ToArray() ?? new System.Linq.Expressions.Expression[] { };
                        var methodCall = System.Linq.Expressions.Expression.Call(
                            instance,
                            method,
                            arguments: expreAry
                            );
                        if (method.GetParameters().Length == 1)
                        {
                            var parameter = method.GetParameters().Single();
                            instance = System.Linq.Expressions.Expression.Parameter(typeof(T), "instance");
                            argument = System.Linq.Expressions.Expression.Parameter(typeof(object), "argument");
                            methodCall = System.Linq.Expressions.Expression.Call(
                                instance,
                                method,
                                arguments: System.Linq.Expressions.Expression.Convert(argument, parameter.ParameterType)
                                );
                        }
                        else
                        {
                        }
                        result = System.Linq.Expressions.Expression.Lambda<Func<T, object, object>>(
                            System.Linq.Expressions.Expression.Convert(methodCall, typeof(object)),
                            instance, argument
                            ).Compile();
                    }
                    catch (Exception ex) { }
                    return result;
                }
                
                /// <summary>
                /// Magic Method with One parameter. Func&lt;T, object, object&gt;
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="method"></param>
                /// <returns></returns>
                public static Func<T, object, object> MagicMethod<T>(System.Reflection.MethodInfo method)
                {
                    Func<T, object, object> result = null;
                    try
                    {
                        var instance = System.Linq.Expressions.Expression.Parameter(typeof(T), "instance");
                        var argument = System.Linq.Expressions.Expression.Parameter(typeof(object), "argument");

                        System.Linq.Expressions.MethodCallExpression methodCall = null;

                        int methodParametersLength = method.GetParameters().Length;
                        switch (methodParametersLength)
                        {
                            case 1:
                                #region method parameter length 1
                                var parameter = method.GetParameters().Single();
                                instance = System.Linq.Expressions.Expression.Parameter(typeof(T), "instance");
                                argument = System.Linq.Expressions.Expression.Parameter(typeof(object), "argument");
                                methodCall = System.Linq.Expressions.Expression.Call(
                                    instance,
                                    method,
                                    arguments: System.Linq.Expressions.Expression.Convert(argument, parameter.ParameterType)
                                    );
                                result = System.Linq.Expressions.Expression.Lambda<Func<T, object, object>>(
                                    System.Linq.Expressions.Expression.Convert(methodCall, typeof(object)),
                                    instance, argument
                                    ).Compile();
                                #endregion
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex) { }
                    return result;
                }

                /// <summary>
                /// Magic Method as U defined (Recommend)
                /// </summary>
                /// <typeparam name="T">Method Result Class</typeparam>
                /// <typeparam name="U">Method Class</typeparam>
                /// <param name="method">the method info
                /// <example language="C#">
                /// typeof(SRExample.ServiceSoapClient).GetMethod(methodName, new[] { typeof(string) })
                /// </example>
                /// </param>
                /// <returns></returns>
                public static U MagicMethod<T, U>(System.Reflection.MethodInfo method)
                {
                    U result = default(U);
                    try
                    {
                        var instance = System.Linq.Expressions.Expression.Parameter(typeof(T), "instance");
                        var argument = System.Linq.Expressions.Expression.Parameter(typeof(object), "argument");

                        System.Linq.Expressions.MethodCallExpression methodCall = null;

                        int methodParametersLength = method.GetParameters().Length;

                        #region method parameter U
                        System.Linq.Expressions.ParameterExpression[] parameterExpressionAry = new System.Linq.Expressions.ParameterExpression[method.GetParameters().Length + 1];
                        parameterExpressionAry[0] = instance;
                        System.Linq.Expressions.ParameterExpression[] parameterExpressionAry2 = new System.Linq.Expressions.ParameterExpression[method.GetParameters().Length + 0];

                        System.Linq.Expressions.Expression[] expreAry =
                        method.GetParameters().Select(delegate(System.Reflection.ParameterInfo pi, int idx)
                        {
                            var _argument = System.Linq.Expressions.Expression.Parameter(pi.ParameterType, "argument" + idx);
                            parameterExpressionAry2[idx] = _argument;
                            parameterExpressionAry[++idx] = _argument;
                            System.Linq.Expressions.Expression.Convert(_argument, pi.ParameterType);
                            return _argument;
                        }).ToArray() ?? new System.Linq.Expressions.Expression[] { };

                        methodCall = System.Linq.Expressions.Expression.Call(
                            instance,
                            method,
                            arguments: expreAry
                            );
                        try
                        {
                            result = System.Linq.Expressions.Expression.Lambda<U>(// Func<T, object, object,object, object>
                                System.Linq.Expressions.Expression.Convert(methodCall, method.ReturnType),
                                parameterExpressionAry
                                ).Compile();
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            try
                            {

                                methodCall = System.Linq.Expressions.Expression.Call(
                                    method,
                                    arguments: parameterExpressionAry2
                                    );
                                result = System.Linq.Expressions.Expression.Lambda<U>(// Func<T, object, object,object, object>
                                    body: methodCall,
                                    parameters: parameterExpressionAry2
                                    ).Compile();
                            }
                            catch (Exception exb)
                            {
                                exb.Message.ToString();
                            }
                        }
                        #endregion

                        #region backup
                        //if (method.GetParameters().Length == 1)
                        //{
                        //    var parameter = method.GetParameters().Single();
                        //    instance = System.Linq.Expressions.Expression.Parameter(typeof(T), "instance");
                        //    argument = System.Linq.Expressions.Expression.Parameter(typeof(object), "argument");
                        //    methodCall = System.Linq.Expressions.Expression.Call(
                        //        instance,
                        //        method,
                        //        arguments: System.Linq.Expressions.Expression.Convert(argument, parameter.ParameterType)
                        //        );
                        //    result = System.Linq.Expressions.Expression.Lambda<Func<T, object, object>>(
                        //        System.Linq.Expressions.Expression.Convert(methodCall, typeof(object)),
                        //        instance, argument
                        //        ).Compile();
                        //}
                        //else
                        //{
                        //    System.Linq.Expressions.ParameterExpression[] parameterExpressionAry = new System.Linq.Expressions.ParameterExpression[method.GetParameters().Length];
                        //    parameterExpressionAry[0] = instance;

                        //    System.Linq.Expressions.Expression[] expreAry =
                        //    method.GetParameters().Select(delegate(System.Reflection.ParameterInfo pi, int idx)
                        //    {
                        //        var _argument = System.Linq.Expressions.Expression.Parameter(pi.ParameterType, "argument" + idx);
                        //        parameterExpressionAry[idx] = _argument;
                        //        System.Linq.Expressions.Expression.Convert(_argument, pi.ParameterType);
                        //        return _argument;
                        //    }).ToArray() ?? new System.Linq.Expressions.Expression[] { };
                        //    methodCall = System.Linq.Expressions.Expression.Call(
                        //        instance,
                        //        method,
                        //        arguments: expreAry
                        //        );
                        //    //parameterExpressionAry[1] = (object)expreAry;

                        //    System.Linq.Expressions.ParameterExpression[] pea = new System.Linq.Expressions.ParameterExpression[2];

                        //    pea.Select(delegate(System.Linq.Expressions.ParameterExpression pe, int idx)
                        //    {
                        //        var _argument = System.Linq.Expressions.Expression.Parameter(typeof(Object), "argument" + idx);
                        //        pea[idx] = _argument;
                        //        return pe;
                        //    }).ToArray();
                        //    pea[0] = instance;
                        //    // var fc = Func<T, object, object>.CreateDelegate(Type.EmptyTypes, method: method);
                        //    result = System.Linq.Expressions.Expression.Lambda<Func<T, object, object>>(
                        //        System.Linq.Expressions.Expression.Convert(methodCall, typeof(object)),
                        //        pea
                        //        ).Compile();
                        //}
                        #endregion
                    }
                    catch (Exception ex) { ex.Message.ToString(); }
                    return result;
                }

                /// <summary>
                /// dynamically get property value of the object
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="obj"></param>
                /// <param name="propertyName"></param>
                /// <returns></returns>
                public static Func<T> MagicProperty<T>(Object obj, string propertyName)
                {
                    Func<T> result = null;
                    try
                    {
                        System.Linq.Expressions.Expression propertyExpr = System.Linq.Expressions.Expression.Property(expression: System.Linq.Expressions.Expression.Constant(obj), propertyName: propertyName);
                        result = System.Linq.Expressions.Expression.Lambda <Func<T>>(propertyExpr).Compile();
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }

                /// <summary>
                /// dynamically get field value of the object
                /// </summary>
                /// <param name="obj"></param>
                /// <param name="fieldName"></param>
                /// <returns></returns>
                public static T MagicField<T>(Object obj, string fieldName)
                {
                    T result = default(T);
                    try
                    {
                        Type type = obj.GetType();
                        result = (T)type.GetField(fieldName).GetValue(Activator.CreateInstance(type));
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }
                #endregion

                #region Dynamical Get Property Names
                /// <summary>
                /// Get Property Names of Object
                /// </summary>
                /// <param name="obj">any object</param>
                /// <returns></returns>
                public static string[] getPropertyNamesOfObject(object obj)
                {
                    string[] propertyNames = new String[] { };
                    try
                    {
                        if (null != obj)
                        {
                            propertyNames = obj.GetType().GetProperties().Select(p => p.Name).ToArray();
                        }
                        else { }
                    }
                    catch (Exception ex)
                    {
                    }
                    return propertyNames;
                }

                /// <summary>
                /// Get Method Names of Object
                /// </summary>
                /// <param name="obj">any object</param>
                /// <returns></returns>
                public static string[] getMethodNamesOfObject(object obj)
                {
                    string[] methodNames = new string [] { };
                    try
                    {
                        if (null != obj)
                        {
                            methodNames = obj.GetType().GetMethods().Select(p => p.Name).ToArray();
                        }
                        else { }
                    }
                    catch (Exception ex)
                    {
                    }
                    return methodNames;
                }

                #endregion

                #region CMD
                /// <summary> 
                /// Command 的摘要说明。 
                /// </summary> 
                /// <example >
                ///     <code description="hwo to use" language="C#">
                ///     Command cmd = new Command(); 
                ///     cmd.RunCmd("dir"); 
                ///     </code>
                /// </example>
                public class Command
                {
                    private System.Diagnostics.Process proc = null;
                    /// <summary> 
                    /// 构造方法 
                    /// </summary> 
                    public Command()
                    {
                        proc = new System.Diagnostics.Process();
                    }
                    /// <summary> 
                    /// 执行CMD语句 
                    /// </summary> 
                    /// <param name="cmd">要执行的CMD命令</param> 
                    public void RunCmd(string cmd)
                    {
                        proc.StartInfo.CreateNoWindow = true;
                        proc.StartInfo.FileName = "cmd.exe";
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.RedirectStandardError = true;
                        proc.StartInfo.RedirectStandardInput = true;
                        proc.StartInfo.RedirectStandardOutput = true;
                        proc.Start();
                        proc.StandardInput.WriteLine(cmd);
                        proc.Close();
                    }
                    /// <summary> 
                    /// 打开软件并执行命令 
                    /// </summary> 
                    /// <param name="programName">软件路径加名称（.exe文件）</param> 
                    /// <param name="cmd">要执行的命令</param> 
                    /// <returns>one line result</returns>
                    public string RunProgram(string programName, string arguments = "", string cmd = "")
                    {
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.StartInfo.CreateNoWindow = true;
                        proc.StartInfo.FileName = programName;
                        proc.StartInfo.Arguments = arguments;
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.RedirectStandardError = true;
                        proc.StartInfo.RedirectStandardInput = true;
                        proc.StartInfo.RedirectStandardOutput = true;
                        proc.Start();
                        if (cmd.Length != 0)
                        {
                            proc.StandardInput.WriteLine(cmd);
                        }
                        string result = proc.StandardOutput.ReadLine();
                        proc.Close();
                        return result;
                    }
                }
                #endregion

            }
            #endregion

            #region Convert
            /// <summary>
            /// <c>此類名將會被停用，請使用Convert；Deprecated，using Convert instead</c>類型轉換類；Convertion
            /// </summary>
            /// <example>
            ///     <code>
            ///         GlobalClass.Utils.Convertion gcuc = new GlobalClass.Utils.Convertion();
            ///     </code>
            /// </example>
            public partial class Convertion : Convert
            {
            }
            /// <summary>
            /// 類型轉換類；Convert
            /// </summary>
            /// <example>
            ///     <code>
            ///         GlobalClass.Utils.Convert gcuc = new GlobalClass.Utils.Convert();
            ///     </code>
            /// </example>
            public partial class Convert
            {
                #region Enum

                #region EnumValueToString
                /// <summary>
                /// 用于 转换 枚举（Enum）的 值为 字符串。
                /// <remarks>
                ///     <para>
                ///         <code>
                ///             GlobalClass.Utils.Convertion.EnumValueToString(monthOffSet);
                ///         </code>
                ///     </para>
                /// </remarks>
                /// </summary>
                /// <param name="enumValue">枚举值</param>
                /// <returns>字符串格式枚举值</returns>
                /// <example>
                ///     <code>
                ///         string enumStr = GlobalClass.Utils.Convertion.EnumValueToString(TypeCode.String);
                ///     </code>
                /// </example>
                public static string EnumValueToString(Enum enumValue)
                {
                    String result = System.Convert.ToString(System.Convert.ToInt64(enumValue));
                    return result;
                }
                #endregion

                #endregion

                #region String
                /// <summary>
                /// format string to number. Specially for the string value of width and height, which might be end with characters like "px" or "em".
                /// </summary>
                /// <param name="width">Can be String, Can be System.Web.UI.WebControls.Unit.</param>
                /// <param name="plus">default 0;</param>
                /// <returns>double</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             dialog.Style.Add("min-width", GlobalClass.Utils.Convertion.StringToNumber(this.Width) + "px");
                ///         </code>
                ///     </para>
                /// </example>
                public static double StringToNumber(object width, double plus = 0)
                {
                    if (width.Equals(new System.Web.UI.WebControls.Unit()))
                    {
                        System.Web.UI.WebControls.Unit unit = (System.Web.UI.WebControls.Unit)width;
                        if (unit.IsEmpty == true)
                        {
                            width = "0.0";// fix the "" bug.
                        }
                    }
                    return Math.Abs(System.Convert.ToDouble(System.Text.RegularExpressions.Regex.Match(width.ToString(), "[0-9]+[0-9.]*", System.Text.RegularExpressions.RegexOptions.ECMAScript).Value) + plus);
                }

                /// <summary>
                /// Encoding String using MD5
                /// </summary>
                /// <param name="str"></param>
                /// <returns></returns>
                public static string StringToMD5(string str)
                {
                    string resultStr = Utils.DB.MD5(str);
                    return resultStr ?? str;
                }

                /// <summary>
                /// Encrypt the string by using MD5
                /// </summary>
                /// <param name="input">string to be encrypted</param>
                /// <param name="md5Hash">the md5hash created by MD5 encryption function, e.g. System.Security.Cryptography.MD5CryptoServiceProvider(); Or MD5.Create();</param>
                /// <param name="stringifyMD5Bytes">the customizable delegate function to stringify the byte[], created by md5Hash.ComputeHash(string);</param>
                /// <returns>the encrypted string</returns>
                public static string GetMd5Hash(string input, System.Security.Cryptography.MD5 md5Hash = null, StringifyMD5Bytes stringifyMD5Bytes=null)
                {
                    md5Hash = md5Hash ?? new System.Security.Cryptography.MD5CryptoServiceProvider();
                    // Convert the input string to a byte array and compute the hash. 
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                    // initiate stringifyMD5Bytes
                    stringifyMD5Bytes = stringifyMD5Bytes ?? (delegate(byte[] MD5Bytes)
                    {
                        // Create a new String builder to collect the bytes 
                        // and create a string.
                        StringBuilder sBuilder = new StringBuilder();
                        // Loop through each byte of the hashed data  
                        // and format each one as a hexadecimal string. 
                        for (int i = 0; i < MD5Bytes.Length; i++)
                        {
                            sBuilder.Append(MD5Bytes[i].ToString("x2"));
                        }
                        return sBuilder.ToString();
                    });

                    // Return the hexadecimal string. 
                    return stringifyMD5Bytes(data);
                }

                /// <summary>
                /// Verify a hash against a string.
                /// </summary>
                /// <param name="hash">the encrypted string to be verified</param>
                /// <param name="input">the unencrypted string to encrypt</param>
                /// <param name="md5Hash">the md5hash created by MD5 encryption function, e.g. System.Security.Cryptography.MD5CryptoServiceProvider(); Or MD5.Create();</param>
                /// <param name="stringifyMD5Bytes">the customizable delegate function to stringify the byte[], created by md5Hash.ComputeHash(string);</param>
                /// <returns>the boolean result. true, passed the verification; false, not.</returns>
                public static bool VerifyMd5Hash(string hash, string input, System.Security.Cryptography.MD5 md5Hash = null, StringifyMD5Bytes stringifyMD5Bytes = null)
                {
                    md5Hash = md5Hash ?? new System.Security.Cryptography.MD5CryptoServiceProvider();
                    // Hash the input. 
                    string hashOfInput = GetMd5Hash(input, md5Hash, stringifyMD5Bytes);

                    // Create a StringComparer an compare the hashes.
                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                    if (0 == comparer.Compare(hashOfInput, hash))
                    {
                        return System.Convert.ToBoolean(GlobalClass.Const.ValidationResult.Passed);
                    }
                    else
                    {
                        return System.Convert.ToBoolean(GlobalClass.Const.ValidationResult.Failed);
                    }
                }

                /// <summary>
                /// Decode base64 string to normal String.
                /// </summary>
                /// <param name="base64Str">base64 string</param>
                /// <returns>normal string</returns>
                public static string Base64ToString(string base64Str)
                {
                    string resultStr = "";
                    resultStr = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64Str));
                    return resultStr;
                }

                /// <summary>
                /// Convert Hexadecimal value String to String
                /// </summary>
                /// <param name="hexadecimal">the hexadecimal with/without 0x prefix</param>
                /// <returns>String value or the Original Hexadeciaml</returns>
                public static string HexadecimalToString(string hexadecimal)
                {
                    string result = hexadecimal;
                    int value;
                    //// Convert the number expressed in base-16 to an integer. 
                    //int value = Int32.TryParse(hexadecimal.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out value) ? value : value;
                    //// Get the character corresponding to the integral value. 
                    //string stringValue = Char.ConvertFromUtf32(value);
                    //char charValue = (char)value;
                    //Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
                    //hexadecimal, value, stringValue, charValue);
                    try
                    {
                        result = Int32.TryParse(hexadecimal.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out value) ? Char.ConvertFromUtf32(value) : result;
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }

                /// <summary>
                /// Convert String to Hexadecimal value
                /// </summary>
                /// <param name="str">the string to convert</param>
                /// <param name="prefix">the prefix of the hexadecimal</param>
                /// <returns>the hexadecimal or the original string</returns>
                public static string StringToHexadecimal(string str, string prefix = "0x")
                {
                    string result = str;
                    try
                    {
                        result = String.Format(prefix + "{0:X}", str);
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }

                /// <summary>
                /// Convert Text to Image
                /// </summary>
                /// <param name="text"></param>
                /// <param name="font"></param>
                /// <param name="textColor"></param>
                /// <param name="backColor"></param>
                /// <param name="imageHeight"></param>
                /// <param name="imageWidth"></param>
                /// <param name="horizontalTextAlign"></param>
                /// <param name="verticalTextAlign"></param>
                /// <returns></returns>
                public static System.Drawing.Image StringToImage(String text, System.Drawing.Font font, System.Drawing.Color textColor = default(System.Drawing.Color), System.Drawing.Color backColor = default(System.Drawing.Color), int imageWidth = -1, int imageHeight = -1, string horizontalTextAlign = "center", string verticalTextAlign = "middle")
                {
                    textColor = (default(System.Drawing.Color) == textColor ? System.Drawing.Color.Black : textColor);
                    backColor = (default(System.Drawing.Color) == backColor ? System.Drawing.Color.Transparent : backColor);
                    //first, create a dummy bitmap just to get a graphics object
                    System.Drawing.Image img = new System.Drawing.Bitmap(1, 1);
                    System.Drawing.Graphics drawing = System.Drawing.Graphics.FromImage(img);

                    //measure the string to see how big the image needs to be
                    System.Drawing.SizeF textSize = drawing.MeasureString(text, font);

                    drawing.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    drawing.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    drawing.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    drawing.PageUnit = System.Drawing.GraphicsUnit.Pixel;
                    drawing.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //free up the dummy image and old graphics object
                    img.Dispose();
                    drawing.Dispose();

                    //create a new image of the right size
                    img = new System.Drawing.Bitmap((int)(-1 == imageWidth ? textSize.Width + 1 : imageWidth), (int)(-1 == imageHeight ? textSize.Height + 1 : imageHeight));

                    drawing = System.Drawing.Graphics.FromImage(img);

                    //paint the background
                    drawing.Clear(backColor);

                    //create a brush for the text
                    System.Drawing.Brush textBrush = new System.Drawing.SolidBrush(textColor);
                    
                    // apply the horizontal and vertical text align
                    int DrawnTextXCoordinate = 0;
                    int DrawTextYCoordinate = 0;
                    switch (horizontalTextAlign)
                    {
                        case "right": DrawnTextXCoordinate = System.Convert.ToInt32((img.Size.Width - textSize.Width)); break;
                        case "center": DrawnTextXCoordinate = (System.Convert.ToInt32((img.Size.Width - textSize.Width)) / 2); break;
                        case "left": break;
                        default: break;
                    }
                    switch (verticalTextAlign)
                    {
                        case "bottom": DrawTextYCoordinate = System.Convert.ToInt32((img.Size.Height - textSize.Height)); break;
                        case "middle": DrawTextYCoordinate = System.Convert.ToInt32((img.Size.Height - textSize.Height) / 2); break;
                        case "top": break;
                        default: break;
                    }

                    // draw text
                    drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    drawing.DrawString(text, font, textBrush, DrawnTextXCoordinate, DrawTextYCoordinate);

                    drawing.Save();

                    textBrush.Dispose();
                    drawing.Dispose();
                    
                    return img;

                }

                /// <summary>
                /// Convert String to Barcode.
                /// </summary>
                /// <param name="code"></param>
                /// <returns></returns>
                public static String StringToBarcode(string code = "WangZhi")
                {
                    return String.Join("", code.Select(delegate(char c)
                    {
                        string result = c.ToString();
                        result = result.Replace("0", "_|_|__||_||_|")
                            .Replace("1", "_||_|__|_|_||")
                            .Replace("2", "_|_||__|_|_||")
                            .Replace("3", "_||_||__|_|_|")
                            .Replace("4", "_|_|__||_|_||")
                            .Replace("5", "_||_|__||_|_|")
                            .Replace("7", "_|_|__|_||_||")
                            .Replace("6", "_|_||__||_|_|")
                            .Replace("8", "_||_|__|_||_|")
                            .Replace("9", "_|_||__|_||_|")
                            .Replace("a", "_||_|_|__|_||")
                            .Replace("b", "_|_||_|__|_||")
                            .Replace("c", "_||_||_|__|_|")
                            .Replace("d", "_|_|_||__|_||")
                            .Replace("e", "_||_|_||__|_|")
                            .Replace("f", "_|_||_||__|_|")
                            .Replace("g", "_|_|_|__||_||")
                            .Replace("h", "_||_|_|__||_|")
                            .Replace("i", "_|_||_|__||_|")
                            .Replace("j", "_|_|_||__||_|")
                            .Replace("k", "_||_|_|_|__||")
                            .Replace("l", "_|_||_|_|__||")
                            .Replace("m", "_||_||_|_|__|")
                            .Replace("n", "_|_|_||_|__||")
                            .Replace("o", "_||_|_||_|__|")
                            .Replace("p", "_|_||_||_|__|")
                            .Replace("r", "_||_|_|_||__|")
                            .Replace("q", "_|_|_|_||__||")
                            .Replace("s", "_|_||_|_||__|")
                            .Replace("t", "_|_|_||_||__|")
                            .Replace("u", "_||__|_|_|_||")
                            .Replace("v", "_|__||_|_|_||")
                            .Replace("w", "_||__||_|_|_|")
                            .Replace("x", "_|__|_||_|_||")
                            .Replace("y", "_||__|_||_|_|")
                            .Replace("z", "_|__||_||_|_|")
                            .Replace("-", "_|__|_|_||_||")
                            .Replace("*", "_|__|_||_||_|")
                            .Replace("/", "_|__|__|_|__|")
                            .Replace("%", "_|_|__|__|__|")
                            .Replace("+", "_|__|_|__|__|")
                            .Replace(".", "_||__|_|_||_|");
                        return result;
                    }).ToArray());
                }

                #endregion

                #region Image

                /// <summary>
                /// Convert Base64 data to System.Drawing.Image
                /// </summary>
                /// <param name="base64String"></param>
                /// <returns></returns>
                public static System.Drawing.Image Base64ToImage(string base64String)
                {
                    System.Drawing.Image result = null;
                    try
                    {
                        // Convert Base64 String to byte[]
                        byte[] imageBytes = System.Convert.FromBase64String(base64String);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes, 0,
                          imageBytes.Length);

                        // Convert byte[] to Image
                        ms.Write(imageBytes, 0, imageBytes.Length);
                        result = System.Drawing.Image.FromStream(ms, true);
                    }
                    catch (Exception ex) { }
                    return result;
                }

                /// <summary>
                /// Convert System.Drawing.Image to Base64 data
                /// </summary>
                /// <param name="image"></param>
                /// <param name="format"></param>
                /// <returns></returns>
                public static string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
                {
                    string result = String.Empty;
                    try
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            // Convert Image to byte[]
                            image.Save(ms, format);
                            byte[] imageBytes = ms.ToArray(); // Convert byte[] to Base64 String
                            result = System.Convert.ToBase64String(imageBytes);
                        }
                    }
                    catch (Exception ex) { }
                    return result;
                }


                #endregion

                #region Object
                /// <summary>
                /// Convert object to json string
                /// </summary>
                /// <param name="o">the object to convert</param>
                /// <returns>JSON string</returns>
                public static string ObjectToJSON(object o)
                {
                    string result = String.Empty;
                    try
                    {
                        if (o.GetType() == typeof(System.Data.DataTable))
                        {
                            System.Data.DataTable dt = o as System.Data.DataTable;
                            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
                            foreach (System.Data.DataRow dataRow in dt.Rows)
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                                foreach (System.Data.DataColumn dataColumn in dt.Columns)
                                {
                                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                                }
                                arrayList.Add(dictionary); //ArrayList集合中添加键值
                            }

                            result = javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
                        }
                        else
                        {
                            result = rf.Newtonsoft.Json.JsonConvert.SerializeObject(o);
                        }
                    }
                    catch (Exception ex) { }
                    return result;
                }
                #endregion

                #region JSON
                /// <summary>
                /// Convert JSON to XNode
                /// </summary>
                /// <param name="sJSON">The JSON String</param>
                /// <returns>XNode</returns>
                public static System.Xml.Linq.XNode JSONToXNode(string sJSON = @"{
                                      '@Id': 1,
                                      'Email': 'james@example.com',
                                      'Active': true,
                                      'CreatedDate': '2013-01-20T00:00:00Z',
                                      'Roles': [
                                        'User'
                                      ],
                                      'Team': {
                                        '@Id': 2,
                                        'Name': 'Software Developers',
                                        'Description': 'Creators of fine software products and services.'
                                      }
                                    }")
                {
                    System.Xml.Linq.XNode node = rf.Newtonsoft.Json.JsonConvert.DeserializeXNode(sJSON, "Root");
                    return node;
                }

                /// <summary>
                /// JsonConvert DeserializeObject
                /// </summary>
                /// <param name="p">jsonText</param>
                /// <param name="type">the type that is supposed to convert to.</param>
                /// <param name="mode">object (type is needed)/array (type is needed)/List&lt;Dictionary&lt;string,string&gt;&gt;/Dictionary&lt;string,string></param>
                /// <returns>object</returns>
                /// <example>the following is an example of calling a
                ///     <c>JSONToObject</c> function:
                ///     <para>
                ///         <code>
                ///             List&lt;Dictionary&lt;string, string&gt;&gt; power = (List&lt;Dictionary&lt;string, string&gt;&gt;)RequestBus.JsonConvertDeserializeObject(resultMsg.Obj.ToString(), mode: "List&lt;Dictionary&lt;string, string&gt;&gt;");
                ///         
                ///             //Power power = (Power)RequestBus.JsonConvertDeserializeObject(resultMsg.Obj.ToString(), type:typeof(Power)) ?? new Power();
                ///         
                ///             resultStr = 0 == power.Count ? "" : (power[0]["pwrid"] ?? "").ToString().Trim() ?? "";
                ///         </code>
                ///     </para>
                /// </example>
                public static object JSONToObject(string p = "{}", Type type = null, String mode = "object")
                {
                    Object result = null;
                    switch (mode.Replace(" ", ""))
                    {
                        case "List<Dictionary<string,string>>":

                            result = rf.Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(p);
                            break;
                        case "Dictionary<string,string>":
                            result = rf.Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(p);
                            break;
                        case "object":
                        case "array":
                            if (null != type)
                            {
                                if ("object" == mode)
                                {
                                    p = System.Text.RegularExpressions.Regex.Replace(p, "^\\[|\\]$", "");
#if DEBUG
                                    // p = System.Text.RegularExpressions.Regex.Replace(p, @"\""", @"'");
#endif
                                }
                                result = rf.Newtonsoft.Json.JsonConvert.DeserializeObject(p, type);
                            }
                            break;
                        default:
                            break;
                    }

                    return result;

                }

                /// <summary>
                /// JsonConvert DeserializeObject
                /// </summary>
                /// <typeparam name="T">the type that is supposed to convert to.</typeparam>
                /// <param name="p">jsonText</param>
                /// <returns>Object</returns>
                public static T JSONToObject<T>(string p)
                {
                    T result = default(T);
                    try
                    {
                        result = rf.Newtonsoft.Json.JsonConvert.DeserializeObject<T>(p);
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }
                #endregion

                #region XML
                /// <summary>
                /// from xml to string
                /// </summary>
                /// <param name="xmlReader">XmlReader,which may be created from a xml file path or textReader of a xml string.</param>
                /// <returns>the string of the xml</returns>
                public static string XMLtoString(System.Xml.XmlReader xmlReader)
                {
                    string resultStr = "";
                    //if (xmlReader.Read())
                    //{
                    //    resultStr = xmlReader.ReadOuterXml();
                    //}


                    StringBuilder output = new StringBuilder();

                    // Create an XmlReader
                    using (System.Xml.XmlReader reader = xmlReader)
                    {
                        System.Xml.XmlWriterSettings ws = new System.Xml.XmlWriterSettings(); 
                        ws.Encoding = Encoding.UTF8;
                        ws.OmitXmlDeclaration = true;
                        ws.Indent = true;
                        using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(output, ws))
                        {
                            // Parse the file and display each of the nodes.
                            while (reader.Read())
                            {
                                switch (reader.NodeType)
                                {
                                    case System.Xml.XmlNodeType.Element:
                                        // if it's an element, then let's look at the attributes.
                                        writer.WriteStartElement(reader.Name);
                                        if (0 < reader.AttributeCount)
                                        {
                                            writer.WriteAttributes(reader, true);
                                        }
                                        for (int i = 0; i < reader.AttributeCount; i++)
                                        {
                                            //writer.WriteStartAttribute(reader.Name);
                                        }
                                        break;
                                    case System.Xml.XmlNodeType.Text:
                                        writer.WriteString(reader.Value);
                                        break;
                                    case System.Xml.XmlNodeType.XmlDeclaration:
                                    case System.Xml.XmlNodeType.ProcessingInstruction:
                                        writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                        break;
                                    case System.Xml.XmlNodeType.Comment:
                                        writer.WriteComment(reader.Value);
                                        break;
                                    case System.Xml.XmlNodeType.EndElement:
                                        writer.WriteFullEndElement();
                                        break;
                                }
                            }

                        }
                    }
                    resultStr = output.ToString();
                    return resultStr;
                }

                /// <summary>
                /// XNodeToJSON
                /// </summary>
                /// <param name="xNode"></param>
                /// <returns></returns>
                public static string XNodeToJSON(System.Xml.XmlNode xNode)
                {
                    string result = rf.Newtonsoft.Json.JsonConvert.SerializeXmlNode(xNode, rf.Newtonsoft.Json.Formatting.None);
                    return result;
                }
                #endregion

                #region List
                /// <summary>
                /// Convert List&ltDictionary&ltstring, string&gt&gt to List&ltDictionary&ltstring, object&gt&gt.
                /// </summary>
                /// <param name="ldss"></param>
                /// <returns></returns>
                public static List<Dictionary<string, object>> ListDictioanryStringStringToListDictionaryStringObject(List<Dictionary<string, string>> ldss)
                {
                    List<Dictionary<string, object>> ldso = new List<Dictionary<string, object>>();
                    ldss.ForEach(delegate(Dictionary<string, string> _dss)
                    {
                        ldso.Add(_dss.ToDictionary(k => k.Key, k => k.Value as Object));
                    });
                    return ldso;
                }
                #endregion

                #region
                #endregion
            }

            #endregion

            #region Format
            /// <summary>
            /// 格式化；Format Values
            /// </summary>
            public partial class Format
            {
                /// <summary>
                /// Formatting the matched postfix string to the style of folder path.
                /// <para>
                ///  System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^/]+$");
                /// </para>
                ///                     System.Text.RegularExpressions.Match rem = r.Match(sValue);
                ///                     string folderPathShouldEndWithSlash = GlobalClass.Utils.Format.formatToFolderPathPostfix(rem);
                /// </summary>
                /// <param name="rem">the matched value</param>
                /// <returns>the postfix string with the format of folder path.</returns>
                /// <example>
                ///                     System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^/]+$");
                ///                     System.Text.RegularExpressions.Match rem = r.Match(sValue);
                ///                     string folderPathShouldEndWithSlash = GlobalClass.Utils.Format.formatToFolderPathPostfix(rem);
                /// </example>
                private static string formatToFolderPathPostfix(System.Text.RegularExpressions.Match rem)
                {
                    return rem.Value + "/";
                }
                /// <summary>
                /// Formatting the matched postfix string to the style of file path.
                /// </summary>
                /// <param name="rem">the matched value</param>
                /// <returns>the postfix string with the format of file path.</returns>
                /// <example>
                ///                     System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^/]+$");
                ///                     System.Text.RegularExpressions.Match rem = r.Match(sValue);
                ///                     string filePathShouldEndWithOutSlash = GlobalClass.Utils.Format.formatToFilePathPostfix(rem);
                /// </example>
                private static string formatToFilePathPostfix(System.Text.RegularExpressions.Match rem)
                {
                    return "";
                }
                /// <summary>
                /// Formatting the matched postfix string to its own postfix. Thus this method will do nothing but return the value its self.
                /// </summary>
                /// <param name="rem">the matched value</param>
                /// <returns>the postfix string its self</returns>
                private static string formatToItsOwnPostfix(System.Text.RegularExpressions.Match rem)
                {
                    return rem.Value;
                }
                /// <summary>
                /// Format Path
                /// </summary>
                /// <param name="sValue">the string of path to format</param>
                /// <param name="sType">the type of the path, available values are "folder", "file".</param>
                /// <returns>the string format as the path type required.</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             Config._logDir = Utils.Format.FormatPath(value, "folder");
                ///         </code>
                ///         <code>
                ///             Config._monitoringLogFileName = Utils.Format.FormatPath(value, "file");
                ///         </code>
                ///     </para>
                /// </example>
                public static string FormatPath(string sValue, string sType)
                {
                    System.Text.RegularExpressions.MatchEvaluator evaluator;

                    string sPattern = "*";
                    switch (sType.ToLower())
                    {
                        case "folder": sPattern = "[^/]+$"; evaluator = new System.Text.RegularExpressions.MatchEvaluator(formatToFolderPathPostfix); break;
                        case "file": sPattern = "[/]+$"; evaluator = new System.Text.RegularExpressions.MatchEvaluator(formatToFilePathPostfix); break;
                        default: sPattern = "*"; evaluator = new System.Text.RegularExpressions.MatchEvaluator(formatToFilePathPostfix); break;
                    }
                    // delegate
                    return System.Text.RegularExpressions.Regex.Replace(sValue, sPattern, evaluator);
                }
                /// <summary>
                /// make sure the string end with the specific value.
                /// </summary>
                /// <param name="sValue">the target string to check</param>
                /// <param name="sSuffix">the specified value that the target string is supposed to be end with.</param>
                /// <returns>the string match the request format</returns>
                public static string MakeSureEndWith(string sValue, string sSuffix = "")
                {
                    string sResult = sValue;
                    if (false == System.Text.RegularExpressions.Regex.IsMatch(sValue, sSuffix + "$"))
                    {
                        sValue = sValue + sSuffix;
                    }
                    return sValue;
                }
            }

            #endregion

            #region WebBrowser
            /// <summary>
            /// 關於瀏覽器的方法集合；collection of the method about the web browser.
            /// </summary>
            public partial class WebBrowser
            {
                /// <summary>
                /// Try to identify the version of the IE web browser.
                /// </summary>
                /// <param name="Request">Instance of the System.Web.HttpRequest</param>
                /// <returns>double -1 :not an IE browser; 7.0: IE7</returns> 
                /// <example>
                /// The following is the Example of how to use this function:
                ///     <para>
                ///         <code>
                ///              double ieVersion = RequestBus.getInternetExplorerVersion(Request);
                ///              
                ///              if (ieVersion > 0.0)
                ///              { //IE
                ///              if (ieVersion > 7.0)
                ///              {
                ///              }
                ///              else if (ieVersion == 7.0)
                ///              {
                ///              }
                ///              else if (ieVersion &lt; 7.0)
                ///              {
                ///              }
                ///              }
                ///              else
                ///              {
                ///              }
                ///         </code>
                ///     </para>
                /// </example>
                public static double getInternetExplorerVersion(HttpRequest Request)
                {
                    // Returns the version of Internet Explorer or a -1
                    // (indicating the use of another browser).
                    float rv = -1;
                    System.Web.HttpBrowserCapabilities browser = Request.Browser;
                    if (browser.Browser == "IE" || browser.Browser == "InternetExplorer")
                        rv = (float)(browser.MajorVersion + browser.MinorVersion);
                    return rv;
                }

                /// <summary>
                /// try to get platform
                /// </summary>
                /// <param name="Request">Instance of the System.Web.HttpRequest</param>
                /// <returns></returns>
                public static double getPlatform(HttpRequest Request)
                {
                    double pf = -1;
                    if ("WinNT" == Request.Browser.Platform || "WinXP" == Request.Browser.Platform)
                    {
                        pf = double.Parse(Request.UserAgent.Split(new String[] { "Windows NT" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    }
                    else if ("WinXP" == Request.Browser.Platform)
                    {
                        pf = double.Parse("5.1");
                    }
                    else { }
                    return pf;
                }
            }
            #endregion

            #region Net
            /// <summary>
            ///     網絡相關類；Network
            /// </summary>
            /// <example>GlobalClass.Utils.Net gcun = new GlobalClass.Utils.Net();</example>
            public partial class Net
            {

                #region URL
                /// <summary>
                /// 瀏覽器鏈接地址；Uniform Resource Locator
                /// </summary>
                public class URL
                {
                    /// <summary>
                    /// 用于 取得 根据传入的URI值 解析得到的 URL基本字符串（不含参数）
                    /// </summary>
                    /// <param name="url">Uri</param>
                    /// <returns>string</returns>
                    public static string getRequestUrlPath(Uri url)
                    {
                        return url.ToString().Split('?').FirstOrDefault();
                    }

                    /// <summary>
                    /// get a new request uri
                    /// </summary>
                    /// <param name="requestURI"></param>
                    /// <param name="requestQueryString"></param>
                    /// <param name="key"></param>
                    /// <param name="value"></param>
                    /// <returns></returns>
                    /// <example>
                    /// <code>
                    /// Response.Redirect(getNewRequestUrl(Request.Url, Request.QueryString.ToString(), "key", "value"));
                    /// </code>
                    /// </example>
                    public static string getNewRequestUrl(Uri requestURI, string requestQueryString, string key, string value)
                    {
                        string result = "";
                        result = getNewRequestUrl(requestURIAbsolutePath: requestURI.AbsolutePath, requestQueryString: requestQueryString, key: key, value: value);
                        return result;
                    }

                    /// <summary>
                    /// get a new request uri
                    /// </summary>
                    /// <param name="requestURIAbsolutePath"></param>
                    /// <param name="requestQueryString"></param>
                    /// <param name="key"></param>
                    /// <param name="value"></param>
                    /// <returns></returns>
                    /// <example>
                    /// <code>
                    /// Response.Redirect(getNewRequestUrl(Request.Url.AbsolutePath, Request.QueryString.ToString(), "key", "value"));
                    /// </code>
                    /// </example>
                    public static string getNewRequestUrl(string requestURIAbsolutePath, string requestQueryString, string key, string value)
                    {
                        string result = "";
                        System.Collections.Specialized.NameValueCollection nameValues = new System.Collections.Specialized.NameValueCollection();
                        string[] removeKeys = new string[] { };
                        if (null == value)
                        {
                            removeKeys = removeKeys.Concat(new string[] { key }).ToArray<string>();
                        }
                        else
                        {
                            nameValues.Set(key, value);
                        }
                        result = getNewRequestUrl(requestURIAbsolutePath: requestURIAbsolutePath, requestQueryString: requestQueryString, nameValues: nameValues, removeKeys: removeKeys);
                        return result;
                    }

                    /// <summary>
                    /// get a new request uri
                    /// </summary>
                    /// <param name="requestURIAbsolutePath"></param>
                    /// <param name="requestQueryString"></param>
                    /// <param name="nameValues"></param>
                    /// <param name="removeKeys"></param>
                    /// <code>
                    /// NameValueCollection nvc = new NameValueCollection();
                    /// nvc.Set("ProductID", nvc["PRO_ID"]);
                    /// nvc.Set("PrevPageNum", pageNum.ToString());
                    /// Response.Redirect(getNewRequestUrl(Request.Url.AbsolutePath, Request.QueryString.ToString(), nvc));
                    /// </code>
                    /// </example>
                    /// <returns></returns>
                    public static string getNewRequestUrl(string requestURIAbsolutePath, string requestQueryString, System.Collections.Specialized.NameValueCollection nameValues=null, string[] removeKeys = null)
                    {
                        string result = "";
                        if (null == nameValues)
                        {
                            nameValues = new System.Collections.Specialized.NameValueCollection();
                        }
                        NameValueCollection nvcReqeustQueryString = HttpUtility.ParseQueryString(requestQueryString);
                        Func<string, NameValueCollection> removeKeysFromNVC = delegate(string key)
                        {// make sure there is no duplicate key.
                            NameValueCollection _result = new NameValueCollection();
                            nvcReqeustQueryString.Set(key, nameValues[key]);
                            nvcReqeustQueryString.Remove(key);
                            _result = nvcReqeustQueryString;
                            return _result;
                        };
                        //nameValues.AllKeys.Select(removeKeysFromNVC).LastOrDefault();
                        if (0 < nameValues.Keys.Count)
                        {
                            nameValues.Add(nameValues.AllKeys.Select(removeKeysFromNVC).LastOrDefault());
                        }
                        if (null != removeKeys)
                        {
                            removeKeys.ToList().ForEach(s => nameValues.Remove(s));
                        }
                        string URI = requestURIAbsolutePath;
                        string updatedQueryString = "?" + String.Join("&", nameValues.AllKeys.Select(key=>String.Format("{0}={1}",HttpUtility.UrlEncode(key),HttpUtility.UrlEncode(nameValues[key]))).ToArray<string>());
                        result = URI + updatedQueryString;
                        return result;
                    }


                    /// <summary>
                    /// get a new request uri
                    /// </summary>
                    /// <param name="requestURI"></param>
                    /// <param name="requestQueryString"></param>
                    /// <param name="nameValues"></param>
                    /// <param name="removeKeys"></param>
                    /// <example>
                    /// <code>
                    /// NameValueCollection nvc = new NameValueCollection();
                    /// nvc.Set("ProductID", nvc["PRO_ID"]);
                    /// nvc.Set("PrevPageNum", pageNum.ToString());
                    /// Response.Redirect(getNewRequestUrl(Request.Url, Request.QueryString.ToString(), nvc));
                    /// </code>
                    /// </example>
                    /// <returns></returns>
                    public static string getNewRequestUrl(Uri requestURI, string requestQueryString, System.Collections.Specialized.NameValueCollection nameValues, string[] removeKeys = null)
                    {
                        string result = "";
                        result = getNewRequestUrl(requestURIAbsolutePath: requestURI.AbsolutePath, requestQueryString: requestQueryString, nameValues: nameValues,removeKeys: removeKeys);
                        return result;
                    }
                }

                #endregion

                #region WebPage
                /// <summary>
                /// 網頁相關類；WebPage
                /// </summary>
                public class web
                {
                    #region RequestWebPageAsStream
                    /// <summary>
                    /// 請求網頁，並以流的形式返回；Request a Web Page and Retrieve the Results as a Stream
                    /// http://msdn.microsoft.com/en-us/library/bay1b5dh%28v=vs.100%29.aspx
                    /// </summary>
                    /// <param name="sURL">請求地址；the URL of the web page.</param>
                    /// <returns>服務器返回的結果流；the System.IO.Stream result of the request.</returns>
                    public static System.IO.Stream RequestWebPageAsStream(String sURL)
                    {
                        System.Net.WebClient myClient = new System.Net.WebClient();
                        System.IO.Stream response = myClient.OpenRead(sURL);
                        // The stream data is used here.
                        response.Close();
                        return response;
                    }
                    #endregion

                    #region RequestWebPageAsString
                    /// <summary>
                    /// 請求網頁，並以字符串的形式返回；Request a Web Page and Retrieve the Results as a String.
                    /// </summary>
                    /// <param name="sURL">請求地址；the URL of the web page.</param>
                    /// <returns>服務器返回的結果字符串；the System.String result of the request.</returns>
                    public static String RequestWebPageAsString(String sURL)
                    {
                        System.Net.WebClient myClient = new System.Net.WebClient();
                        System.IO.Stream dataStream = myClient.OpenRead(sURL);
                        // Open the stream using a StreamReader for easy access.
                        System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        dataStream.Close();
                        return responseFromServer;
                    }
                    #endregion
                }
                #endregion

                #region GetData
                /// <summary>
                /// 通過URI請求數據。
                /// Request Data By Provide the URI.
                /// http://msdn.microsoft.com/en-us/library/456dfw4f%28v=vs.100%29.aspx
                /// </summary>
                /// <param name="sURI">請求； URI</param>
                /// <returns>從服務器返回的字符串結果；String result of the response from the server.</returns>
                public static string GetData(string sURI)
                {
                    // Create a request for the URL. 
                    System.Net.WebRequest request = System.Net.WebRequest.Create(sURI);
                    // If required by the server, set the credentials.
                    request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    // Get the response.
                    System.Net.WebResponse response = request.GetResponse();
                    // Display the status.
                    System.Diagnostics.Debug.WriteLineIf(Config.IsUnderconstruction, (((System.Net.HttpWebResponse)response).StatusDescription));
                    // Get the stream containing content returned by the server.
                    System.IO.Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Clean up the streams and the response.
                    reader.Close();
                    response.Close();
                    // Display the content.
                    return responseFromServer;
                }
                #endregion

                #region SendData
                /// <summary>
                /// 以Post方式發送數據 Send Data
                /// </summary>
                /// <param name="sURL">用於接收請求的URL地址；a URL that can receive a post.</param>
                /// <param name="postData">請求中要附帶的參數數據；the data to be post.</param>
                /// <param name="sMethod">請求的方式；the request type.</param>
                /// <param name="sRequestContentType">請求的類容格式；the content type of request.</param>
                /// <returns>服務器返回的結果字符串；String result of the response from the server.</returns>
                public static string SendData(String sURL, String postData = "", String sMethod = "POST", String sRequestContentType = "application/x-www-form-urlencoded")
                {
                    // Create a request using a URL that can receive a post. 
                    System.Net.WebRequest request = System.Net.WebRequest.Create(sURL);
                    // Set the Method property of the request to POST.
                    request.Method = sMethod;
                    // Create POST data and convert it to a byte array.
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = sRequestContentType;
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;
                    // Get the request stream.
                    System.IO.Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                    // Get the response.
                    System.Net.WebResponse response = request.GetResponse();
                    // Display the status.
                    System.Diagnostics.Debug.WriteLineIf(Config.IsUnderconstruction, ((System.Net.HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Clean up the streams.
                    reader.Close();
                    // dataStream.Close();// 警告	1	CA2202 : Microsoft.Usage : 可以在方法 'Utils.PostData(string, string, string)' 中多次释放对象 'dataStream'。若要避免生成 System.ObjectDisposedException，不应对一个对象多次调用 Dispose。: Lines: 115	D:\Workspace\2014-09-09 personalTools\WZToolsClass\WZToolsClass\Utils.cs	115	WZTC

                    response.Close();
                    // Display the content.
                    return responseFromServer;
                }
                #endregion

                #region SendRequest: String url, String param; C# HTTP Request请求程序模拟向服务器送出请求

                /********************  *C# HTTP Request请求程序模拟***   * 向服务器送出请求   * */
                /// <summary>
                /// 用于 向服务器送出请求 。
                /// C# HTTP Request请求程序模拟。             
                /// </summary> 
                /// <param name="sURL">标识Internet资源的URI。</param>
                /// <param name="sParam">包含要编码的字符的 System.String</param>
                /// <param name="sMethod">請求方式；request method</param>
                /// <param name="sType">請求類型，如：SOAP；the request type, eg: SOAP</param>
                /// <param name="sContentType">請求內容類型</param>
                /// <param name="htRequestHeader">請求頭自定義參數</param>
                /// <returns>来自服务器的响应体的 流的 字符串。</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             Hashtable htRequestHeader = new Hashtable();
                ///             htRequestHeader.Add("SOAPAction", @"redforce");
                ///             System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                ///             xmlDocument.LoadXml(GlobalClass.Utils.Net.SendRequest(@"http://192.4.200.38/bb/NoteMange/NoteService.asmx?op=GetInfoCls", GlobalClass.Utils.XML.UseSOAPTemplateString(@"&lt;GetAllTbInfo xmlns=""redforce"">
                ///             &lt;clsid>note_lx&lt;/clsid>
                ///             &lt;/GetAllTbInfo>"), sType: "SOAP", sContentType: "text/xml; charset=utf-8", htRequestHeader: htRequestHeader));
                ///         </code>
                ///     </para>
                /// </example>
                public static string SendRequest(string sURL, string sParam, string sMethod = "POST", string sAccept = "	text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8", string sType = "", string sContentType = "application/x-www-form-urlencoded", System.Collections.Hashtable htRequestHeader = null)
                {
                    string responseText = "";
                    try
                    {
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                        request.Method = sMethod;
                        request.Accept = sAccept;
#if NET40
                        request.Host = HttpContext.Current.Request.UserHostAddress;
                        //request.Host = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        //request.Host = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
#endif
                        
                        //request.Connection = "keep-alive";
                        //request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentType = sContentType ?? "text/xml; charset=utf-8";
                        string propKey = "";
                        if (null != htRequestHeader)
                        {
                            foreach (System.Collections.DictionaryEntry prop in htRequestHeader)
                            {
                                propKey = prop.Key.ToString();
                                switch (propKey)
                                {
                                    case "Accept":
                                        request.Accept = prop.Value.ToString();
                                        break;
                                    case "Content-Type":
                                        request.ContentType = prop.Value.ToString();
                                        break;
                                    case "Content-Length":
                                        request.ContentLength = long.Parse(prop.Value.ToString());
                                        break;
                                    case "Keep-Alive":
                                        request.KeepAlive = System.Convert.ToBoolean(prop.Value);
                                        break;
                                    case "Referer":
                                        request.Referer = prop.Value.ToString();
                                        break;
                                    case "Host":
                                        SetRequestHeaderValue(request.Headers, propKey, prop.Value.ToString());
                                        break;
                                    case "Connection":
                                        SetRequestHeaderValue(request.Headers, propKey, prop.Value.ToString());
                                        break;
                                    case "User-Agent":
                                        SetRequestHeaderValue(request.Headers, propKey, prop.Value.ToString());
                                        break;
                                    default:
                                        request.Headers.Add(prop.Key.ToString(), prop.Value.ToString());
                                        break;
                                }
                            }
                        }
                        System.IO.Stream sm;
                        switch (sType)
                        {
                            case "SOAP":
                                //request.Headers.Add(@"SOAP:Action");
                                break;
                            default:
                                break;
                        }

                        byte[] data = encoding.GetBytes(sParam);
                        request.ContentLength = data.Length;

                        sm = request.GetRequestStream();
                        sm.Write(data, 0, data.Length);
                        sm.Close();



                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                        if (response.StatusCode.ToString() != "OK")
                        {
                            System.Diagnostics.Debug.WriteLineIf(Config.IsUnderconstruction, "response.StatusDescription.ToString():" + response.StatusDescription.ToString());
                            //MessageBox.Show(response.StatusDescription.ToString());
                            //return "";
                        }
                        // Get the stream containing content returned by the server.

                        sm = response.GetResponseStream();
                        System.IO.StreamReader responseReader = new System.IO.StreamReader(sm, System.Text.Encoding.UTF8);

                        responseText = responseReader.ReadToEnd();
                        responseReader.Close();

                            sm.Close();

                        response.Close();
                        request.GetResponse().Close();
                        return responseText;
                    }
                    catch (System.Net.WebException we)
                    {
                        // TODO redirect to error page.
                        //throw we;
                        responseText = we.Message;
                        return responseText;
                        //we.Message.ToString();
                    }
                }

                /// <summary>
                /// 用于 向服务器送出请求 。
                /// C# HTTP Request请求程序模拟。             
                /// </summary> 
                /// <param name="sURL">标识Internet资源的URI。</param>
                /// <param name="sParam">包含要编码的字符的 System.String</param>
                /// <param name="sMethod">請求方式；request method</param>
                /// <param name="sType">請求類型，如：SOAP；the request type, eg: SOAP</param>
                /// <param name="sContentType">請求內容類型</param>
                /// <param name="htRequestHeader">請求頭自定義參數</param>
                /// <returns>来自服务器的响应体的 流的 字符串。</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             Hashtable htRequestHeader = new Hashtable();
                ///             htRequestHeader.Add("SOAPAction", @"redforce");
                ///             System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                ///             xmlDocument.LoadXml(GlobalClass.Utils.Net.SendRequest(@"http://192.4.200.38/bb/NoteMange/NoteService.asmx?op=GetInfoCls", GlobalClass.Utils.XML.UseSOAPTemplateString(@"&lt;GetAllTbInfo xmlns=""redforce"">
                ///             &lt;clsid>note_lx&lt;/clsid>
                ///             &lt;/GetAllTbInfo>"), sType: "SOAP", sContentType: "text/xml; charset=utf-8", htRequestHeader: htRequestHeader));
                ///         </code>
                ///     </para>
                /// </example>
                public static string SendRequestV2(string sURL, string sParam, string sMethod = "POST", string sType = "", string sContentType = "application/x-www-form-urlencoded", System.Collections.Hashtable htRequestHeader = null)
                {
                    string responseText = "";
                    try
                    {
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                        request.Method = sMethod;
                        request.Accept = "	text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
#if NET40
                        request.Host = HttpContext.Current.Request.UserHostAddress;
                        //request.Host = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        //request.Host = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
#endif

                        //request.Connection = "keep-alive";
                        //request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentType = sContentType ?? "text/xml; charset=utf-8";
                        string propKey = "";
                        if (null != htRequestHeader)
                        {
                            foreach (System.Collections.DictionaryEntry prop in htRequestHeader)
                            {
                                propKey = prop.Key.ToString();
                                switch (propKey)
                                {
                                    case "Accept":
                                        request.Accept = prop.Value.ToString();
                                        break;
                                    case "Content-Type":
                                        request.ContentType = prop.Value.ToString();
                                        break;
                                    case "Content-Length":
                                        request.ContentLength = long.Parse(prop.Value.ToString());
                                        break;
                                    case "Keep-Alive":
                                        request.KeepAlive = System.Convert.ToBoolean(prop.Value);
                                        break;
                                    case "Referer":
                                        request.Referer = prop.Value.ToString();
                                        break;
                                    case "Host":
                                        SetRequestHeaderValue(request.Headers, propKey, prop.Value.ToString());
                                        break;
                                    case "Connection":
                                        SetRequestHeaderValue(request.Headers, propKey, prop.Value.ToString());
                                        break;
                                    case "User-Agent":
                                        SetRequestHeaderValue(request.Headers, propKey, prop.Value.ToString());
                                        break;
                                    default:
                                        request.Headers.Add(prop.Key.ToString(), prop.Value.ToString());
                                        break;
                                }
                            }
                        }
                        System.IO.Stream sm;
                        switch (sType)
                        {
                            case "SOAP":
                                //request.Headers.Add(@"SOAP:Action");
                                break;
                            default:
                                break;
                        }

                        byte[] data = encoding.GetBytes(sParam);
                        request.ContentLength = data.Length;

                        /* 2014-11-06
                        sm = request.GetRequestStream();
                        sm.Write(data, 0, data.Length);
                        sm.Close();
                         * **/

                        System.IO.StreamWriter requestWriter = new System.IO.StreamWriter(request.GetRequestStream());
                        requestWriter.Write(data);
                        requestWriter.Close();

                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                        if (response.StatusCode.ToString() != "OK")
                        {
                            System.Diagnostics.Debug.WriteLineIf(Config.IsUnderconstruction, "response.StatusDescription.ToString():" + response.StatusDescription.ToString());
                            //MessageBox.Show(response.StatusDescription.ToString());
                            //return "";
                        }
                        // Get the stream containing content returned by the server.
                        /* 2014-11-06
                        sm = response.GetResponseStream();
                        System.IO.StreamReader responseReader = new System.IO.StreamReader(sm, System.Text.Encoding.UTF8);
                         * */

                        System.IO.StreamReader responseReader = new System.IO.StreamReader(request.GetResponse().GetResponseStream());


                        responseText = responseReader.ReadToEnd();
                        responseReader.Close();
                        /* 2014-11-06
                            sm.Close();
                         */
                        response.Close();
                        request.GetResponse().Close();
                        return responseText;
                    }
                    catch (System.Net.WebException we)
                    {
                        // TODO redirect to error page.
                        //throw we;
                        responseText = we.Message;
                        return responseText;
                        //we.Message.ToString();
                    }
                }

                /// <summary>
                /// 設置請求頭的值
                /// </summary>
                /// <param name="header">請求頭</param>
                /// <param name="name">鍵名稱</param>
                /// <param name="value">值</param>
                public static void SetRequestHeaderValue(System.Net.WebHeaderCollection header, string name, string value)
                {
                    var property = typeof(System.Net.WebHeaderCollection).GetProperty("InnerCollection",
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    if (property != null)
                    {
                        var collection = property.GetValue(header, null) as System.Collections.Specialized.NameValueCollection;
                        collection[name] = value;
                    }
                } 
                //**C# HTTP Request请求程序模拟   * 进行UTF-8的URL编码转换(针对汉字参数)   * */
                //public string EncodeConver(string instring)  {
                //    return HttpUtility.UrlEncode(instring, Encoding.UTF8);
                //}

                //**C# HTTP Request请求程序模拟   * 进行登录操作并返回相应结果   * */
                //public bool DoLogin(string username,   string password)  {
                //password = System.Web.Security.FormsAuthentication.  HashPasswordForStoringInConfigFile(password, "MD5");
                //string param = string.Format("do=login&u={0}&p={1}",   this.EncodeConver(username), this.EncodeConver(password));
                //string result = this.SendRequest(param);
                // MessageBox.Show(result);        解析 Result ,我这里是作为一个XML Document来解析的  return true;
                //} 

                #endregion

            }
            #endregion

            #region XML
            /// <summary>
            /// 可擴展標記語言文檔相關類；eXtensible Markup Language  Documents.
            /// </summary>
            public partial class XML
            {
                #region getXMLSingleNodeAttributeValue: XmlDocument xml, string xpath, string attributeName
                /// <summary>
                /// 用于 取得 可扩展标记语言（XML）的 指定的单一节点的 值。
                /// </summary>
                /// <param name="xml">用于被搜查的可扩展标记语言文档</param>
                /// <param name="xpath">xPath 表达式</param>
                /// <param name="attributeName">要检索的元素的限定名</param>
                /// <returns>可扩展标记语言 元素 或 null</returns>
                private static object getXMLSingleNodeAttributeValue(System.Xml.XmlDocument xml, string xpath, string attributeName)
                {
                    object resultObj;
                    System.Xml.XmlNode xmlNode = xml.SelectSingleNode(xpath); // /hits
                    //XmlNodeList xmlNodeList = xml.GetElementsByTagName("hits");
                    if (null != xmlNode)
                    {
                        resultObj = xmlNode[attributeName];
                    }
                    else
                    {
                        resultObj = null;
                    }
                    return resultObj;
                }
                #endregion

                #region LINQ to XML

                #region SaveXML
                /// <summary>
                /// Save XML String as xml file.
                /// </summary>
                /// <param name="sXMLContent">the String of XML.</param>
                /// <param name="sXMLFileName">the file name of the XML.</param>
                public static void SaveXML(String sXMLFileName, String sXMLContent = @"")
                {
                    System.Xml.Linq.XDocument xdoc = System.Xml.Linq.XDocument.Parse(sXMLContent);
                    xdoc.Save(sXMLFileName);
                }
                #endregion

                #region LoadXML
                /// <summary>
                /// Load XML file as XDocument.
                /// </summary>
                /// <param name="sXMLFileName">the file name of the XML.</param>
                /// <returns>System.XML.Linq.XDocument, the XDocument result.</returns>
                public static System.Xml.Linq.XDocument LoadXML(String sXMLFileName)
                {
                    return System.Xml.Linq.XDocument.Load(sXMLFileName);
                }

                #endregion

                #endregion

                /// <summary>
                /// 獲取Xml命名空間管理者。get the new XmlNamespaceManager.
                /// </summary>
                /// <param name="xmlDocument">the nameTable owner</param>
                /// <param name="htNamespace">the Hashtable of the Namespaces with format like : [prefix:uri]</param>
                /// <returns>XmlNamespaceManager</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             System.Collections.Hashtable htN = new System.Collections.Hashtable();
                ///             htN.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                ///             htN.Add("rf", "redforce");
                ///             System.Xml.XmlNode xmlNode = xmlDocument.SelectSingleNode("//soap:Envelope/soap:Body/rf:GetInfoClsResponse/rf:GetInfoClsResult/rf:Obj", GlobalClass.Utils.XML.GetXmlNamespaceManager(xmlDocument, htN));
                ///         </code>
                ///     </para>
                /// </example>
                public static System.Xml.XmlNamespaceManager GetXmlNamespaceManager(System.Xml.XmlDocument xmlDocument, System.Collections.Hashtable htNamespace)
                {
                    //System.Xml.XmlNamespaceManager xnm = new System.Xml.XmlNamespaceManager(xmlDocument.NameTable);
                    //xnm.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                    //xnm.AddNamespace("rf", "redforce");
                    //System.Xml.XmlNode xmlNode = xmlDocument.SelectSingleNode("//soap:Envelope/soap:Body/rf:GetAllTbInfoResponse/rf:GetAllTbInfoResult", xnm);

                    System.Xml.XmlNamespaceManager xnm = new System.Xml.XmlNamespaceManager(xmlDocument.NameTable);
                    foreach (System.Collections.DictionaryEntry prop in htNamespace)
                    {
                        xnm.AddNamespace(System.Convert.ToString(prop.Key), System.Convert.ToString(prop.Value));
                    }
                    return xnm;
                }

                #region GetSOAPTemplateString
                /// <summary>
                /// 使用SOAP的XML模板，將soap:Body內的字符串形式的XML節點嵌入其中，並得到完整的SOAP XML字符串值。
                /// Get the String of SOAP's XML by passing the Nodes inside the soap:Body as string param to the method.
                /// </summary>
                /// <param name="sParam">soap:Body內的字符串形式的XML節點;the string of the Nodes inside the soap:Body</param>
                /// <returns>string of SOAP's XML</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             Hashtable htRequestHeader = new Hashtable();
                ///             htRequestHeader.Add("SOAPAction", @"redforce");
                ///             System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                ///             xmlDocument.LoadXml(GlobalClass.Utils.Net.SendRequest(@"http://192.4.200.38/bb/NoteMange/NoteService.asmx?op=GetInfoCls", GlobalClass.Utils.XML.UseSOAPTemplateString(@"&lt;GetAllTbInfo xmlns=""redforce"">
                ///             &lt;clsid>note_lx&lt;/clsid>
                ///             &lt;/GetAllTbInfo>"), sType: "SOAP", sContentType: "text/xml; charset=utf-8", htRequestHeader: htRequestHeader));
                ///         </code>
                ///     </para>
                /// </example>
                public static string UseSOAPTemplateString(string sParam)
                {
                    return @"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                      <soap:Body>" +
                                                  sParam
                                      + @"</soap:Body>
                                    </soap:Envelope>";
                }
                #endregion


                #region XML Validation
                /// <summary>
                /// Get XmlSchemaSet of XSD from XML.
                /// </summary>
                /// <example>
                ///     
                ///     <para>
                ///     <code>
                ///     foreach (System.Xml.Schema.XmlSchema s in schemaSet.Schemas())
                ///     {
                ///         s.Write(Console.Out);
                ///     }
                ///     </code>
                ///     </para>
                /// </example>
                /// <param name="xmlFilePath">the file path of the xml</param>
                /// <returns>XmlSchemaSet</returns>
                public static System.Xml.Schema.XmlSchemaSet GetXSDFromXML(string xmlFilePath)
                {
                    System.Xml.XmlReader reader;
#if DEBUG
                    System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                    settings.ConformanceLevel = System.Xml.ConformanceLevel.Fragment;
                    settings.IgnoreWhitespace = true;
                    settings.IgnoreComments = true;
                    settings.ValidationType = System.Xml.ValidationType.None;
                    settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(XmlReaderSettingsValidationEventHandler);
                    string xmlStr = XML.LoadXML(xmlFilePath).ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                    System.IO.TextReader tr = new System.IO.StringReader(xmlStr);
                    reader = System.Xml.XmlReader.Create(tr);
#endif
                    // http://msdn.microsoft.com/en-us/library/system.xml.schema.xmlschemainference.aspx
                    reader = System.Xml.XmlReader.Create(xmlFilePath);
                    System.Xml.Schema.XmlSchemaSet schemaSet = new System.Xml.Schema.XmlSchemaSet();
                    System.Xml.Schema.XmlSchemaInference schema = new System.Xml.Schema.XmlSchemaInference();
                    schemaSet = schema.InferSchema(reader);
#if DEBUG
                    string SSDStr = "";
                    foreach (System.Xml.Schema.XmlSchema s in schemaSet.Schemas())
                    {
                        SSDStr += s.ToString();
                    }
#endif
                    return schemaSet;
                }

                /// <summary>
                /// validate XML by using XSD.
                /// </summary>
                /// <param name="xmlFilePath">the file path of XML</param>
                /// <param name="xsdFilePath">the file path of XSD</param>
                /// <param name="xsdNamespace">the xsdNamespace</param>
                /// <returns>the System.Xml.XmlReader</returns>
                public static System.Xml.XmlReader ValidateXMLWidthXSD(string xmlFilePath, string xsdFilePath, string xsdNamespace = null){
                    System.Xml.XmlReaderSettings booksSettings = new System.Xml.XmlReaderSettings();
                    booksSettings.Schemas.Add(xsdNamespace, xsdFilePath);
                    booksSettings.ValidationType = System.Xml.ValidationType.Schema;
                    booksSettings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(XmlReaderSettingsValidationEventHandler);

                    System.Xml.XmlReader books = System.Xml.XmlReader.Create(xmlFilePath, booksSettings);

                    // while (books.Read()) { }
                    return books;
                }


                /// <summary>
                /// Validate the XML with XSD file
                /// </summary>
                /// <param name="xmlTextReader">the textReader of the XML</param>
                /// <param name="xsdFilePath">the file path of the XSD</param>
                /// <param name="xsdNamespace">the name-space of the XSD</param>
                /// <returns>if passed the validate, return "passed"; Or return the validate log.</returns>
                public static string ValidateXMLWidthXSD(System.IO.TextReader xmlTextReader, string xsdFilePath, string xsdNamespace = null)
                {
                    System.Xml.Schema.XmlSchemaSet xSchemaSet = new System.Xml.Schema.XmlSchemaSet();
                    xSchemaSet.Add(xsdNamespace, xsdFilePath);
                    System.Xml.Linq.XDocument xDoc = System.Xml.Linq.XDocument.Load(xmlTextReader);
                    string validationResult = GlobalClass.Const.ValidationResult.Failed.ToString();
                    System.Xml.Schema.Extensions.Validate(xDoc, xSchemaSet, (o, ex) =>
                    {
                        if (ex.Severity == System.Xml.Schema.XmlSeverityType.Warning)
                        {
                            Console.Write("WARNING: ");
                            Console.WriteLine(ex.Message);
                            validationResult += "WARNING:" + ex.Message + "(" + ((System.Xml.Linq.XElement)o).Value + ")\r\n";
                        }
                        else if (ex.Severity == System.Xml.Schema.XmlSeverityType.Error)
                        {
                            Console.Write("ERROR: ");
                            Console.WriteLine(ex.Message);
                            validationResult += "ERROR:" + ex.Message + "(" + ((System.Xml.Linq.XElement)o).Value + ")\r\n";
                        }
                    });
                    return validationResult ?? GlobalClass.Const.ValidationResult.Passed.ToString();
                }

                /// <summary>
                /// validateXML by using ExampleXML
                /// </summary>
                /// <param name="xmlFilePath">the file path of the target XML</param>
                /// <param name="exampleXmlFilePath">the file path of the example XML</param>
                /// <returns></returns>
                public static System.Xml.XmlReader ValidateXMLWidthExampleXml(string xmlFilePath, string exampleXmlFilePath){
                    System.Xml.XmlReaderSettings booksSettings = new System.Xml.XmlReaderSettings();
                    booksSettings.Schemas = GetXSDFromXML(exampleXmlFilePath);
                    booksSettings.ValidationType = System.Xml.ValidationType.Schema;
                    booksSettings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(XmlReaderSettingsValidationEventHandler);

                    System.Xml.XmlReader books = System.Xml.XmlReader.Create(xmlFilePath, booksSettings);

                    // while (books.Read()) { }
                    return books;
                }
                /// <summary>
                /// Validate the XML via Example XML.
                /// </summary>
                /// <param name="xmlTextReader">the System.IO.TextReader of the target XML.</param>
                /// <param name="exampleXmlFilePath">the file path of the example XML</param>
                /// <returns>the XmlReader of the validated XML.</returns>
                public static System.Xml.XmlReader ValidateXMLWidthExampleXml(System.IO.TextReader xmlTextReader, string exampleXmlFilePath)
                {
                    System.Xml.XmlReaderSettings booksSettings = new System.Xml.XmlReaderSettings();
                    booksSettings.Schemas = GetXSDFromXML(exampleXmlFilePath);
                    booksSettings.ValidationType = System.Xml.ValidationType.Schema;
                    booksSettings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(XmlReaderSettingsValidationEventHandler);
                    booksSettings.IgnoreComments = true;
                    booksSettings.IgnoreWhitespace = true;

                    System.Xml.XmlReader books = System.Xml.XmlReader.Create(xmlTextReader, booksSettings);

                   // while (books.Read()) { }
                    return books;
                }
                private static void XmlReaderSettingsValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
                {
                    Type t = sender.GetType();
                    if (e.Severity == System.Xml.Schema.XmlSeverityType.Warning)
                    {
                        Console.Write("WARNING: ");
                        Console.WriteLine(e.Message);
                    }
                    else if (e.Severity == System.Xml.Schema.XmlSeverityType.Error)
                    {
                        Console.Write("ERROR: ");
                        Console.WriteLine(e.Message);
                    }
                }

                #endregion

            }
            #endregion

            #region IO
            /// <summary>
            /// 有關文件讀寫的類；Input and Output
            /// </summary>
            public partial class IO
            {

                #region WriteToFile
                /// <summary>
                /// Append Contents to specific file.
                /// </summary>
                /// <param name="sURI">the URI of the file that will be appended to.</param>
                /// <param name="hContents">the Hashtable of the Contents. line number is the suggested key(won't care what it actually is.), and string of the content is the value.</param>
                /// <param name="sNewLine">the new line string to be used by TextWriter.</param>
                /// <param name="ifCompress">if need to compress the file.</param>
                /// <param name="createNewFile" >if need to create new file or override the exist file.</param>
                public static void WriteToFile(String sURI, System.Collections.Hashtable hContents, String sNewLine = "\r\n", String ifCompress = "nocompress", bool createNewFile = false)
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(sURI, "/[^/]*$", ""),"\\\\[^\\\\]*$",""));
                        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(sURI, !createNewFile, System.Text.Encoding.UTF8);
                        switch (ifCompress.ToLower())
                        {
                            case "compress":
                                System.IO.Compression.GZipStream gzipStream = new System.IO.Compression.GZipStream(streamWriter.BaseStream, System.IO.Compression.CompressionMode.Compress, true);
                                break;
                            default: break;
                        }
                        streamWriter.NewLine = sNewLine;
                        foreach (System.Collections.DictionaryEntry o in hContents)
                        {
                            streamWriter.WriteLine("Value:" + o.Value);
                            //streamWriter.WriteLine(hContents[o.Key]);
                        }
                        streamWriter.Close();
                    }
                    catch (System.IO.IOException ioe)
                    {
                        System.Diagnostics.Debug.WriteLineIf(Config.IsMonitoring, "AN IO exception has been thrown!");
                        System.Diagnostics.Debug.WriteLineIf(Config.IsMonitoring, ioe.ToString());
                        return;
                    }
                }
                #endregion

                #region AppendToFile
                /// <summary>
                /// Append string Content to specific file.
                /// </summary>
                /// <param name="sURI">the URI of the file that will be appended to.</param>
                /// <param name="sContent">the string to be appended.</param>
                /// <param name="sNewLine">the new line string to be used by TextWriter.</param>
                /// <param name="ifCompress">if need to compress the file.</param>
                /// <example>
                /// <code language="C#" title="Append text to file">
                /// #if LOGIT
                ///		RF.GlobalClass.Utils.IO.AppendToFile(Application.StartupPath + @"\swing_card_log\" + this.Name + "_" + RF.GlobalClass.Utils.DateTime.GetDateTimeString(DateTime.Now).Replace(':', '.').Replace(" ", "_"), "刷卡金额为" + amount + "\r\n刷卡返回信息s:" + s);
				///	#endif
                /// </code>
                /// </example>
                public static void AppendToFile(String sURI, String sContent = "", String sNewLine = "\r\n", String ifCompress = "nocompress")
                {
                    System.Collections.Hashtable hContents = new System.Collections.Hashtable();
                    hContents.Add(0, sContent);
                    Utils.IO.WriteToFile(sURI, hContents, sNewLine: sNewLine, ifCompress: ifCompress, createNewFile: false);
                }
                #endregion

                #region CreateFile
                /// <summary>
                /// Create a new file. Will override the old file if it exist.
                /// </summary>
                /// <param name="sURI">the URI of the file to be created.</param>
                /// <param name="sContent">the content text of the new file.</param>
                /// <param name="sNewLine">the new line string to be used by TextWriter.</param>
                /// <param name="ifCompress">if need to compress the file.</param>
                /// <example>
                /// <code language="C#" title="Create Log File">
                /// #if DEBUG && LOGIT
				///	RF.GlobalClass.Utils.IO.CreateNewFile(Server.MapPath("~/log/" + method_dds + "_response_data_" + RF.GlobalClass.Utils.DateTime.GetDateTimeString(DateTime.Now).Replace(':', '.').Replace(" ", "_")), responseData);
				///	#endif
                /// </code>
                /// </example>
                public static void CreateNewFile(String sURI, String sContent, String sNewLine = "\r\n", String ifCompress = "nocompress")
                {
                    System.Collections.Hashtable hContents = new System.Collections.Hashtable();
                    hContents.Add(1, sContent);
                    Utils.IO.WriteToFile(sURI, hContents, sNewLine: sNewLine, ifCompress: ifCompress, createNewFile: true);
                }
                #endregion

                #region AddNewLine
                /// <summary>
                /// Add a new line to file.
                /// </summary>
                /// <param name="sURI">the URI of the file to be modified.</param>
                /// <param name="sNewLine">the new line string to be used by TextWriter.</param>
                /// <param name="ifCompress">if need to compress the file.</param>
                public static void AddNewLine(String sURI, String sNewLine = "\r\n", String ifCompress = "nocompress")
                {
                    AppendToFile(sURI, sNewLine: sNewLine, ifCompress: ifCompress);
                }
                #endregion

                #region ReadLines
                /// <summary>
                /// Read specific Lines from File.
                /// </summary>
                /// <param name="sURI">the URI of the File.</param>
                /// <param name="sPattern">the pattern that the lines to match.</param>
                /// <param name="sEncodingName">the Encoding Name of the file encoding.</param>
                /// <param name="roRegexOptions">the RegexOptions which used in Regex.IsMatch</param>
                /// <returns>the hashtable of the result, line number is the key and content of the line is value.</returns>
                public static System.Collections.Hashtable ReadLines(String sURI, String sPattern = "*", String sEncodingName = "UTF8", System.Text.RegularExpressions.RegexOptions roRegexOptions = System.Text.RegularExpressions.RegexOptions.ECMAScript)
                {
                    System.Collections.Hashtable hResult = new System.Collections.Hashtable();
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(sURI, System.Text.Encoding.GetEncoding(sEncodingName));
                    int lineNum = 0;
                    String sOneLine = streamReader.ReadLine();
                    while (sOneLine != null)
                    {
                        lineNum++;
                        if (true == System.Text.RegularExpressions.Regex.IsMatch(sOneLine, sPattern, roRegexOptions))
                        {
                            hResult[lineNum] = sOneLine;
                        }
                        sOneLine = streamReader.ReadLine();
                    }
                    return hResult;
                }
                #endregion

                #region UpdateLines
                /// <summary>
                /// update lines in specific file.
                /// </summary>
                /// <param name="sURI">the URI of the file to be updated.</param>
                /// <param name="hLinesToBeUpdated">the Hashtable of the lines, line number is the key and new content of the line is value.</param>
                /// <param name="sEncodingName">the Encoding Name of the file encoding.</param>
                public static void UpdateLines(String sURI, System.Collections.Hashtable hLinesToBeUpdated, String sEncodingName = "UTF8")
                {
                    String sTmpFileSuffix = ".tmp";
                    String sTmpFileName = sURI;
                    System.IO.FileInfo fileInfo;
                    // find a proper name for tmp file.
                    do
                    {
                        sTmpFileName += sTmpFileSuffix;
                        fileInfo = new System.IO.FileInfo(sTmpFileName);
                    } while (true == fileInfo.Exists);
                    // update file to tmp file.
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(sURI, System.Text.Encoding.GetEncoding(sEncodingName), true);
                    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(sTmpFileName, false, System.Text.Encoding.GetEncoding(sEncodingName));
                    int iLineNum = 0;
                    String sLine = streamReader.ReadLine();
                    while (sLine != null)
                    {
                        iLineNum++;
                        if (null != hLinesToBeUpdated[iLineNum])
                        {
                            streamWriter.WriteLine(hLinesToBeUpdated[iLineNum]);
                        }
                        else
                        {
                            streamWriter.WriteLine(sLine);
                        }
                    }
                    streamReader.Close();
                    streamWriter.Close();
                    // copy tmp file to destination file.
                    System.IO.File.Copy(sTmpFileName, sURI, true);
                    // delete tmp file.
                    System.IO.File.Delete(sTmpFileName);
                }

                #endregion

                #region EmbeddedResource

                #region GetEmbeddedResourceNames
                /// <summary>
                /// Get the list of all embedded resources in the assembly.
                /// </summary>
                /// <param name="assembly">
                /// The Assembly created from the <c>outside</c> of the NameSpace(or .dll) enables this method reach resources outside. Or method will use the GetCallingAssembly;
                ///     <para>e.g:
                ///         <code>
                ///             System.Reflection.Assembly.GetExecutingAssembly()
                ///         </code>
                ///     </para>
                /// </param>
                /// <returns>An array of fully qualified resource names</returns>
                public static string[] GetEmbeddedResourceNames(System.Reflection.Assembly assembly = null)
                {
                    assembly = assembly ?? System.Reflection.Assembly.GetCallingAssembly();
                    return assembly.GetManifestResourceNames() ?? new string[0];
                }
                #endregion

                #region GetEmbeddedResourceStream
                /// <summary>
                /// GetEmbeddedResourceStream
                /// </summary>
                /// <param name="nameSpace">name space of this project</param>
                /// <param name="filePath">file path of the resource file.</param>
                /// <param name="assembly">
                ///     The Assembly created from the <c>outside</c> of the NameSpace(or .dll) enables this method reach resources outside. Or method will use the GetCallingAssembly;
                ///     <para>e.g:
                ///         <code>
                ///             System.Reflection.Assembly.GetExecutingAssembly()
                ///         </code>
                ///     </para>
                /// </param>
                /// <returns>System.IO.Stream</returns>
                /// <example>
                ///     <code>
                ///     System.IO.Stream stream = GlobalClass.Utils.IO.GetResourceFileStream(nameSpace, filePath, System.Reflection.Assembly.GetExecutingAssembly());
                ///     using (StreamReader reader = new StreamReader(stream))
                ///     {
                ///         string result = reader.ReadToEnd();
                ///     }
                ///     </code>
                /// </example>
                public static System.IO.Stream GetEmbeddedResourceStream(String nameSpace, String filePath, System.Reflection.Assembly assembly)
                {
                    String pseduoName = filePath.Replace('\\', '.');
                    return GetEmbeddedResourceStream(nameSpace + "." + pseduoName, assembly);
                }

                /// <summary>
                /// Takes the full name of a resource and loads it in to a stream.
                /// 
                /// </summary>
                /// <example>
                ///     <code>
                ///     System.IO.Stream stream = GlobalClass.Utils.IO.GetEmbeddedResourceStream(resourceName, System.Reflection.Assembly.GetExecutingAssembly());
                ///     using (StreamReader reader = new StreamReader(stream))
                ///     {
                ///         string result = reader.ReadToEnd();
                ///     }
                ///     </code>
                /// </example>
                /// <param name="resourceName">Assuming an embedded resource is a file
                /// called info.png and is located in a folder called Resources, it
                /// will be compiled in to the assembly with this fully qualified
                /// name: Full.Assembly.Name.Resources.info.png. That is the string
                /// that you should pass to this method.</param>
                /// <param name="assembly">
                ///     The Assembly created from the <c>outside</c> of the NameSpace(or .dll) enables this method reach resources outside. Or method will use the GetCallingAssembly;
                ///     <para>e.g:
                ///         <code>
                ///             System.Reflection.Assembly.GetExecutingAssembly()
                ///         </code>
                ///     </para>
                /// </param>
                /// <returns>System.IO.Stream</returns>
                public static System.IO.Stream GetEmbeddedResourceStream(string resourceName, System.Reflection.Assembly assembly = null)
                {
                    assembly = assembly ?? System.Reflection.Assembly.GetCallingAssembly();
                    System.IO.Stream stream = assembly.GetManifestResourceStream(resourceName) ?? new System.IO.MemoryStream();
                    return stream;
                }
                #endregion

                #endregion
            }
            #endregion

            #region DateTime
            /// <summary>
            /// 關於 日期時間 的類；the class of collections about DateTime.
            /// </summary>
            public partial class DateTime
            {
                /// <summary>
                /// Get the DateTime of the first day of the month.
                /// </summary>
                /// <param name="dateTime"></param>
                /// <param name="monthOffSet">month offset of the dateTime</param>
                /// <returns></returns>
                public static System.DateTime GetFirstDateOfMonth(System.DateTime dateTime, int monthOffSet = 0)
                {
                    return dateTime.AddMonths(monthOffSet).AddDays(1 - dateTime.Day);
                }
                /// <summary>
                /// Get the DateTime of the last day of the month.
                ///     <para>
                ///         <example>
                ///             <code>
                ///                 this.dateTime = GlobalClass.Utils.DateTime.GetLastDateOfMonth(dateTime, Convert.ToInt32(GlobalClass.Utils.Convertion.EnumValueToString(monthOffSet))).AddDays(indexOfDisplay - numberOfVisibleDayInMonth);
                ///             </code>
                ///         </example>
                ///     </para>
                /// </summary>
                /// <param name="dateTime">dateTime</param>
                /// <param name="monthOffSet">month offset of the dateTime</param>
                /// <returns></returns>
                public static System.DateTime GetLastDateOfMonth(System.DateTime dateTime, int monthOffSet = 0)
                {
                    return GetFirstDateOfMonth(dateTime, monthOffSet).AddMonths(1).AddDays(-1);
                }
                /// <summary>
                /// 获取日期时间字符串
                /// </summary>
                /// <param name="dateTime">日期时间对象</param>
                /// <returns>日期时间字符串：2014-10-21 08:33</returns>
                /// <example>
                ///     <para>
                ///         <code>
                ///             String dateTimeStr = GetDateTimeString(DateTime.Now);
                ///         </code>
                ///     </para>
                /// </example>
                public static string GetDateTimeString(System.DateTime dateTime, string culture = "zh-CN", string dateSeparator = "-")
                {
                    string resultStr = "";
                    System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
                    System.Globalization.DateTimeFormatInfo dtfi = cultureInfo.DateTimeFormat;
                    dtfi.DateSeparator = dateSeparator;
                    //resultStr = dateTime.ToShortDateString() + " " + dateTime.ToLongTimeString();
                    resultStr = System.Text.RegularExpressions.Regex.Replace(dateTime.ToString("s"), "T", " ");
                    return resultStr;
                }
                /// <summary>
                /// 获取日期字符串
                /// </summary>
                /// <param name="dateTime"></param>
                /// <param name="culture"></param>
                /// <param name="dateSeparator"></param>
                /// <returns>日期时间字符串：2014-10-21</returns>
                public static string GetDateString(System.DateTime dateTime, string culture = "zh-CN", string dateSeparator = "-", string format = "d")
                {
                    string resultStr = "";
                    System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
                    System.Globalization.DateTimeFormatInfo dtfi = cultureInfo.DateTimeFormat;
                    dtfi.DateSeparator = dateSeparator;
                    resultStr = dateTime.ToString(format, dtfi);
                    return resultStr;
                }

                /// <summary>
                /// Get the DateTimeFormatInfo
                /// </summary>
                /// <param name="culture"></param>
                /// <param name="dateSeparator"></param>
                /// <returns></returns>
                public static System.Globalization.DateTimeFormatInfo GetDateTimeInfo(string culture = "zh-CN", string dateSeparator = "-")
                {
                    System.Globalization.DateTimeFormatInfo result = System.Globalization.CultureInfo.CreateSpecificCulture(culture).DateTimeFormat;
                    result.DateSeparator = dateSeparator;
                    return result;
                }

                /// <summary>
                /// DateTime Format Info Constant value
                /// </summary>
                public partial class FormatInfo
                {
                    #region DateTimeFormatInfo
                    //{System.Globalization.DateTimeFormatInfo}
                    //    AbbreviatedDayNames: {string[7]}
                    //    AbbreviatedMonthGenitiveNames: {string[13]}
                    //    AbbreviatedMonthNames: {string[13]}
                    //    AMDesignator: "上午"
                    //    Calendar: {System.Globalization.GregorianCalendar}
                    //    CalendarWeekRule: FirstDay
                    //    DateSeparator: "-"
                    //    DayNames: {string[7]}
                    //    FirstDayOfWeek: Sunday
                    //    FullDateTimePattern: "yyyy'年'M'月'd'日' H:mm:ss"
                    //    IsReadOnly: false
                    //    LongDatePattern: "yyyy'年'M'月'd'日'"
                    //    LongTimePattern: "H:mm:ss"
                    //    MonthDayPattern: "M'月'd'日'"
                    //    MonthGenitiveNames: {string[13]}
                    //    MonthNames: {string[13]}
                    //    NativeCalendarName: "公历"
                    //    PMDesignator: "下午"
                    //    RFC1123Pattern: "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'"
                    //    ShortDatePattern: "yyyy/M/d"
                    //    ShortestDayNames: {string[7]}
                    //    ShortTimePattern: "H:mm"
                    //    SortableDateTimePattern: "yyyy'-'MM'-'dd'T'HH':'mm':'ss"
                    //    TimeSeparator: ":"
                    //    UniversalSortableDateTimePattern: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'"
                    //    YearMonthPattern: "yyyy'年'M'月'"
                    //DateTime.Now.ToString(dtfi.ShortDatePattern);
                    //"2015/5/15"
                    //DateTime.Now.ToString("d",dtfi.ShortDatePattern);
                    //与“System.DateTime.ToString(string, System.IFormatProvider)”最匹配的重载方法具有一些无效参数
                    //DateTime.Now.ToString("d",dtfi);
                    //"2015-5-15"
                    //DateTime.Now.ToString("D",dtfi);
                    //"2015年5月15日"
                    //DateTime.Now.ToString("d",dtfi);
                    //"2015-5-15"
                    #endregion
                    /// <summary>
                    /// "yyyy'年'M'月'd'日' H:mm:ss"
                    /// </summary>
                    public const string FullDateTimePattern = "yyyy'年'M'月'd'日' H:mm:ss";
                    /// <summary>
                    /// "yyyy'年'M'月'd'日'"
                    /// </summary>
                    public const string LongDatePattern = "yyyy'年'M'月'd'日'";
                    /// <summary>
                    /// "H:mm:ss"
                    /// </summary>
                    public const string LongTimePattern = "H:mm:ss";

                    /// <summary>
                    /// "yyyy'年'M'月'"
                    /// </summary>
                    public const string YearMonthPattern = "yyyy'年'M'月'";
                    /// <summary>
                    /// "M'月'd'日'"
                    /// </summary>
                    public const string MonthDayPattern = "M'月'd'日'";
                    /// <summary>
                    /// "yyyy/M/d"
                    /// </summary>
                    public const string ShortDatePattern = "yyyy/M/d";
                    /// <summary>
                    /// "H:mm"
                    /// </summary>
                    public const string ShortTimePattern = "H:mm";
                    /// <summary>
                    /// "yyyy'-'MM'-'dd'T'HH':'mm':'ss"
                    /// </summary>
                    public const string SortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
                }

            }
            #endregion

            #region DataBase
            /// <summary>
            /// DB 的摘要说明
            /// </summary>
            public partial class DB
            {
                /// <summary>
                /// DataBase
                /// </summary>
                public DB()
                {
                    //
                    // TODO: 在此处添加构造函数逻辑
                    //
                }
                private string _sqlConnectionStr = "Data Source=(local);Initial Catalog=ShortMessageBox;Integrated Security=True;";

                /// <summary>
                /// the string used by sql connection.
                /// </summary>
                /// <example>
		        /// Standard Security
		        /// Server=myServerAddress;Database=myDataBase;User Id=myUsername;
		        /// Password=myPassword;
		        /// 	SQL Server 2000SQL Server 2005SQL Server 2008SQL Server 2012SQL Server 7.0
		        /// Trusted Connection
		        /// Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
		        /// 	SQL Server 2000SQL Server 2005SQL Server 2008SQL Server 2012SQL Server 7.0
		        /// Connection to a SQL Server instance
		        ///
		        /// The server/instance name syntax used in the server option is the same for all SQL Server connection strings.
		        /// Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername;
		        /// Password=myPassword;
		        /// </example>
                public string SqlConnectionStr
                {
                    get { return _sqlConnectionStr; }
                    set { _sqlConnectionStr = value; }
                }

                /// <summary>
                /// 连接数据库
                /// </summary>
                /// <param name="sqlConnectionStr">default null,if null, then the SqlConnectionStr of DB class instance will be used.</param>
                /// <returns>返回SqlConnection对象</returns>
                public System.Data.SqlClient.SqlConnection GetCon(string sqlConnectionStr = null)
                {
                    SqlConnectionStr = sqlConnectionStr ?? SqlConnectionStr;
                    return new System.Data.SqlClient.SqlConnection(SqlConnectionStr);
                }

                /// <summary>
                /// 执行SQL语句
                /// </summary>
                ///<param name="cmdstr">SQL语句</param>
                /// <returns>返回值为int型：成功返1，失败返回0</returns>
                public int sqlEx(string cmdstr)
                {
                    System.Data.SqlClient.SqlConnection con = GetCon();//连接数据库
                    con.Open();//打开连接
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(cmdstr, con);
                    try
                    {
                        cmd.ExecuteNonQuery();//执行SQL 语句并返回受影响的行数
                        return 1;//成功返回１
                    }
                    catch (Exception e)
                    {
                        return 0;//失败返回０
                    }
                    finally
                    {
                        con.Dispose();//释放连接对象资源
                    }
                }
                /// <summary>
                /// 执行SQL查询语句
                /// </summary>
                /// <param name="cmdstr">查询语句</param>
                /// <returns>返回DataTable数据表</returns>
                public System.Data.DataSet reDs(string cmdstr)
                {
                    System.Data.SqlClient.SqlConnection con = GetCon();
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmdstr, con);
                    System.Data.DataSet ds = new System.Data.DataSet();
                    da.Fill(ds);
                    return ds;
                }
                /// <summary>
                /// 执行SQL查询语句
                /// </summary>
                /// <param name="cmdstr">查询语句</param>
                /// <returns>返回DataTable数据表</returns>
                public System.Data.DataTable reDt(string cmdstr)
                {
                    System.Data.SqlClient.SqlConnection con = GetCon();
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmdstr, con);
                    System.Data.DataSet ds = new System.Data.DataSet();
                    da.Fill(ds);
                    return (ds.Tables[0]);
                }
                /// <summary>
                /// 执行SQL查询语句
                /// </summary>
                /// <param name="str">查询语句</param>
                /// <returns>返回SqlDataReader对象dr</returns>
                public System.Data.SqlClient.SqlDataReader reDr(string str)
                {
                    System.Data.SqlClient.SqlConnection conn = GetCon();//连接数据库
                    conn.Open();//并打开了连接
                    System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand(str, conn);
                    System.Data.SqlClient.SqlDataReader dr = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    return dr;//返回SqlDataReader对象dr
                }
                /// <summary>
                /// MD5加密
                /// </summary>
                /// <param name="strPwd">被加密的字符串</param>
                /// <returns>返回加密后的字符串</returns>
                public static string MD5(string strPwd)
                {
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);//将字符编码为一个字节序列
                    byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值
                    md5.Clear();
                    string str = "";
                    for (int i = 0; i < md5data.Length - 1; i++)
                    {
                        str += md5data[i].ToString("x").PadLeft(2, '0');
                    }
                    return str;
                }

               
            }

            #endregion

            #endregion

            #region business
            public partial class Convert
            {
                /// <summary>
                /// Stringify the bytes from MD5 encryption.
                /// </summary>
                /// <param name="MD5Bytes">the byte[], created by md5Hash.ComputeHash(string);.</param>
                /// <returns>result string</returns>
                public delegate string StringifyMD5Bytes(byte[] MD5Bytes);
            }
            #endregion

        }

    }
}
/* vim: set si ts=4 sts=4 sw=4 fdm=indent : */
