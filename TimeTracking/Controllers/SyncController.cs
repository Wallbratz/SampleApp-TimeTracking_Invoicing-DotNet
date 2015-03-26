﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracking.Models;
using TimeTracking.Models.Service;

namespace TimeTracking.Controllers
{
    public class SyncController : BaseController
    {
        // GET: Sync
        SyncService syncService = null;
        Syncdto syncObjects = null;

        public ActionResult Employee(Int64 id)
        {
            OAuthorizationdto oAuthDetails = new OAuthService(new OAuthorizationdto()).GetAccessToken(this);
            syncService = new SyncService(oAuthDetails);
            syncObjects = id>0?syncService.GetSyncObjects(this,id):new  Syncdto();
            syncObjects.OauthToken = oAuthDetails;
            syncObjects.CompanyId = oAuthDetails.Realmid;
            
            if (!syncService.IsEmpSync(syncObjects, syncService).IsEmployeeSync)
            {
                syncObjects = syncService.GetDatafromDBEmployee(syncObjects);
                syncObjects = syncService.SyncEmployees(this,syncObjects);
            }
            return RedirectToAction("Sync", "Home", new { id = syncObjects.Id, isConnected = oAuthDetails.IsConnected });
        }
        public ActionResult Customer(Int64 id)
        {
            OAuthorizationdto oAuthDetails = new OAuthService(new OAuthorizationdto()).GetAccessToken(this);
            syncService = new SyncService(oAuthDetails);
            syncObjects = id > 0 ? syncService.GetSyncObjects(this, id) : new Syncdto();
            syncObjects.OauthToken = oAuthDetails;
            syncObjects.CompanyId = oAuthDetails.Realmid;
         
            if (!syncService.IsCustSync(syncObjects, syncService).IsCustomerSync)
            {
                syncObjects = syncService.GetDatafromDBCustomer(syncObjects);
                syncObjects = syncService.SyncCustomer(this, syncObjects);
            }
            return RedirectToAction("Sync", "Home", new { id = syncObjects.Id, isConnected = oAuthDetails.IsConnected });
        }
        public ActionResult ServiceItem(Int64 id)
        {
            OAuthorizationdto oAuthDetails = new OAuthService(new OAuthorizationdto()).GetAccessToken(this);
            syncService = new SyncService(oAuthDetails);
            syncObjects = id > 0 ? syncService.GetSyncObjects(this, id) : new Syncdto();
            syncObjects.OauthToken = oAuthDetails;
            syncObjects.CompanyId = oAuthDetails.Realmid;
          
            if (!syncService.IsServiceItemSync(syncObjects, syncService).IsServiceItemSync)
            {
                syncObjects = syncService.GetDatafromDBItem(syncObjects);
                syncObjects = syncService.SyncServiceItems(this, syncObjects);
            }
            return RedirectToAction("Sync", "Home", new { id = syncObjects.Id, isConnected = oAuthDetails.IsConnected });
        }
    }
}