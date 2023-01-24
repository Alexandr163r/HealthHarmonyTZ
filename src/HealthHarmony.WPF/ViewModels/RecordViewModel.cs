using HealthHarmony.WPF.Models;
using System.Collections.Generic;
using System.Windows.Input;
using HealthHarmony.WPF.Services;
using HealthHarmony.WPF.ICommandUpdater;
using System.Windows;
using System;

namespace HealthHarmony.WPF.ViewModels
{
    public class RecordViewModel : RecordViewModelNotifyPropchanged
    {
        private ICommand _showRecordCommand;
        private ICommand _addRecordCommand;
        private ICommand _deleteRecordCommand;
        private ICommand _updateRecordCommand;

        private HttpService httpService;

        private RecordModel _selectedRecordModel;

        public RecordModel selectedRecordModel
        {
            get {
                if (_selectedRecordModel.Id != null)
                {
                    return _selectedRecordModel;
                }
                else
                {
                    selectedRecordModel.Id = Guid.NewGuid();
                    return _selectedRecordModel;
                }
            }
            set
            {
                _selectedRecordModel = value;
                NotifyPropertyChanged("SelectedRecordModel");
            }
        }

        private List<RecordModel> RecordModels { get; set; }

        public List<RecordModel> recordModels
        {
            get { return RecordModels; }
            set
            {
                RecordModels = value;
                NotifyPropertyChanged("RecordModels");
            }
        }

        public RecordViewModel()
        {
            selectedRecordModel = new RecordModel();
            httpService = new HttpService();
        }


        public ICommand ShowRecordCommand
        {
            get
            {
                if (_showRecordCommand == null)
                {
                    _showRecordCommand = new RelayCommand(param => this.GetRecords(), null);
                }

                return _showRecordCommand;
            }
        }

        public ICommand AddRecordCommand
        {
            get
            {
                if (_addRecordCommand == null)
                {
                    _addRecordCommand = new RelayCommand(param => this.AddRecord(selectedRecordModel), null);
                }

                return _addRecordCommand;
            }
        }


        public ICommand DeleteRecordCommand
        {
            get
            {
                if (_deleteRecordCommand == null)
                {
                    _deleteRecordCommand = new RelayCommand(param => this.DeleteRecord(selectedRecordModel), null);
                }

                return _deleteRecordCommand;
            }
        }


        public ICommand UpdateRecordCommand
        {
            get
            {
                if (_updateRecordCommand == null)
                {
                    _updateRecordCommand = new RelayCommand(param => this.UpdateRecord(selectedRecordModel), null);
                }

                return _updateRecordCommand;
            }
        }


        private void AddRecord(RecordModel selectedRecordModel)
        {
            try
            {
                if (String.IsNullOrEmpty(selectedRecordModel.Title))
                {
                    MessageBox.Show("Поле 'Название' не заполнено");
                }
                else
                {
                    var result = httpService.AddRecord(selectedRecordModel).Result;

                    if (result)
                    {
                        MessageBox.Show("Запись сохранена");
                    }
                    else
                    {
                        MessageBox.Show("С таким названием уже существует");
                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при обновлении " + e.InnerException, "Ошибка записи",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                GetRecords();
            }
        }

        private async void GetRecords()
        {
            try
            {
                var result = await httpService.GetRecords();

                recordModels = result;
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных " + e.InnerException, "Ошибка записи",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateRecord(RecordModel recordModel)
        {
            try
            {
                var result = httpService.UpdateRecord(recordModel).Result;

                if (result)
                {
                    MessageBox.Show("Запись обновлена");
                    GetRecords();
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при обновлении записи ", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка " + e.InnerException, "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            finally
            {
                GetRecords();
            }
        }

        private void DeleteRecord(RecordModel selectedRecordModel)
        {
            try
            {
                var question = MessageBox.Show("Вы точно хотите удалить эту запись?", "Удаление",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (question == MessageBoxResult.Yes)
                {
                    var result = httpService.DeleteRecord(selectedRecordModel).Result;

                    if (!result)
                    {
                        MessageBox.Show("Произошла ошибка при удалении", "Ошибка", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Произошла ошибка при удалении" + e.InnerException, "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                GetRecords();
            }
        }
    }
}