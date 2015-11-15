using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    class Optimizate : AbsAlgorithm
    {
        AbsAlgorithm _alg;
        List<Parametr> algParamAll;
        List<ParametrNow> algParamNow;

        public Optimizate(IAlgorithm alg)
        {
            if (alg is AbsAlgorithm)
                _alg = (AbsAlgorithm)alg;
            else return;
            Update();
            Name = alg.Name;
            GetNowListParams = new List<Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>>();
        }

        private void Update()
        {
            algParamNow = null;
            algParamAll = _alg.GetAllParam;
            //GetNextParams();
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            Update();
            var dict = new Dictionary<ParametrNow, DataFormat.IOutBlackBoxParam>();
            GetNowListParams.Add(new Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>());
            var best = new  DataFormat.OutBlackBoxParam(double.MaxValue);

            if (_alg is Genetic_Algorithm)
            {
                int n = algParamAll.Count - 1;
                int count = 1;
                double step = 0.1;

                for (int i = 0; i < n; i++)
                {
                    double c = (algParamAll[i].maxValue - algParamAll[i].minValue) / step;
                    int s = (int)Math.Max(Math.Ceiling(c) + 1, 2);
                    count = count * s;
                }

                for (int i = 0; i < count; i++)
                {
                    if (algParamNow == null)
                    {
                        algParamNow = new List<ParametrNow>();
                        foreach (var algParam in algParamAll)
                        {
                            algParamNow.Add(new ParametrNow { name = algParam.name, value = algParam.minValue });
                        }

                    }
                    else
                    {
                        algParamNow[n - 1] = new ParametrNow { value = algParamNow[n - 1].value + step, name = algParamNow[n - 1].name };
                        for (int j = n - 1; j > 0; j--)
                        {
                            if (algParamNow[j].value > algParamAll[j].maxValue)
                            {
                                algParamNow[j] = new ParametrNow { value = algParamAll[j].minValue, name = algParamNow[j].name };
                                algParamNow[j - 1] = new ParametrNow { value = algParamNow[j - 1].value + step, name = algParamNow[j - 1].name };
                            }
                        }
                        for (int k = 0; k < n; k++)
                            if (algParamNow[k].value > algParamAll[k].maxValue)
                                algParamNow[k] = new ParametrNow { value = algParamAll[k].maxValue, name = algParamNow[k].name };
                    }
                    var alg = GetNewAlg(algParamNow);
                    alg.EnterParam = this.EnterParam;
                    alg.SetFunction(_function);
                    alg.Refresh();
                    var result = alg.Calculate();
                    best = best.Cost < result.Cost ? best : (DataFormat.OutBlackBoxParam)result;
                    List<ParametrNow> outParam = new List<ParametrNow>(alg.ParamNow.Count);
                    foreach (var t in alg.ParamNow)
                    {
                        outParam.Add(t);
                    }
                    var outResult = new DataFormat.OutBlackBoxParam(result.Cost);
                    GetNowListParams[GetNowListParams.Count - 1].Add(outParam, outResult);

                }
            }
            else
            {
                while (algParamNow == null || !Compleate())
                {
                    GetNextParams();
                    var alg = GetNewAlg(algParamNow);
                    alg.EnterParam = this.EnterParam;
                    alg.SetFunction(_function);
                    alg.Refresh();
                    var result = alg.Calculate();
                    best = best.Cost < result.Cost ? best : (DataFormat.OutBlackBoxParam)result;
                    List<ParametrNow> outParam = new List<ParametrNow>(alg.ParamNow.Count);
                    foreach (var t in alg.ParamNow)
                    {
                        outParam.Add(t);
                    }
                    var outResult = new DataFormat.OutBlackBoxParam(result.Cost);
                    GetNowListParams[GetNowListParams.Count - 1].Add(outParam, outResult);
                }
            }
            return best;

        }

        private AbsAlgorithm GetNewAlg(List<ParametrNow> pn)
        {
            Type TestType = _alg.GetType();
            System.Reflection.ConstructorInfo construct = TestType.GetConstructor(new Type[] { typeof(List<ParametrNow>) });
            return construct != null ? (AbsAlgorithm)construct.Invoke(new object[] { pn }) : null;
        }

        private void GetNextParams()
        {
            try
            {
                if (algParamNow == null)
                {
                    algParamNow = new List<ParametrNow>();
                    foreach (var algParam in algParamAll)
                    {
                        algParamNow.Add(new ParametrNow { name = algParam.name, value = algParam.minValue });
                    }
                }
                else
                {
                    foreach (var algParam in algParamAll)
                    {
                        var iter = GetPositionInNowParams(algParam.name);
                        if (iter < algParamNow.Count)
                        {
                            if (TypeParams.discrete == algParam.tp)
                            {
                                if (algParamNow[iter].value < algParam.maxValue)
                                {
                                    var step = Math.Ceiling((algParam.maxValue - algParam.minValue) / 10);
                                    algParamNow[iter] = new ParametrNow { value = algParamNow[iter].value + step, name = algParamNow[iter].name };
                                    break;
                                }
                                else
                                {
                                    algParamNow[iter] = new ParametrNow { value = algParam.minValue, name = algParamNow[iter].name };
                                    continue;
                                }
                            }
                            else if (TypeParams.continuous == algParam.tp)
                            {
                                if (algParamNow[iter].value < algParam.maxValue)
                                {
                                    var step = Math.Ceiling((algParam.maxValue - algParam.minValue) / 10);
                                    algParamNow[iter] = new ParametrNow { value = algParamNow[iter].value + step, name = algParamNow[iter].name };
                                    break;
                                }
                                else
                                {
                                    algParamNow[iter] = new ParametrNow { value = algParam.minValue, name = algParamNow[iter].name };
                                    continue;
                                }

                            }

                        }
                        else
                        {
                            throw new Exception("Нет такого параметра");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private int GetPositionInNowParams(string nameAlg)
        {
            int iter = -1;
            foreach (var aP in algParamNow)
            {
                iter++;
                if (aP.name == nameAlg)
                {
                    break;
                }
            }
            return iter;
        }

        private bool Compleate()
        {
            foreach (var algParam in algParamAll)
            {
                if (algParamNow[GetPositionInNowParams(algParam.name)].value < algParam.maxValue)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Refresh()
        {
            base.Refresh();
            
        }

        public List<Dictionary<List<ParametrNow>, DataFormat.IOutBlackBoxParam>> GetNowListParams { get; set; }

        public override List<Parametr> GetAllParam
        {
            get { return _alg.GetAllParam; }
        }
    }
}
