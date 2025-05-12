import AsyncStorage from '@react-native-async-storage/async-storage';
import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, TouchableOpacity, Pressable } from 'react-native';
import { useWindowDimensions } from 'react-native';

export default function FreelancerListing() {
  const { width } = useWindowDimensions();
  const isDesktop = width >= 768;

  const [userData, setUserData] = useState<any>(null);
  const [projectList, setProjectList] = useState<any>(null);
  const [categoryFilter, setCategoryFilter] = useState<any>(null);
  const [filteredProjectList, setFilteredProjectList] = useState<any>(null);
  const [refreshed, setRefreshed] = useState<boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const raw = await AsyncStorage.getItem('userData');
        if (!raw) return;

        const parsed = JSON.parse(raw);
        setUserData(parsed);

        try {
          const response = await fetch('http://localhost:7292/listarpendentes', {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
              Authorization: `Bearer ${parsed.token}`,
            },
          });

          if (!response.ok) {
            console.error(`Erro: ${response.status} ${response.statusText}`);
            return;
          }

          const data = await response.json();
          setProjectList(data);
          setFilteredProjectList(data);
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
  }, [refreshed]);

  function formatDate(dateString: string) {
    const date = new Date(dateString);

    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Mês começa do 0
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
  }

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

  useEffect(() => {
    setFilteredProjectList(
      categoryFilter
        ? projectList.filter((project: any) => project.escopo === categoryFilter)
        : projectList
    );
  }, [categoryFilter]);

  async function assumirProjeto(projetoId: number) {
    try {
      const { id, token } = userData;
      console.log(userData);

      const response = await fetch(`http://localhost:7292/aceitar/${projetoId}/${id}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token,
        },
      });

      if (!response.ok) {
        throw new Error(`Erro ao assumir projeto: ${response.status}`);
      }

      alert('Projeto assumido com sucesso.');
      setRefreshed(!refreshed);
    } catch (error) {
      console.error('Erro ao assumir projeto:', error);
    }
  }

  {
    return (
      <ScrollView className="w-screen bg-purple-500 sm:px-52 sm:py-32">
        <View className="pb-23 relative flex h-full w-full flex-col rounded-sm bg-white p-6 pt-48 sm:p-24">
          <View className="absolute top-0 flex h-32 w-full items-center justify-end border-b border-gray-200 pb-5 sm:hidden">
            <Text className="text-3xl font-light text-purple-500 sm:hidden">Talent Link</Text>
          </View>

          <View className="flex flex-col gap-10">
            <Text className="mb-5 w-3/4 border-l-4 border-purple-500 pl-2 text-2xl font-bold text-purple-500">
              Listagem para Freelancers
            </Text>

            <View className="flex flex-col items-start gap-4">
              <Text className="text-lg font-semibold text-purple-500">
                Filtrar serviços por categoria:
              </Text>
              <View className="flex flex-row flex-wrap gap-3">
                {categorias.map((option) => (
                  <TouchableOpacity
                    key={option}
                    className={`flex-row items-center rounded-md border-2 px-4 py-2 ${
                      categoryFilter === option
                        ? 'border-purple-500 bg-purple-100'
                        : 'border-purple-300'
                    }`}
                    onPress={() => {
                      categoryFilter === option
                        ? setCategoryFilter(null)
                        : setCategoryFilter(option);
                    }}>
                    <View className="mr-2 h-5 w-5 items-center justify-center rounded-full border-2 border-purple-500">
                      {categoryFilter === option && (
                        <View className="h-2.5 w-2.5 rounded-full bg-purple-500" />
                      )}
                    </View>
                    <Text className="text-base capitalize text-purple-500">{option}</Text>
                  </TouchableOpacity>
                ))}
              </View>
            </View>

            <View className="flex flex-col gap-10 border-2 border-purple-200 p-4">
              <Text className="text-lg font-semibold text-purple-500">
                Lista de serviços disponíveis para aceitar:
              </Text>

              {/* Desktop view - table layout */}
              {isDesktop ? (
                <View className="hidden flex-col md:flex">
                  {/* Table Headers */}
                  <View className="mb-2 flex flex-row">
                    <View className="flex-[3] px-2">
                      <Text className="text-sm font-semibold text-purple-500">
                        Título do Serviço
                      </Text>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm font-semibold text-purple-500">
                        Data da publicação
                      </Text>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm font-semibold text-purple-500">Prazo Estimado</Text>
                    </View>
                    <View className="flex-1 items-center px-2">
                      <Text className="text-sm font-semibold text-purple-500">Orçamento</Text>
                    </View>
                    <View className="flex-1 px-2"></View>
                    <View className="flex-1 px-2"></View>
                  </View>

                  {/* Table Rows */}
                  {!projectList?.length ? (
                    <View className="p-3">
                      <Text className="text-2xl font-light text-purple-500">
                        Nenhum projeto disponível por enquanto
                      </Text>
                    </View>
                  ) : (
                    filteredProjectList.map((listing: any, index: any) => (
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
                          <Text className="text-sm">{listing.valor}</Text>
                        </View>
                        <View className="flex-1 px-2">
                          <Pressable className="rounded-md bg-purple-500 px-2 py-2 text-center text-sm font-semibold text-white">
                            <Text className="text-center text-sm font-semibold text-white">
                              Ver detalhes
                            </Text>
                          </Pressable>
                        </View>
                        <View className="flex-1 px-2">
                          <Pressable
                            className="rounded-md bg-purple-500 px-2 py-2 text-center text-sm font-semibold text-white"
                            onPress={() => assumirProjeto(listing.projetoId)}>
                            <Text className="text-center text-sm font-semibold text-white">
                              Assumir
                            </Text>
                          </Pressable>
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
                    filteredProjectList.map((listing: any, index: any) => (
                      <View
                        key={index}
                        className="mb-4 rounded-md border border-gray-200 p-4 shadow-sm">
                        <View className="mb-3">
                          <View className="mb-3 rounded bg-purple-500 px-3 py-2">
                            <Text className="text-sm font-medium text-white">{listing.nome}</Text>
                          </View>
                        </View>

                        <View className="mb-4 flex flex-row flex-wrap">
                          <View className="mb-2 w-1/2">
                            <Text className="text-xs text-gray-500">Data da publicação</Text>
                            <Text className="text-sm font-medium">{listing.dataRegistro}</Text>
                          </View>
                          <View className="mb-2 w-1/2">
                            <Text className="text-xs text-gray-500">Prazo Estimado</Text>
                            <Text className="text-sm font-medium">{listing.dataFim}</Text>
                          </View>
                          <View className="w-full">
                            <Text className="text-xs text-gray-500">Orçamento Disponível</Text>
                            <Text className="text-sm font-medium">{listing.valor}</Text>
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
}
