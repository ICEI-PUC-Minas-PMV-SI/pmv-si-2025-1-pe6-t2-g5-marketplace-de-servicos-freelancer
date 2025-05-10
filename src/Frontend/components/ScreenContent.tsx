import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { View, Text, Button } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';

import { LoginRegister } from './LoginRegister';
import { SolicitarNovoServico } from './SolicitarNovoServico';
import ListagemServicos from './ListagemServicos';
import MeusProjetos from './MeusProjetos';

type ScreenContentProps = {
  title: string;
  path: string;
  children?: React.ReactNode;
};

export type RootStackParamList = {
  Login: undefined;
  SolicitarServico: undefined;
  Listagem: undefined;
  MeusProjetos: undefined;
};

const Stack = createNativeStackNavigator<RootStackParamList>();

export const ScreenContent = ({ title, path, children }: ScreenContentProps) => {
  const handleLogout = async (navigation: any) => {
    await AsyncStorage.clear();
    navigation.navigate('Login');
  };

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login">
        <Stack.Screen name="Login" options={{ title: 'Login' }}>
          {(props: any) => <LoginRegister {...props} path={path} />}
        </Stack.Screen>

        <Stack.Screen
          name="SolicitarServico"
          options={({ navigation }) => ({
            title: 'Novo Serviço',
            headerRight: () => <Button title="Logout" onPress={() => handleLogout(navigation)} />,
          })}>
          {(props: any) => <SolicitarNovoServico {...props} path={path} />}
        </Stack.Screen>

        <Stack.Screen
          name="Listagem"
          options={({ navigation }) => ({
            title: 'Serviços',
            headerRight: () => <Button title="Logout" onPress={() => handleLogout(navigation)} />,
          })}>
          {(props: any) => <ListagemServicos {...props} />}
        </Stack.Screen>

        <Stack.Screen
          name="MeusProjetos"
          options={({ navigation }) => ({
            title: 'Meus Projetos',
            headerRight: () => <Button title="Logout" onPress={() => handleLogout(navigation)} />,
          })}>
          {(props: any) => <MeusProjetos {...props} />}
        </Stack.Screen>
      </Stack.Navigator>
    </NavigationContainer>
  );
};
