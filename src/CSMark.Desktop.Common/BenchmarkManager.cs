using CSMarkLib;
using CSMarkLib.BenchmarkLib;
using System;
using System.Threading.Tasks;

namespace CSMark.Desktop.Common{
    /// <summary>
    /// 
    /// </summary>
    public class BenchmarkManager {

        private Result resultX;

        private StressTestController stc = new StressTestController();
        private BenchmarkController btc = new BenchmarkController();

        public void HandleStressTest(bool IsRunning) {           
            if (IsRunning) {
                //Start the Stress Test as a new Task to ensure good UI performance.          
                Task x = new Task(() => stc.StartMultiStressTest());
                x.Start();        
            }
            else {
                stc.StopStressTest();
                //  var stopStressTest = new Task(() => stc.StopStressTest());
                //    stopStressTest.Start();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Result StartBenchmark(){
            try{
                var benchmarkWorkTask = new Task(() => BenchmarkWork());
                benchmarkWorkTask.Start();
                benchmarkWorkTask.Wait();
                return resultX;
            }
            catch(Exception ex){
                Console.WriteLine(ex);
                return new Result();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private Result BenchmarkWork(){
            //     var warmupTask = Task.Factory.StartNew(() => btc.DoWarmup());
            //   warmupTask.Wait((60) * 1000);
            var task1 = Task.Factory.StartNew(() => btc.StartSingleBenchmarkTests());
            task1.Wait((120 * 5) * 1000);
            var task2 = Task.Factory.StartNew(() => btc.StartMultiBenchmarkTests());
            task2.Wait((120 * 5) * 1000);

            /*    HashMap<BenchmarkType, CSMarkLib.BenchmarkLib.Benchmark> hash = btc.ReturnBenchmarkObjects();
                var resultSaver = new ResultSaver();
                var result = resultSaver.SaveResult(true, hash);
                */

                resultX = btc.SaveResult(true, true);
                return btc.SaveResult(true, true);
            //    Results.Default.BenchmarkResult = result;
          //  Results.Default.Save();
        }
    }
}