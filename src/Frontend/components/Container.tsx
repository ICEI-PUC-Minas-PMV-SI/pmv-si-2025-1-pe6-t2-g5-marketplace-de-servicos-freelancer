import { SafeAreaView } from 'react-native-safe-area-context';

export const Container = ({ children }: { children: React.ReactNode }) => {
  return <SafeAreaView className="flex-1 justify-between p-6">{children}</SafeAreaView>;
};
