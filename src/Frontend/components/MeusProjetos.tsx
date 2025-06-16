import { Entypo } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import React, { useEffect, useState } from 'react';
import {
  Alert,
  Linking,
  Pressable,
  ScrollView,
  Text,
  TextInput,
  TouchableOpacity,
  useWindowDimensions,
  View,
} from 'react-native';
import { TextInputMask } from 'react-native-masked-text';

import { RootStackParamList } from './ScreenContent';

export default function MeusProjetos() {
  const { width } = useWindowDimensions();
  const isDesktop = width >= 768;
  const [userData, setUserData] = useState<any>(null);
  const [projectList, setProjectList] = useState<any>(null);
  const [detailedFreelancerId, setDetailedFreelancerId] = useState<any>(null);
  const [modalDeleteProject, setModalDeleteProject] = useState<boolean>(false);
  const [modalEditProject, setModalEditProject] = useState<boolean>(false);
  const [projectIdToDelete, setProjectIdToDelete] = useState<string | null>(null);
  const [projectIdToEdit, setProjectIdToEdit] = useState<string | null>(null);
  const [freelancerDetails, setFreelancerDetails] = useState<any>(null);

  useEffect(() => {
    const fetchRefreshed = async () => {
      const refreshed = await AsyncStorage.getItem('refreshed');

      if (refreshed) return;

      AsyncStorage.setItem('refreshed', 'true');
      window.location.reload();
    };

    fetchRefreshed();
  }, []);

  const fetchData = async () => {
    try {
      const raw = await AsyncStorage.getItem('userData');
      if (!raw) return;

      const parsed = JSON.parse(raw);
      setUserData(parsed);

      try {
        const response = await fetch(
          `https://localhost:443/api/projeto/idcontratante/${parsed.id}`,
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
  useEffect(async () => {
    await fetchData();
  }, []);

  useEffect(() => {
    if (!detailedFreelancerId) {
      setFreelancerDetails(null);
      return;
    }
    const fetchFreelancerDetails = async () => {
      try {
        const response = await fetch(
          `https://localhost:443/nometelefonefreelancer/${detailedFreelancerId}`,
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

  function formatDate(utcDateString: string | null): string | null {
    if (!utcDateString) return null;

    const date = new Date(utcDateString);
    if (isNaN(date.getTime())) return null; // data inválida

    const day = date.getUTCDate().toString().padStart(2, '0');
    const month = (date.getUTCMonth() + 1).toString().padStart(2, '0');
    const year = date.getUTCFullYear();

    return `${day}/${month}/${year}`;
  }

  const [form, setForm] = useState({
    nome: null,
    descricao: null,
    escopo: null,
    dataFim: null,
    valor: null,
  });

  function clearProjetoForm() {
    setForm({
      nome: null,
      descricao: null,
      escopo: null,
      dataFim: null,
      valor: null,
    });
  }

  const handleChange = (campo: string, valor: string) => {
    if (campo === 'dataFim') {
      // Apenas atualiza o valor como string no formato DD/MM/YYYY
      setForm({
        ...form,
        [campo]: valor,
      });
    } else {
      setForm({
        ...form,
        [campo]: valor,
      });
    }
  };

  const categorias = ['Desenvolvimento', 'Design', 'SEO e Marketing', 'Consultoria'];

  const edtitProject = async (projectId: string) => {
    if (!form.nome || form.nome.trim() === '') {
      Alert.alert('Erro', 'O campo Título do Serviço é obrigatório.');
      alert('O campo "Título do Serviço" é obrigatório.');
      return; // interrompe o envio enquanto não preencher
    }

    if (!projectId) {
      Alert.alert('Erro', 'ID do projeto não disponível.');
      Alert.alert('ID do projeto não disponível.');
      return;
    }

    // Converter dataFim para ISO UTC no momento do envio
    let dataFimUTC = null;
    if (form.dataFim) {
      const [day, month, year] = form.dataFim.split('/');

      if (
        day?.length === 2 &&
        month?.length === 2 &&
        year?.length === 4 &&
        !isNaN(parseInt(day, 10)) &&
        !isNaN(parseInt(month, 10)) &&
        !isNaN(parseInt(year, 10))
      ) {
        const utcDate = new Date(
          Date.UTC(parseInt(year, 10), parseInt(month, 10) - 1, parseInt(day, 10))
        );
        dataFimUTC = utcDate.toISOString();
      } else {
        // Se não for válido, mantém null ou pode tratar aqui como erro
        dataFimUTC = null;
      }
    }

    // Montar payload para envio, substituindo dataFim pela data convertida
    const payload = {
      ...form,
      dataFim: dataFimUTC,
    };

    try {
      const response = await fetch(`https://localhost:443/api/Projeto/${projectId}`, {
        method: 'PATCH',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',
          Authorization: `Bearer ${userData.token}`,
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        console.error(`Erro: ${response.status} ${response.statusText}`);
        Alert.alert('Erro', 'Não foi possível editar o projeto.');
        return;
      }

      Alert.alert('Sucesso', 'Projeto editado com sucesso.');
      alert('Projeto editado com sucesso.');
      setModalEditProject(false);
      await fetchData();
    } catch (error) {
      console.error('Erro ao editar o projeto:', error);
      Alert.alert('Erro', 'Não foi possível editar o projeto.');
    } finally {
      clearProjetoForm();
    }
  };

  const deleteProject = async (projectId: string) => {
    if (!projectId) {
      Alert.alert('Erro', 'ID do projeto não disponível.');
      return;
    }

    try {
      const response = await fetch(`https://localhost:443/api/Projeto/${projectId}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',
          Authorization: `Bearer ${userData.token}`,
        },
      });

      if (!response.ok) {
        console.error(`Erro: ${response.status} ${response.statusText}`);
        Alert.alert('Erro', 'Não foi possível excluir o projeto.');
        return;
      }

      Alert.alert('Sucesso', 'Projeto excluído com sucesso.');
      alert('Projeto excluído com sucesso.');
      setModalDeleteProject(false);
      await fetchData();
    } catch (error) {
      console.error('Erro ao excluir o projeto:', error);
      Alert.alert('Erro', 'Não foi possível excluir o projeto.');
    } finally {
      setProjectIdToDelete(null);
    }
  };

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
    <ScrollView className="w-screen bg-purple-500 sm:px-52 sm:py-32">
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

        {modalDeleteProject && (
          <View className="absolute left-0 top-0 z-50 h-full w-full items-center justify-center bg-black/40 px-4 py-10">
            <View className="w-full max-w-md rounded-2xl bg-white p-6 shadow-xl">
              <Text className="mb-4 text-lg font-semibold text-purple-600">Excluir Projeto</Text>

              <View className="mb-6 space-y-2">
                <Text className="text-lg text-gray-800">
                  Você tem certeza que deseja excluir o projeto?
                </Text>
                <Text className="text-lg text-gray-700">Esta ação não é reversível!</Text>
              </View>

              <View className="flex w-full flex-row gap-2">
                <Pressable
                  onPress={() => setModalDeleteProject(false)}
                  className="flex-1 rounded-md bg-purple-500 py-2">
                  <Text className="text-center text-sm font-semibold text-white">Fechar</Text>
                </Pressable>
                <Pressable
                  onPress={() => {
                    if (projectIdToDelete) {
                      deleteProject(projectIdToDelete);
                    }
                  }}
                  className="flex-1 rounded-md bg-green-500 py-2">
                  <Text className="text-center text-sm font-semibold text-white">Sim</Text>
                </Pressable>
              </View>
            </View>
          </View>
        )}

        {modalEditProject && (
          <View className="absolute left-0 top-0 z-50 h-full w-full items-center justify-center bg-black/40 px-4 py-10">
            <View className="mt-2 w-full max-w-md rounded-2xl bg-white p-6 shadow-xl">
              <Text className="mb-4 text-lg font-semibold text-purple-600">Editar Projeto</Text>

              <View className="mb-6">
                <View className="flex flex-col gap-6">
                  <View className="flex flex-col items-start gap-4">
                    <Text className="text-lg font-semibold text-purple-500">
                      Título do Serviço:
                    </Text>
                    <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                      <Entypo name="feather" size={20} color="#c084fc" />
                      <TextInput
                        placeholder="Digite o título do serviço"
                        placeholderTextColor="#c084fc"
                        className="ml-2 flex-1 text-base text-purple-800 outline-none"
                        value={form.nome}
                        onChangeText={(text) => handleChange('nome', text)}
                      />
                    </View>
                  </View>

                  <View className="flex flex-col items-start gap-4">
                    <Text className="text-lg font-semibold text-purple-500">Descrição:</Text>
                    <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                      <Entypo name="text" size={20} color="#c084fc" />
                      <TextInput
                        placeholder="Descreva detalhadamente seu serviço"
                        placeholderTextColor="#c084fc"
                        className="ml-2 flex-1 text-base text-purple-800 outline-none"
                        onChangeText={(text) => handleChange('descricao', text)}
                        value={form.descricao}
                        multiline
                        textAlignVertical="top"
                      />
                    </View>
                  </View>

                  <View className="flex flex-col items-start gap-4">
                    <Text className="text-lg font-semibold text-purple-500">
                      Selecionar Categoria:
                    </Text>
                    <View className="flex flex-row flex-wrap gap-3">
                      {categorias.map((option) => (
                        <TouchableOpacity
                          key={option}
                          className={`flex-row items-center rounded-md border-2 px-4 py-2 ${
                            form.escopo === option
                              ? 'border-purple-500 bg-purple-100'
                              : 'border-purple-300'
                          }`}
                          onPress={() => handleChange('escopo', option)}>
                          <View className="mr-2 h-5 w-5 items-center justify-center rounded-full border-2 border-purple-500">
                            {form.escopo === option && (
                              <View className="h-2.5 w-2.5 rounded-full bg-purple-500" />
                            )}
                          </View>
                          <Text className="text-base capitalize text-purple-500">{option}</Text>
                        </TouchableOpacity>
                      ))}
                    </View>
                  </View>

                  <View className="flex flex-col items-start gap-4">
                    <Text className="text-lg font-semibold text-purple-500">Data de deadline:</Text>
                    <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                      <Entypo name="calendar" size={20} color="#c084fc" />
                      <TextInputMask
                        type="datetime"
                        options={{
                          format: 'DD/MM/YYYY',
                        }}
                        placeholder="Ex: 01/12/2025"
                        placeholderTextColor="#c084fc"
                        className="ml-2 flex-1 px-3 py-2 text-base text-purple-800 outline-none"
                        onChangeText={(text) => handleChange('dataFim', text)}
                        keyboardType="numeric"
                        value={form.dataFim}
                        style={{
                          borderWidth: 0,
                          fontSize: 16,
                          flex: 1,
                          outlineColor: 'transparent',
                          color: '#c084fc',
                          marginLeft: 12,
                        }}
                      />
                    </View>
                  </View>

                  <View className="flex flex-col items-start gap-4">
                    <Text className="text-lg font-semibold text-purple-500">
                      Orçamento Disponível:
                    </Text>
                    <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                      <Entypo name="credit" size={20} color="#c084fc" />
                      <TextInput
                        placeholder="Ex: R$ 500,00"
                        placeholderTextColor="#c084fc"
                        className="ml-2 flex-1 text-base text-purple-800 outline-none"
                        onChangeText={(text) => handleChange('valor', text)}
                        keyboardType="numeric"
                        value={form.valor}
                      />
                    </View>
                  </View>
                </View>
              </View>

              <View className="flex w-full flex-row gap-2">
                <Pressable
                  onPress={() => {
                    setModalEditProject(false);
                    setProjectIdToEdit(null);
                    clearProjetoForm();
                  }}
                  className="flex-1 rounded-md bg-purple-500 py-2">
                  <Text className="text-center text-sm font-semibold text-white">Fechar</Text>
                </Pressable>
                <Pressable
                  onPress={() => {
                    if (projectIdToEdit) {
                      edtitProject(projectIdToEdit);
                    }
                  }}
                  className="flex-1 rounded-md bg-green-500 py-2">
                  <Text className="text-center text-sm font-semibold text-white">Sim</Text>
                </Pressable>
              </View>
            </View>
          </View>
        )}

        <View className="absolute top-0 flex hidden h-20 w-full items-center justify-end border-b border-gray-200 pb-5">
          <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
        </View>

        <View className="flex flex-col gap-10">
          <View className="mb-5 flex w-full flex-col sm:flex-row sm:items-center sm:justify-between">
            <Text className="border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
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
                  <View className="flex-1 items-center">
                    <Text className="text-sm font-semibold text-purple-500">Editar Projeto</Text>
                  </View>
                  <View className="flex-1 items-center">
                    <Text className="text-sm font-semibold text-purple-500">Excluir Projeto</Text>
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
                      <View className="flex-1">
                        {listing.status === 1 ? (
                          <Pressable className="flex cursor-default items-center">
                            <Entypo
                              name="edit"
                              size={20}
                              color="#6b7280"
                              className="cursor-default"
                            />
                          </Pressable>
                        ) : (
                          <Pressable
                            className="flex items-center"
                            onPress={() => {
                              setModalEditProject(true);
                              setProjectIdToEdit(listing.projetoId);
                              setForm({
                                nome: listing.nome,
                                descricao: listing.descricao,
                                escopo: listing.escopo,
                                dataFim: formatDate(listing.dataFim),
                                valor: listing.valor,
                              });
                            }}>
                            <Entypo name="edit" size={20} color="#EFD64A" />
                          </Pressable>
                        )}
                      </View>
                      <View className="flex-1">
                        {listing.status === 1 ? (
                          <Pressable className="flex cursor-default items-center">
                            <Entypo
                              name="trash"
                              size={20}
                              color="#6b7280"
                              className="cursor-default"
                            />
                          </Pressable>
                        ) : (
                          <Pressable
                            className="flex items-center"
                            onPress={() => {
                              setModalDeleteProject(true);
                              setProjectIdToDelete(listing.projetoId);
                            }}>
                            <Entypo name="trash" size={20} color="#ef4444" />
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
                      className="mb-4 rounded-md border border-gray-200 p-4 shadow-sm">
                      <View className="mb-3 rounded bg-purple-500 px-3 py-2">
                        <Text className="text-sm font-medium text-white">{listing.nome}</Text>
                      </View>

                      <View className="mb-2 flex flex-row justify-between">
                        <View className="w-1/2 pr-2">
                          <Text className="text-xs text-gray-500">Data da publicação</Text>
                          <Text className="text-sm font-medium">
                            {formatDate(listing.dataRegistro)}
                          </Text>
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
                        onPress={
                          listing.status === 1
                            ? () => setDetailedFreelancerId(listing.freelancerId)
                            : undefined
                        }
                        disabled={listing.status !== 1}>
                        <Text className="text-center text-sm font-semibold text-white">
                          Entrar em contato
                        </Text>
                      </Pressable>

                      <View className="mt-3 flex flex-row items-center justify-between gap-2 ">
                        {listing.status === 1 ? (
                          <View className="flex w-1/2 flex-row items-center justify-center space-x-2 rounded-md bg-[#6b7280] py-1">
                            <Pressable className="flex cursor-default items-center">
                              <Entypo
                                name="edit"
                                size={20}
                                color="white"
                                className="cursor-default"
                              />
                            </Pressable>
                          </View>
                        ) : (
                          <View className="flex w-1/2 flex-row items-center justify-center space-x-2 rounded-md bg-[#EFD64A] py-1">
                            <Pressable
                              className="flex items-center"
                              onPress={() => {
                                setModalEditProject(true);
                                setProjectIdToEdit(listing.projetoId);
                                setForm({
                                  nome: listing.nome,
                                  descricao: listing.descricao,
                                  escopo: listing.escopo,
                                  dataFim: formatDate(listing.dataFim),
                                  valor: listing.valor,
                                });
                              }}>
                              <Entypo name="edit" size={20} color="white" />
                            </Pressable>
                          </View>
                        )}

                        {listing.status === 1 ? (
                          <View className="flex w-1/2 flex-row items-center justify-center space-x-2 rounded-md bg-[#6b7280] py-1">
                            <Pressable className="flex cursor-default items-center">
                              <Entypo
                                name="trash"
                                size={20}
                                color="white"
                                className="cursor-default"
                              />
                            </Pressable>
                          </View>
                        ) : (
                          <View className="flex w-1/2 flex-row items-center justify-center space-x-2 rounded-md bg-[#ef4444] py-1">
                            <Pressable
                              className="flex items-center"
                              onPress={() => {
                                setModalDeleteProject(true);
                                setProjectIdToDelete(listing.projetoId);
                              }}>
                              <Entypo name="trash" size={20} color="white" />
                            </Pressable>
                          </View>
                        )}
                      </View>
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
