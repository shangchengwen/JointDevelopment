using UnityEngine;
using KIT;

public class LogHelperTest : MonoBehaviour
{

    void Start()
    {
        //print(Application.persistentDataPath);
        //LogHelper.m_logLevel = LogLevel.All;
        //Debug.Log("Debug debug,这是一个测试");
        //LogHelper.Log("LogHelper debug,这是一个测试");
        //Debug.LogError("Debug error,这是一个测试");
        //LogHelper.LogError("LogHelper error,这是一个测试");
        //Debug.LogWarning("Debug warning,这是一个测试");
        //LogHelper.LogWarning("LogHelper warning,这是一个测试");
        Application.logMessageReceived += ProcessExceptionReport;
        Debug.LogWarning("1");
        Debug.LogWarning("2");
    }

    private static void ProcessExceptionReport(string message, string stackTrace, LogType type)
    {
        Debug.LogError("有事情发生");
        LogLevel dEBUG = LogLevel.Debug;
        switch (type)
        {
            case LogType.Assert:
                dEBUG = LogLevel.Debug;
                break;
            case LogType.Error:
                dEBUG = LogLevel.Error;
                break;
            case LogType.Exception:
                dEBUG = LogLevel.Exception;
                break;
            case LogType.Log:
                dEBUG = LogLevel.Debug;
                break;
            case LogType.Warning:
                dEBUG = LogLevel.Warning;
                break;
            default:
                break;
        }

    }
}