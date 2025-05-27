import DateTimePicker from 'react-native-modal-datetime-picker';
import React, { useState } from 'react';

export default function DatePickerComponent({ listing }: { listing: any }) {
  const [isDatePickerVisible, setDatePickerVisibility] = useState(false);
  const [selectedDate, setSelectedDate] = useState(listing.dataFim);

  const showDatePicker = () => {
    setDatePickerVisibility(true);
  };

  const hideDatePicker = () => {
    setDatePickerVisibility(false);
  };

  const handleConfirm = (date: Date) => {
    setSelectedDate(date.toISOString().split('T')[0]); // Formata a data para YYYY-MM-DD
    hideDatePicker();
  };

  return (
    <View className="mb-2 w-1/2">
      <Text className="text-xs text-gray-500">Prazo Estimado</Text>
      <Text className="text-sm font-medium" onPress={showDatePicker}>
        {selectedDate}
      </Text>
      <DateTimePicker
        isVisible={isDatePickerVisible}
        mode="date"
        onConfirm={handleConfirm}
        onCancel={hideDatePicker}
      />
    </View>
  );
}