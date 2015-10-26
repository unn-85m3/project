using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Algorithm
{
    class Optimizate : AbsAlgorithm
    {
        IAlgorithm _alg;
        List<Dictionary<string, string>> bestParams;

        Optimizate(IAlgorithm alg)
        {
            _alg =  alg;
        }

        public override DataFormat.IOutBlackBoxParam Calculate()
        {
            _alg.StartOptimaze();
            DataFormat.IOutBlackBoxParam ret = _alg.Calculate();
            bestParams.Add(_alg.GetNowParams());
            _alg.NextOptimaze();
            while (_alg.EndOptimaze())
            {
                var t = _alg.Calculate();
                if (ret.Cost < t.Cost)
                {
                    ret = t;
                    bestParams[bestParams.Count -1] = _alg.GetNowParams();
                }
                _alg.NextOptimaze();
            }
            return ret;
        }

        public override bool EndOptimaze()
        {
            throw new NotImplementedException();
        }

        public override void NextOptimaze()
        {
            throw new NotImplementedException();
        }

        public override void StartOptimaze()
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GetNowParams()
        {
            throw new NotImplementedException();
        }

        public List<Dictionary<string, string>> GetNowListParams()
        {
            return bestParams;
        }
    }
}
