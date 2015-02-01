using Alchemy;
using Alchemy.Classes;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using UnityEngine;

public class StartHost : MonoBehaviour, IDisposable
{
    public string host = "http://*:1337/";
    public string webrootPath = "webroot";

    private AppHost appHost;

    public void Start()
    {
        try
        {
            // create and start the host
            appHost = new AppHost();
            appHost.Config.WebHostPhysicalPath = Path.Combine(Directory.GetCurrentDirectory(), webrootPath);
            appHost.Init();
            appHost.Start(host);

            //Debug.Log(appHost.Config.WebHostPhysicalPath);
            //Debug.Log(appHost.Config.WebHostUrl);
            //Debug.Log(appHost.Config.ServiceStackHandlerFactoryPath);
            Debug.Log("started at " + host);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
        var instance = FindObjectOfType(typeof(Exec)) as Exec;
        if (instance == null)
        {
            instance = gameObject.AddComponent<Exec>();
        }
    }

    void OnDisable()
    {
        if (appHost != null)
        {
            appHost.Stop();
        }
    }
	
    public void Dispose()
    {
        appHost.Dispose();
    }
}

