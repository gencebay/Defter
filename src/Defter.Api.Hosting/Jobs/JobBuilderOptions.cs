using NCrontab;
using System;
using System.Collections.Generic;

namespace Defter.Api.Hosting
{
    public class JobBuilderOptions
    {
        internal List<JobDescriptor> JobList { get; }

        public JobBuilderOptions()
        {
            JobList = new List<JobDescriptor>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJob">Job type</typeparam>
        /// <param name="cronExpression">Cron expression, <see cref="Cron"/> </param>
        public void Register<TJob>(string cronExpression) where TJob : IJob
        {
            var cron = CrontabSchedule.TryParse(cronExpression);
            if (cron == null)
            {
                throw new ArgumentOutOfRangeException($"{nameof(cronExpression)} is not valid!");
            }

            var jobDescriptor = new JobDescriptor(typeof(TJob))
            {
                Cron = cron
            };

            JobList.Add(jobDescriptor);
        }

        public void Register<TJob, TThrottler>() where TJob : IJob
            where TThrottler : IThrottler
        {
            var jobDescriptor = new JobDescriptor(typeof(TJob))
            {
                ThrottlerType = typeof(TThrottler)
            };

            JobList.Add(jobDescriptor);
        }
    }
}
