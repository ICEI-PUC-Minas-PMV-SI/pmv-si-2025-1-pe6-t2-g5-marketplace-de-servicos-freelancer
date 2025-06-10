import AsyncStorage from '@react-native-async-storage/async-storage';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, TouchableOpacity, Pressable } from 'react-native';
import { useWindowDimensions } from 'react-native';
import { RootStackParamList } from './ScreenContent';
import { Entypo } from '@expo/vector-icons';
import { Linking, Alert } from 'react-native';

export default function MeusProjetos() {
  const { width } = useWindowDimensions();
  const isDesktop = width >= 768;
  const [userData, setUserData] = useState<any>(null);
  const [projectList, setProjectList] = useState<any>(null);
  const [detailedFreelancerId, setDetailedFreelancerId] = useState<any>(null);
  const [freelancerDetails, setFreelancerDetails] = useState<any>(null);

  useEffect(() => {
    const fetchRefreshed = async () => {
      const refreshed = await AsyncStorage.getItem('refreshed');

      if (refreshed) return;

      AsyncStorage.setItem('refreshed', 'true');
      window.location.reload();
    }

    fetchRefreshed();
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const raw = await AsyncStorage.getItem('userData');
        if (!raw) return;

        const parsed = JSON.parse(raw);
        setUserData(parsed);

        try {
          const response = await fetch(
            `https://70ba-2804-d45-8614-e000-8848-797a-a4a7-1f2e.ngrok-free.app/api/projeto/idcontratante/${parsed.id}`,
            {
              method: 'GET',
              headers: {
                'Content-Type': 'application/json',
                'ngrok-skip-browser-warning': 'true',
                Authorization: `Bearer ${parsed.token}`,
              },
            }
          );

          if (!response.ok) {
            console.error(`Erro: ${response.status} ${response.statusText}`);
            return;
          }

          const data = await response.json();
          setProjectList(data);
        } catch (error) {
          console.error('Erro ao enviar os dados:', error);
          alert('Erro ao enviar os dados: ' + error);
          return null;
        }
      } catch (e) {
        console.error('Erro:', e);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    if (!detailedFreelancerId) {
      setFreelancerDetails(null);
      return;
    }
    const fetchFreelancerDetails = async () => {
      try {
        const response = await fetch(
          `https://70ba-2804-d45-8614-e000-8848-797a-a4a7-1f2e.ngrok-free.app/nometelefonefreelancer/${detailedFreelancerId}`,
          {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
              
              Authorization: `Bearer ${userData.token}`,
            },
          }
        );

        if (!response.ok) {
          console.error(`Erro: ${response.status} ${response.statusText}`);
          return;
        }

        const data = await response.json();
        setFreelancerDetails(data);
      } catch (error) {
        console.error('Erro ao enviar os dados:', error);
        alert('Erro ao enviar os dados: ' + error);
        return null;
      }
    };

    fetchFreelancerDetails();
  }, detailedFreelancerId);

  function formatDate(dateString: string) {
    const date = new Date(dateString);

    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Mês começa do 0
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
  }

  const abrirWhatsapp = (phone: string) => {
    if (!phone) {
      Alert.alert('Erro', 'Número de telefone não disponível.');
      return;
    }

    const cleanedPhone = phone.replace(/\D/g, '');

    const internationalPhone = cleanedPhone.startsWith('55') ? cleanedPhone : `55${cleanedPhone}`;

    const message = 'Olá! Gostaria de conversar sobre o andamento do meu projeto.';
    const url = `https://wa.me/${internationalPhone}?text=${encodeURIComponent(message)}`;

    Linking.canOpenURL(url)
      .then((supported) => {
        if (!supported) {
          Alert.alert('Erro', 'WhatsApp não está instalado no dispositivo.');
        } else {
          return Linking.openURL(url);
        }
      })
      .catch((err) => Alert.alert('Erro', 'Não foi possível abrir o WhatsApp.'));
  };

  const navigation = useNavigation<NativeStackNavigationProp<RootStackParamList>>();

  return (
    <ScrollView className="w-screen bg-white sm:px-52 sm:py-32">
      <View className="pb-23 relative flex h-full w-full flex-col rounded-sm bg-white p-6 sm:p-24">
        {freelancerDetails && (
          <View className="absolute left-0 top-0 z-50 h-full w-full items-center justify-center bg-black/40 px-4 py-10">
            <View className="w-full max-w-md rounded-2xl bg-white p-6 shadow-xl">
              <Text className="mb-4 text-lg font-semibold text-purple-600">
                Detalhes do Freelancer
              </Text>

              <View className="mb-6 space-y-2">
                <Text className="text-sm text-gray-800">Nome: {freelancerDetails.nome}</Text>
                <Text className="text-sm text-gray-800">Nota: ⭐ 4.8</Text>
                <Text className="text-sm text-gray-800">
                  Telefone: {freelancerDetails.telefone}
                </Text>
              </View>

              <View className="flex w-full flex-row gap-2">
                <Pressable
                  onPress={() => setDetailedFreelancerId(null)}
                  className="flex-1 rounded-md bg-purple-500 py-2">
                  <Text className="text-center text-sm font-semibold text-white">Fechar</Text>
                </Pressable>
                <Pressable
                  onPress={() => abrirWhatsapp(freelancerDetails.telefone)}
                  className="flex-1 rounded-md bg-green-500 py-2">
                  <Text className="text-center text-sm font-semibold text-white">Whatsapp</Text>
                </Pressable>
              </View>
            </View>
          </View>
        )}

        <View className="absolute top-0 flex h-20 w-full items-center justify-end border-b border-gray-200 pb-5 hidden">
          <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
        </View>

        <View className="flex flex-col gap-10">
          <View className="w-full flex flex-col sm:flex-row sm:items-center sm:justify-between">
          <Text className="mb-5 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
              Meus projetos
            </Text>

            <Pressable
              onPress={() => {
                navigation.navigate('SolicitarServico');
              }}>
              <Text className="text-xl font-semibold text-purple-500">
                › Cadastrar novo projeto
              </Text>
            </Pressable>
          </View>

          <View className="flex flex-col gap-10 border-2 border-purple-200 p-4">
            <Text className="text-lg font-semibold text-purple-500">
              Seus projetos cadastrados:
            </Text>

            {/* Desktop view - table layout */}
            {isDesktop ? (
              <View className="hidden flex-col md:flex">
                {/* Table Headers */}
                <View className="mb-2 flex flex-row">
                  <View className="flex-[3] px-2">
                    <Text className="text-sm font-semibold text-purple-500">Nome</Text>
                  </View>
                  <View className="flex-1 items-center px-2">
                    <Text className="text-sm font-semibold text-purple-500">
                      Data da publicação
                    </Text>
                  </View>
                  <View className="flex-1 items-center px-2">
                    <Text className="text-sm font-semibold text-purple-500">Deadline</Text>
                  </View>
                  <View className="flex-1 items-center px-2">
                    <Text className="text-sm font-semibold text-purple-500">Status</Text>
                  </View>
                  <View className="flex-1 items-center px-2">
                    <Text className="text-sm font-semibold text-purple-500">Detalhes</Text>
                  </View>
                </View>

                {/* Table Rows */}
                {!projectList?.length ? (
                  <View className="p-3">
                    <Text className="text-2xl font-light text-purple-500">
                      Nenhum projeto disponível por enquanto
                    </Text>
                  </View>
                ) : (
                  projectList.map((listing: any, index: any) => (
                    <View
                      key={index}
                      className="flex flex-row items-center border-b border-gray-200 py-2">
                      <View className="flex-[3] px-2">
                        <View className="rounded border-2 border-purple-500 px-3 py-1">
                          <Text className="truncate text-sm font-medium text-purple-500">
                            {listing.nome}
                          </Text>
                        </View>
                      </View>
                      <View className="flex-1 items-center px-2">
                        <Text className="text-sm">{formatDate(listing.dataRegistro)}</Text>
                      </View>
                      <View className="flex-1 items-center px-2">
                        <Text className="text-sm">{formatDate(listing.dataFim)}</Text>
                      </View>
                      <View className="flex-1 items-center px-2">
                        <Text className="text-sm">
                          {listing.status === 1 ? 'Assumida' : 'Pendente'}
                        </Text>
                      </View>
                      <View className="flex-1 px-2">
                        {listing.status === 1 ? (
                          <Pressable
                            className="flex items-center"
                            onPress={() => setDetailedFreelancerId(listing.freelancerId)}>
                            <Entypo name="plus" size={20} color="#a855f7" />
                          </Pressable>
                        ) : (
                          <Pressable className="flex items-center" onPress={() => {}}>
                            <Entypo name="plus" size={20} color="#6b7280" />
                          </Pressable>
                        )}
                      </View>
                    </View>
                  ))
                )}
              </View>
            ) : (
              /* Mobile view - card layout */
              <ScrollView className="flex-1 md:hidden">
                {!projectList?.length ? (
                  <View className="p-3">
                    <Text className="text-2xl font-light text-purple-500">
                      Nenhum projeto disponível por enquanto
                    </Text>
                  </View>
                ) : (
                  projectList.map((listing: any, index: any) => (
    <View
      key={index}
      className="mb-4 rounded-md border border-gray-200 p-4 shadow-sm"
    >
      <View className="mb-3 rounded bg-purple-500 px-3 py-2">
        <Text className="text-sm font-medium text-white">{listing.nome}</Text>
      </View>

      <View className="mb-2 flex flex-row justify-between">
        <View className="w-1/2 pr-2">
          <Text className="text-xs text-gray-500">Data da publicação</Text>
          <Text className="text-sm font-medium">{formatDate(listing.dataRegistro)}</Text>
        </View>
        <View className="w-1/2 pl-2">
          <Text className="text-xs text-gray-500">Prazo Estimado</Text>
          <Text className="text-sm font-medium">{formatDate(listing.dataFim)}</Text>
        </View>
      </View>

      <View className="mb-2">
        <Text className="text-xs text-gray-500">Status</Text>
        <Text className="text-sm font-medium">
          {listing.status === 1 ? 'Assumida' : 'Pendente'}
        </Text>
      </View>

      <Pressable
        className={`rounded-md px-2 py-2 ${
          listing.status === 1 ? 'bg-purple-500' : 'bg-gray-400'
        }`}
        onPress={listing.status === 1 ? () => setDetailedFreelancerId(listing.freelancerId) : undefined}
        disabled={listing.status !== 1}
      >
        <Text className="text-center text-sm font-semibold text-white">
          Ver detalhes
        </Text>
      </Pressable>
    </View>
  ))
)}
              </ScrollView>
            )}
          </View>
        </View>
      </View>
    </ScrollView>
  );
}
