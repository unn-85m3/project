using System;
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
            List<TestSystem.Tasks.ITaskPackage> temp = new List<TestSystem.Tasks.ITaskPackage>();

            TestSystem.test_system.TestSystem tst = new test_system.TestSystem(temp);

            System.Collections.Generic.Dictionary<string, Form_Draw> newMDIChild = new Dictionary<string, Form_Draw>();

            string tmp = Alghoritms[Alghoritm].Name + " - " + Tasks[Task].Name;

            //foreach (System.Windows.Forms.Form frm in System.Windows.Forms.Application.OpenForms)
            //    if (frm.Name == tmp)
            //    {
            //        flg = true;
            //    }
            if (!newMDIChild.ContainsKey(tmp))
            {
                newMDIChild[tmp] = new Form_Draw();
                newMDIChild[tmp].Name = tmp;
                newMDIChild[tmp].Text = tmp;
                newMDIChild[tmp].ClientSize = new System.Drawing.Size(500, 400);

                
                temp.Add(Tasks[Task]);
                tst.SetListener(newMDIChild[tmp]);
                tst.AddAlgorithm(Alghoritms[0]);
                //tst.Test();

                //tst.DelAlgorithm(tst.Length - 1);
                tst.AddAlgorithm(Alghoritms[Alghoritm]);
                

                //newMDIChild[tmp].MdiParent = Form1.ActiveForm;
            }
            else
            {
                //перерисовка
            }

                
                newMDIChild[tmp].Show();
                tst.Test();
        }
    }
}
