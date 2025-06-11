import React, {useEffect, useState} from 'react';
import {NavigationContainer, useNavigationState} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import {Pressable, SafeAreaView, Text, View} from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';

import {LoginRegister} from './LoginRegister';
import {SolicitarNovoServico} from './SolicitarNovoServico';
import ListagemServicos from './ListagemServicos';
import MeusProjetos from './MeusProjetos';

export type RootStackParamList = {
    Login: undefined;
    SolicitarServico: undefined;
    Listagem: undefined;
    MeusProjetos: undefined;
};

const Stack = createNativeStackNavigator<RootStackParamList>();

const CustomHeader = ({onLogout, nome}: { onLogout: () => void; nome?: string }) => {
    const navigationState = useNavigationState((state) => state.routes[state.index]?.name); // Obtém o nome da tela atual

    // Não renderiza o botão de logout na tela de login
    if (navigationState === 'Login') {
        return (
            <View className="flex-row items-center justify-between border-b border-gray-200 bg-white px-4 py-3">
                <Text className="text-3xl font-bold text-purple-500">TalentLink</Text>
            </View>
        );
    }

    return (
        <View className="flex-row items-center justify-between border-b border-gray-200 bg-white px-4 py-3">
            <Text className="text-3xl font-bold text-purple-500">TalentLink</Text>
            {nome && (
                <View className="flex-row flex-wrap items-end space-x-6">
                    <Pressable onPress={onLogout} className="rounded bg-red-500 px-3 py-1">
                        <Text className="text-lg font-semibold text-white">Logout</Text>
                    </Pressable>
                </View>
            )}
        </View>
    );
};

export const ScreenContent = ({path}: { path: string }) => {
    const [userData, setUserData] = useState<{ nome?: string } | null>(null);

    const handleLogout = async (navigation: any) => {
        await AsyncStorage.clear();
        setUserData(null);
        navigation.navigate('Login');
    };

    useEffect(() => {
        const loadUser = async () => {
            const data = await AsyncStorage.getItem('userData');
            if (data) {
                try {
                    const parsed = JSON.parse(data);
                    setUserData(parsed);
                } catch {
                    setUserData(null);
                }
            }
        };
        loadUser();
    }, []);

    return (
        <NavigationContainer>
            <Stack.Navigator initialRouteName="Login" screenOptions={{headerShown: false}}>
                <Stack.Screen name="Login">
                    {(props: any) => (
                        <SafeAreaView className="flex-1 bg-white">
                            <CustomHeader onLogout={() => handleLogout(props.navigation)} nome={userData?.nome}/>
                            <LoginRegister/>
                        </SafeAreaView>
                    )}
                </Stack.Screen>

                <Stack.Screen name="SolicitarServico">
                    {(props: any) => (
                        <SafeAreaView className="flex-1 bg-white">
                            <CustomHeader onLogout={() => handleLogout(props.navigation)} nome={userData?.nome}/>
                            <SolicitarNovoServico {...props} path={path}/>
                        </SafeAreaView>
                    )}
                </Stack.Screen>

                <Stack.Screen name="Listagem">
                    {(props: any) => (
                        <SafeAreaView className="flex-1 bg-white">
                            <CustomHeader onLogout={() => handleLogout(props.navigation)} nome={userData?.nome}/>
                            <ListagemServicos {...props} />
                        </SafeAreaView>
                    )}
                </Stack.Screen>

                <Stack.Screen name="MeusProjetos">
                    {(props: any) => (
                        <SafeAreaView className="flex-1 bg-white">
                            <CustomHeader onLogout={() => handleLogout(props.navigation)} nome={userData?.nome}/>
                            <MeusProjetos {...props} />
                        </SafeAreaView>
                    )}
                </Stack.Screen>
            </Stack.Navigator>
        </NavigationContainer>
    );
};
