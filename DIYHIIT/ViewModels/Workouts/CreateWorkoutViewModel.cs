﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Library.Helpers;
using DIYHIIT.Library.Models;
using DIYHIIT.ViewModels.Base;
using DIYHIIT.ViewModels.Exercises;
using DIYHIIT.Views;
using DIYHIIT.Views.Exercises;
using Newtonsoft.Json;
using Xamarin.Forms;

using static DIYHIIT.Constants.Messages;


namespace DIYHIIT.ViewModels.Workouts
{
    public class CreateWorkoutViewModel : BaseViewModel
    {
        public CreateWorkoutViewModel(object data,
                                      IUserDataService userDataService,
                                      INavigation navigation,
                                      IDialogService dialogService)
            : base(navigation, dialogService)
        {
            InitializeAsync(data);
            _userDataService = userDataService;
        }

        #region Private Fields

        private Random rand;
        private int _selectedWorkoutType;
        private List<string> _workoutTypes;
        private ObservableCollection<Exercise> _exercises;
        private readonly IUserDataService _userDataService;

        #endregion

        #region Public Members and Commands

        public string ActiveEntry { get; set; }
        public string RestEntry { get; set; }

        public int SelectedWorkoutType
        {
            get => _selectedWorkoutType;
            set
            {
                _selectedWorkoutType = value;
                RaisePropertyChanged(() => SelectedWorkoutType);
            }
        }

        public ObservableCollection<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                _exercises = value;
                RaisePropertyChanged(() => Exercises);
            }
        }

        public List<string> WorkoutTypes
        {
            get => _workoutTypes;
            set
            {
                _workoutTypes = value;
                RaisePropertyChanged(() => WorkoutTypes);
            }
        }

        public ICommand DoneCommand => new Command(OnDoneCommand);
        public ICommand AddExerciseCommand => new Command(OnAddExerciseCommand);

        #endregion

        #region Public Methods

        public void RemoveObject(int index)
        {
            Exercise ex = Exercises.Where(e => e.Index == index) as Exercise;
            Exercises.Remove(ex);
        }

        public override Task InitializeAsync(object data)
        {
            Exercises = new ObservableCollection<Exercise>();
            rand = new Random();

            WorkoutTypes = Enum.GetNames(typeof(WorkoutType)).ToList();

            MessagingCenter.Subscribe<AddExerciseViewModel, Exercise>(this, ExerciseAdded, (sender, arg) =>
            {
                arg.Index = rand.Next(0, 0xFFFF);
                Exercises.Add(arg);
            });

            return base.InitializeAsync(data);
        }

        #endregion

        #region Private Methods

        private async void OnDoneCommand()
        {
            double activeInterval, restInterval;
            int workoutType;

            // Check all required params aren't null.
            try
            {
                activeInterval = double.Parse(ActiveEntry);
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the active interval.", "OK");
                return;
            }

            try
            {
                restInterval = double.Parse(RestEntry);
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the rest interval.", "OK");
                return;
            }

            try
            {
                workoutType = SelectedWorkoutType;
            }
            catch (Exception)
            {
                await _dialogService.ShowAlertAsync("Empty field.", "Please enter a value for the workout type.", "OK");
                return;
            }

            if (_exercises.Count == 0 || _exercises == null)
            {
                await _dialogService.ShowAlertAsync("No exercises.", "Pleases add at least one exercise.", "OK");
                return;
            }

            var Ids = new List<int>();
            foreach (var ex in _exercises)
            {
                Ids.Add(ex.Id);
            }

            var name = await GetWorkoutName();

            // Create workout with the specified parameters/exercises.
            var workout = new Workout()
            {
                ActiveInterval = activeInterval,
                RestInterval = restInterval,
                ExerciseIDs = JsonConvert.SerializeObject(Ids),
                Type = (WorkoutType)workoutType,
                DateAdded = DateTime.Now,
                Exercises = _exercises.ToList(),
                Duration = Helpers.GetWorkoutDuration(_exercises.ToList(), activeInterval, restInterval),
                ExerciseCount = Helpers.GetWorkoutCountString(_exercises.ToList()),
                User = App.CurrentUser,
                UserId = App.CurrentUser.Id,
                Name = name
            };

            workout = await _userDataService.SaveWorkout(workout);

            MessagingCenter.Send(this, WorkoutsUpdated);

            await _navigation.PopAsync();           
        }

        private async void OnAddExerciseCommand()
        {
            await _navigation.PushAsync(new AddExerciseView());
        }

        private async Task<string> GetWorkoutName()
        {
            var getName = await _dialogService.ShowConfirmAsync("Workout name", "Do you wish to name your workout for easier reference?", "Yes", "No");

            var name = string.Empty;

            if (getName)
            {
                string input = await _dialogService.ShowPromptAsync("Workout Name", "Enter workout name below", "OK", "Cancel");

                if (!string.IsNullOrWhiteSpace(input) || !string.IsNullOrEmpty(input))
                {
                    name = input;
                }
                else
                {
                    _dialogService.Popup("Please type a workout name when prompted.");
                    var t = Enum.GetName(typeof(WorkoutType), SelectedWorkoutType);
                    name = t + " Workout";
                }
            }
            else
            {
                var t = Enum.GetName(typeof(WorkoutType), SelectedWorkoutType);
                name =  t + " Workout";
            }

            return name;
        }

        #endregion
    }
}
