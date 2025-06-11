import { ImageBackground, ScrollView, Text, TextInput, TouchableOpacity, View } from 'react-native';
import { useEffect, useState } from 'react';
import { Entypo, Feather } from '@expo/vector-icons';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RootStackParamList } from './ScreenContent';
import AsyncStorage from '@react-native-async-storage/async-storage';

export const LoginRegister = () => {
  const navigation = useNavigation<NativeStackNavigationProp<RootStackParamList>>();
  const [userData, setUserData] = useState<any>(null);

  function navigateUser(userData: any) {
    if (userData) {
      if (userData.tipoUsuario === 'F') {
        navigation.navigate('Listagem');
        return;
      }
      navigation.navigate('MeusProjetos');
    }
  }

  useEffect(() => {
    const obterUserData = async () => {
      try {
        const valor = await AsyncStorage.getItem('userData');
        if (valor) {
          setUserData(JSON.parse(valor));
        }
      } catch (e) {
        console.error('Erro ao buscar token:', e);
      }
    };

    obterUserData();
  }, []);

  useEffect(() => {
    if (userData) {
      navigateUser(userData);
    }
  }, [userData]);

  const [loginForm, setLoginForm] = useState({
    email: '',
    senha: '',
  });

  const handleLoginChange = (field: string, value: string) => {
    setLoginForm({
      ...loginForm,
      [field]: value,
    });
  };

  async function submitLoginForm(loginForm: any) {
    let validForm = true;

    Object.keys(loginForm).forEach((key) => {
      if (loginForm[key]) return;
      validForm = false;
    });

    if (!validForm) {
      alert('Você deve informar login e senha.');
      return;
    }

    try {
      const response = await fetch('https://localhost:443/api/Auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',
        },
        body: JSON.stringify(loginForm),
      });

      if (!response.ok) {
        console.error(`Erro: ${response.status} ${response.statusText}`);
        return;
      }

      const data = await response.json();
      setUserData(data);
      AsyncStorage.setItem('userData', JSON.stringify(data));
      return data;
    } catch (error) {
      console.error('Erro ao enviar os dados:', error);
      return null;
    }
  }

  const initialRegisterForm = {
    userType: 'freela', // ou 'cliente'
    nome: '',
    email: '',
    telefone: '',
    cpf: '',
    senha: '',
    confirmPassword: '',
    dataNascimento: '2002-05-24T00:46:31.412Z',
  };

  const [registerForm, setRegisterForm] = useState(initialRegisterForm);

  const [registerPage, setRegisterPage] = useState(false);

  const handleRegisterChange = (field: string, value: string) => {
    setRegisterForm({
      ...registerForm,
      [field]: value,
    });
  };

  async function submitRegisterForm(registerForm: any) {
    let validForm = true;
    Object.keys(registerForm).forEach((key) => {
      if (registerForm[key]) return;
      validForm = false;
    });

    if (!validForm) {
      alert('Você deve preencher todos os campos solicitados para o cadastro.');
      return;
    }

    const apiRoute = registerForm.userType === 'freelancer' ? 'Freelancer' : 'Contratante';

    const { confirmPassword, userType, ...payload } = registerForm;

    try {
      const response = await fetch(`https://localhost:443/api/${apiRoute}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'ngrok-skip-browser-warning': 'true',
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        console.error(`Erro: ${response.status} ${response.statusText}`);
        alert(`Erro: ${response.status} ${response.statusText}`);
        return;
      }

      alert('Usuário cadastrado com sucesso.');
      setRegisterPage(!registerPage);
      setRegisterForm(initialRegisterForm);
    } catch (error) {
      console.error('Erro ao enviar os dados:', error);
      return null;
    }
  }

  return (
    <View className="h-screen w-screen items-center justify-center bg-purple-500 px-4 py-8 sm:px-52 sm:py-32">
      <View className="mx-auto flex h-full w-full max-w-screen-lg flex-col overflow-hidden rounded-sm bg-purple-400 sm:flex-row">
        <ImageBackground
          source={{
            uri: 'https://images.unsplash.com/photo-1575737698350-52e966f924d4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
          }}
          resizeMode="cover"
          className="relative box-border hidden flex-[2] items-center justify-center gap-8 px-6 py-16 lg:flex lg:flex-col">
          <View className="p-30 absolute inset-0 z-10 bg-black/40" />

          <View className="relative z-20 rounded-lg p-4">
            <Text className="mx-auto text-center text-5xl font-semibold text-white">
              Bem-vindo(a) ao TalentLink!
            </Text>
            <Text className="mx-auto my-5 max-w-[90%] text-center text-base font-normal text-white sm:max-w-md sm:text-sm">
              Nossa plataforma foi criada para transformar a maneira como freelancers e contratantes
              se conectam.
            </Text>
          </View>
        </ImageBackground>

        {registerPage ? (
          <View className="box-border flex items-center justify-center gap-8 bg-white p-4 sm:p-20">
            <ScrollView className="w-full">
              <View className=" flex h-5 w-full items-center justify-end border-b border-purple-500 pb-5 sm:hidden">
                <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
              </View>

              <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
                USER REGISTER
              </Text>

              <View className="flex w-full flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">
                  Me cadastrar como
                </Text>
                <View className="w-full flex-row justify-between gap-4">
                  {['freelancer', 'contratante'].map((option) => (
                    <TouchableOpacity
                      key={option}
                      className="flex-1 flex-row items-center rounded-md border-2 border-purple-500 px-4 py-2"
                      onPress={() => handleRegisterChange('userType', option)}>
                      <View className="mr-2 h-5 w-5 items-center justify-center rounded-full border-2 border-purple-500">
                        {registerForm.userType === option && (
                          <View className="h-2.5 w-2.5 rounded-full bg-purple-500" />
                        )}
                      </View>
                      <Text className="text-base capitalize text-purple-500">{option}</Text>
                    </TouchableOpacity>
                  ))}
                </View>
              </View>

              <View className="flex flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">Nome</Text>
                <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-0 sm:py-3">
                  <Feather name="user" size={20} color="#c084fc" />
                  <TextInput
                    placeholder="Nome"
                    placeholderTextColor="#c084fc"
                    className="ml-2 flex-1 text-base text-purple-800 outline-none"
                    value={registerForm.nome}
                    onChangeText={(text) => handleRegisterChange('nome', text)}
                  />
                </View>
              </View>

              <View className="flex flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">E-mail</Text>
                <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-0 sm:py-3">
                  <Feather name="mail" size={20} color="#c084fc" />
                  <TextInput
                    placeholder="Email"
                    placeholderTextColor="#c084fc"
                    className="ml-2 flex-1 text-base text-purple-800 outline-none"
                    value={registerForm.email}
                    onChangeText={(text) => handleRegisterChange('email', text)}
                  />
                </View>
              </View>

              <View className="flex flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">Telefone</Text>
                <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-0 sm:py-3">
                  <Feather name="phone" size={20} color="#c084fc" />
                  <TextInput
                    placeholder="Telefone"
                    placeholderTextColor="#c084fc"
                    className="ml-2 flex-1 text-base text-purple-800 outline-none"
                    value={registerForm.telefone}
                    onChangeText={(text) => handleRegisterChange('telefone', text)}
                  />
                </View>
              </View>

              <View className="flex flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">CPF</Text>
                <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-0 sm:py-3">
                  <Entypo name="documents" size={20} color="#c084fc" />
                  <TextInput
                    placeholder="CPF"
                    placeholderTextColor="#c084fc"
                    className="ml-2 flex-1 text-base text-purple-800 outline-none"
                    value={registerForm.cpf}
                    onChangeText={(text) => handleRegisterChange('cpf', text)}
                  />
                </View>
              </View>

              <View className="flex flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">Senha</Text>
                <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-0 sm:py-3">
                  <Entypo name="lock" size={20} color="#c084fc" />
                  <TextInput
                    placeholder="Senha"
                    placeholderTextColor="#c084fc"
                    className="ml-2 flex-1 text-base text-purple-800 outline-none"
                    value={registerForm.senha}
                    onChangeText={(text) => handleRegisterChange('senha', text)}
                    secureTextEntry={true}
                  />
                </View>
              </View>

              <View className="flex flex-col gap-2">
                <Text className="text-base font-semibold text-purple-500 sm:text-lg">
                  Confirmar senha
                </Text>
                <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-0 sm:py-3">
                  <Entypo name="lock" size={20} color="#c084fc" />
                  <TextInput
                    placeholder="Senha"
                    placeholderTextColor="#c084fc"
                    className="ml-2 flex-1 text-base text-purple-800 outline-none"
                    value={registerForm.confirmPassword}
                    onChangeText={(text) => handleRegisterChange('confirmPassword', text)}
                    secureTextEntry={true}
                  />
                </View>
              </View>

              <View className="my-5 flex flex-row items-center justify-between">
                <TouchableOpacity onPress={() => setRegisterPage(!registerPage)}>
                  <Text className="text-lg font-semibold text-purple-500">Fazer login</Text>
                </TouchableOpacity>
                <TouchableOpacity
                  className="items-center justify-center rounded-full bg-purple-500 px-10 py-2 shadow-md"
                  onPress={() => submitRegisterForm(registerForm)}>
                  <Text className="text-lg font-semibold text-white">Registrar</Text>
                </TouchableOpacity>
              </View>

              <Text className="mx-auto my-5 text-center text-sm font-normal text-purple-500 sm:hidden">
                Freelas e clientes, juntos no lugar certo. 💜
              </Text>
            </ScrollView>
          </View>
        ) : (
          <View className="box-border flex-[1.4] flex-col items-center justify-center gap-3 bg-white p-4 sm:p-20">
            <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
              USER LOGIN
            </Text>

            <View className="mb-4 w-3/4 flex-row items-center rounded-full bg-purple-200 px-4 py-3">
              <Feather name="user" size={20} color="#c084fc" />
              <TextInput
                placeholder="Email"
                placeholderTextColor="#c084fc"
                className="ml-2 flex-1 text-base text-purple-800 outline-none"
                value={loginForm.email}
                onChangeText={(text) => handleLoginChange('email', text)}
              />
            </View>

            <View className="mb-4 w-3/4 flex-row items-center rounded-full bg-purple-200 px-4 py-3">
              <Entypo name="lock" size={20} color="#c084fc" />
              <TextInput
                placeholder="Senha"
                placeholderTextColor="#c084fc"
                className="ml-2 flex-1 text-base text-purple-800 outline-none"
                value={loginForm.senha}
                onChangeText={(text) => handleLoginChange('senha', text)}
                secureTextEntry={true}
              />
            </View>

            <View className="mx-auto flex flex-col items-center justify-center gap-5 md:flex-row">
              <TouchableOpacity onPress={() => setRegisterPage(!registerPage)}>
                <Text className="text-lg font-semibold text-purple-500">Registrar</Text>
              </TouchableOpacity>

              <TouchableOpacity className="items-center justify-center rounded-full bg-purple-500 px-10 py-2 shadow-md">
                <Text
                  className="text-lg font-semibold text-white"
                  onPress={() => submitLoginForm(loginForm)}>
                  Login
                </Text>
              </TouchableOpacity>
            </View>

            <Text className="absolute bottom-20 text-sm font-normal text-purple-500 sm:hidden">
              Freelas e clientes, juntos no lugar certo. 💜
            </Text>
          </View>
        )}
      </View>
    </View>
  );
};
