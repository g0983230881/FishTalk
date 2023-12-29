using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mongoDB_Schema
{
    //string poolId, poolRctime, poolChlorine, poolSalinity, poolPH, poolDO,
    //        poolEC, poolTurbidity, poolChlorophyll, poolORP, poolNH4, poolTemperature,
    //        poolWL, pooldate, poolNumber, fishIsdead;

    //    string[] poolData = new string[] {
    //        poolId,
    //        poolRctime,
    //        poolChlorine,
    //        poolSalinity,
    //        poolPH,
    //        poolDO,
    //        poolEC,
    //        poolTurbidity,
    //        poolChlorophyll,
    //        poolORP,
    //        poolNH4,
    //        poolTemperature,
    //        poolWL,
    //        pooldate,
    //        poolNumber,
    //        fishIsdead
    //};

    [Serializable]
    public class Schema
    {
        public string _id;
        public string rctime;
        public float Chlorine;
        public float Salinity;
        public float PH;
        public float DO;
        public float EC;
        public float Turbidity;
        public float Chlorophyll;
        public float ORP;
        public float NH4;
        public float Temperature;
        public float WL;
        public string date;
        public string pool;
        public int isDead;
    }

    public string[] GetPoolDataFromSchema(string json)
    {
        Schema schema = JsonUtility.FromJson<Schema>(json);
        string[] poolData = new string[] {
            schema._id,                     //index=0
            schema.rctime,                  //index=1
            schema.Chlorine.ToString(),     //index=2
            schema.Salinity.ToString(),     //index=3
            schema.PH.ToString(),           //index=4
            schema.DO.ToString(),           //index=5
            schema.EC.ToString(),           //index=6
            schema.Turbidity.ToString(),    //index=7
            schema.Chlorophyll.ToString(),  //index=8
            schema.ORP.ToString(),          //index=9
            schema.NH4.ToString(),          //index=10
            schema.Temperature.ToString(),  //index=11
            schema.WL.ToString(),           //index=12
            schema.date,                    //index=13
            schema.pool,                    //index=14
            schema.isDead.ToString()        //index=15
        };
        return poolData;
    }
}
