using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSequenceGenerator
{
    public class JobEngine
    {
        public string GenerateJobSequence(string[] inputJobArr)
        {
            try
            {
                #region Variable Initialization
                List<string> sequence = new List<string>();
                #endregion

                #region Data Validation
                var validationOutput = ValidateInput(inputJobArr);
                if (validationOutput != ValidationStatus.Success)
                    return validationOutput.ToString();
                #endregion

                #region Job Sequence Processing
                foreach (var job in inputJobArr)
                {
                    var jobArr = job.Replace("=>", "*").Split('*');
                    var jobModel = new JobModel() { DependentJob = jobArr[0].Trim(), ParentJob = jobArr[1].Trim() };
                    if (sequence.Where(x => x == jobModel.DependentJob).Count() > 0)
                    {
                        if (jobModel.ParentJob.Length > 0)
                        {
                            var reliantIndex = sequence.IndexOf(jobModel.DependentJob);
                            sequence.Insert(reliantIndex, jobModel.ParentJob);
                        }
                    }
                    else
                    {
                        if (jobModel.ParentJob.Length > 0)
                        {
                            var depIndex = sequence.IndexOf(jobModel.ParentJob);
                            if (depIndex >= 0)
                                sequence.Insert(depIndex + 1, jobModel.DependentJob);
                            else
                            {
                                sequence.Add(jobModel.ParentJob);
                                sequence.Add(jobModel.DependentJob);
                            }
                        }
                        else
                            sequence.Add(jobModel.DependentJob);
                    }
                }
                #endregion

                #region Circular dependency check
                if (sequence.GroupBy(x => x).Where(g => g.Count() > 1).Count() > 0)
                    return ValidationStatus.Jobs_cannot_have_circular_dependencies.ToString();
                #endregion

                return sequence.Aggregate((i, j) => i + j);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
                return ValidationStatus.Invalid_Job.ToString();
            }
        }
        private ValidationStatus ValidateInput(string[] inputJobArr)
        {
            try
            {
                //Check for NoJobs if the input data is empty
                if (inputJobArr.Length == 0 || inputJobArr.Where(x => x.Length == 0).Count() > 0)
                    return ValidationStatus.No_Jobs;

                foreach (var job in inputJobArr)
                {
                    //Check for job format
                    if (job.IndexOf("=>") == -1)
                        return ValidationStatus.Invalid_job_input_format;
                    var jobArr = job.Replace("=>", "*").Split('*');
                    // Check for Self dependent jobs
                    if (jobArr.Length > 1 && jobArr[0].Trim() == jobArr[1].Trim())
                        return ValidationStatus.Jobs_cannot_depend_on_themselves;
                }
                return ValidationStatus.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
                return ValidationStatus.Invalid_Job;
            }
        }
    }
}
