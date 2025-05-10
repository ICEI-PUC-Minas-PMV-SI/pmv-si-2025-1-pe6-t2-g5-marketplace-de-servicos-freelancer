import React, { useState } from 'react';
import { View, Text, ScrollView, TouchableOpacity, Pressable } from 'react-native';
import { useWindowDimensions } from 'react-native';

export default function MeusProjetos() {
  const { width } = useWindowDimensions();
  const isDesktop = width >= 768;

  const listings = [
    {
      title: 'Tradução de manual técnico (EN-PT)',
      publicationDate: '15/04/2025',
      deadline: '30/05/2025',
      budget: 'R$ 2.500,00',
    },
    {
      title: 'Desenvolvimento de API REST em Node.js',
      publicationDate: '20/04/2025',
      deadline: '15/06/2025',
      budget: 'R$ 1.800,00',
    },
    {
      title: 'Design de interface para aplicativo mobile',
      publicationDate: '01/05/2025',
      deadline: '20/05/2025',
      budget: 'R$ 3.200,00',
    },
    {
      title: 'Tradução de website corporativo (EN-PT)',
      publicationDate: '28/04/2025',
      deadline: '10/06/2025',
      budget: 'R$ 4.500,00',
    },
    {
      title: 'Desenvolvimento de plugin WordPress',
      publicationDate: '03/05/2025',
      deadline: '25/06/2025',
      budget: 'R$ 1.200,00',
    },
    {
      title: 'Design de logo e identidade visual',
      publicationDate: '05/05/2025',
      deadline: '15/07/2025',
      budget: 'R$ 2.800,00',
    },
  ];

  const categorias = ['Desenvolvimento', 'Design', 'SEO e Marketing', 'Consultoria'];

  const [form, setForm] = useState({
    categoria: '',
  });

  const handleChange = (campo: string, valor: string) => {
    setForm({
      ...form,
      [campo]: valor,
    });
  };

  return (
    <ScrollView className="w-screen bg-purple-500 sm:px-52 sm:py-32">
      <View className="pb-23 relative flex h-full w-full flex-col rounded-sm bg-white p-6 pt-48 sm:p-24">
        <View className="absolute top-0 flex h-32 w-full items-center justify-end border-b border-gray-200 pb-5 sm:hidden">
          <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
        </View>

        <View className="flex flex-col gap-10">
          <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
            Meus projetos
          </Text>

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
                    <Text className="text-sm font-semibold text-purple-500">Título do Serviço</Text>
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
                    <Text className="text-sm font-semibold text-purple-500">Assumida por</Text>
                  </View>
                  <View className="flex-1 px-2"></View>
                </View>

                {/* Table Rows */}
                {listings.map((listing, index) => (
                  <View
                    key={index}
                    className="flex flex-row items-center border-b border-gray-200 py-2">
                    <View className="flex-[3] px-2">
                      <View className="rounded border-2 border-purple-500 px-3 py-1">
                        <Text className="truncate text-sm font-medium text-purple-500">
                          {listing.title}
                        </Text>
                      </View>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm">{listing.publicationDate}</Text>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm">{listing.deadline}</Text>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm">{listing.budget}</Text>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm">André pintudo</Text>
                    </View>
                    <View className="flex-1 px-2">
                      <Pressable className="rounded-md bg-purple-500 px-2 py-2 text-center text-sm font-semibold text-white">
                        Conversar
                      </Pressable>
                    </View>
                  </View>
                ))}
              </View>
            ) : (
              /* Mobile view - card layout */
              <ScrollView className="flex-1 md:hidden">
                {listings.map((listing, index) => (
                  <View
                    key={index}
                    className="mb-4 rounded-md border border-gray-200 p-4 shadow-sm">
                    <View className="mb-3">
                      <View className="mb-3 rounded bg-purple-500 px-3 py-2">
                        <Text className="text-sm font-medium text-white">{listing.title}</Text>
                      </View>
                    </View>

                    <View className="mb-4 flex flex-row flex-wrap">
                      <View className="mb-2 w-1/2">
                        <Text className="text-xs text-gray-500">Data da publicação</Text>
                        <Text className="text-sm font-medium">{listing.publicationDate}</Text>
                      </View>
                      <View className="mb-2 w-1/2">
                        <Text className="text-xs text-gray-500">Prazo Estimado</Text>
                        <Text className="text-sm font-medium">{listing.deadline}</Text>
                      </View>
                      <View className="w-full">
                        <Text className="text-xs text-gray-500">Orçamento Disponível</Text>
                        <Text className="text-sm font-medium">{listing.budget}</Text>
                      </View>
                    </View>

                    <View className="flex flex-row gap-2">
                      <Pressable className="flex-1 rounded-md bg-purple-500 px-2 py-2">
                        <Text className="text-center text-sm font-semibold text-white">
                          Ver detalhes
                        </Text>
                      </Pressable>
                      <Pressable className="flex-1 rounded-md bg-purple-500 px-2 py-2">
                        <Text className="text-center text-sm font-semibold text-white">
                          Ver detalhes
                        </Text>
                      </Pressable>
                    </View>
                  </View>
                ))}
              </ScrollView>
            )}
          </View>
        </View>
      </View>
    </ScrollView>
  );
}
