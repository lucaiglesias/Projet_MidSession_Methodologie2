using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Coupon
{
    public string objectId;
    public string updatedAt;
    public string createdAt;
    [NonSerialized] public string ACL;
    public string Code;
    public string userId;
    public bool Redeemed;
    public string Type;

}

[Serializable]
public class CouponResults
{
    public Coupon[] results;
}


