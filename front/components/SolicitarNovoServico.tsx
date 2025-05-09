import { Entypo } from '@expo/vector-icons';
import { useState } from 'react';
import { View, Text, TextInput, Pressable, ScrollView, TouchableOpacity } from 'react-native';

export const SolicitarNovoServico = ({ path }: { path: string }) => {
  const [form, setForm] = useState({
    titulo: '',
    descricao: '',
    categoria: '',
    prazo: '',
    orcamento: '',
  });

  const categorias = ['Desenvolvimento', 'Design', 'SEO e Marketing', 'Consultoria'];

  const handleChange = (campo: string, valor: string) => {
    setForm({
      ...form,
      [campo]: valor,
    });
  };

  const handleSubmit = () => {
    console.log('Dados do formulário:', form);
  };

  return (
    <ScrollView className="w-screen bg-purple-500 sm:px-52 sm:py-32">
      <View className="relative flex h-full w-full flex-col rounded-sm bg-white p-10 pt-48 pb-23 sm:p-24">
        <View className="absolute top-0 flex h-32 w-screen items-center justify-end border-b border-gray-200 pb-5 sm:hidden">
          <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
        </View>
        <View className="flex flex-col gap-6 space-y-4">
          <View className="flex flex-col">
            <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
              Solicitação de Novo Serviço
            </Text>
            <Text className="text-lg text-purple-500">
              Nesta tela, você pode solicitar um novo serviço personalizado conforme suas
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
                value={form.titulo}
                onChangeText={(text) => handleChange('titulo', text)}
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
                    form.categoria === option
                      ? 'border-purple-500 bg-purple-100'
                      : 'border-purple-300'
                  }`}
                  onPress={() => handleChange('categoria', option)}>
                  <View className="mr-2 h-5 w-5 items-center justify-center rounded-full border-2 border-purple-500">
                    {form.categoria === option && (
                      <View className="h-2.5 w-2.5 rounded-full bg-purple-500" />
                    )}
                  </View>
                  <Text className="text-base capitalize text-purple-500">{option}</Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>

          <View className="flex flex-col items-start gap-4">
            <Text className="text-lg font-semibold text-purple-500">Prazo Desejado:</Text>
            <View className="w-full flex-row items-center rounded-md border-2 border-purple-500 bg-white px-4 py-3">
              <Entypo name="calendar" size={20} color="#c084fc" />
              <TextInput
                placeholder="Ex: 30 dias"
                placeholderTextColor="#c084fc"
                className="ml-2 flex-1 text-base text-purple-800 outline-none"
                value={form.prazo}
                onChangeText={(text) => handleChange('prazo', text)}
                keyboardType="numeric"
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
                value={form.orcamento}
                onChangeText={(text) => handleChange('orcamento', text)}
                keyboardType="numeric"
              />
            </View>
          </View>
        </View>

        <View className="mt-8 flex flex-row justify-end">
          <Pressable
            className="items-center justify-center rounded-full bg-purple-500 px-10 py-2 shadow-md"
            onPress={handleSubmit}>
            <Text className="text-lg font-semibold text-white">Solicitar</Text>
          </Pressable>
        </View>
      </View>
    </ScrollView>
  );
};
