﻿#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2016
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dnn.PersonaBar.Library;
using Dnn.PersonaBar.Library.Attributes;
using Dnn.PersonaBar.Servers.Components.DatabaseServer;
using DotNetNuke.Instrumentation;

namespace Dnn.PersonaBar.Servers.Services
{
    [ServiceScope(Scope = ServiceScope.Host)]
    public class SystemInfoDatabaseController : PersonaBarApiController
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(SystemInfoDatabaseController));
        private readonly DatabaseController _databaseController = new DatabaseController();

        /// GET: api/Servers/GetDatabaseServerInfo
        /// <summary>
        /// Gets database server info
        /// </summary>
        /// <param></param>
        /// <returns>database server info</returns>
        [HttpGet]
        public HttpResponseMessage GetDatabaseServerInfo()
        {
            try
            {
                var dbInfo = _databaseController.GetDbInfo();
                var dbBackups = _databaseController.GetDbBackups().Select(b => new
                {
                    b.Name,
                    b.StartDate,
                    b.FinishDate,
                    b.Size,
                    b.BackupType
                });
                var dbFileInfo = _databaseController.GetDbFileInfo().Select(f => new
                {
                    f.Name,
                    f.Size,
                    f.Megabytes,
                    f.FileType,
                    f.FileName
                });

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    dbInfo.ProductVersion,
                    dbInfo.ServicePack,
                    dbInfo.ProductEdition,
                    dbInfo.SoftwarePlatform,
                    Backups = dbBackups,
                    Files = dbFileInfo
                });
            }
            catch (Exception exc)
            {
                Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }
    }
}