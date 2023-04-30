using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using Simulation.Facade;

public class Test_IDManager
{
    [Test]
    public void Test_IDManager_GetNewID()
    {
        Stopwatch sw = new();

        int numberOfChecks = 10;

        IDManager manager = new();
        uint[] ids = new uint[numberOfChecks];

        for (int i = 0; i < numberOfChecks; i++)
        {
            sw.Start();
            uint newID = manager.GetNewID();
            sw.Stop();

            for (int j = 0; j < i; j++)
                Assert.That(newID != ids[j], "Non-unique values generated.");

            ids[i] = newID;
        }
    }

    [Test]
    public void Test_IDManager_FreeID()
    {
        int numberOfChecks = 10;

        IDManager manager = new();
        uint[] ids = new uint[numberOfChecks];

        for (uint i = 0; i < numberOfChecks; i++) ids[i] = manager.GetNewID();

        for (uint i = 0; i < numberOfChecks; i++) manager.FreeID(i);
    }

    [Test]
    public void Test_IDManager_FireTest()
    {
        Stopwatch addSW = new();
        Stopwatch removeSW = new();

        int addCount = 0;
        int removeCount = 0;

        IDManager manager = new();
        List<uint> ids = new();

        for (int i = 0; i < 10000; i++)
        {
            for (int add = 0; add < UnityEngine.Random.Range(1, 20); add++)
            {
                addSW.Start();
                ids.Add(manager.GetNewID());
                addSW.Stop();

                addCount++;
            }

            for (int remove = 0; remove < UnityEngine.Random.Range(1, 10); remove++)
            {
                removeSW.Start();
                manager.FreeID(PickRandom(ids.ToArray()));
                removeSW.Stop();

                removeCount++;
            }
        }

        double averageAddCalculationTime = (double)addSW.ElapsedTicks / Stopwatch.Frequency / addCount * 1000000;
        double roundedAverageAddCalculationTime = Math.Round(averageAddCalculationTime, 3);
        UnityEngine.Debug.Log("IDManager.GetNewID: " + roundedAverageAddCalculationTime + "μs");

        double averageRemoveCalculationTime = (double)removeSW.ElapsedTicks / Stopwatch.Frequency / removeCount * 1000000;
        double roundedAverageRemoveCalculationTime = Math.Round(averageRemoveCalculationTime, 3);
        UnityEngine.Debug.Log("IDManager.FreeID: " + roundedAverageRemoveCalculationTime + "μs");

        static T PickRandom<T>(T[] array) => array[UnityEngine.Random.Range(0, array.Length)];
    }
}
