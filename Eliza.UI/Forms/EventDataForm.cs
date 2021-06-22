using Eliza.Model.SaveData;
using Eliza.UI.Widgets;
using Eto.Forms;
using System;

namespace Eliza.UI.Forms
{
    public class EventDataForm : Form
    {
        delegate void ListUpdateDelegate();

        public EventDataForm(RF5EventData eventData)
        {
            Title = "Event Data";

            var scroll = new Scrollable();
            var vBox = new VBox();

            var currentEventId = new SpinBox(new Ref<int>(() => eventData.EventSaveParameter.CurrentEventId, v => { eventData.EventSaveParameter.CurrentEventId = v; }), "Current Event ID");
            var currentEventStep = new SpinBox(new Ref<int>(() => eventData.EventSaveParameter.CurrentEventStep, v => { eventData.EventSaveParameter.CurrentEventStep = v; }), "Current Event Step");
            var isTalking = new Widgets.CheckBox(new Ref<bool>(() => eventData.EventSaveParameter.IsTalking, v => { eventData.EventSaveParameter.IsTalking = v; }), "Talking");
            var selectMenuGroupId = new SpinBox(new Ref<int>(() => eventData.EventSaveParameter.SelectMenuGroupId, v => { eventData.EventSaveParameter.SelectMenuGroupId = v; }), "Select Menu Group ID");
            var isSelectMenu = new Widgets.CheckBox(new Ref<bool>(() => eventData.EventSaveParameter.IsSelectMenu, v => { eventData.EventSaveParameter.IsSelectMenu = v; }), "Select Menu");
            var targetNpcId = new SpinBox(new Ref<int>(() => eventData.EventSaveParameter.TargetNpcId, v => { eventData.EventSaveParameter.TargetNpcId = v; }), "Target NPC ID");

            //OrderNpcIds
            var orderNpcIds = new GroupBox()
            {
                Text = "Order NPC IDs"
            };

            {
                var orderNpcIdsHBox = new HBox();
                var orderNpcIdsList = new ListBox();
                var orderNpcIdsData = new VBox();

                if (eventData.EventSaveParameter.OrderNpcIds != null)
                {
                    for (int i = 0; i < eventData.EventSaveParameter.OrderNpcIds.Length; i++)
                    {
                        orderNpcIdsList.Items.Add($"Order NPC ID {i}");
                    }
                }

                var orderNpcIdSpinBox = new SpinBox("NPC ID");
                orderNpcIdsList.SelectedIndexChanged += (object sender, EventArgs e) =>
                {
                    orderNpcIdSpinBox.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.OrderNpcIds[orderNpcIdsList.SelectedIndex], v => { eventData.EventSaveParameter.OrderNpcIds[orderNpcIdsList.SelectedIndex] = v; }));
                };

                StackLayoutItem[] orderNpcIdsDataItems =
                {
                    orderNpcIdSpinBox
                };

                foreach (var item in orderNpcIdsDataItems)
                {
                    orderNpcIdsData.Items.Add(item);
                }

                StackLayoutItem[] orderNpcIdsHBoxItems =
                {
                    orderNpcIdsList,
                    orderNpcIdsData
                };

                foreach (var item in orderNpcIdsHBoxItems)
                {
                    orderNpcIdsHBox.Items.Add(item);
                }

                orderNpcIds.Content = orderNpcIdsHBox;
            }

            var forceScriptName = new LineEdit(new Ref<string>(() => eventData.EventSaveParameter.ForceScriptName, v => { eventData.EventSaveParameter.ForceScriptName = v; }), "Force Script Name");
            var forceEvent = new Widgets.CheckBox(new Ref<bool>(() => eventData.EventSaveParameter.ForceEvent, v => { eventData.EventSaveParameter.ForceEvent = v; }), "Force Event");
            var orderOccuredId = new SpinBox(new Ref<int>(() => eventData.EventSaveParameter.orderOccuredId, v => { eventData.EventSaveParameter.orderOccuredId = v; }), "Order Occured ID");

            // Need to finish EventScheduleData
            //EventScheduleData
            var eventScheduleDatas  = new GroupBox()
            {
                Text = "Event Schedule Data"
            };

