using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Raven.Client.Indexes;

namespace Demo5
{
    public class GeekerUsersActivities_ByUserId : AbstractIndexCreationTask<GeekerUserActivity, GeekerUserActivities>
    {
        public GeekerUsersActivities_ByUserId()
        {
            Map = docs => from doc in docs
                          select new GeekerUserActivities
                                {
                                    UserId = doc.UserId,
                                    ActivitiesIds = new string[]{ doc.ActivityId }
                                };

            Reduce = results => from result in results 
                                group result by result.UserId into g
                                select new GeekerUserActivities 
                                    {
                                        UserId = g.Key,
                                        ActivitiesIds = g.SelectMany(x => x.ActivitiesIds).Distinct().ToArray(),
                                    };

            TransformResults = (db, results) => from result in results
                                                let user = db.Load<GeekerUser>(result.UserId)
                                                let activities = db.Load<Activity>(result.ActivitiesIds)
                                                select new GeekerUsersActivitiesView
                                                    {
                                                        User = user,
                                                        Activities = activities
                                                    };

            Index(x => x.UserId, Raven.Abstractions.Indexing.FieldIndexing.Analyzed);
        }
    }


    public class GeekerUserActivities
    {
        public string UserId { get; set; } 

        public string[] ActivitiesIds { get; set; }
    }


    public class GeekerUsersActivitiesView
    {
        public GeekerUser User { get; set; }

        public Activity[] Activities { get; set; }
    }
}
