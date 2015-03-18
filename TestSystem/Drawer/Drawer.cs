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
            System.Collections.Generic.Dictionary<string, System.Windows.Forms.Form> newMDIChild = new Dictionary<string,System.Windows.Forms.Form>();

            string tmp = Alghoritms[Alghoritm].Name + " - " + Tasks[Task].Name;

            //foreach (System.Windows.Forms.Form frm in System.Windows.Forms.Application.OpenForms)
            //    if (frm.Name == tmp)
            //    {
            //        flg = true;
            //    }
            if (!newMDIChild.ContainsKey(tmp))
            {
                newMDIChild[tmp] = new System.Windows.Forms.Form();
                newMDIChild[tmp].Name = tmp;
                newMDIChild[tmp].Text = tmp;
                newMDIChild[tmp].ClientSize = new System.Drawing.Size(500, 400);

                List<TestSystem.Tasks.ITaskPackage> temp = new List<TestSystem.Tasks.ITaskPackage>();
                temp.Add(Tasks[Task]);
                TestSystem.test_system.TestSystem tst = new test_system.TestSystem(temp);
                tst.AddAlgorithm(Alghoritms[0]);
                tst.Test();

                tst.DelAlgorithm(tst.Length - 1);
                tst.AddAlgorithm(Alghoritms[Alghoritm]);
                tst.Test();

                //newMDIChild[tmp].MdiParent = Form1.ActiveForm;
            }
            else
            {
                //перерисовка
            }

                
                newMDIChild[tmp].Show();
        }
    }
}
