using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserIdCheck
{
    public string objectId;
    public string username;
    public string email;
    public string createdAt;
    public string updatedA;
    public bool emailVerified;
    public string sessionToken;
    
    [NonSerialized] public string ACL;
}
