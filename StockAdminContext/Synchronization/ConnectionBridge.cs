using System;
using System.Activities.Debugger;
using System.Activities.Statements;
using System.Collections.Generic;
 
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using StockAdminContext;
using StockAdminContext.Helpers;

namespace SigiFluent
{
    public  class ConnectionBridge
    {

        public void ConnectToServer(bool wait=false)
        {

           AsyncHelper.DoAsync(()=> ConnectTohub(false));

        }

        private void ConnectTohub(bool wait= false)
        {

            //if(wait) Thread.Sleep(new TimeSpan(0,0,10)); // wait 10secs
                var queryString = new Dictionary<string, string>();

                queryString.Add("whscode", Config.WhsCode);

                //HubConnection = null;
                //notificationsProxy = null;

                HubConnection = new HubConnection(Config.WebApiUri + "/NotifyHub", queryString, useDefaultUrl: false);

                notificationsProxy = HubConnection.CreateHubProxy("NotifyHub");


                notificationsProxy.On("FireSync", FireSynchro);
                ConnectEvents();

                HubConnection.Start();
        }
 

        private void FireSynchro()
        {
            //!Synchronization.IsRunningSync;
            //if (OnFireSync != null)
            //{
            //    OnFireSync();
            //}
        }

        private void ConnectEvents()
        {
             HubConnection.Closed += () =>  {  if (OnConnectionClosed != null) OnConnectionClosed();  };
            HubConnection.ConnectionSlow += () => { if (OnConnectionSlow != null) OnConnectionSlow();  };
            HubConnection.Reconnecting += () => { if (OnReconnecting != null) OnReconnecting(); };
            HubConnection.Reconnected += () => { if (OnReconnected != null) OnReconnected(); };
            HubConnection.StateChanged += (stateChange) =>
                                          {

                                              StateConnection = stateChange.NewState;
                                              if (StateConnection == ConnectionState.Disconnected) TryReconnect();
                                              
                                              if (OnStateChanged != null) OnStateChanged(stateChange.NewState.ToString());

                                              if (ConnectionState.Connected == StateConnection
                                                  &&  onFirstConnect != null && !IsLockFirstSync)
                                              { 
                                                  onFirstConnect();
                                                  IsLockFirstSync = true;
                                              }

                                          };
                                          
            HubConnection.Error += (error) =>
                                   {

                                   //if(!isTryngReconect)  TryReconnect();
                                   };

        }

        public void TryReconnect()
        {
            isTryngReconect = true;
            HubConnection = null;
            notificationsProxy = null;
            AsyncHelper.DoAsync(() =>
                                {
                                    Thread.Sleep(TimeSpan.FromSeconds(10));
                                    ConnectTohub();
                                    isTryngReconect = false;
                                });
        }
       

        public event Action OnFireSync;
        public static event Action OnConnectionSlow;
        public static event Action OnConnectionClosed;
        public static event Action OnReconnecting;
        public static event Action OnReconnected;
        public static event Action<string> OnStateChanged;
        public static event Action OnRecived;
        public static event Action OnError; 
        private bool isTryngReconect = false;
        private bool IsLockFirstSync = false;

        public event Action onFirstConnect;

        public static ConnectionState StateConnection;

        public static string StateConnectionString()
        {
            return StateConnection.ToString();
        }

        private HubConnection HubConnection = null;
        private IHubProxy notificationsProxy;
        private bool IsConnectionPending = false;
        
        private object connectionLocker = new object();
    }

    
}
