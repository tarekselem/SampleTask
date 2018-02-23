﻿using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Futura.Services.API.Handlers
{
        /// <summary>
        /// Public class to return input status
        /// </summary>
        [Serializable]
        [DataContract]
        public class ServiceStatus
        {
            #region Public Properties
            /// <summary>
            /// Get/Set property for accessing Status Code
            /// </summary>
            [JsonProperty("StatusCode")]
            [DataMember]
            public int StatusCode { get; set; }
            /// <summary>
            /// Get/Set property for accessing Status Message
            /// </summary>
            [JsonProperty("StatusMessage")]
            [DataMember]
            public string StatusMessage { get; set; }
            /// <summary>
            /// Get/Set property for accessing Status Message
            /// </summary>
            [JsonProperty("ReasonPhrase")]
            [DataMember]
            public string ReasonPhrase { get; set; }

            #endregion
        }
}