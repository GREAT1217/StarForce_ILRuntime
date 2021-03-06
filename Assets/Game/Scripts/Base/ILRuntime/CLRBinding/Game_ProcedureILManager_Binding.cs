using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Game_ProcedureILManager_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Game.ProcedureILManager);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("StartHotfixProcedure", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, StartHotfixProcedure_0);
            args = new Type[]{typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>), typeof(System.String)};
            method = type.GetMethod("ChangeHotfixProcedure", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ChangeHotfixProcedure_1);


        }


        static StackObject* StartHotfixProcedure_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @hotfixTypeName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Game.ProcedureILManager instance_of_this_method = (Game.ProcedureILManager)typeof(Game.ProcedureILManager).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.StartHotfixProcedure(@hotfixTypeName);

            return __ret;
        }

        static StackObject* ChangeHotfixProcedure_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @hotfixTypeName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager> @procedureOwner = (GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Game.ProcedureILManager instance_of_this_method = (Game.ProcedureILManager)typeof(Game.ProcedureILManager).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ChangeHotfixProcedure(@procedureOwner, @hotfixTypeName);

            return __ret;
        }



    }
}
