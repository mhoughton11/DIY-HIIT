﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
{
    // Principle Entity
    public class Workout : IWorkout
    {
        // Principle Key
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string BodyFocus { get; set; }

        public double? RestInterval { get; set; }
        public double? ActiveInterval { get; set; }
        public double? Effort { get; set; }
        public double? Duration { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateUsed { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public WorkoutType Type { get; set; }
    }
}