            {
                var eventScheduleDatasHBox = new HBox();
                var eventScheduleDatasList = new ListBox();
                eventScheduleDatasList.Width = 200;
                var eventScheduleDatasData = new VBox();

                if (eventData.EventSaveParameter.EventScheduleDatas != null)
                {
                    for (int i = 0; i < eventData.EventSaveParameter.EventScheduleDatas.Length; i++)
                    {
                        eventScheduleDatasList.Items.Add($"Event Schedule Data {i}");
                    }
                }

                var eventID = new SpinBox("Event ID");
                var eventStep = new SpinBox("Event Step");

                //StartTime
                var startTime = new GroupBox()
                {
                    Text = "Start Time"
                };

                ListUpdateDelegate startTimerUpdate = delegate() { };

                {
                    var startTimeHBox = new HBox();
                    var startTimeList = new ListBox();
                    var startTimeData = new VBox();

                    if (eventScheduleDatasList.SelectedIndex < eventData.EventSaveParameter.EventScheduleDatas.Length && eventScheduleDatasList.SelectedIndex >= 0)
                    {
                        if (eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime != null)
                        {
                            for (int i = 0; i < eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime.Length; i++)
                            {       
                                startTimeList.Items.Add($"Start Time {i}");
                            }
                        }
                    }

                    var year = new SpinBox("Year");
                    var season = new SpinBox("Season");
                    var week = new SpinBox("Week");
                    var day = new SpinBox("Day");
                    var hour = new SpinBox("Hour");
                    var minutes = new SpinBox("Minutes");
                    var timezone = new SpinBox("Timezone");
                    var weather = new SpinBox("Weather");

                    year.numericStepper.Enabled = false;
                    season.numericStepper.Enabled = false;
                    week.numericStepper.Enabled = false;
                    day.numericStepper.Enabled = false;
                    hour.numericStepper.Enabled = false;
                    minutes.numericStepper.Enabled = false;
                    timezone.numericStepper.Enabled = false;
                    weather.numericStepper.Enabled = false;

                    startTimeList.SelectedIndexChanged += (object sender, EventArgs e) =>
                    {
                        year.numericStepper.Enabled = true;
                        season.numericStepper.Enabled = true;
                        week.numericStepper.Enabled = true;
                        day.numericStepper.Enabled = true;
                        hour.numericStepper.Enabled = true;
                        minutes.numericStepper.Enabled = true;
                        timezone.numericStepper.Enabled = true;
                        weather.numericStepper.Enabled = true;

                        year.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Year, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Year = v; }));
                        season.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Season, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Season = v; }));
                        week.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Week, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Week = v; }));
                        day.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Day, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Day = v; }));
                        hour.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Hour, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Hour = v; }));
                        minutes.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Minutes, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Minutes = v; }));
                        timezone.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].TimeZone, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].TimeZone = v; }));
                        weather.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Weather, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime[startTimeList.SelectedIndex].Weather = v; }));
                    };


                    startTimerUpdate = delegate ()
                    {
                        startTimeList.Items.Clear();
                        year.numericStepper.Enabled = false;
                        season.numericStepper.Enabled = false;
                        week.numericStepper.Enabled = false;
                        day.numericStepper.Enabled = false;
                        hour.numericStepper.Enabled = false;
                        minutes.numericStepper.Enabled = false;
                        timezone.numericStepper.Enabled = false;
                        weather.numericStepper.Enabled = false;

                        if (eventScheduleDatasList.SelectedIndex < eventData.EventSaveParameter.EventScheduleDatas.Length && eventScheduleDatasList.SelectedIndex >= 0)
                        {
                            if (eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime != null)
                            {
                                for (int i = 0; i < eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime.Length; i++)
                                {
                                    startTimeList.Items.Add($"Start Time {i}");
                                }
                            }
                        }
                    };

                    StackLayoutItem[] startTimeDataItems =
                    {
                        year,
                        season,
                        week,
                        day,
                        hour,
                        minutes,
                        timezone,
                        weather
                    };

                    foreach (var item in startTimeDataItems)
                    {
                        startTimeData.Items.Add(item);
                    }

                    StackLayoutItem[] startTimeHBoxItems =
                    {
                        startTimeList,
                        startTimeData
                    };

                    foreach (var item in startTimeHBoxItems)
                    {
                        startTimeHBox.Items.Add(item);
                    }

                    startTime.Content = startTimeHBox;
                }

                //EndTime
                var endTime = new GroupBox()
                {
                    Text = "End Time"
                };

                ListUpdateDelegate endTimerUpdate = delegate () { };

                {
                    var endTimeHBox = new HBox();
                    var endTimeList = new ListBox();
                    var endTimeData = new VBox();

                    if (eventScheduleDatasList.SelectedIndex < eventData.EventSaveParameter.EventScheduleDatas.Length && eventScheduleDatasList.SelectedIndex >= 0)
                    {
                        if (eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime != null)
                        {
                            for (int i = 0; i < eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime.Length; i++)
                            {
                                endTimeList.Items.Add($"End Time {i}");
                            }
                        }
                    }

                    var year = new SpinBox("Year");
                    var season = new SpinBox("Season");
                    var week = new SpinBox("Week");
                    var day = new SpinBox("Day");
                    var hour = new SpinBox("Hour");
                    var minutes = new SpinBox("Minutes");
                    var timezone = new SpinBox("Timezone");
                    var weather = new SpinBox("Weather");

                    year.numericStepper.Enabled = false;
                    season.numericStepper.Enabled = false;
                    week.numericStepper.Enabled = false;
                    day.numericStepper.Enabled = false;
                    hour.numericStepper.Enabled = false;
                    minutes.numericStepper.Enabled = false;
                    timezone.numericStepper.Enabled = false;
                    weather.numericStepper.Enabled = false;

                    endTimeList.SelectedIndexChanged += (object sender, EventArgs e) =>
                    {
                        year.numericStepper.Enabled = true;
                        season.numericStepper.Enabled = true;
                        week.numericStepper.Enabled = true;
                        day.numericStepper.Enabled = true;
                        hour.numericStepper.Enabled = true;
                        minutes.numericStepper.Enabled = true;
                        timezone.numericStepper.Enabled = true;
                        weather.numericStepper.Enabled = true;

                        year.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Year, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Year = v; }));
                        season.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Season, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Season = v; }));
                        week.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Week, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Week = v; }));
                        day.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Day, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Day = v; }));
                        hour.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Hour, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Hour = v; }));
                        minutes.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Minutes, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Minutes = v; }));
                        timezone.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].TimeZone, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].TimeZone = v; }));
                        weather.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Weather, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime[endTimeList.SelectedIndex].Weather = v; }));
                    };


                    endTimerUpdate = delegate ()
                    {
                        endTimeList.Items.Clear();
                        year.numericStepper.Enabled = false;
                        season.numericStepper.Enabled = false;
                        week.numericStepper.Enabled = false;
                        day.numericStepper.Enabled = false;
                        hour.numericStepper.Enabled = false;
                        minutes.numericStepper.Enabled = false;
                        timezone.numericStepper.Enabled = false;
                        weather.numericStepper.Enabled = false;

                        if (eventScheduleDatasList.SelectedIndex < eventData.EventSaveParameter.EventScheduleDatas.Length && eventScheduleDatasList.SelectedIndex >= 0)
                        {
                            if (eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime != null)
                            {
                                for (int i = 0; i < eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EndTime.Length; i++)
                                {
                                    endTimeList.Items.Add($"End Time {i}");
                                }
                            }
                        }
                    };

                    StackLayoutItem[] endTimeDataItems =
                    {
                        year,
                        season,
                        week,
                        day,
                        hour,
                        minutes,
                        timezone,
                        weather
                    };

                    foreach (var item in endTimeDataItems)
                    {
                        endTimeData.Items.Add(item);
                    }

                    StackLayoutItem[] endTimeHBoxItems =
                    {
                        endTimeList,
                        endTimeData
                    };

                    foreach (var item in endTimeHBoxItems)
                    {
                        endTimeHBox.Items.Add(item);
                    }

                    endTime.Content = endTimeHBox;
                }

                var joinNpcNumMin = new SpinBox("Min Joining NPC");
                var joinNpcNumMax = new SpinBox("Max Joining NPC");


                //JoinNpcs
                var joinNpcs = new GroupBox()
                {
                    Text = "Join NPCs"
                };

                ListUpdateDelegate joinNpcsUpdate = delegate () { };

                {
                    var joinNpcsHBox = new HBox();
                    var joinNpcsList = new ListBox();
                    var joinNpcsData = new VBox();

                    if (eventScheduleDatasList.SelectedIndex < eventData.EventSaveParameter.EventScheduleDatas.Length && eventScheduleDatasList.SelectedIndex >= 0)
                    {
                        if (eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime != null)
                        {
                            for (int i = 0; i < eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].StartTime.Length; i++)
                            {
                                joinNpcsList.Items.Add($"Join NPC {i}");
                            }
                        }
                    }

                    
                }

                eventScheduleDatasList.SelectedIndexChanged += (object sender, EventArgs e) =>
                {
                    eventID.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EventId, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EventId = v; }));
                    eventStep.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EventStep, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].EventStep = v; }));
                    startTimerUpdate();
                    endTimerUpdate();
                    joinNpcNumMin.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].JoinNpcNumMin, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].JoinNpcNumMin = v; }));
                    joinNpcNumMax.ChangeReferenceValue(new Ref<int>(() => eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].JoinNpcNumMax, v => { eventData.EventSaveParameter.EventScheduleDatas[eventScheduleDatasList.SelectedIndex].JoinNpcNumMax = v; }));
                };



                StackLayoutItem[] eventScheduleDatasDataItems =
                {
                    eventID,
                    eventStep,
                    startTime,
                    endTime,
                    joinNpcNumMin,
                    joinNpcNumMax
                };

                foreach (var item in eventScheduleDatasDataItems)
                {
                    eventScheduleDatasData.Items.Add(item);
                }

                StackLayoutItem[] eventScheduleDatasHBoxItems =
                {
                    eventScheduleDatasList,
                    eventScheduleDatasData
                };

                foreach (var item in eventScheduleDatasHBoxItems)
                {
                    eventScheduleDatasHBox.Items.Add(item);
                }

                eventScheduleDatas.Content = eventScheduleDatasHBox;
            }

            StackLayoutItem[] vBoxItems =
            {
                currentEventId,
                currentEventStep,
                isTalking,
                selectMenuGroupId,
                isSelectMenu,
                targetNpcId,
                orderNpcIds,
                forceScriptName,
                forceEvent,
                eventScheduleDatas
            };

            foreach (var item in vBoxItems)
            {
                vBox.Items.Add(item);
            }

            scroll.Content = vBox;
            Content = scroll;
        }
    }
}