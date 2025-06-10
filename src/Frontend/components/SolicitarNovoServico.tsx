import { Entypo } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { useState } from 'react';
import { Pressable, ScrollView, Text, TextInput, TouchableOpacity, View } from 'react-native';
import { RootStackParamList } from './ScreenContent';
import { TextInputMask } from 'react-native-masked-text';

export const SolicitarNovoServico = ({ path }: { path: string }) => {
  const [form, setForm] = useState({
    nome: '',
    descricao: '',
    escopo: '',
    dataInicio: new Date().toISOString(),
    dataFim: '',
    valor: '',
  });

  function clearProjetoForm() {
    setForm({
      nome: '',
      descricao: '',
      escopo: '',
      dataInicio: new Date().toISOString(),
      dataFim: '',
      valor: '',
    });
  }

  const categorias = ['Desenvolvimento', 'Design', 'SEO e Marketing', 'Consultoria'];

  const handleChange = (campo: string, valor: string) => {
    if (campo === 'dataFim') {
      // Tenta converter DD/MM/YYYY para ISO UTC
      const [day, month, year] = valor.split('/');

      if (day && month && year && day.length === 2 && month.length === 2 && year.length === 4) {
        const utcDate = new Date(Date.UTC(
          parseInt(year, 10),
          parseInt(month, 10) - 1,
          parseInt(day, 10)
        ));

        setForm({
          ...form,
          [campo]: utcDate.toISOString(),
        });
      } else {
        // Se estiver incompleto ou inválido, mantém o valor digitado (pra não atrapalhar input)
        setForm({
          ...form,
          [campo]: valor,
        });
      }
    } else {
      setForm({
        ...form,
        [campo]: valor,
      });
    }
  };

  async function getUserData() {
    const userData = await AsyncStorage.getItem('userData');

    if (userData) {
      const parsed = JSON.parse(userData);

      return parsed;
    }
  }

  async function submitNewProject(form: any) {
    console.log(form);
    let validForm = true;
    Object.keys(form).forEach((key) => {
      if (form[key]) return;
      validForm = false;
    });

    if (!validForm) {
      alert('Você deve preencher todos os campos solicitados.');
      return;
    }

    const userData = await getUserData();

    if (!userData.id || !userData.token) {
      console.error('Não foi possivel obter contratanteId ou token de autenticação');
      return;
    }

    try {
      const response = await fetch('https://localhost:443/api/Projeto', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',
          Authorization: `Bearer ${userData.token}`,
        },
        body: JSON.stringify({ ...form, contratanteId: userData.id }),
      });

      if (!response.ok) {
        console.error(`Erro: ${response.status} ${response.statusText}`);
      }

      const data = await response.json();

      alert('Projeto criado com sucesso.');
      clearProjetoForm();
      return data;
    } catch (error) {
      console.error('Erro ao enviar os dados:', error);
      return null;
    }
  }

  const navigation = useNavigation<NativeStackNavigationProp<RootStackParamList>>();

  return (
    <ScrollView className="w-screen bg-purple-500 sm:px-52 sm:py-32">
      <View className="pb-23 relative flex h-full w-full flex-col rounded-sm bg-white p-10 pt-48 iphone-xr:pt-10 sm:p-24">
        <View className="absolute top-0 flex h-32 w-screen items-center justify-end border-b border-gray-200 pb-5 iphone-xr:hidden sm:hidden">
          <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
        </View>
        <View className="flex flex-col gap-6 space-y-4">
          <View className="flex flex-col iphone-xr:gap-6">
            <View className="flex w-full flex-row items-center iphone-xr:flex-col iphone-xr:items-start">
              <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500 iphone-xr:w-full">
                Cadastrar novo projeto
              </Text>

              <Pressable
                onPress={() => {
                  navigation.navigate('MeusProjetos');
                }}>
                <Text className="text-xl font-semibold text-purple-500">
                  › Acompanhar meus projetos
                </Text>
              </Pressable>
            </View>
            <Text className="text-lg text-purple-500">
              Nesta tela, você pode cadastrar um novo projeto personalizado conforme suas
              necessidades. Basta preencher as informações abaixo para que freelancers qualificados
              possam visualizar seu pedido:
            </Text>
          </View>

          <View className="flex flex-col items-start gap-4">
            <Text className="text-lg font-semibold text-purple-500">Título do Serviço:</Text>
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
                value={form.descricao}
                onChangeText={(text) => handleChange('descricao', text)}
                multiline
                numberOfLines={4}
                textAlignVertical="top"
              />
            </View>
          </View>

          <View className="flex flex-col items-start gap-4">
            <Text className="text-lg font-semibold text-purple-500">Selecionar Categoria:</Text>
            <View className="flex flex-row flex-wrap gap-3">
              {categorias.map((option) => (
                <TouchableOpacity
                  key={option}
                  className={`flex-row items-center rounded-md border-2 px-4 py-2 ${
                    form.escopo === option ? 'border-purple-500 bg-purple-100' : 'border-purple-300'
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
                type={'datetime'}
                options={{
                  format: 'DD/MM/YYYY',
                }}
                placeholder="Ex: 01/12/2025"
                placeholderTextColor="#c084fc"
                className="ml-2 flex-1 text-base text-purple-800 outline-none px-3 py-2"
                value={form.dataFim}
                onChangeText={(text) => handleChange('dataFim', text)}
                keyboardType="numeric"
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
            <Text className="text-lg font-semibold text-purple-500">Orçamento Disponível:</Text>
            <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
              <Entypo name="credit" size={20} color="#c084fc" />
              <TextInput
                placeholder="Ex: R$ 500,00"
                placeholderTextColor="#c084fc"
                className="ml-2 flex-1 text-base text-purple-800 outline-none"
                value={form.valor}
                onChangeText={(text) => handleChange('valor', text)}
                keyboardType="numeric"
              />
            </View>
          </View>
        </View>

        <View className="mt-8 flex flex-row justify-end">
          <Pressable
            className="items-center justify-center rounded-full bg-purple-500 px-10 py-2 shadow-md"
            onPress={() => submitNewProject(form)}>
            <Text className="text-lg font-semibold text-white">Solicitar</Text>
          </Pressable>
        </View>
      </View>
    </ScrollView>
  );
};
