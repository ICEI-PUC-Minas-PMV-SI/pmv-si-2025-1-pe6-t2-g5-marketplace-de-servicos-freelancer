import { ScreenContent } from 'components/ScreenContent';
import { SafeAreaProvider, SafeAreaView } from 'react-native-safe-area-context';
import './global.css'; // funciona no Expo Web, não no mobile nativo

export default function App() {
  return (
    <SafeAreaProvider>
      <SafeAreaView className="flex-1 bg-white">
        <ScreenContent />
      </SafeAreaView>
    </SafeAreaProvider>
  );
}
