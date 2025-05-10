import { Text, TextInput, TouchableOpacity, View, ImageBackground } from 'react-native';
import { useState } from 'react';
import { Feather, Entypo } from '@expo/vector-icons';

export const LoginRegister = ({ path }: { path: string }) => {
  const [loginForm, setLoginForm] = useState({
    email: '',
    password: '',
  });

  const [registerForm, setRegisterForm] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    userType: 'freela', // ou 'cliente'
  });

  const [registerPage, setRegisterPage] = useState(false);

  const handleLoginChange = (field: string, value: string) => {
    setLoginForm({
      ...loginForm,
      [field]: value,
    });
  };

  const handleRegisterChange = (field: string, value: string) => {
    setRegisterForm({
      ...registerForm,
      [field]: value,
    });
  };

  return (
    <View className="h-screen w-screen items-center justify-center bg-purple-500 sm:px-52 sm:py-32">
      <View className="flex h-full w-full flex-row rounded-sm bg-purple-400">
        <ImageBackground
          source={{
            uri: 'https://images.unsplash.com/photo-1575737698350-52e966f924d4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
          }}
          resizeMode="cover"
          className="relative box-border hidden flex-[2] justify-center gap-16 p-32 pb-64 lg:flex lg:flex-col">
          <View className="absolute inset-0 z-10 bg-black/30 p-80" />

          <View className="relative z-20 rounded-lg p-4">
            <Text className="text-5xl font-semibold text-white">Bem-vindo(a) ao TalentLink!</Text>
            <Text className="mt-4 text-xl font-normal text-white">
              Nossa plataforma foi criada para transformar a maneira como freelancers e contratantes
              se conectam.
            </Text>
          </View>
        </ImageBackground>

        {registerPage ? (
          <View className="box-border flex-[1.4] flex-col items-center justify-center gap-8 bg-white p-4 sm:p-20">
            <View className="absolute top-0 flex h-32 w-screen items-center justify-end border-b border-gray-200 pb-5 sm:hidden">
              <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
            </View>

            <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
              USER REGISTER
            </Text>

            <View className="flex w-3/4 flex-col gap-2">
              <Text className="text-lg font-semibold text-purple-500">Me cadastrar como</Text>
              <View className="w-full flex-row justify-between gap-4">
                {['freela', 'cliente'].map((option) => (
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

            <View className="flex w-3/4 flex-col gap-2">
              <Text className="text-lg font-semibold text-purple-500">Nome</Text>
              <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                <Feather name="user" size={20} color="#c084fc" />
                <TextInput
                  placeholder="Email"
                  placeholderTextColor="#c084fc"
                  className="ml-2 flex-1 text-base text-purple-800 outline-none"
                  value={registerForm.email}
                  onChangeText={(text) => handleRegisterChange('email', text)}
                />
              </View>
            </View>

            <View className="flex w-3/4 flex-col gap-2">
              <Text className="text-lg font-semibold text-purple-500">E-mail</Text>
              <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                <Feather name="user" size={20} color="#c084fc" />
                <TextInput
                  placeholder="Email"
                  placeholderTextColor="#c084fc"
                  className="ml-2 flex-1 text-base text-purple-800 outline-none"
                  value={registerForm.email}
                  onChangeText={(text) => handleRegisterChange('email', text)}
                />
              </View>
            </View>

            <View className="flex w-3/4 flex-col gap-2">
              <Text className="text-lg font-semibold text-purple-500">Senha</Text>
              <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
                <Entypo name="lock" size={20} color="#c084fc" />
                <TextInput
                  placeholder="Senha"
                  placeholderTextColor="#c084fc"
                  className="ml-2 flex-1 text-base text-purple-800 outline-none"
                  value={registerForm.password}
                  onChangeText={(text) => handleRegisterChange('password', text)}
                  secureTextEntry={true}
                />
              </View>
            </View>

            <View className="flex w-3/4 flex-col gap-2">
              <Text className="text-lg font-semibold text-purple-500">Confirmar senha</Text>
              <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
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

            <View className="mt-5 flex w-3/4 flex-row items-center justify-between">
              <TouchableOpacity onPress={() => setRegisterPage(!registerPage)}>
                <Text className="text-lg font-semibold text-purple-500">Fazer login</Text>
              </TouchableOpacity>
              <TouchableOpacity className="items-center justify-center rounded-full bg-purple-500 px-10 py-2 shadow-md">
                <Text className="text-lg font-semibold text-white">Registrar</Text>
              </TouchableOpacity>
            </View>

            <Text className="absolute bottom-14 text-sm font-normal text-purple-500 sm:hidden">
              Freelas e clientes, juntos no lugar certo. 💜
            </Text>
          </View>
        ) : (
          <View className="box-border flex-[1.4] flex-col items-center justify-center gap-3 bg-white p-4 sm:p-20">
            <View className="absolute top-0 flex h-32 w-screen items-center justify-end border-b border-gray-200 pb-5 sm:hidden">
              <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
            </View>

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
                value={loginForm.password}
                onChangeText={(text) => handleLoginChange('password', text)}
                secureTextEntry={true}
              />
            </View>

            <TouchableOpacity className="mb-6 w-3/4 flex-row items-center justify-end px-1">
              <Text className="text-gray-400">Esqueci a senha</Text>
            </TouchableOpacity>

            <View className="flex w-3/4 flex-row items-center justify-between">
              <TouchableOpacity onPress={() => setRegisterPage(!registerPage)}>
                <Text className="text-lg font-semibold text-purple-500">Registrar</Text>
              </TouchableOpacity>
              <TouchableOpacity className="items-center justify-center rounded-full bg-purple-500 px-10 py-2 shadow-md">
                <Text
                  className="text-lg font-semibold text-white"
                  onPress={() => console.log(loginForm)}>
                  Login
                </Text>
              </TouchableOpacity>
            </View>

            <Text className="absolute bottom-14 text-sm font-normal text-purple-500 sm:hidden">
              Freelas e clientes, juntos no lugar certo. 💜
            </Text>
          </View>
        )}
      </View>
    </View>
  );
};
