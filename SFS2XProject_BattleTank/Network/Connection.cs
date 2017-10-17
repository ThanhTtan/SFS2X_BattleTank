using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SFS2XProject_BattleTank.Network
{
    public class Connection
    {
        private SmartFox _sfs;
        private ConfigData _config;

        private string HOST;
        private int PORT;
        private string ZONE;

        private List<Room> _roomList;
        public Connection()
        {
            _sfs = new SmartFox();
            _config = new ConfigData();
            _roomList = new List<Room>();

            HOST = "127.0.0.1";
            PORT = 9933;
            ZONE = "BasicExamples";
        }

        public void Init()
        {
            _sfs.ThreadSafeMode = true;
            // setup congfig
            _config.Host = HOST;
            _config.Port = PORT;
            _config.Zone = ZONE;
            _config.Debug = true;

            // add listener
            AddListener();
        }
        public void Update()
        {
            if (_sfs != null)
                _sfs.ProcessEvents();
        }

        // send request
        public void SendConnectRequest(string zoneName = "BattleTank")
        {
            try
            {
                ZONE = zoneName;
                _config.Zone = ZONE;
                _sfs.Connect(_config);
            }
            catch (Exception exp)
            {
                Debug.WriteLine("config error: " + exp.Message);
            }
        }
        public void SendLoginRequest(string userName, string zoneName = "BattleTank")
        {
            _sfs.Send(new LoginRequest(userName, "", zoneName));
        }
        public void SendCreateRoomRequest(string roomName)
        {
            RoomSettings setting = new RoomSettings(roomName);
            setting.Name = roomName;
            setting.MaxUsers = 4;
            /*
             * Parameters:
                    id - The name of the Extension as deployed on the server; it's the name of the folder containing the Extension classes inside the main [sfs2x-install-folder]/SFS2X/extensions folder.
                    className - The fully qualified name of the main class of the Extension.
             */
            setting.Extension = new RoomExtension("", "");
            setting.IsGame = true;
            _sfs.Send(new CreateRoomRequest(setting, true));
        }
        public void SendJoinRoom(string roomName)
        {
            _sfs.Send(new JoinRoomRequest(roomName));
        }
        public void SendLeaveRoom()
        {
            _sfs.Send(new LeaveRoomRequest());
        }

        // get properties
        public List<Room> GetRoomList()
        {
            return _roomList;
        }

        private void AddListener()
        {
            _sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            _sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            _sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            _sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            _sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
            _sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
            _sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionReponse);
            _sfs.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdd);
            _sfs.AddEventListener(SFSEvent.ROOM_REMOVE, OnRoomRemove);
        }

        // hanlde events
        /// <summary>
        /// when a connection between the client and a SmartFoxServer 2X instance is attempted.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api </param>
        private void OnConnection(BaseEvent e)
        {
            bool success = (bool)e.Params["success"];
            if (success) Debug.WriteLine("connected");

        }
        /// <summary>
        /// when the connection between the client and the SmartFoxServer 2X instance is interrupted.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api </param>
        private void OnConnectionLost(BaseEvent e)
        {

        }
        /// <summary>
        ///  when the current user performs a successful login in a server Zone.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api</param>
        private void OnLogin(BaseEvent e)
        {

        }
        /// <summary>
        ///  if an error occurs while the user login is being performed.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api</param>
        private void OnLoginError(BaseEvent e)
        {

        }
        /// <summary>
        /// when a Room is joined by the current user.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api</param>
        private void OnJoinRoom(BaseEvent e)
        {

        }
        /// <summary>
        ///  when an error occurs while the current user is trying to join a Room.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api</param>
        private void OnJoinRoomError(BaseEvent e)
        {

        }
        /// <summary>
        ///  when a new Room is created inside the Zone under any of the Room Groups that the client subscribed. 
        ///  This event is fired in response to the CreateRoomRequest and CreateSFSGameRequest requests in case the operation is executed successfully.
        /// </summary>
        private void OnRoomAdd(BaseEvent e)
        {
            Room r = (Room)e.Params["room"];
            AddRoom(r);
        }
        /// <summary>
        /// when a Room belonging to one of the Groups subscribed by the client is removed from the Zone.
        /// </summary>
        private void OnRoomRemove(BaseEvent e)
        {
            Room r = (Room)e.Params["room"];
            RemoveRoom(r);
        }
        /// <summary>
        ///  when data coming from a server-side Extension is received by the current user.
        /// </summary>
        /// <param name="e">Contain all events dispatched by sfs 2x C# api</param>
        private void OnExtensionReponse(BaseEvent e)
        {

        }

        // update room events
        private void AddRoom(Room r)
        {
            if (r != null)
            {
                _roomList.Add(r);
            }
        }
        private void RemoveRoom(Room r)
        {
            if (r != null)
            {
                _roomList.Remove(r);
            }
        }
        private void UpdateRoomUsers(Room r)
        {
            if (r.PlayerList.Count <= 0)
            {
                RemoveRoom(r);
            }
        }

    }
}
