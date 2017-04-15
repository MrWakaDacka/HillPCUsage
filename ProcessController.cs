using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace HILPcUsage
{
    class ProcessController
    {
        /*FONKSİYONUN GÖREVİ
         *2.1-Süreçleri al.
         *2.2-Süreçlerin Ram,Cpu larını hesaplayıp belirli bir yapıda tut.
         *2.3-Benzer olanları birleştir.
         */
        public static ProcessInfo[] listProcesses()
        {
            //HAZIRLIK
            int a = Environment.ProcessorCount;
            int waitTime = 100;

            Process[] allRunningProcesses = Process.GetProcesses();
            List<ProcessInfo> calculatedProcesses = new List<ProcessInfo>();
            PerformanceCounter[] myAppCpu = new PerformanceCounter[allRunningProcesses.Length];
            float[] cpuValue = new float[allRunningProcesses.Length];
            Boolean[] couldAccessProcess = new Boolean[allRunningProcesses.Length];

            for (int i = 0; i < couldAccessProcess.Length; i++)
            {
                couldAccessProcess[i] = true;
            }
            for (int i = 0; i < myAppCpu.Length; i++)
            {
                myAppCpu[i] = new PerformanceCounter("Process", "% Processor Time", allRunningProcesses[i].ProcessName, true);
            }

            //CPU HESAPLAMAK İÇİN FARK ALMA
            for (int i = 0; i < allRunningProcesses.Length; i++)
            {
                try
                {
                    myAppCpu[i].NextValue();
                }
                catch (Exception e)
                {
                    couldAccessProcess[i] = false;
                }
            }

            Thread.Sleep(waitTime); // CPU hesaplamak için gereken zaman.

            for (int i = 0; i < allRunningProcesses.Length; i++)
            {
                try
                {
                    cpuValue[i] = myAppCpu[i].NextValue();
                }
                catch (Exception e)
                {
                    couldAccessProcess[i] = false;
                }
            }

            //CPU HESAPLA

            for (int i = 0; i < allRunningProcesses.Length; i++)
            {
                if (couldAccessProcess[i])
                {
                    Boolean equalFound = false;
                    foreach (ProcessInfo pi in calculatedProcesses) //BENZERLİK KONTROLÜ
                    {
                        if (pi.Name == allRunningProcesses[i].ProcessName)
                        {
                            pi.CPUUsage += cpuValue[i] / Environment.ProcessorCount;
                            pi.RAMUsage += (float)(allRunningProcesses[i].PeakWorkingSet64 / Math.Pow(1024, 2));
                            equalFound = true;
                            break;
                        }
                    }

                    if (!equalFound)
                    {
                        ProcessInfo pi = new ProcessInfo();
                        pi.Name = allRunningProcesses[i].ProcessName;
                        pi.CPUUsage = cpuValue[i] / Environment.ProcessorCount;
                        pi.RAMUsage = (float)(allRunningProcesses[i].PeakWorkingSet64 / Math.Pow(1024, 2));
                        calculatedProcesses.Add(pi);
                    }
                }
            }

            return calculatedProcesses.ToArray();
        }
    }
}
