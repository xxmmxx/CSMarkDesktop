using CSMarkLib;
using CSMarkLib.BenchmarkManagement;
using CSMarkWinForms.Settings;
using System;
using System.Threading.Tasks;

namespace CSMark.Desktop.Common{
    /// <summary>
    /// 
    /// </summary>
    public class BenchmarkManager {

        private StressTestController stc = new StressTestController();
        private BenchmarkController btc = new BenchmarkController();

        public void HandleStressTest(bool IsRunning) {
            if (!IsRunning) {
                //Start the Stress Test as a new Task to ensure good UI performance.
                var startStressTest = new Task(() => stc.StartMultiStressTest());
                startStressTest.Start();
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
        public void StartBenchmark(){
            try{
                var benchmarkWorkTask = new Task(() => BenchmarkWork());
                benchmarkWorkTask.Start();
                benchmarkWorkTask.Wait();
            }
            catch(Exception ex){
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private async void BenchmarkWork(){
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

            Results.Default.BenchmarkResult = btc.SaveResult(true, true);
            //    Results.Default.BenchmarkResult = result;
          //  Results.Default.Save();
        }
    }
}