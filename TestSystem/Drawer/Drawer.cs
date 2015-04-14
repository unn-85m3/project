﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.test_system;

namespace TestSystem.Drawer
{
    class Drawer
    {

        private int Alg, Task;
        private string NameAlg, NameTask;
        private List<IPoint> Point;

        public static void DrawGraphics(List<Tasks.ITaskPackage> Tasks, List<Algorithm.IAlgorithm> Alghoritms, int Alghoritm, int Task)
        {
            BlackBox.BlackBoxFunction fn = new BlackBox.BlackBoxFunction();
            fn.Init(Tasks[Task]);
            //List<TestSystem.Tasks.ITaskPackage> temp = new List<TestSystem.Tasks.ITaskPackage>();

            //TestSystem.test_system.TestSystem tst = new test_system.TestSystem(temp);

            System.Collections.Generic.Dictionary<string, Plot.Plot> newMDIChild = new Dictionary<string, Plot.Plot>();

            string tmp = Alghoritms[Alghoritm].Name + " - " + Tasks[Task].Name;

            //foreach (System.Windows.Forms.Form frm in System.Windows.Forms.Application.OpenForms)
            //    if (frm.Name == tmp)
            //    {
            //        flg = true;
            //    }
            if (!newMDIChild.ContainsKey(tmp))
            {
                newMDIChild[tmp] = new Plot.Plot(fn, Tasks[Task].EnterParams);
                //newMDIChild[tmp].Size_draw(Tasks[Task].EnterParams.x1_max, Tasks[Task].EnterParams.x2_max);
                newMDIChild[tmp].Name = tmp;
                newMDIChild[tmp].Text = tmp;
                //newMDIChild[tmp].ClientSize = new System.Drawing.Size(500, 400);
                newMDIChild[tmp].UpdateComponent();
                
                //temp.Add(Tasks[Task]);
                //tst.SetListener(newMDIChild[tmp]);
                //tst.AddAlgorithm(Alghoritms[0]);
                //tst.Test();

                //tst.DelAlgorithm(tst.Length - 1);
                //tst.AddAlgorithm(Alghoritms[Alghoritm]);
                

                //newMDIChild[tmp].MdiParent = Form1.ActiveForm;
            }
            else
            {
                //перерисовка
            }

                
                newMDIChild[tmp].Show();
                newMDIChild[tmp].StartCalculate();
                
                //tst.Test();
        }
    }
}
