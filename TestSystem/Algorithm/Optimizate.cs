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
            calls = 0;
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
                calls += alg.Calls;
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
                                    var step = Math.Ceiling((algParam.maxValue - algParam.minValue) / 4);
                                    algParamNow[iter] = new ParametrNow { value = Math.Round(algParamNow[iter].value + step, 9), name = algParamNow[iter].name };
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
                                    var step = (algParam.maxValue - algParam.minValue) / 4;
                                    algParamNow[iter] = new ParametrNow { value = Math.Round(algParamNow[iter].value + step, 9), name = algParamNow[iter].name };
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
